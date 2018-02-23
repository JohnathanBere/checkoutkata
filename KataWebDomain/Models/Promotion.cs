using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataWebDomain.Models
{
    public class Promotion
    {
        public Promotion(string promotionName, int? numberOfItemsAffected, string itemsAffected, int? priceSum)
        {
            PromotionName = promotionName;
            NumberOfItemsAffected = numberOfItemsAffected.GetValueOrDefault();
            ItemsAffected = itemsAffected;
            PriceSum = priceSum.GetValueOrDefault();
        }

        public string PromotionName { get; set; }
        public int NumberOfItemsAffected { get; set; }
        public string ItemsAffected { get; set; }
        public int PriceSum { get; set; }
    }
}
