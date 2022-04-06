using MyAbilityFirst.Domain;
using System.Collections.Generic;

namespace MyAbilityFirst.Models
{
	public class PatientAttachmentViewModel
	{
		public int PatientID { get; set; }
		public int ClientID { get; set; }

		public Dictionary<int, string> AttachmentUrlList { get; set; }

		// multi selects
		public List<Subcategory> AttachmentList { get; set; }

		// for tracking changes
		public List<Subcategory> PreviousAttachmentList { get; set; }
		public int[] UploadFileSubCategoryIDs { get; set; }
	}
}