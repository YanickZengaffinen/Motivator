using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivator.Models;
using System.Diagnostics;

namespace Motivator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("userinfo")]
        [Authorize]
        public IActionResult UserInformation()
        {
            return View();
        }
    }
}
