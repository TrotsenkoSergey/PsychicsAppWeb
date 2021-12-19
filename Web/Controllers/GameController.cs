using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Preparation() => View(new ValidUserValue());

        [HttpPost]
        public IActionResult Preparation(ValidUserValue validData)
        {
            if (!ModelState.IsValid) return View();

            _playGroundService.CreateNewPlayGround(validData.NumberOfPsychic).UpdateSession();

            return RedirectToAction("PsychicMove", "Game");
        }

        public IActionResult PsychicMove(ValidUserValue validData)
        {
            if (!_playGroundService.TryGetPlayGround(out IPlayGround playGround)) 
                return View("Preparation");
            
            ViewBag.ValidData = validData;
            ViewData["isPsychicMove"] = playGround.IsPsychicsMove; 
            // флаг для работы с Partial View

            _playGroundService.Run().UpdateSession(); 

            return View("Result", playGround);
        }

        public IActionResult Result(ValidUserValue validData)
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
