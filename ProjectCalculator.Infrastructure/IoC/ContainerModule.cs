using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.IoC
{
    public class ContainerModule: Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommandModule>();
        }
    }
}
