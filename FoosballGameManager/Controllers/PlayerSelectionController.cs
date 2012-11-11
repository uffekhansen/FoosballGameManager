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
		private readonly IGetPlayersQuery _getPlayersQuery;
		private readonly IGetEntitiesQuery<Player> _getPlayerEntitiesQuery;
		private readonly ITeamCreator _teamCreator;

		public PlayerSelectionController(IGetEntitiesQuery<Player> getPlayerEntitiesQuery, IGetPlayersQuery getPlayersQuery, ITeamCreator teamCreator)
		{
			_getPlayerEntitiesQuery = getPlayerEntitiesQuery;
			_getPlayersQuery = getPlayersQuery;
			_teamCreator = teamCreator;
		}

		public ActionResult Index()
		{
			var viewModel = new PlayersViewModel
			{
				Players = _getPlayerEntitiesQuery.Execute(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(PlayersViewModel playersViewModel)
		{
			IEnumerable<Player> players;
			try
			{
				players = _getPlayersQuery.Execute(playersViewModel.SelectedPlayers);
			}
			catch (NotFoundException exception)
			{
				ModelState.AddModelError("_FORM", exception.Message);
				return View("Index");
			}

			_teamCreator.CreateTeams();

			return RedirectToAction("Create", "Tournament");
		}
	}
}
