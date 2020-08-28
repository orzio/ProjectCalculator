using Autofac;
using ProjectCalculator.Infrastructure.Commands;
using System;

namespace ProjectCalculator.Infrastructure.IoC
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //doesnt work toget assembly like this
            var assembly = typeof(CommandModule).GetType().Assembly;

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}