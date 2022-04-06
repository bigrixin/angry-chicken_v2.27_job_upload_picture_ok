using MyAbilityFirst.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAbilityFirst.Models
{
	public class CareWorkerPublicProfileViewModel
	{
		public int CareWorkerID { get; set; }
		public ShortlistViewModel ShortlistViewModel { get; set; }
	}
}