using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace artgelina.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            const string url = "https://api.instagram.com/v1/users/491476900/media/recent?access_token=258106817.3e16bcf.e954382bdeec4c7bb3fe239d76ba1fe6";
            var webClient = new WebClient();
            var response = webClient.DownloadString(url);
            var responseObj = JsonConvert.DeserializeObject<dynamic>(response);
            var images = ((IEnumerable<dynamic>)responseObj.data)
                .Select(x => x.images).Select(i => i.standard_resolution.url);
            return View(images);
        }


        //public ActionResult Index()
        //{
        //    return RedirectToAction("About");
        //    ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
        //    var url = string.Format(
        //        "https://api.instagram.com/oauth/authorize/?client_id={0}&redirect_uri={1}&response_type=code",
        //        "3e16bcf4e12641e4b34059d81c8dbbb5",
        //        "http://artgelina.com/home/gettoken");
        //    return Redirect(url);
        //}

        public async Task<ActionResult> GetToken(string code)
        {
            var values = new Dictionary<string, string>
                {
                    {"client_id", "3e16bcf4e12641e4b34059d81c8dbbb5"},
                    {"client_secret", "addb4bb417ec422bb8bd138f57e31211"},
                    {"code", code},
                    {"grant_type", "authorization_code"},
                    {"redirect_uri", "http://artgelina.com/home/gettoken" }
                };

            var content = new FormUrlEncodedContent(values);
            const string url = "https://api.instagram.com/oauth/access_token";
            var webClient = new HttpClient();
            var response = await webClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<dynamic>(responseString);
            var accessToken = responseObj.access_token.ToString();
            System.IO.File.WriteAllText(
                @"C:\Users\Max\Documents\Visual Studio 2012\Projects\artgelina.web\artgelina.web\access_token.txt",
                accessToken);

            return Content(accessToken);
        }

        public ActionResult About()
        {
           
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
