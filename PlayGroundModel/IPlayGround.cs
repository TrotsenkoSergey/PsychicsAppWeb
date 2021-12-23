using System.Collections.Generic;

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
        /// Участник игры.
        /// </summary>
        IParticipant GetUser();

        /// <summary>
        /// Экстрасенсы.
        /// </summary>
        IEnumerable<IPsychic> GetPsychics();

        public void SetNextDesiredValue(int desiredValue);

        void Switch();

        byte[] Save();

        void Run();
    }
}