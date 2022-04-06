using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyAbilityFirst.Domain
{
	public class Job
	{

		#region Properties

		public int ID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public int SuburbId { get; set; }
		public int GenderId { get; set; }
		public int ServiceId { get; set; }

		public DateTime? ServiceAt { get; set; }
		public string JobStatus { get; set; }
		public string PictureURL { get; set; }

		public int ClientId { get; private set; }
		public int PatientId { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		#endregion

		#region Ctor

		protected Job()
		{
			// required by EF
		}

		public Job(int clientId)
		{
			this.ClientId = clientId;
		}

		#endregion

	}
}