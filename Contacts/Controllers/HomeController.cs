using System.Web.Mvc;

namespace Contacts.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         return View();
      }

      public ActionResult About()
      {
         ViewBag.Message = "A simple company-wide contacts database system.";

         return View();
      }

      public ActionResult Contact()
      {
         ViewBag.Message = "Contact via email is always to be recommended over other forms of media";

         return View();
      }
   }
}