using System.Collections.Generic;

namespace PlayGroundModel
{
    internal class Participant : IParticipant
    {
        public int DesiredValue { get; set; }

        public List<int> HistoryOfDesiredValue { get; set; } = new List<int>();

        public IEnumerable<int> GetHistoryOfDesiredValue()
        {
            return HistoryOfDesiredValue;
        }
    }
}
