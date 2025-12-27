using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeToSurvive.DLL
{
    public class OwnedOutfit
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public string OutfitName { get; set; }
        public string ImagePath { get; set; }
        public int StyleBonus { get; set; }
    }
}
