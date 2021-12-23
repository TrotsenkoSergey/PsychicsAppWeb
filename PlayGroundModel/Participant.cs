using System.Collections.Generic;

namespace PlayGroundModel
{
    internal class Participant : IParticipant
    {
        /// <summary>
        /// Загаданное значение.
        /// </summary>
        public int DesiredValue { get; set; }

        /// <summary>
        /// История загаданных значений.
        /// </summary>
        public List<int> HistoryOfDesiredValue { get; set; } = new List<int>();

        /// <summary>
        /// Получить историю загаданных значений.
        /// </summary>
        /// <returns>Историю загаданных значений</returns>
        public IEnumerable<int> GetHistoryOfDesiredValue()
        {
            return HistoryOfDesiredValue;
        }
    }
}
