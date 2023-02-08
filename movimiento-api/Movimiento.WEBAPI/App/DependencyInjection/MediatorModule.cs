using MediatR;
using System.Reflection;
using Autofac;
using Movimiento.Application.Movimientos.Commands;
//using Movimiento.Application.Cuentas.Commands;

namespace Movimiento.WebAPI.App.DependencyInjection
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register mediator
            builder.RegisterAssemblyTypes(typeof(IMediator)
                .GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register Query & Command classes
            builder.RegisterAssemblyTypes(typeof(CreateMovementCommand).GetTypeInfo().Assembly)
             .AsClosedTypesOf(typeof(IRequestHandler<,>));


            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

    }
}