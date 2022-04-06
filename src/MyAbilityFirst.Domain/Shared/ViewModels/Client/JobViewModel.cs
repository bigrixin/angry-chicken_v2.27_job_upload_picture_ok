using MyAbilityFirst.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyAbilityFirst.Models
{
	public class JobViewModel
	{
		[Key]
		public int Id { get; set; }
		[Display(Name ="Patient")]
		public int PatientId { get; set; }

		[Required(AllowEmptyStrings = false)]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required]
		[StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Suburb")]
		public int SuburbId { get; set; }

		[Display(Name = "Gender")]
		public int GenderId { get; set; }

		[Display(Name = "Service Required")]
		public int ServiceId { get; set; }
 
		[Display(Name = "Picture")]
		public string PictureURL { get; set; }

		[Required]
		[Display(Name = "Start Date")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? ServicedAt { get; set; }

		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public IEnumerable<SelectListItem> SuburbDropDownList { get; set; }
		public IEnumerable<SelectListItem> GenderDropDownList { get; set; }
		public IEnumerable<SelectListItem> ServiceDropDownList { get; set; }
		public IEnumerable<SelectListItem> PatientDropDownList { get; set; }
	}
}