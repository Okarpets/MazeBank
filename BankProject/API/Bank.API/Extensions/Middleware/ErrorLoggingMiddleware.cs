namespace BANK.API.Extensions.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogError(context, ex);
                throw;
            }
        }

        private void LogError(HttpContext context, Exception exception)
        {
            var timestamp = DateTime.UtcNow;
            var controllerName = context.GetRouteValue("controller") as string;
            var actionName = context.GetRouteValue("action") as string;
            var requestType = context.Request.Method;
            var requestData = context.Request.Body != null ? new StreamReader(context.Request.Body).ReadToEnd() : "N/A";


            var errorMessage = $"Timestamp: {timestamp}, Controller: {controllerName}, Action: {actionName}, " +
                               $"Request Type: {requestType}, Data: {requestData}, Exception: {exception.Message}";

            _logger.LogError(errorMessage);
        }
    }
}
