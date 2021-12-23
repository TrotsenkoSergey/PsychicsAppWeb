using PlayGroundModel;

namespace Web.Services
{
    /// <summary>
    /// Интерфейс для взаимодействия с контроллером.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Пытается получить PlayGround из Session.
        /// </summary>
        /// <param name="playGroundInterface">Интерфейс игровой площадки</param>
        /// <returns>True если значение в Session имеется</returns>
        bool TryGetPlayGround(out IPlayGround playGround);

        /// <summary>
        /// Инициализирует новую игровую площадку.
        /// </summary>
        /// <param name="numberOfPsychics">Количество экстрасенсов</param>
        public void SetPlayGround(IPlayGround playGround);
    }
}
