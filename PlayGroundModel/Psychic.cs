using System.Collections.Generic;

namespace PlayGroundModel
{
    /// <summary>
    /// Сущность экстрасенса.
    /// </summary>
    internal class Psychic : IPsychic
    {
        /// <summary>
        /// История ответов.
        /// </summary>
        private List<int> _answerHistory = new List<int>();

        /// <summary>
        /// Создание экстрасенса.
        /// </summary>
        /// <param name="name">Имя.</param>
        public Psychic(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Создание экстрасенса.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="successfulAttempts">Количество успешных попыток.</param>
        /// <param name="confidenceLevel">Текущий уровень достоверности.</param>
        /// <param name="previousConfidenceLevel">Уровень достоверности за предыдущий ход.</param>
        /// <param name="currentAnswer">Последний известный ответ.</param>
        /// <param name="answerHistory">История ответов.</param>
        public Psychic(string name, int successfulAttempts, int confidenceLevel,
            int previousConfidenceLevel, int currentAnswer, List<int> answerHistory) : this(name) 
        {
            SuccessfulAttempts = successfulAttempts;
            ConfidenceLevel = confidenceLevel;
            PreviousConfidenceLevel = previousConfidenceLevel;
            CurrentAnswer = currentAnswer;
            _answerHistory = answerHistory;
        } // для сериализации

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Количество успешных попыток.
        /// </summary>
        public int SuccessfulAttempts { get; private set; } = 0;

        /// <summary>
        /// Уровень достоверности.
        /// </summary>
        public int ConfidenceLevel { get; private set; } = 100;

        /// <summary>
        /// Уровень достоверности за предыдущий ход.
        /// </summary>
        public int PreviousConfidenceLevel { get; private set; } = 100;

        /// <summary>
        /// Ответ на текущей итерации.
        /// </summary>
        public int CurrentAnswer { get; private set; }

        public void SetSuccessfulAttempt() =>
            SuccessfulAttempts++;

        public void SetConfidenceLevel(int value)
        {
            PreviousConfidenceLevel = ConfidenceLevel;
            ConfidenceLevel = value;
        }

        /// <summary>
        /// Получить историю ответов.
        /// </summary>
        /// <returns>История ответов</returns>
        public IEnumerable<int> GetAnswerHistory() =>
            _answerHistory;

        /// <summary>
        /// Получить количество значений в списке истории ответов.
        /// </summary>
        /// <returns>Количество значений в списке.</returns>
        public int GetCountOfAnswerHistory() => // для сериализации
            _answerHistory.Count; // для сериализации

        /// <summary>
        /// Угадать число.
        /// </summary>
        /// <returns>Число</returns>
        public void GuessNumber()
        {
            CurrentAnswer = Tools.GetRandomValue();
            _answerHistory.Add(CurrentAnswer);
        }
    }
}
