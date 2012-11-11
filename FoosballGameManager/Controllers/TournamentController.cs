using System;
using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;
using FoosballGameManager.ViewModels;

namespace FoosballGameManager.Controllers
{
	public class TournamentController : Controller
	{
		private readonly IGetEntityByIdQuery<Tournament> _getTournamentByIdQuery;

		public TournamentController(IGetEntityByIdQuery<Tournament> getTournamentByIdQuery)
		{
			_getTournamentByIdQuery = getTournamentByIdQuery;
		}

		public ActionResult Show(Guid id)
		{
			var viewModel = new TournamentViewModel
			{
				Tournament = _getTournamentByIdQuery.Execute(id),
			};

			return View(viewModel);
		}
	}
}
