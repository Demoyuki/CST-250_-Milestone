using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperClasses
{
    public class GameStats
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public int BoardSize { get; set; }
        public float Difficulty { get; set; }
        public int Score { get; set; } = 0;
        public bool IsWinner { get; set; }
    }
}
