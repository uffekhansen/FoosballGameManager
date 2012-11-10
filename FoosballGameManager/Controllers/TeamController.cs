using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FoosballGameManager.Controllers
{
	public class TeamController : Controller
	{
		[HttpPost]
		public ActionResult Create(IEnumerable<Guid> playerIds)
		{
			throw new NotImplementedException();
		}
	}
}
