using MyAbilityFirst.Domain;
using MyAbilityFirst.Domain.ClientFunctions;
using MyAbilityFirst.Infrastructure.Auth;
using MyAbilityFirst.Models;
using System.Web.Mvc;

namespace MyAbilityFirst.Controllers
{
	[Authorize(Roles = "Client")]
	public class ClientController : Controller
	{

		#region Fields

		private readonly IAutomapperWrapper _mapper;
		private readonly IClientService _clientServices;
		private readonly IPresentationService _categoryServices;
		private readonly Client _loggedInClient;

		#endregion

		#region Ctor

		public ClientController(IAutomapperWrapper mapper, IClientService clientServices, IPresentationService categoryServices, ILoginService loginServices)
		{
			this._mapper = mapper;
			this._clientServices = clientServices;
			this._categoryServices = categoryServices;
			this._loggedInClient = _clientServices.RetrieveClientByLoginID(loginServices.GetCurrentLoginIdentityID());
		}

		#endregion

		#region Actions
		
		[HttpGet, Route("client/editprofile")]
		public ActionResult EditProfile()
		{
			ClientDetailsViewModel vm = mapClientToVM();
			return View(vm);
		}


		[HttpPost, Route("client/editprofile")]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(ClientDetailsViewModel vm)
		{
			Client updatedClient = mapVMToClient(vm);
			if (ModelState.IsValid) 
			{
				_clientServices.UpdateClient(updatedClient);
				persistCategories(vm);
			}
			return RedirectToAction("MyAccount/");
		}

		[HttpGet, Route("client/myaccount")]
		public ActionResult MyAccount()
		{
			ClientDetailsViewModel vm = mapClientToVM();
			return View(vm);
		}

		[HttpGet]
		public ActionResult ShortlistButton (int careWorkerID) {
			ShortlistViewModel vm = new ShortlistViewModel();
			Shortlist shortlist = _clientServices.RetrieveShortlistByCareWorkerID(_loggedInClient.ID, careWorkerID);
			vm = _mapper.Map<Shortlist, ShortlistViewModel>(shortlist, vm) ?? new ShortlistViewModel();
			vm.CareWorkerID = shortlist == null ? careWorkerID : shortlist.CareWorkerID;
			return PartialView(vm);
		}


		[HttpPost]
		public ActionResult UpdateShortlist (ShortlistViewModel vm)
		{
			Shortlist shortlist = 
				_clientServices.RetrieveShortlistByCareWorkerID(_loggedInClient.ID, vm.CareWorkerID) ?? 
				new Shortlist(_loggedInClient.ID, vm.CareWorkerID, vm.Selected);
			shortlist = _mapper.Map<ShortlistViewModel,Shortlist>(vm, shortlist);
			if (ModelState.IsValid)
			{
				_clientServices.AddOrUpdateShortlist(_loggedInClient.ID, shortlist);
			}
			return Json(vm);
		}

		#endregion

		#region Helpers

		private void persistCategories(ClientDetailsViewModel vm)
		{
			_categoryServices.PersistUserSubCategory(vm.PostedMarketingInfo, "MarketingInfo", vm.ClientID);
		}

		private ClientDetailsViewModel mapClientToVM()
		{
			ClientDetailsViewModel vm = new ClientDetailsViewModel();
			return _mapper.Map<Client, ClientDetailsViewModel>(_loggedInClient);
		}

		private Client mapVMToClient(ClientDetailsViewModel vm)
		{
			return _mapper.Map(vm, _loggedInClient);
		}

		#endregion

	}
}