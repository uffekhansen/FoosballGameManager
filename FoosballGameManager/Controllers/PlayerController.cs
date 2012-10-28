using System;
using System.Web.Mvc;
using DAL.Commands;
using Domain.Entities;

namespace FoosballGameManager.Controllers
{
	public class PlayerController : Controller
	{
		private readonly IAddCommand<Player> _addPlayerQuery;

		public PlayerController(IAddCommand<Player> addPlayerQuery)
		{
			_addPlayerQuery = addPlayerQuery;
		}

		public ActionResult AddPlayer()
		{
			var player = new Player
			{
				Affiliation = "foo",
				Id = Guid.NewGuid(),
				Name = "bar",
			};

			_addPlayerQuery.Execute(player);
			return RedirectToAction("Index", "Team");
		}
	}
}
