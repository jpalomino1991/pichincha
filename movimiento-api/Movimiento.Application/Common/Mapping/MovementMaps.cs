using AutoMapper;
using AutoMapper.Internal;
using Movimiento.Application.Movimientos.Commands;
using Movimiento.Application.Response;
using Movimiento.Domain.Entities;

namespace Movimiento.Application.Common.Mapping
{
    public class MovementMaps : Profile
    {
        public MovementMaps()
        {
            CreateMap<CreateMovementCommand, MovementEntity>()
               .ForMember(dest => dest.MovementType, opt => opt.MapFrom(src => src.AccountType))
               .ForMember(dest => dest.MovementState, opt => opt.MapFrom(src => src.AccountState))
               .ForMember(dest => dest.MovementBalance, opt => opt.MapFrom(src => src.AccountAmount));
            CreateMap<MovementEntity, CreateMovementResponseModel>();
            CreateMap<MovementEntity, GetMovementByDataResponseModel>()
               .ForMember(dest => dest.MovementInitialAmount, opt => opt.MapFrom(src => src.MovementBalance - src.MovementAmount));
        }
    }
}
