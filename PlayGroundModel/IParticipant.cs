using System.Collections.Generic;

namespace PlayGroundModel
{
    public interface IParticipant
    {
        int DesiredValue { get; }
        IEnumerable<int> GetHistoryOfDesiredValue();
    }
}