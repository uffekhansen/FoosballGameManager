using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;
using Domain.Services;
using FoosballGameManager.ViewModels;

namespace FoosballGameManager.Controllers
{
	public class TeamController : Controller
	{
		private readonly IGetEntitiesQuery<Player> _getPlayersQuery;
		private readonly ITeamCreator _teamCreator;

		public TeamController(IGetEntitiesQuery<Player> getPlayersQuery, ITeamCreator teamCreator)
		{
			_getPlayersQuery = getPlayersQuery;
			_teamCreator = teamCreator;
		}

		public ActionResult Index()
		{
			var viewModel = new PlayersViewModel
			{
				Players = _getPlayersQuery.Execute(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(PlayersViewModel playersViewModel)
		{
			_teamCreator.CreateTeams();

			return RedirectToAction("Index");
		}
	}
}
