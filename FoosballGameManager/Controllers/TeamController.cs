using System.Web.Mvc;

namespace FoosballGameManager.Controllers
{
    public class TeamController : Controller
    {
        public ActionResult Players()
        {
            return View();
        }
    }
}
