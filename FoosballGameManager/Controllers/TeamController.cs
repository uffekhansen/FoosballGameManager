using System.Web.Mvc;
using DAL.Queries;

namespace FoosballGameManager.Controllers
{
	public class TeamController : Controller
	{
		private readonly IGetPlayersQuery _getPlayersQuery;

		public TeamController(IGetPlayersQuery getPlayersQuery)
		{
			_getPlayersQuery = getPlayersQuery;
		}

		public ActionResult Index()
		{
			return View(_getPlayersQuery.Execute());
		}
	}
}
