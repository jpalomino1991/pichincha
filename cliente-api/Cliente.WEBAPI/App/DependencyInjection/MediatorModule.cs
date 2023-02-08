using MediatR;
using System.Reflection;
using Autofac;
using Cliente.Application.Cuentas.Commands;
using Cliente.Application.Clientes.Commands;
using Cliente.Application.Clientes.Queries;

namespace Cliente.WebAPI.App.DependencyInjection
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
            builder.RegisterAssemblyTypes(typeof(GetClientByIdQuery).GetTypeInfo().Assembly)
             .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(GetClientByNameQuery).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>)); 
            builder.RegisterAssemblyTypes(typeof(CreateClientCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(UpdateClientCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(DeleteClientCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

    }
}