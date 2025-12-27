namespace CodeToSurvive.DLL
{
    public class Outfit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsOwned { get; set; } = false;
        // Style effect
        public int StyleBonus { get; set; }
        // Image path used in UI
        public string ImagePath { get; set; }
        public string Description { get; set; }

    }
}