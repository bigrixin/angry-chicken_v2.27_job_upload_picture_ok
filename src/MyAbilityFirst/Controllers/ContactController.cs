using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAbilityFirst.Infrastructure;
using MyAbilityFirst.Models;
using MyAbilityFirst.Domain.ClientFunctions;
using MyAbilityFirst.Domain;
using System.Web.Mvc;

namespace MyAbilityFirst.Controllers
{
    public class ContactController : Controller
    {

		#region Fields

		private readonly IPresentationService _viewModelServices;
		private readonly IClientService _clientServices;

		#endregion

		#region Ctor

		public ContactController(IPresentationService viewModelServices, IClientService clientServices)
		{
			this._viewModelServices = viewModelServices;
			this._clientServices = clientServices;
		}

		#endregion

		[HttpGet]
		public ActionResult _Contacts(int id)
		{
			PatientDetailsViewModel vm = new PatientDetailsViewModel();
			return PartialView(vm);
		}

	}
}