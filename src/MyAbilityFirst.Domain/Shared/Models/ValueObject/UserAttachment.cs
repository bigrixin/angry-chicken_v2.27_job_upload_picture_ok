using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbilityFirst.Domain.Shared.Models.ValueObject
{
	public class UserAttachment
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int SubCategoryID { get; set; }
		public string URL { get; set; }
	}
}
