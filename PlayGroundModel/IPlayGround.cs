using System.Collections.Generic;

namespace PlayGroundModel
{
    public interface IPlayGround
    {
        bool IsPsychicsMove { get; set; }
        int Iterations { get; }
        List<Psychic> Psychics { get; }
        Participant User { get; }
    }
}