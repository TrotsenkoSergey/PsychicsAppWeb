using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            User user;
            if (HttpContext.Session.GetString("User") != null)
            {
                user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                ViewData["UserName"] = user.UserName;
            }
            else user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Index([Bind("UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

                return RedirectToAction("Start", "Game");
            }
            else return View(user);
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
