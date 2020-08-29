using Autofac;
using ProjectCalculator.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProjectCalculator.Infrastructure.IoC
{
    public class ServiceModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                 .GetTypeInfo()
                 .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();


            builder.RegisterType<JwtService>()
                .As<IJwtService>()
                .SingleInstance();
        }
    }
}
