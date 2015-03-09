using System.Web.Mvc;
using System.Web.Security;
using artgelina.web.Models;

namespace artgelina.web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            var model = ConfigurationManager.GetConfig();
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveSettings(ArtgelinaModel model)
        {
            ConfigurationManager.UpdateConfig(model);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
