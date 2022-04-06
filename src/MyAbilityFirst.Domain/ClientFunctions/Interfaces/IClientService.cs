using MyAbilityFirst.Models;
using System;
using System.Collections.Generic;

namespace MyAbilityFirst.Domain.ClientFunctions
{
	public interface IClientService
	{
		// Client Services
		Client RetrieveClient(int clientID);
		Client RetrieveClientByLoginID(string identityId);
		Client UpdateClient(Client updatedClient);

		// Patient Services
		Patient CreatePatient(int clientID, Patient patientData);
		Patient RetrievePatient(int clientID, int patientID);
		List<Patient> RetrieveAllPatients(int clientID);
		Patient UpdatePatient(int clientID, Patient patientData);

		// Contact Services
		Contact CreateContact(int clientID, int patientID, Contact contactData);
		Contact RetrieveContact(int clientID, int patientID, int contactID);
		List<Contact> RetrieveAllContacts(int clientID, int patientID);
		Contact UpdateContact(int clientID, int patientID, Contact contactData);
		bool DeleteContact(int clientID, int patientID, int contactID);
		List<Contact> ReplaceAllContacts(int clientID, int patientID, List<Contact> contacts);

		// Shortlist Services
		Shortlist CreateShortlist(int clientID, Shortlist shortlistData);
		Shortlist RetrieveShortlistByCareWorkerID(int clientID, int careWorkerID);
		List<Shortlist> RetrieveAllShortlists(int clientID);
		Shortlist UpdateShortlist(int clientID, Shortlist shortlistData);
		Shortlist AddOrUpdateShortlist(int clientID, Shortlist shortlistData);

		Job PostNewJob(int ownerClientId, JobViewModel model);

		void EditJob(int ownerClientId, JobViewModel model);

		void DeleteJob(int ownerClientId, int jobId);

		void UpdatePatientAttachment(int userId, int itemId, string url);
		void DeletePatientAttachment(int userId, int itemId);
	}
}
