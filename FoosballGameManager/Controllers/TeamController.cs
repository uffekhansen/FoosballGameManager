using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;
using FoosballGameManager.ViewModels;

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
			var viewModel = new PlayersViewModel
			{
				Players = _getPlayersQuery.Execute(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(PlayersViewModel playersViewModel)
		{
			return RedirectToAction("Index");
		}
	}
}
