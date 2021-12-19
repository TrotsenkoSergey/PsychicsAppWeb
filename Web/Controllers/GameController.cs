using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayGroundModel;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IPlayGroundService _playGroundService;

        public GameController(IPlayGroundService playGroundService)
        {
            _playGroundService = playGroundService;
        }

        [HttpGet]
        public IActionResult Preparation() => View(new ValidControl());

        [HttpPost]
        public IActionResult Preparation(ValidControl validData)
        {
            if (!ModelState.IsValid) return View();

            _playGroundService.SetNewPlayGround(validData.NumberOfPsychic);

            return RedirectToAction("PsychicMove", "Game");
        }

        public IActionResult PsychicMove(ValidControl validData)
        {
            if (!_playGroundService.TryGetPlayGround(out IPlayGround playGround)) 
                return View("Preparation");
            
            ViewBag.ValidData = validData;
            ViewData["isPsychicMove"] = playGround.IsPsychicsMove; 
            // флаг для работы с Partial View

            _playGroundService.Run().UpdateSession(); 

            return View("Result", playGround);
        }

        public IActionResult Result(ValidControl validData)
        {
            if (!_playGroundService.TryGetPlayGround(out IPlayGround playGround))
                return View("Preparation");

            if (!ModelState.IsValid)
            {
                playGround.IsPsychicsMove = true;
                _playGroundService.UpdateSession();

                ViewData["isPsychicMove"] = playGround.IsPsychicsMove;
                ViewBag.ValidData = validData;

                return View(playGround);
            }

            ViewBag.ValidData = validData;
            ViewData["isPsychicMove"] = playGround.IsPsychicsMove;

            _playGroundService.SetNextDesiredValue(validData.DesiredValue)
                              .Run()
                              .UpdateSession(); 

            return View(playGround);
        }
    }
}
