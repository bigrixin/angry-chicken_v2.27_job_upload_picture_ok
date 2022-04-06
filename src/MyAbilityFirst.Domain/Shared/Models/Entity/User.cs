using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyAbilityFirst.Domain
{
	public abstract class User
	{

		#region Properties

		public int ID { get; set; }

		public UserStatus Status { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int GenderID { get; set; }
		public DateTime? DoB { get; set; }
		public string Email { get; set; }
		public int ResidentialSuburbID { get; set; }
		public string Phone { get; set; }

		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public string LoginIdentityId { get; private set; }

		#endregion

		#region Ctor

		protected User()
		{
			// required by EF
		}

		public User(string LoginIdentityId)
		{
			this.LoginIdentityId = LoginIdentityId;
		}

		#endregion

	}
}
