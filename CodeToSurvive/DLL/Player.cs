using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeToSurvive.DLL
{
    public class Player
    {
        public int Energy { get; set; } = 75;
        public int Money { get; set; } = 1250;
        public string CurrentOutfit { get; set; } = "default";
        public int ActiveLoan { get; set; } = 0;
        public int CreditScore { get; set; } = 650;
        public int CodingSkill { get; set; } = 0;
        public int Reputation { get; set; } = 0;
        public int Day { get; set; } = 1;   
    }
}
