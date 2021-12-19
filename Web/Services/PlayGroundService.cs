using Microsoft.AspNetCore.Http;
using PlayGroundModel;

namespace Web.Services
{
    /// <summary>
    /// Сервис для взаимодействия Session и PlayGroundModel.
    /// </summary>
    public class PlayGroundService : IPlayGroundService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private PlayGround _playGround;

        public PlayGroundService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Получает текущую Session.
        /// </summary>
        public ISession Session => _httpContextAccessor.HttpContext.Session;

        /// <summary>
        /// Пытается получить PlayGround из Session.
        /// </summary>
        /// <param name="playGroundInterface">Интерфейс игровой площадки</param>
        /// <returns>True если значение в Session имеется</returns>
        public bool TryGetPlayGround(out IPlayGround playGroundInterface)
        {
            if (Session.TryGetPlayGround(out PlayGround playGround))
            {
                playGroundInterface = playGround;
                _playGround = playGround;
                return true;
            }

            playGroundInterface = null;
            return false;
        }

        /// <summary>
        /// Инициализирует новую игровую площадку.
        /// </summary>
        /// <param name="numberOfPsychics">Количество экстрасенсов</param>
        public IPlayGroundService CreateNewPlayGround(int numberOfPsychics)
        {
            _playGround = new PlayGround(numberOfPsychics);
            return this;
        }

        /// <summary>
        /// Устанавливает в имеющийся PlayGround значение загаданного числа.
        /// </summary>
        /// <param name="desiredValue">Загаданное число</param>
        public IPlayGroundService SetNextDesiredValue(int desiredValue)
        {
            _playGround.SetNextDesiredValue(desiredValue);
            return this;
        }

        /// <summary>
        /// Запускает в PlayGround ход участника или экстрасенсов (в порядке очередности).
        /// </summary>
        public IPlayGroundService Run()
        {
            _playGround.Run();
            return this;
        }

        /// <summary>
        /// Обновляет PlayGround в Session.
        /// </summary>
        public void UpdateSession()
        {
            if (_playGround != null)
                Session.SetPlayGround(_playGround);
        }
    }
}
