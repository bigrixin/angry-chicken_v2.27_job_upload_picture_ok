using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbilityFirst.Domain
{
	public class ContactComparer : IEqualityComparer<Contact>
	{
		public bool Equals (Contact c1, Contact c2)
		{
			return c1.ID == c2.ID;
		}

		public int GetHashCode(Contact c)
		{
			return c.ID.GetHashCode();
		}
	}
}
