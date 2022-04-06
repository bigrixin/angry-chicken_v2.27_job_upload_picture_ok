using Microsoft.AspNet.Identity.EntityFramework;
using MyAbilityFirst.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAbilityFirst.Domain
{
	public class UserService : IUserService
	{

		#region Fields

		private readonly IWriteEntities _entities;

		#endregion

		#region Ctor

		public UserService(IWriteEntities entities)
		{
			this._entities = entities;
		}

		#endregion

		#region IUserService

		public void CreateClient(IdentityUser aspNetUser)
		{
			if (aspNetUser == null)
				throw new ArgumentNullException("aspNetUser");

			var now = DateTime.Now;

			var client = new Client(aspNetUser.Id);
			client.CreatedAt = now;
			client.UpdatedAt = now;
			client.Email = aspNetUser.Email;
			client.Status = UserStatus.Registered;
			this._entities.Create(client);
			this._entities.Save();
		}

		public void CreateCareWorker(IdentityUser aspNetUser)
		{
			if (aspNetUser == null)
				throw new ArgumentNullException("aspNetUser");

			var now = DateTime.Now;

			var worker = new CareWorker(aspNetUser.Id);
			worker.CreatedAt = now;
			worker.UpdatedAt = now;
			worker.Email = aspNetUser.Email;
			worker.Status = UserStatus.Registered;
			this._entities.Create(worker);
			this._entities.Save();
		}

		public User FindUser(int? id)
		{
			return this._entities.Get<User>(u => u.ID == id).Single();
		}

		public List<User> GetAllUser()
		{
			var users = this._entities.Get<User>().ToList();
			return users;
		}

		public User FindUserByLoginID(string loginID)
		{
			var users = this._entities.Get<User>(u => u.LoginIdentityId == loginID);
			if (users.Any())
			{
				return users.First();
			}
			return null;
		}

		#endregion

	}
}