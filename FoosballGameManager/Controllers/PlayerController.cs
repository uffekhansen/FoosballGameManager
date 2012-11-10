using System.Web.Mvc;
using DAL.Commands;
using Domain.Entities;
using Domain.Exceptions;

namespace FoosballGameManager.Controllers
{
	public class PlayerController : Controller
	{
		private readonly IAddPlayerCommand _addPlayerCommand;

		public PlayerController(IAddPlayerCommand addPlayerCommand)
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

			return RedirectToAction("Index", "TeamSelection");
		}
	}
}
