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
        bool IsPsychicsMove { get; set; }

        /// <summary>
        /// Участник игры.
        /// </summary>
        Participant User { get; }
         
        /// <summary>
        /// Экстрасенсы.
        /// </summary>
        List<Psychic> Psychics { get; }
    }
}