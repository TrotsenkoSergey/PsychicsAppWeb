using PlayGroundModel;

namespace Web.Services
{
    /// <summary>
    /// Интерфейс для взаимодействия с контроллером.
    /// </summary>
    public interface IPlayGroundService
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
        IPlayGroundService CreateNewPlayGround(int numberOfPsychics);

        /// <summary>
        /// Устанавливает в имеющийся PlayGround значение загаданного числа.
        /// </summary>
        /// <param name="desiredValue">Загаданное число</param>
        IPlayGroundService SetNextDesiredValue(int desiredValue);

        /// <summary>
        /// Запускает в PlayGround ход участника или экстрасенсов (в порядке очередности).
        /// </summary>
        IPlayGroundService Run();

        /// <summary>
        /// Обновляет PlayGround в Session.
        /// </summary>
        void UpdateSession();
    }
}
