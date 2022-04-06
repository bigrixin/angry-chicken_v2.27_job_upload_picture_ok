using MyAbilityFirst.Domain.Shared.Models.ValueObject;
using System.Collections.Generic;
using System.Linq;

namespace MyAbilityFirst.Domain
{
	public class Patient : User
	{

		#region Properties

		public int ClientID { get; set; }

		public int ProvisionLocationSuburbID { get; set; }
		public string Allergies { get; set; }

		public virtual ICollection<Contact> Contacts { get; set; }

		public string EmergencyResponse { get; set; }
		public HouseDetails HouseDetails { get; set; }
		public MedicationDetails MedicationDetails { get; set; }
		public int FirstLanguageID { get; set; }
		public int SecondLanguageID { get; set; }
		public int CultureID { get; set; }
		public int ReligionID { get; set; }
		public int CareTypeID { get; set; }
		public string GoalsAndAspirations { get; set; }

		#endregion

		#region Ctor

		protected Patient()
		{
			// required by EF

			this.Contacts = new List<Contact>();
		}

		public Patient(int clientID)
		{
			this.ClientID = clientID;
			this.Contacts = new List<Contact>();
			this.HouseDetails = new HouseDetails();
			this.MedicationDetails = new MedicationDetails();
		}

		#endregion

		public Contact AddNewContact(Contact contact)
		{
			var contacts = Contacts.Where(c => c.ID == contact.ID);
			if (contacts.Any())
			{
				return null;
			}
			Contacts.Add(contact);
			return contact;
		}

		public Contact UpdateExistingContact(Contact contact)
		{
			var contacts = Contacts.Where(c => c.ID == contact.ID);
			if (contacts.Any())
			{
				this.Contacts.Remove(contacts.Single(c => c.ID == contact.ID));
				this.Contacts.Add(contact);
			}
			return contact;
		}

		public Contact GetExistingContact(int contactID)
		{
			var contacts = Contacts.Where(c => c.ID == contactID);
			if (contacts.Any())
			{
				return contacts.Single(c => c.ID == contactID);
			}
			return null;
		}

		public List<Contact> GetAllExistingContacts()
		{
			return this.Contacts == null ? new List<Contact>() : this.Contacts.ToList();
		}

		public Contact RemoveContact(int contactID)
		{
			Contact existingContact = GetExistingContact(contactID);
			if (existingContact != null && this.Contacts.Remove(existingContact)) {
				return existingContact;
			}
			return null;
		}

	}
}