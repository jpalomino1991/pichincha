using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WEBAPI.App.Util
{
    public class ServiceEntity
    {
        public int Port { get; set; }
        public String? ServiceName { get; set; }
        public String? ConsulIP { get; set; }
        public int ConsulPort { get; set; }
    }
}
