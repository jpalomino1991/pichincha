using MediatR;
using System.Reflection;
using Autofac;
using Cuenta.Application.Cuentas.Commands;

namespace Account.WebAPI.App.DependencyInjection
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
            builder.RegisterAssemblyTypes(typeof(CreateAccountCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(UpdateAccountCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(DeleteAccountCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

    }
}