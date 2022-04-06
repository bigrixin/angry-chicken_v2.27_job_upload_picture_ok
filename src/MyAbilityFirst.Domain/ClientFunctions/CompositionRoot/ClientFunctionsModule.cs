using Autofac;
using MyAbilityFirst.Domain.ClientFunctions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbilityFirst.Domain
{
	public class ClientFunctionsModule : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			// register ClientService
			builder
					.RegisterType<ClientService>()
					.As<IClientService>();
		}
	}
}