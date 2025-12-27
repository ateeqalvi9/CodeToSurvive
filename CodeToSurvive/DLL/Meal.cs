using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeToSurvive.DLL
{
    public class Meal
    {
        public int MealId { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public string MealName { get; set; }
        public int Price { get; set; }
        public int EnergyGained { get; set; }
        public DateTime PurchaseTime { get; set; }
    }
}
