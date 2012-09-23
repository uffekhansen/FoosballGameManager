using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;

namespace FoosballGameManager.Controllers
{
	public class PlayerController : Controller
	{
		private readonly IAddPlayerQuery _addPlayerQuery;

		public PlayerController(IAddPlayerQuery addPlayerQuery)
		{
			_addPlayerQuery = addPlayerQuery;
		}

		public ActionResult AddPlayer()
		{
			_addPlayerQuery.Execute(new Player());
			return RedirectToAction("Index", "Team");
		}
	}
}
