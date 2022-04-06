using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbilityFirst.Domain
{
	public class Shortlist
	{
		#region Properties

		public int ID { get; set; }
		public int ClientID { get; set; }
		public int CareWorkerID { get; set; }
		public bool Selected { get; set; }

		#endregion

		#region Ctor

		protected Shortlist()
		{
			// Required by EF
		}

		public Shortlist(int clientID, int careworkerID, bool selected)
		{
			this.ClientID = clientID;
			this.CareWorkerID = careworkerID;
			this.Selected = selected;
		}

		#endregion
	}
}
