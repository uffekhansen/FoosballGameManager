using System.Web.Mvc;
using DAL.Queries;

namespace FoosballGameManager.Controllers
{
	public class PlayerController : Controller
	{
		public PlayerController()
		{
		}

		public ActionResult AddPlayer()
		{
			return RedirectToAction("Index", "Team");
		}
	}
}
