using PlayGroundModel;

namespace Web.Services
{
    /// <summary>
    /// Интерфейс для получения и сохранения данных IPlayGround.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Пытается получить PlayGround из Session.
        /// </summary>
        /// <param name="playGround">Интерфейс игровой площадки</param>
        /// <returns>True если значение в Session имеется</returns>
        bool TryGetPlayGround(out IPlayGround playGround);

        /// <summary>
        /// Инициализирует новую игровую площадку.
        /// </summary>
        /// <param name="playGround">Интерфейс игровой площадки</param>
        public void SetPlayGround(IPlayGround playGround);
    }
}
