using System;
using System.Collections.Generic;
using System.Text;

namespace PlayGroundModel
{
    public class Participant
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int NumberOfPsychic { get; set; }

        public int DesiredValue { get; set; } = 10;

        public List<int> HistoryOfDesiredValue { get; set; } = new List<int>();
    }
}
