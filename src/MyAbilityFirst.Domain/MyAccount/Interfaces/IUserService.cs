using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;


namespace MyAbilityFirst.Domain
{
	public interface IUserService
	{
		void CreateClient(IdentityUser aspNetUser);
		void CreateCareWorker(IdentityUser aspNetUser);

		User FindUser(int? id);
		List<User> GetAllUser();
		User FindUserByLoginID(string loginID);
	}
}