using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbilityFirst.Domain
{
	public interface IAutomapperWrapper
	{
		TDestination Map<TSource, TDestination>(TSource source);
		TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
	}
}
