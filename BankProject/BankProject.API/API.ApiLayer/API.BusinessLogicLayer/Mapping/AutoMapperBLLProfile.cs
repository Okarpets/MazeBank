using API.DataLayer.Entities;
using API.DataLayer.Models;
using AutoMapper;

namespace API.BusinessLogicLayer.Mapping;

public class AutoMapperBLLProfile : Profile
{
    public AutoMapperBLLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(
            dest => dest.Username,
            opt => opt.MapFrom(src => src.Username))
            .ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(src => src.Email))
            .ForMember(
            dest => dest.PasswordHash,
            opt => opt.MapFrom(src => src.PasswordHash))
            .ForMember(
            dest => dest.Role,
            opt => opt.MapFrom(src => src.Role))
            .ForMember(
            dest => dest.CreatedAt,
            opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(
            dest => dest.Accounts,
            opt => opt.MapFrom(src => src.Accounts))
            .ReverseMap();

        CreateMap<TransactionEntity, TransactionModel>()
            .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(
            dest => dest.FromAccountId,
            opt => opt.MapFrom(src => src.FromAccountId))
            .ForMember(
            dest => dest.FromAccount,
            opt => opt.MapFrom(src => src.FromAccount))
            .ForMember(
            dest => dest.ToAccountId,
            opt => opt.MapFrom(src => src.ToAccount))
            .ForMember(
            dest => dest.Amount,
            opt => opt.MapFrom(src => src.Amount))
            .ForMember(
            dest => dest.TransactionDate,
            opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(
            dest => dest.Description,
            opt => opt.MapFrom(src => src.Description))
            .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => src.Status));


        CreateMap<AccountEntity, AccountModel>()
            .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(
            dest => dest.UserId,
            opt => opt.MapFrom(src => src.UserId))
            .ForMember(
            dest => dest.User,
            opt => opt.MapFrom(src => src.User))
            .ForMember(
            dest => dest.AccountNumber,
            opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(
            dest => dest.Balance,
            opt => opt.MapFrom(src => src.Balance))
            .ForMember(
            dest => dest.Currency,
            opt => opt.MapFrom(src => src.Currency))
            .ForMember(
            dest => dest.CreatedAt,
            opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(
            dest => dest.SentTransactions,
            opt => opt.MapFrom(src => src.SentTransactions))
            .ReverseMap()
            .ForMember(
            dest => dest.ReceivedTransactions,
            opt => opt.MapFrom(src => src.ReceivedTransactions))
            .ReverseMap();
    }
}