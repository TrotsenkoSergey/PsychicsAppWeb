using System.Collections.Generic;

namespace PlayGroundModel
{
    public interface IPsychic
    {
        /// <summary>
        /// Имя.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Количество успешных попыток.
        /// </summary>
        public int SuccessfulAttempts { get;}
        
        /// <summary>
        /// Уровень достоверности.
        /// </summary>
        int ConfidenceLevel { get; }

        /// <summary>
        /// Уровень достоверности за предыдущий ход.
        /// </summary>
        int PreviousConfidenceLevel { get; }

        /// <summary>
        /// Ответ на текущей итерации.
        /// </summary>
        int CurrentAnswer { get; }

        /// <summary>
        /// Получить историю ответов.
        /// </summary>
        /// <returns>История ответов</returns>
        IEnumerable<int> GetAnswerHistory();
    }
}