using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayGroundModel;
using Web.Models;

namespace Web.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Start()
        {
            User user;
            if (HttpContext.Session.GetString("User") != null)
            {
                user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                ViewData["UserName"] = user.UserName;
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Preparation()
        {
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            ViewData["UserName"] = user.UserName;
            return View();
        }

        [HttpPost]
        public IActionResult Preparation([Bind("NumberOfPsychic, UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.Remove("User");
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

                var playGround = new PlayGround(HttpContext.Session.Id, user.NumberOfPsychic);
                playGround.RunNextIteration();
                HttpContext.Session.SetString("PlayGround", JsonConvert.SerializeObject(playGround));

                return RedirectToAction("PsychicMove", "Game", user);
            }
            else return View();
        }

        public IActionResult PsychicMove(User user)
        {
            PlayGround playGround;
            if (HttpContext.Session.GetString("PlayGround") != null)
            {
                playGround = JsonConvert.DeserializeObject<PlayGround>(HttpContext.Session.GetString("PlayGround"));

                if (user == null)
                { user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User")); }

                ViewBag.PlayGround = playGround;

                return View(user);
            }
            else return View("Preparation");
        }

        public IActionResult Result([Bind("UserName, NumberOfPsychic, DesiredValue")] User user)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.Remove("User");
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

                var playGround = JsonConvert.DeserializeObject<PlayGround>(
                    HttpContext.Session.GetString("PlayGround")
                    );
                playGround.User.DesiredValue = user.DesiredValue;

                playGround.Result();
                { if (playGround.Iterations != 0) playGround.RunNextIteration(); }

                HttpContext.Session.Remove("PlayGround");
                HttpContext.Session.SetString("PlayGround", JsonConvert.SerializeObject(playGround));

                ViewBag.PlayGround = playGround;

                return View(user);
            }
            else
            {
                return RedirectToAction("PsychicMove", "Game", user);
            }
        }

    }
}
