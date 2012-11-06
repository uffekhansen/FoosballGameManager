using System.Web.Mvc;
using DAL.Commands;
using Domain.Entities;
using Domain.Exceptions;

namespace FoosballGameManager.Controllers
{
	public class PlayerController : Controller
	{
		private readonly AddPlayerCommand _addPlayerCommand;

		public PlayerController(AddPlayerCommand addPlayerCommand)
		{
			_addPlayerCommand = addPlayerCommand;
		}

		public ActionResult New()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Player player)
		{
			try
			{
				_addPlayerCommand.Execute(player);
			}
			catch (AlreadyExistsException exception)
			{
				ModelState.AddModelError("_FORM", exception.Message);
				return View("New");
			}

			return RedirectToAction("Index", "Team");
		}
	}
}
