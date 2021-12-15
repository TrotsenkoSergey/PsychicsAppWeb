﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayGroundModel;
using Web.Models;

namespace Web.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult Preparation() => View(new ValidControl());

        [HttpPost]
        public IActionResult Preparation(ValidControl validData)
        {
            if (!ModelState.IsValid) return View();

            var playGround = new PlayGround(validData.NumberOfPsychic);

            var stringPlayGround = JsonConvert.SerializeObject(playGround);
            HttpContext.Session.SetString("PlayGround", stringPlayGround);

            return RedirectToAction("PsychicMove", "Game");
        }

        public IActionResult PsychicMove(ValidControl validData)
        {
            if (HttpContext.Session.GetString("PlayGround") == null) View("Preparation");

            var playSession = HttpContext.Session.GetString("PlayGround");
            var playGround = JsonConvert.DeserializeObject<PlayGround>(playSession);

            playGround.RunNextIteration(); // итерирует основную модель

            HttpContext.Session.Remove("PlayGround");
            HttpContext.Session.SetString("PlayGround", JsonConvert.SerializeObject(playGround));

            ViewBag.PlayGround = playGround;
            ViewData["isPsychicMove"] = true; // флаг для работы с Partial View

            return View("Result", validData);
        }


        public IActionResult Result(ValidControl validData)
        {
            var playSession = HttpContext.Session.GetString("PlayGround");
            var playGround = JsonConvert.DeserializeObject<PlayGround>(playSession);

            if (!ModelState.IsValid)
            {
                ViewBag.PlayGround = playGround;
                ViewData["isPsychicMove"] = true;

                return View("Result", validData);
            }

            playGround.User.DesiredValue = validData.DesiredValue;
            playGround.Result(); // вычисляет результат на основании итерированной модели и числа пользователя

            HttpContext.Session.Remove("PlayGround");
            HttpContext.Session.SetString("PlayGround", JsonConvert.SerializeObject(playGround));

            ViewBag.PlayGround = playGround;
            ViewData["isPsychicMove"] = false;

            return View(validData);
        }
    }
}
