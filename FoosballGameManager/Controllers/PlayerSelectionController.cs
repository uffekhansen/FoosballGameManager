using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Commands;
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
		private readonly IAddCommand<Tournament> _addTournamentCommand;
		private readonly ITeamCreatorFactory _teamCreatorFactory;
		private readonly IGetEveryEntityQuery<Player> _getEveryPlayerEntityQuery;
		private readonly ITournamentCreator _tournamentCreator;

		public PlayerSelectionController(IGetEveryEntityQuery<Player> getEveryPlayerEntityQuery, IGetPlayersByIdsQuery getPlayersByIdsQuery, 
			IAddCommand<Tournament> addTournamentCommand, ITeamCreatorFactory teamCreatorFactory, ITournamentCreator tournamentCreator)
		{
			_getEveryPlayerEntityQuery = getEveryPlayerEntityQuery;
			_getPlayersByIdsQuery = getPlayersByIdsQuery;
			_addTournamentCommand = addTournamentCommand;
			_teamCreatorFactory = teamCreatorFactory;
			_tournamentCreator = tournamentCreator;
		}

		public ActionResult Index()
		{
			var viewModel = new PlayersViewModel
			{
				Players = _getEveryPlayerEntityQuery.Execute(),
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

			var teamCreator = _teamCreatorFactory.MakeTeamCreator(playersViewModel.TeamGenerationMethod);
			var teams = teamCreator.CreateTeams();
			var tournament = CreateTournament(teams);

			_addTournamentCommand.Execute(tournament);

			return RedirectToAction("Show", "Tournament", new { tournament.Id });
		}

		private Tournament CreateTournament(IEnumerable<Team> teams)
		{
			return _tournamentCreator.CreateTournament(teams);
		}
	}
}
