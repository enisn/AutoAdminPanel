using System.Web.Mvc;

namespace AutoAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string table)
        {
            return View();
        }

        public ActionResult AnotherLink()
        {
            return View("Index");
        }
    }
}
