using System.Collections.Generic;

namespace PlayGroundModel
{
    /// <summary>
    /// Участник игры с экстрасенсами.
    /// </summary>
    internal class Participant : IParticipant
    {
        /// <summary>
        /// История загаданных значений.
        /// </summary>
        private List<int> _historyOfDesiredValue = new List<int>();

        /// <summary>
        /// Создание участника.
        /// </summary>
        public Participant() { }

        /// <summary>
        /// Создание участника.
        /// </summary>
        /// <param name="desiredValue">Последнее загаданное значение.</param>
        /// <param name="historyOfDesiredValue">История загаданных значений.</param>
        public Participant(int desiredValue, List<int> historyOfDesiredValue) : this() // для сериализации
        {
            DesiredValue = desiredValue;
            _historyOfDesiredValue = historyOfDesiredValue;
        }

        /// <summary>
        /// Загаданное значение.
        /// </summary>
        public int DesiredValue { get; private set; }

        /// <summary>
        /// Установить новое загаднное значение.
        /// </summary>
        /// <param name="desiredValue">Значение загаданного числа.</param>
        public void SetNextDesiredValue(int desiredValue)
        {
            DesiredValue = desiredValue;
            _historyOfDesiredValue.Add(desiredValue);
        }

        /// <summary>
        /// Получить историю загаданных значений.
        /// </summary>
        /// <returns>Историю загаданных значений</returns>
        public IEnumerable<int> GetHistoryOfDesiredValue() =>
            _historyOfDesiredValue;

        /// <summary>
        /// Получить количество значений в списке истории значений.
        /// </summary>
        /// <returns>Количество значений в списке.</returns>
        public int GetCountOfHistoryValues() => // для сериализации
            _historyOfDesiredValue.Count;
    }
}
