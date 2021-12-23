using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<IEnumerable<IPsychic>> GetPsychicsAsync();

        public void SetNextDesiredValue(int desiredValue);

        void Switch();

        byte[] Save();

        void Run();
    }
}