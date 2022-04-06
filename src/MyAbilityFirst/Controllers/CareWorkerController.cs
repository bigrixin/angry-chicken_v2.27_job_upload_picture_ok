using System.Web.Mvc;
using MyAbilityFirst.Models;
using MyAbilityFirst.Domain.ClientFunctions;
using MyAbilityFirst.Domain;
using MyAbilityFirst.Infrastructure.Auth;

namespace MyAbilityFirst.Controllers
{
	[Authorize(Roles = "CareWorker")]
	public class CareWorkerController : Controller
	{

		#region Fields

		private readonly IClientService _clientServices;
		private readonly IAutomapperWrapper _mapper;
		private readonly ILoginService _loginServices;

		#endregion

		#region Ctor

		public CareWorkerController(IClientService clientServices, IAutomapperWrapper mapper, ILoginService loginServices)
		{
			this._clientServices = clientServices;
			this._mapper = mapper;
			this._loginServices = loginServices;
			
		}

		#endregion

		#region Actions
		public ActionResult Info(string usertype)
		{
			ViewBag.UserEmail = User.Identity.Name;
			ViewBag.TypeOfUser = usertype;
			return View();
		}

		// GET: CareWorker
		public ActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult PublicProfile(int id)
		{
			CareWorkerPublicProfileViewModel vm = new CareWorkerPublicProfileViewModel();
			vm.CareWorkerID = id;
			return View(vm);
		}
		#endregion
	}
}