using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAbilityFirst.Domain
{
	public class UserSubcategory
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int SubCategoryID { get; set; }
		public string CustomValue { get; set; }
	}
}