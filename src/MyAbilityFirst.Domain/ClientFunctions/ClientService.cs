using MyAbilityFirst.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using MyAbilityFirst.Domain.Common;
using MyAbilityFirst.Domain.Shared.Models.ValueObject;
using MyAbilityFirst.Models;

namespace MyAbilityFirst.Domain.ClientFunctions
{
	public class ClientService : IClientService
	{

		#region Fields

		private readonly IWriteEntities _entities;
		private IAutomapperWrapper _mapper;
		private readonly IUploadService _uploadServices;
		private readonly IUserService _userServices;

		#endregion

		#region Ctor

		public ClientService(IWriteEntities entities, IAutomapperWrapper mapper, IUploadService uploadServices, IUserService userServices)
		{
			this._entities = entities;
			this._mapper = mapper;
			this._uploadServices = uploadServices;
			this._userServices = userServices;
		}

		#endregion

		#region IClientService

		public Client RetrieveClient(int clientID)
		{
			return this._entities.Single<Client>(c => c.ID == clientID);
		}

		public Client RetrieveClientByLoginID(string identityId)
		{
			return this._entities.Single<Client>(c => c.LoginIdentityId == identityId);
		}

		public Client UpdateClient(Client clientData)
		{
			clientData.Status = UserStatus.Active;
			clientData.UpdatedAt = DateTime.Now;
			this._entities.Update(clientData);
			this._entities.Save();
			return clientData;
		}

		public Patient CreatePatient(int clientID, Patient patientData)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				Patient newPatient = parentClient.AddNewPatient(patientData);
				this._entities.Update(parentClient);
				this._entities.Save();
				return newPatient;
			}
			return null;
		}

		public Patient RetrievePatient(int clientID, int patientID)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				return parentClient.GetExistingPatient(patientID);
			}
			return null;
		}

		public List<Patient> RetrieveAllPatients(int clientID)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				return parentClient.GetAllExistingPatients();
			}
			return null;
		}

		public Patient UpdatePatient(int clientID, Patient patientData)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				parentClient.UpdateExistingPatient(patientData);
				this._entities.Update(parentClient);
				this._entities.Save();
				return patientData;
			}
			return null;

		}

		public Contact CreateContact(int clientID, int patientID, Contact contactData)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				Patient parentPatient = parentClient.GetExistingPatient(patientID);
				if (parentPatient != null)
				{
					Contact newContact = parentPatient.AddNewContact(contactData);
					parentClient.UpdateExistingPatient(parentPatient);
					this._entities.Update(parentClient);
					this._entities.Save();
					return newContact;
				}
				return null;
			}
			return null;
		}

		public Contact RetrieveContact(int clientID, int patientID, int contactID)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				Patient parentPatient = parentClient.GetExistingPatient(patientID);
				if (parentPatient != null)
				{
					Contact contact = parentPatient.GetExistingContact(contactID);
					return contact;
				}
				return null;
			}
			return null;
		}

		public List<Contact> RetrieveAllContacts(int clientID, int patientID)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				Patient parentPatient = parentClient.GetExistingPatient(patientID);
				if (parentPatient != null)
				{
					var contacts = parentPatient.GetAllExistingContacts();
					return contacts;
				}
				return null;
			}
			return null;
		}

		public Contact UpdateContact(int clientID, int patientID, Contact contactData)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				Patient parentPatient = parentClient.GetExistingPatient(patientID);
				if (parentPatient != null)
				{
					Contact existingContact = RetrieveContact(clientID, patientID, contactData.ID);
					_mapper.Map(contactData, existingContact);
					parentPatient.UpdateExistingContact(existingContact);
					parentClient.UpdateExistingPatient(parentPatient);
					this._entities.Update(existingContact);
					this._entities.Save();
					return existingContact;
				}
				return null;
			}
			return null;
		}

		public bool DeleteContact(int clientID, int patientID, int contactID)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				Patient parentPatient = parentClient.GetExistingPatient(patientID);
				if (parentPatient != null)
				{
					Contact contact = parentPatient.RemoveContact(contactID);
					this._entities.Delete(contact);
					this._entities.Save();
					return true;
				}
				return false;
			}
			return false;
		}

		public List<Contact> ReplaceAllContacts(int clientID, int patientID, List<Contact> contacts)
		{
			List<Contact> existingContacts = RetrieveAllContacts(clientID, patientID) ?? new List<Contact>();
			IEnumerable<Contact> actionablecontacts;

			// CREATE new Contact if old dataset doesn't have new contacts
			actionablecontacts = contacts.Except(existingContacts, new ContactComparer());
			foreach (Contact contact in actionablecontacts)
			{
				contact.PatientID = patientID;
				CreateContact(clientID, contact.PatientID, contact);
			}

			// UPDATE Contact if already exists
			actionablecontacts = contacts.Intersect(existingContacts, new ContactComparer());
			foreach (Contact contact in actionablecontacts)
			{
				UpdateContact(clientID, contact.PatientID, contact);
			}

			// DELETE old Contact if new dataset doesn't have old contacts
			actionablecontacts = existingContacts.Except(contacts, new ContactComparer());
			foreach (Contact contact in actionablecontacts)
			{
				DeleteContact(clientID, contact.PatientID, contact.ID);
			}
			return contacts;
		}

		public Shortlist CreateShortlist(int clientID, Shortlist shortlistData)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				parentClient.AddNewShortlist(shortlistData);

				_entities.Update(parentClient);
				_entities.Save();
				return shortlistData;
			}
			return null;
		}

		public Shortlist RetrieveShortlistByCareWorkerID(int clientID, int careWorkerID)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				return parentClient.GetExistingShortlist(careWorkerID);
			}
			return null;
		}

		public List<Shortlist> RetrieveAllShortlists(int clientID)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				return parentClient.GetAllExistingShortlists();
			}
			return null;
		}

		public Shortlist UpdateShortlist(int clientID, Shortlist shortlistData)
		{
			Client parentClient = RetrieveClient(clientID);
			if (parentClient != null)
			{
				parentClient.UpdateExistingShortlist(shortlistData);

				this._entities.Update(parentClient);
				_entities.Save();
				return shortlistData;
			}
			return null;
		}

		public Shortlist AddOrUpdateShortlist(int clientID, Shortlist shortlistData)
		{
			Shortlist shortlist = RetrieveShortlistByCareWorkerID(clientID, shortlistData.CareWorkerID);
			if (shortlist == null)
			{
				shortlist = CreateShortlist(clientID, shortlistData);
			}
			else
			{
				shortlist = _mapper.Map<Shortlist, Shortlist>(shortlistData, shortlist);
				UpdateShortlist(clientID, shortlist);
			}
			return shortlist;
		}

		public Job PostNewJob(int ownerClientId, JobViewModel model)
		{
			if (ownerClientId < 1)
				throw new ArgumentException("ownerClientId must be an ID greater than 1.");

			var client = this._entities.Single<Client>(c => c.ID == ownerClientId);
			if (client == null)
				throw new ArgumentNullException("client");

			var now = DateTime.Now;
			var clientId = client.ID;

			var job = new Job(clientId);
			job = _mapper.Map<JobViewModel, Job>(model);
			job.CreatedAt = now;
			job.UpdatedAt = now;
			job.JobStatus = "New";

			client.PostedJobs.Add(job);

			this._entities.Update(client);
			this._entities.Save();

			return job;
		}

		public void EditJob(int ownerClientId, JobViewModel model)
		{
			if (ownerClientId < 1)
				return;

			var client = this._entities.Single<Client>(c => c.ID == ownerClientId);
			if (client == null)
				throw new ArgumentNullException("client");

			var now = DateTime.Now;
			var clientId = client.ID;
 

			Job job = client.PostedJobs.SingleOrDefault<Job>(j => j.ID == model.Id);
			
			if (job != null)
			{
				job = _mapper.Map<JobViewModel, Job>(model);
				job.UpdatedAt = now;
				job.JobStatus = "Update";

				//Here need change, entity update does not work? why ??
				this._entities.Update(client);
				this._entities.Save();
			}
		}

		public void DeleteJob(int ownerClientId, int jobId)
		{
			if (ownerClientId < 1)
				return;

			var client = this._entities.Single<Client>(c => c.ID == ownerClientId);
			if (client == null)
				throw new ArgumentNullException("client");

			Job job = client.PostedJobs.SingleOrDefault(j => j.ID == jobId);
			if (job != null)
			{
				//if (!String.IsNullOrEmpty(job.PictureURL))
				//	this._uploadService.DeleteFileFromServer(job.PictureURL);        //delete picture from server
				client.PostedJobs.Remove(job);
				this._entities.Delete(job);
				this._entities.Update(client);
				this._entities.Save();
			}
		}

		public void UpdatePatientAttachment(int userId, int itemId, string url)
		{
			var currentPatient = this._entities.Get<Patient>(a => a.ID == userId).Single();
			if (currentPatient != null)
			{
				UserAttachment attachment = new UserAttachment();
				attachment.UserID = currentPatient.ID;
				attachment.SubCategoryID = itemId;
				attachment.URL = url;
				var userAttachment = this._entities.Get<UserAttachment>(c => c.UserID == userId && c.SubCategoryID == itemId);
				DeletePatientAttachment(userId, itemId);
				this._entities.Create(attachment);
				this._entities.Save();
			}

		}

		public void DeletePatientAttachment(int userId, int itemId)
		{
			var currentPatient = this._entities.Get<Patient>(a => a.ID == userId).Single();
			if (currentPatient != null)
			{
				var userAttachments = this._entities.Get<UserAttachment>(c => c.UserID == userId && c.SubCategoryID == itemId);
				foreach (var item in userAttachments)
				{
					this._entities.Delete(item);
					this._entities.Save();
				}
			}
		}
		#endregion

	}
}
