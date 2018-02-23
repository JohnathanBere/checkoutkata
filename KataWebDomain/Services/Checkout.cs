using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataWebDomain.Models;
using KataWebDomain.Services.Interfaces;

namespace KataWebDomain.Services
{
    public class Checkout : ICheckout
    {
        public IList<Product> Products { get; }
        private int TotalPrice { get; set; }

        public Checkout()
        {
            Products = new List<Product>();
            TotalPrice = 0;
        }

        public void Scan(Product item)
        {
            Products.Add(item);
        }

        public int GetTotalPrice()
        {
            TotalPrice = TotalPrice > 0 ? TotalPrice : Products.Sum(p => p.UnitPrice);
            return TotalPrice;
        }

        public void ApplyDiscounts(Tuple<int, int> prices)
        {
            var nonDiscountedPrice = prices.Item1;
            var discountedPrice = prices.Item2;

            // Takes away the affected unit prices from the total price
            TotalPrice = TotalPrice - nonDiscountedPrice;
            // Adds the discounted price to the unit price
            TotalPrice = TotalPrice + discountedPrice;
        }

//        public IList<Product> GetCurrentProducts()
//        {
//            return Products;
//        }
    }
}
