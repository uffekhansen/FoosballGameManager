﻿using System.Web.Mvc;
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
			_addPlayerQuery.Execute(new Player());
			return RedirectToAction("Index", "Team");
		}
	}
}
