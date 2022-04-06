using Autofac;
using MyAbilityFirst.Domain.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbilityFirst.Domain
{
	public class CommonModule : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			// register presentation layer services
			builder
				.RegisterType<PresentationService>()
				.As<IPresentationService>();

			// register Automapper Wrapper service
			builder
				.RegisterType<AutomapperWrapper>()
				.As<IAutomapperWrapper>();

			builder
				.RegisterType<UploadService>()
				.As<IUploadService>();

		}
	}
}
