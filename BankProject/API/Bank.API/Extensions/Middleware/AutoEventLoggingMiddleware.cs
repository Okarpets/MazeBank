namespace BANK.API.Extensions.Middleware
{
    public class AutoEventLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AutoEventLoggingMiddleware> _logger;

        public AutoEventLoggingMiddleware(RequestDelegate next, ILogger<AutoEventLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var startTime = DateTime.UtcNow;


            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;
            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);


            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            var controllerName = context.GetRouteValue("controller") as string;
            var actionName = context.GetRouteValue("action") as string;
            var requestType = context.Request.Method;
            var requestPath = context.Request.Path;
            var requestData = requestBodyText;


            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await _next(context);


            var endTime = DateTime.UtcNow;

            var responseStatusCode = context.Response.StatusCode;
            var elapsedTime = endTime - startTime;
            var ipAddress = context.Connection.RemoteIpAddress;

            var logMessage = $"Controller: {controllerName}, Action: {actionName}, " +
                             $"Request Type: {requestType}, Path: {requestPath}, " +
                             $"Data: {requestData}, Status Code: {responseStatusCode}, " +
                             $"Elapsed Time: {elapsedTime}," +
                             $"IP Address: {ipAddress}";


            _logger.LogInformation(logMessage);
        }
    }
}
