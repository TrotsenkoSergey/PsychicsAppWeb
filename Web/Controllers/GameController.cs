using Microsoft.AspNetCore.Mvc;
using PlayGroundModel;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IDataService _dataService;

        public GameController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Preparation() =>
            View(new ValidUserValue());

        public IActionResult PostPreparation(ValidUserValue validUserValue)
        {
            if (!ModelState.IsValid)
                return View("Preparation", validUserValue);

            var playGround = PlayGroundFactory.GetPlayGround(validUserValue.NumberOfPsychic);
            playGround.Run();
            _dataService.SetPlayGround(playGround);

            return RedirectToAction(nameof(PsychicsMove));
        }

        public IActionResult PsychicsMove()
        {
            if (!_dataService.TryGetPlayGround(out IPlayGround playGround))
                return View("Preparation");

            return View(playGround);
        }

        public IActionResult PostPsychicsMove(ValidUserValue validUserValue)
        {
            if (!_dataService.TryGetPlayGround(out IPlayGround playGround))
                return View("Preparation");

            if (!ModelState.IsValid)
            {
                ViewData["ValidUserValue"] = validUserValue; 
                //так я избавился от ViewBag и прокидываю во View->PlayGround
                return View("PsychicsMove", playGround);
            }

            playGround
                .SetNextDesiredValue(validUserValue.DesiredValue)
                .Run();
            _dataService.SetPlayGround(playGround);

            return RedirectToAction(nameof(Result));
        }

        public IActionResult Result()
        {
            if (!_dataService.TryGetPlayGround(out IPlayGround playGround))
                return View("Preparation");

            return View(playGround);
        }

        public IActionResult PostResult()
        {
            if (!_dataService.TryGetPlayGround(out IPlayGround playGround))
                return View("Preparation");

            playGround.Run();
            _dataService.SetPlayGround(playGround);

            return RedirectToAction(nameof(PsychicsMove));
        }
    }
}
