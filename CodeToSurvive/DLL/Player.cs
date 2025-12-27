using CodeToSurvive.DLL;

namespace CodeToSurvive.DLL
{
    public class Player
    {
        public int Id { get; set; }
        public int Money { get; set; } = 100;
        public int Energy { get; set; } = 100;
        public int CodingSkill { get; set; } = 0;
        public int Reputation { get; set; } = 0;
        public int CurrentDay { get; set; } = 1;
        public int CreditScore { get; set; } = 600;
        // Bank
        public int ActiveLoan { get; set; } = 0;
        public bool HasLoan => ActiveLoan > 0;
        // Cosmetic
        public int StyleLevel { get; set; } = 0;
        public JobLevel JobLevel { get; set; } = JobLevel.Intern;
        public int TotalWorkDays { get; set; } = 0;
        public int Day { get; set; } = 1;
        public string CurrentOutfitPath { get; set; }
            = "/Assets/Outfits/default.png";

        public int Style { get; set; } = 0;
        public List<OwnedOutfit> OwnedOutfit { get; set; }
           = new List<OwnedOutfit>();
        public List<Outfit> OwnedOutfits { get; set; } = new List<Outfit>();

        //Currently equipped outfit
        public Outfit? CurrentOutfit { get; set; }

        public void AdvanceDay()
        {
            CurrentDay++;
            Energy -= 10;
            if (Energy < 0) Energy = 0;
        }
        public static class GameStateService
        {
            public static void SavePlayer(Player player)
            {
                // TODO: Save to database
            }

            public static Player LoadPlayer()
            {
                // TODO: Load from database
                return new Player();
            }
        }
    }
}