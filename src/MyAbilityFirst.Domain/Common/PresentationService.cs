using MyAbilityFirst.Domain.Shared.Models.ValueObject;
using MyAbilityFirst.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyAbilityFirst.Domain
{
	public class PresentationService : IPresentationService
	{

		#region Fields

		private readonly IWriteEntities _entities;

		#endregion

		#region Ctor

		public PresentationService(IWriteEntities entities)
		{
			_entities = entities;
		}

		#endregion

		#region PresentationService

		public SelectList GetSuburbSelectList()
		{
			return new SelectList(
					this._entities.Get<Suburb>(
									null,
									o => o.OrderBy(s => s.Postcode)),
					"ID",
					"Name");
		}

		public SelectList GetSubCategorySelectList(string categoryName)
		{
			return new SelectList(
					getSubCategory(getCategory(categoryName).ID),
					"ID",
					"Name");
		}

		public List<Subcategory> GetSubCategoryList(string categoryName)
		{
			return getSubCategory(getCategory(categoryName).ID);
		}

		public List<Subcategory> GetSubCategoryListByUser(string categoryName, int userID)
		{
			int categoryID = getCategory(categoryName).ID;
			IEnumerable<Subcategory> scs = getSubCategory(categoryID);
			IEnumerable<UserSubcategory> usc = this._entities.Get<UserSubcategory>(x => x.UserID == userID);
			return scs.Where(x => usc.Any(y => y.SubCategoryID == x.ID)).ToList();
		}

		public bool PersistUserSubCategory(int[] postedSubCategoryIDs, string categoryName, int userID)
		{
			try
			{
				int[] previousSubCategoryIDs = GetSubCategoryListByUser(categoryName, userID).Select(x => x.ID).ToArray();
				postedSubCategoryIDs = postedSubCategoryIDs ?? new int[0];

				// remove unselected
				IEnumerable<int> unselectedIDs = previousSubCategoryIDs.Except(postedSubCategoryIDs);
				foreach (int ids in unselectedIDs)
				{
					UserSubcategory usc = this._entities.Single<UserSubcategory>(x => x.SubCategoryID == ids && x.UserID == userID);
					this._entities.Delete(usc);
					this._entities.Save();
				}
				// persist selected
				IEnumerable<int> selectedIDs = postedSubCategoryIDs.Except(previousSubCategoryIDs);
				foreach (int ids in selectedIDs)
				{
					UserSubcategory usc = new UserSubcategory();
					usc.SubCategoryID = ids; usc.UserID = userID;
					this._entities.Create(usc);
					this._entities.Save();
				}
				return true;
			}
			catch (Exception e)
			{
				throw (e);
			}
		}

		public List<Subcategory> GetUserAttachmentList(string categoryName, int userId)
		{
			int categoryID = getCategory(categoryName).ID;
			IEnumerable<Subcategory> scs = getSubCategory(categoryID);
			IEnumerable<UserAttachment> usc = this._entities.Get<UserAttachment>(x => x.UserID == userId);
			return scs.Where(x => usc.Any(y => y.SubCategoryID == x.ID)).ToList();
		}

		public Dictionary<int, string> GetUserAttachmentUrlList(int userId)
		{
			return this._entities.Get<UserAttachment>(x => x.UserID == userId,
			o => o.OrderBy(x => x.SubCategoryID)).ToDictionary(p => p.SubCategoryID, p => p.URL);
		}

		public SelectList GetPatientSelectList(int id)
		{
			return new SelectList(
			getPatientList(id),
			"ID",
			"LastName");
		}
		#endregion

		#region Helper

		private Category getCategory(string categoryName)
		{
			return this._entities.Get<Category>(
			cc => cc.Name.Equals(categoryName),
			null).First();
		}

		private List<Subcategory> getSubCategory(int categoryID)
		{
			return this._entities.Get<Subcategory>(
			 sc => sc.CategoryID == categoryID,
			 o => o.OrderBy(sc => sc.ID)).ToList();
		}

		private List<Patient> getPatientList(int id)
		{
			return this._entities.Get<Patient>(a => a.ClientID == id).ToList();
		}

		#endregion

	}
}