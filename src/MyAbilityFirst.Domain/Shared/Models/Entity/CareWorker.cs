using System;

namespace MyAbilityFirst.Domain
{
	public class CareWorker : User
	{

		#region Properties

		public bool HasWWCC { get; set; }
		public bool HasWWVPC { get; set; }

		#endregion

		#region Ctor

		protected CareWorker()
		{
			// required by EF
		}

		public CareWorker(string aspNetIdentity) : base(aspNetIdentity)
		{
		}

		#endregion

	}
}