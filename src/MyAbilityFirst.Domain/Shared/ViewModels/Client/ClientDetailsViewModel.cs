using MyAbilityFirst.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyAbilityFirst.Models
{
	public class ClientDetailsViewModel
	{
		public int ClientID { get; set; }

		// User
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DoB { get; set; }
		public int GenderID { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string Phone { get; set; }
		public int ResidentialSuburbID { get; set; }

		// Client
		public Notifications Notifications { get; set; }
		public Disclaimers Disclaimers { get; set; }
		public int ProvisionLocationSuburbID { get; set; }

		// View Texts
		public string Gender { get; set; }
		public string Suburb { get; set; }
		public string ProvisionLocation { get; set; }

		// single select dropdowns
		public SelectList SuburbDropDownList { get; set; }
		public SelectList GenderDropDownList { get; set; }

		// multi selects
		public List<Subcategory> MarketingInfoList { get; set; }

		// for tracking changes
		public List<Subcategory> PreviousMarketingInfo { get; set; }
		public int[] PostedMarketingInfo { get; set; }

	}
}