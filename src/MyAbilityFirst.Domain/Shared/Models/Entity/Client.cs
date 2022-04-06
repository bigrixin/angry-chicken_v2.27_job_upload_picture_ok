using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAbilityFirst.Domain
{
	public class Client : User
	{

		#region Properties

		public int ProvisionLocationSuburbID { get; set; }
		public Disclaimers Disclaimers { get; set; }
		public Notifications Notifications { get; set; }

		public virtual ICollection<Patient> Patients { get; set; }
		public virtual ICollection<Job> PostedJobs { get; set; }
		public virtual ICollection<Shortlist> Shortlists { get; set; }

		#endregion

		#region Ctor

		protected Client()
		{
			// required by EF
			this.Disclaimers = new Disclaimers();
			this.Notifications = new Notifications();
			this.Patients = new List<Patient>();
			this.PostedJobs = new List<Job>();
			this.Shortlists = new List<Shortlist>();
		}

		public Client(string loginIdentityId) : base(loginIdentityId)
		{
			this.Disclaimers = new Disclaimers();
			this.Notifications = new Notifications();
			this.Patients = new List<Patient>();
			this.PostedJobs = new List<Job>();
			this.Shortlists = new List<Shortlist>();
		}

		#endregion

		public Patient AddNewPatient(Patient patientData)
		{
			var patients = Patients.Where(x => x.ID == patientData.ID);
			if (patients.Any())
			{
				return null;
			}
			patientData.Status = UserStatus.Active;
			patientData.CreatedAt = DateTime.Now;
			patientData.UpdatedAt = DateTime.Now;
			Patients.Add(patientData);
			return patientData;
		}

		public Patient UpdateExistingPatient(Patient patientData)
		{
			var patients = Patients.Where(p => p.ID == patientData.ID);
			if (patients.Any())
			{
				Patients.Remove(patients.Single(p => p.ID == patientData.ID));
				patientData.UpdatedAt = DateTime.Now;
				Patients.Add(patientData);
			}
			return patientData;
		}

		public Patient GetExistingPatient(int patientID)
		{
			var patients = Patients.Where(p => p.ID == patientID);
			if (patients.Any())
			{
				return patients.Single(p => p.ID == patientID);
			}
			return null;
		}

		public List<Patient> GetAllExistingPatients()
		{
			return this.Patients == null ? new List<Patient>() : this.Patients.ToList();
		}

		public Shortlist AddNewShortlist(Shortlist shortlistData)
		{
			
			var shortlists = Shortlists.Where(s => s.ClientID == this.ID && s.CareWorkerID == shortlistData.CareWorkerID);
			if (shortlists.Any())
			{
				return null;
			}
			Shortlists.Add(shortlistData);
			return shortlistData;
		}

		public Shortlist UpdateExistingShortlist(Shortlist shortlistData)
		{
			var shortlists = Shortlists.Where(s => s.ID == shortlistData.ID);
			if (shortlists.Any())
			{
				Shortlists.Remove(shortlists.Single(s => s.ID == shortlistData.ID));
				Shortlists.Add(shortlistData);
			}
			return null;
		}

		public Shortlist GetExistingShortlist(int careworkerID)
		{
			var shortlists = Shortlists.Where(s => s.CareWorkerID == careworkerID);
			if (shortlists.Any())
			{
				return shortlists.Single(s => s.CareWorkerID == careworkerID);
			}
			return null;
		}

		public List<Shortlist> GetAllExistingShortlists()
		{
			return this.Shortlists == null ? new List<Shortlist>() : this.Shortlists.ToList();
		}
	}
}