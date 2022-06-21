using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public int BetNumber { get; set; }
        public int BetAmount { get; set; }
        public int RollId { get; set; }
        public bool Winner { get; set; }
        public bool Payout { get; set; }
    }
}