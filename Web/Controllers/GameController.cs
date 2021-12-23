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

            return RedirectToAction(nameof(PsychicsMove));
        }

        public IActionResult PsychicsMove()
        {
            if (!_playGroundService.TryGetPlayGround(out IPlayGround playGround)) 
                return View("Preparation");

            _playGroundService.Run().UpdateSession();

            ViewBag.ValidData = playGround.GetPsychics();
            return View("PsychicsMove", new ValidUserValue());
        }

        public IActionResult Result(ValidUserValue validData)
        {
            if (!_playGroundService.TryGetPlayGround(out IPlayGround playGround))
                return View("Preparation");

            if (!ModelState.IsValid)
            {
                playGround.Switch();
                ViewBag.ValidData = playGround;
                return View("PsychicsMove", validData);
            }

            _playGroundService.SetNextDesiredValue(validData.DesiredValue)
                              .Run()
                              .UpdateSession(); 

            return View("Result", playGround);
        }
    }
}
