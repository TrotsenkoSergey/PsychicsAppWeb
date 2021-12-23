using System.Collections.Generic;

namespace PlayGroundModel
{
    /// <summary>
    /// Сущность экстрасенса.
    /// </summary>
    internal class Psychic : IPsychic
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество успешных попыток.
        /// </summary>
        public int SuccessfulAttempts { get; set; } = 0;

        /// <summary>
        /// Уровень достоверности.
        /// </summary>
        public int ConfidenceLevel { get; set; } = 100;

        /// <summary>
        /// Уровень достоверности за предыдущий ход.
        /// </summary>
        public int PreviousConfidenceLevel { get; set; } = 100;

        /// <summary>
        /// Ответ на текущей итерации.
        /// </summary>
        public int CurrentAnswer { get; set; }

        /// <summary>
        /// История ответов.
        /// </summary>
        public List<int> AnswerHistory { get; set; } = new List<int>();

        public IEnumerable<int> GetAnswerHistory()
        {
            return AnswerHistory;
        }

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
