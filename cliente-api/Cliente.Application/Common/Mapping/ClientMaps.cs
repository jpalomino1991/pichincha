using AutoMapper;
using AutoMapper.Internal;
using Cliente.Application.Cuentas.Commands;
using Cliente.Application.Response;
using Cliente.Domain.Entities;
using System;

namespace Cliente.Application.Common.Mapping
{
   public class ClientMaps : Profile
   {
      public ClientMaps()
      {
         CreateMap<CreateClientCommand, ClientEntity>();
         CreateMap<ClientEntity, CreateClientResponseModel>()
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.PersonName))
            .ForMember(dest => dest.PersonDirection, opt => opt.MapFrom(src => src.Person.PersonDirection))
            .ForMember(dest => dest.PersonPhone, opt => opt.MapFrom(src => src.Person.PersonPhone));
         CreateMap<CreateClientCommand, PersonEntity>();
         CreateMap<ClientEntity, GetClientResponseModel>();
      }
   }
}
