using Microsoft.AspNetCore.Mvc;
using PlayGroundModel;
using System.Threading.Tasks;
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

        [HttpGet]
        public IActionResult Preparation() => View(new ValidUserValue());

        [HttpPost]
        public IActionResult Preparation(ValidUserValue validData)
        {
            if (!ModelState.IsValid)
            { return View(); }

            var playGround = PlayGroundFactory.GetPlayGround(validData.NumberOfPsychic);
            _dataService.SetPlayGround(playGround);

            return RedirectToAction(nameof(PsychicsMove));
        }

        public IActionResult PsychicsMove()
        {
            if (!_dataService.TryGetPlayGround(out IPlayGround playGround)) 
                return View("Preparation");

            playGround.Run();
            _dataService.SetPlayGround(playGround);

            ViewBag.PlayGround = playGround;
            return View("PsychicsMove", new ValidUserValue());
        }

        public IActionResult Result(ValidUserValue validData)
        {
            if (!_dataService.TryGetPlayGround(out IPlayGround playGround))
                return View("Preparation");

            if (!ModelState.IsValid)
            {
                playGround.Switch();
                ViewBag.PlayGround = playGround;
                return View("PsychicsMove", validData);
            }

            playGround
                .SetNextDesiredValue(validData.DesiredValue)
                .Run();

            _dataService.SetPlayGround(playGround);

            return View("Result", playGround);
        }
    }
}
