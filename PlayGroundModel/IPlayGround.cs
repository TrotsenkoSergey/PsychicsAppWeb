using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayGroundModel
{
    public interface IPlayGround
    {
        /// <summary>
        /// Количество попыток.
        /// </summary>
        int Iterations { get; }

        /// <summary>
        /// Указывает чей сейчас ход (экстрасенсов или участника).
        /// </summary>
        bool IsPsychicsMove { get; }

        /// <summary>
        /// Получить участника.
        /// </summary>
        /// <returns>Участник</returns>
        IParticipant GetUser();

        /// <summary>
        /// Получает экстрасенсов.
        /// </summary>
        /// <returns>Поддерживает ожидание - получает экстрасенсов</returns>
        Task<IEnumerable<IPsychic>> GetPsychicsAsync();

        /// <summary>
        /// Записывает последующее загаданное значение Участнику.
        /// </summary>
        /// <param name="desiredValue">Загаданное значение</param>
        public IPlayGround SetNextDesiredValue(int desiredValue);

        /// <summary>
        /// Сохраняет значение PlayGround.
        /// </summary>
        /// <returns>Значение PlayGround в byte[]</returns>
        byte[] Save();

        /// <summary>
        /// Запуск следующего хода (программа сама решит кто это Экстрасенсы или Участник).
        /// </summary>
        void Run();
    }
}