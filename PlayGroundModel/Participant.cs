using System.Collections.Generic;

namespace PlayGroundModel
{
    public class Participant
    {
        public int DesiredValue { get; set; }

        public List<int> HistoryOfDesiredValue { get; set; } = new List<int>();
    }
}
