using MyAbilityFirst.Domain.Shared.Models.ValueObject;
using MyAbilityFirst.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyAbilityFirst.Domain
{
	public interface IPresentationService
	{
		SelectList GetSuburbSelectList();
		SelectList GetSubCategorySelectList(string categoryName);
		List<Subcategory> GetSubCategoryList(string categoryName);
		bool PersistUserSubCategory(int[] postedSubCategoryIDs, string categoryName, int userID);
		List<Subcategory> GetSubCategoryListByUser(string categoryName, int userID);
		List<Subcategory> GetUserAttachmentList(string categoryName, int userId);
		Dictionary<int, string> GetUserAttachmentUrlList(int userId);
		SelectList GetPatientSelectList(int id);
	}
}
