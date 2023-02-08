using AutoMapper;
using AutoMapper.Internal;
using Cuenta.Application.Cuentas.Commands;
using Cuenta.Application.Response;
using Cuenta.Domain.Entities;

namespace Account.Application.Common.Mapping
{
    public class AccountMaps : Profile
    {
        public AccountMaps()
        {
            CreateMap<CreateAccountCommand, AccountEntity>();
            CreateMap<AccountEntity, CreateAccountResponseModel>();
            CreateMap<AccountEntity, GetAccountResponseModel>();
        }
    }
}
