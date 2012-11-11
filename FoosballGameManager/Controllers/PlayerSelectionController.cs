using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using FoosballGameManager.ViewModels;

namespace FoosballGameManager.Controllers
{
	public class PlayerSelectionController : Controller
	{
		private readonly IGetPlayersByIdsQuery _getPlayersByIdsQuery;
		private readonly IGetEveryEntityQuery<Player> _getPlayerEveryEntityQuery;
		private readonly ITeamCreator _teamCreator;
		private readonly ITournamentCreator _tournamentCreator;

		public PlayerSelectionController(IGetEveryEntityQuery<Player> getPlayerEveryEntityQuery, IGetPlayersByIdsQuery getPlayersByIdsQuery, ITeamCreator teamCreator, ITournamentCreator tournamentCreator)
		{
			_getPlayerEveryEntityQuery = getPlayerEveryEntityQuery;
			_getPlayersByIdsQuery = getPlayersByIdsQuery;
			_teamCreator = teamCreator;
			_tournamentCreator = tournamentCreator;
		}

		public ActionResult Index()
		{
			var viewModel = new PlayersViewModel
			{
				Players = _getPlayerEveryEntityQuery.Execute(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(PlayersViewModel playersViewModel)
		{
			IEnumerable<Player> players;
			try
			{
				players = _getPlayersByIdsQuery.Execute(playersViewModel.SelectedPlayers);
			}
			catch (NotFoundException exception)
			{
				ModelState.AddModelError("_FORM", exception.Message);
				return View("Index");
			}

			CreateTeams(players);
			CreateTournament();

			return RedirectToAction("Show", "Tournament");
		}

		private Tournament CreateTournament()
		{
			return _tournamentCreator.CreateTournament();
		}

		private IEnumerable<Team> CreateTeams(IEnumerable<Player> players)
		{
			_teamCreator.Players = players;
			return _teamCreator.CreateTeams();
		}
	}
}
