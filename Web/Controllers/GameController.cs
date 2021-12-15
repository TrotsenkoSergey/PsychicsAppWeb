using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayGroundModel;
using Web.Models;

namespace Web.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult Preparation()
        {
            return View(new ValidControl());
        }

        [HttpPost]
        public IActionResult Preparation(ValidControl validData)
        {
            if (!ModelState.IsValid) return View();

            var playGround = new PlayGround(validData.NumberOfPsychic);
            playGround.RunNextIteration();

            var stringPlayGround = JsonConvert.SerializeObject(playGround);
            HttpContext.Session.SetString("PlayGround", stringPlayGround);

            return RedirectToAction("PsychicMove", "Game");
        }

        public IActionResult PsychicMove(ValidControl validData)
        {
            if (HttpContext.Session.GetString("PlayGround") != null)
            {
                var playSession = HttpContext.Session.GetString("PlayGround");
                var playGround = JsonConvert.DeserializeObject<PlayGround>(playSession);

                ViewBag.PlayGround = playGround;
                ViewData["IsResult"] = false; //флаг для работы с одной вьюшкой и двумя контроллерами

                return View("Result", validData);
            }

            return View("Preparation");
        }


        public IActionResult Result(ValidControl validData)
        {
            var playSession = HttpContext.Session.GetString("PlayGround");
            var playGround = JsonConvert.DeserializeObject<PlayGround>(playSession);

            if (ModelState.IsValid)
            {
                playGround.User.DesiredValue = validData.DesiredValue;

                playGround.Result();
                { if (playGround.Iterations != 0) playGround.RunNextIteration(); }

                HttpContext.Session.Remove("PlayGround");
                HttpContext.Session.SetString("PlayGround", JsonConvert.SerializeObject(playGround));

                ViewBag.PlayGround = playGround;
                ViewData["IsResult"] = true;

                return View(validData);
            }
            else
            {
                ViewBag.PlayGround = playGround;
                ViewData["IsResult"] = (playGround.Iterations > 1);
                return View("Result", validData);
            }
        }
    }
}
