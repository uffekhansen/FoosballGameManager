using System;
using System.Web.Mvc;
using DAL.Commands;
using DAL.Queries;
using Domain.Entities;

namespace FoosballGameManager.Controllers
{
    public class GameController : Controller
    {
        private readonly IAddCommand<Game> _addGameCommand;
        private readonly IGetEntityByIdQuery<Game> _getGameByIdQuery;

        public GameController(IAddCommand<Game> addGameCommand, IGetEntityByIdQuery<Game> getGameByIdQuery)
        {
            _addGameCommand = addGameCommand;
            _getGameByIdQuery = getGameByIdQuery;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewGame()
        {
            var game = new Game();
            _addGameCommand.Execute(game);

            return RedirectToAction("Show",  new { Id = game.Id });
        }

        public ActionResult Show(Guid id)
        {
            var game = _getGameByIdQuery.Execute(id);

            return View(game);
        }
    }
}
