using System.Collections.Generic;

namespace PlayGroundModel
{
    public interface IPsychic
    {

        IEnumerable<int> GetAnswerHistory();
        /// <summary>
        /// Количество успешных попыток.
        /// </summary>
        public int SuccessfulAttempts { get;} 
        int ConfidenceLevel { get; }
        int CurrentAnswer { get; }
        /// <summary>
        /// Имя.
        /// </summary>
        string Name { get; }
        int PreviousConfidenceLevel { get; }
    }
}