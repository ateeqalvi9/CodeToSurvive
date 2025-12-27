
namespace CodeToSurvive
{
    internal class MealPurchase
    {
        public int PlayerId { get; set; }
        public string MealName { get; set; }
        public int Price { get; set; }
        public int EnergyGained { get; set; }
        public DateTime PurchaseTime { get; set; }
    }
}