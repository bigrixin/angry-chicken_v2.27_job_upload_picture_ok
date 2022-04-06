using MyAbilityFirst.Domain;
using MyAbilityFirst.Domain.ClientFunctions;
using MyAbilityFirst.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using MyAbilityFirst.Infrastructure.Auth;
using System.Configuration;
using System.Web;
using MyAbilityFirst.Domain.Common;

namespace MyAbilityFirst.Controllers
{
	[Authorize(Roles = "Client")]
	public class PatientController : Controller
	{

		#region Fields

		private readonly IClientService _clientServices;
		private readonly IPresentationService _categoryServices;
		private readonly IAutomapperWrapper _mapper;
		private readonly Client _loggedInClient;
		private readonly IUploadService _uploadServices;

		#endregion

		#region Ctor

		public PatientController(IClientService clientServices, IPresentationService categoryServices, IAutomapperWrapper mapper, ILoginService loginServices, IUploadService uploadServices)
		{
			this._clientServices = clientServices;
			this._categoryServices = categoryServices;
			this._mapper = mapper;
			this._loggedInClient = _clientServices.RetrieveClientByLoginID(loginServices.GetCurrentLoginIdentityID());
			this._uploadServices = uploadServices;
		}

		#endregion

		#region Actions

		[HttpGet, Route("patient/newprofile")]
		public ActionResult NewProfile()
		{
			PatientDetailsViewModel vm = mapPatientToVM(new Patient(_loggedInClient.ID));
			vm.Contacts = new List<Contact>();
			vm.Contacts.Add(new Contact());
			vm.Contacts.Add(new Contact());
			vm.Contacts.Add(new Contact());
			return View(vm);
		}

		[HttpPost, Route("patient/newprofile")]
		[ValidateAntiForgeryToken]
		public ActionResult NewProfile(PatientDetailsViewModel vm)
		{
			if (ModelState.IsValid)
			{
				Patient persistedPatient = _clientServices.CreatePatient(_loggedInClient.ID, mapVMToPatient(vm));
				vm.PatientID = persistedPatient.ID;
				_clientServices.ReplaceAllContacts(_loggedInClient.ID, persistedPatient.ID, vm.Contacts);

				persistCategories(vm);
				return RedirectToAction("updateattachment/" + persistedPatient.ID.ToString());
			}
			return RedirectToAction("MyAccount", "Client");
		}

		[HttpGet, Route("patient/editprofile/{id:int}")]
		public ActionResult EditProfile(int id)
		{
			Patient currentPatient = _clientServices.RetrievePatient(_loggedInClient.ID, id);
			if (currentPatient != null)
			{
				PatientDetailsViewModel vm = mapPatientToVM(currentPatient);
				return View(vm);
			}
			return RedirectToAction("MyAccount", "Client");
		}

		[HttpPost, Route("patient/editprofile/{id:int}")]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(PatientDetailsViewModel vm)
		{
			Patient currentPatient = _clientServices.RetrievePatient(_loggedInClient.ID, vm.PatientID);
			if (ModelState.IsValid && currentPatient != null)
			{
				Patient persistedPatient = _clientServices.UpdatePatient(_loggedInClient.ID, mapVMToPatient(vm));
				_clientServices.ReplaceAllContacts(_loggedInClient.ID, persistedPatient.ID, vm.Contacts);

				persistCategories(vm);
				return RedirectToAction("MyAccount/" + persistedPatient.ID.ToString());
			}
			return RedirectToAction("MyAccount", "Client");
		}

		[HttpGet, Route("patient/myaccount/{id:int}")]
		public ActionResult MyAccount(int id)
		{
			Patient currentPatient = _clientServices.RetrievePatient(_loggedInClient.ID, id);
			if (currentPatient != null)
			{
				PatientDetailsViewModel vm = mapPatientToVM(currentPatient);
				return View(vm);
			}
			return RedirectToAction("MyAccount", "Client");
		}

		[HttpGet, Route("patient/UpdateAttachment/{id:int}")]
		public ActionResult UpdateAttachment(int id)
		{
			Patient currentPatient = _clientServices.RetrievePatient(_loggedInClient.ID, id);
			if (currentPatient != null)
			{
				PatientAttachmentViewModel vm = mapPatientAttachmentToVM(currentPatient);
				ViewBag.PathUpload = "/Patient/UploadFileToAzure";
				ViewBag.PathDelete = "/Patient/DeleteFileFromAzure";
				return View(vm);
			}
			return RedirectToAction("MyAccount", "Client");
		}

		[HttpPost, Route("patient/UpdateAttachment/{id:int}")]
		public ActionResult UpdateAttachment(PatientAttachmentViewModel vm)
		{
			Patient currentPatient = _clientServices.RetrievePatient(_loggedInClient.ID, vm.PatientID);
			if (ModelState.IsValid && currentPatient != null)
			{
				return RedirectToAction("MyAccount", "Patient");
			}
			return RedirectToAction("MyAccount", "Client");
		}

		//upload file to Azure storage blobs for patient
		[HttpPost]
		public virtual ActionResult UploadFileToAzure(int itemId, int userId)
		{
			string path = ConfigurationManager.AppSettings["uploadAzurePath_Patient"];
			HttpPostedFileBase file = Request.Files[0];

			string url = this._uploadServices.UploadToAzureStorage(file, path);

			this._clientServices.UpdatePatientAttachment(userId, itemId, url);
			return userAttachmentProcess(url);
		}

		[HttpDelete]
		public virtual ActionResult DeleteFileFromAzure(string fileName, int userId, int itemId)
		{
			string path = ConfigurationManager.AppSettings["uploadAzurePath_Patient"];
			if (this._uploadServices.DeleteFromAzureStorage(fileName, path))
			{
				this._clientServices.DeletePatientAttachment(userId, itemId);
				return Json(new { message = "The file has delete !" }, "text/html");
			}

			else
				return Json(new { message = "Error" }, "text/html");
		}

		#endregion

		#region Helper

		private void persistCategories(PatientDetailsViewModel vm)
		{
			_categoryServices.PersistUserSubCategory(vm.PostedInterestSubCategoryIDs, "Interest", vm.PatientID);
			_categoryServices.PersistUserSubCategory(vm.PostedMedicalLivingNeedsSubCategoryIDs, "MedicalLivingNeed", vm.PatientID);
		}

		private PatientDetailsViewModel mapPatientToVM(Patient patient)
		{
			PatientDetailsViewModel vm = new PatientDetailsViewModel();
			vm = _mapper.Map<Patient, PatientDetailsViewModel>(patient);
			return vm;
		}

		private Patient mapVMToPatient(PatientDetailsViewModel vm)
		{
			Patient patient = _clientServices.RetrievePatient(_loggedInClient.ID, vm.PatientID);
			return _mapper.Map(vm, patient);
		}

		private PatientAttachmentViewModel mapPatientAttachmentToVM(Patient patient)
		{
			PatientAttachmentViewModel vm = new PatientAttachmentViewModel();
			vm = _mapper.Map<Patient, PatientAttachmentViewModel>(patient);
			return vm;
		}

		private JsonResult userAttachmentProcess(string url)
		{
			bool isUploaded = false;
			if (url != null)
			{
				isUploaded = true;
				string message = "100% complete";

				return Json(new
				{
					statusCode = 200,
					status = "File uploaded.",
					file = url,
					isUploaded = isUploaded,
					message = message
				}, "text/html");

			}
			else
			{
				string message = "Error";
				return Json(new
				{
					statusCode = 500,
					status = "Error uploading image.",
					file = string.Empty,
					isUploaded = isUploaded,
					message = message
				}, "text/html");
			}
		}
		#endregion

	}
}