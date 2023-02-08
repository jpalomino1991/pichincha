using Account.Application.Common.Mapping;
using AutoMapper;

namespace Cuenta.Application.Test.Moq
{
   public class MapperSetup
   {
      private static readonly object SyncObj = new object();
      private static bool _created;
      private static IMapper _mapper;

      public static IMapper InitMappings()
      {
         lock (SyncObj)
            if (!_created)
            {
               var config = new MapperConfiguration(cfg =>
               {
                  cfg.AddProfile<AccountMaps>();
               });

               _mapper = config.CreateMapper();
               _created = true;
            }

         return _mapper;
      }
   }
}
