﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Application.Clientes.Commands
{
   public class DeleteClientCommand : IRequest<int>
   {
      public int Id { get; set; }
   }
}
