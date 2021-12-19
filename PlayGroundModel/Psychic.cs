using System.Collections.Generic;

namespace PlayGroundModel
{
    /// <summary>
    /// Сущность экстрасенса.
    /// </summary>
    public class Psychic
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество попыток.
        /// </summary>
        public int AttepmtsCounter { get; set; } = 0;

        /// <summary>
        /// Количество успешных попыток.
        /// </summary>
        public int SuccessfulAttempts { get; set; } = 0;

        /// <summary>
        /// Уровень достоверности.
        /// </summary>
        public int ConfidenceLevel { get; set; } = 100;

        /// <summary>
        /// История ответов.
        /// </summary>
        public List<int> AnswerHistory { get; set; } = new List<int>();

        /// <summary>
        /// Ответ на текущей итерации.
        /// </summary>
        public int CurrentAnswer { get; set; }

        /// <summary>
        /// Угадать число.
        /// </summary>
        /// <returns>Число</returns>
        public int GuessNumber()
        {
            return Tools.GetRandomValue();
        }
    }
}
