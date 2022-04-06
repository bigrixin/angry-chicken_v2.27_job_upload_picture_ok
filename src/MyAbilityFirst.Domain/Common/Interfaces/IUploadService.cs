using System.Collections.Generic;
using System.Web;

namespace MyAbilityFirst.Domain.Common
{
	public interface IUploadService
	{
		string UploadToAzureStorage(HttpPostedFileBase file, string containerName);
		bool DeleteFromAzureStorage(string fileURL, string containerName);
		void ListBlobItemFromAzure(string containerName);
	}
}
