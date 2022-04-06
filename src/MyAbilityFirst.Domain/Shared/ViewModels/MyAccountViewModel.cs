using MyAbilityFirst.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAbilityFirst.Models
{
	public class MyAccountViewModel
	{
		// for tracking verified email
		public bool EmailVerified { get; set; }
		public string UserName { get; set; }
		public ICollection<Patient> PatientList { get; set; }
	}
}