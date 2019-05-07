using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class ScoreRecord
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int ScoreLimit { get; set; }


        public ScoreRecord(string PName, int score, int scorelimit)
        {
            PlayerName = PName;
            Score = score;
            ScoreLimit = scorelimit;
        }

    }
}
