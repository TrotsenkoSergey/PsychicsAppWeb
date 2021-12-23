using System.Collections.Generic;

namespace PlayGroundModel
{
    public interface IParticipant
    {
        /// <summary>
        /// Загаданное значение.
        /// </summary>
        int DesiredValue { get; }

        /// <summary>
        /// Получить историю загаданных значений.
        /// </summary>
        /// <returns>Историю загаданных значений</returns>
        IEnumerable<int> GetHistoryOfDesiredValue();
    }
}