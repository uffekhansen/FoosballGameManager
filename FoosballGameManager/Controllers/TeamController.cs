using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;

namespace FoosballGameManager.Controllers
{
	public class TeamController : Controller
	{
		private readonly IGetEntitiesQuery<Player> _getPlayersQuery;

		public TeamController(IGetEntitiesQuery<Player> getPlayersQuery)
		{
			_getPlayersQuery = getPlayersQuery;
		}

		public ActionResult Index()
		{
			return View(_getPlayersQuery.Execute());
		}
	}
}
