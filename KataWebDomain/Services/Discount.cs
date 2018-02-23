using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataWebDomain.Models;
using KataWebDomain.Services.Interfaces;

namespace KataWebDomain.Services
{
    public class Discount : IDiscount
    {
        private readonly IList<Product> _products;

        public ICheckout Checkout { get; }

        public Discount(ICheckout checkout)
        {
            Checkout = checkout;
            _products = Checkout.Products;
        }

        public IList<Product> GetProducts()
        {
            return _products;
        }

        /// <summary>Gets a pair of prices for the discount, one unaffected and one discounted.</summary>
        public Tuple<int, int> GetDiscountedPrice(IDictionary<int, Promotion> possiblePromotions)
        {
            var nonDiscountedPrice = 0;
            var discountedPrice = 0;
            if (!possiblePromotions.Any())
                return Tuple.Create(nonDiscountedPrice, discountedPrice);

            foreach (var promo in possiblePromotions)
            {
                // Extracts affected products to an array.
                var affectedProducts = _products.Where(p => p.SkuName == promo.Value.ItemsAffected).ToArray();

                for (var i = 1; i < affectedProducts.Length + 1; i++)
                {
                    // Checks if index is a perfect ratio of the number of items affected in promo (wherein prices are added)
                    // AND the number of affected products equal to or exceeds the number of items affected.
                    if (i % promo.Value.NumberOfItemsAffected == 0 && affectedProducts.Length >= promo.Value.NumberOfItemsAffected)
                    {
                        // We have the discounted price
                        discountedPrice += promo.Value.PriceSum;
                        // Compared to the price items would have cost normally
                        nonDiscountedPrice += (affectedProducts.ElementAt(i).UnitPrice * promo.Value.NumberOfItemsAffected);
                    }
                }
            }

            return Tuple.Create(nonDiscountedPrice, discountedPrice);
        }
    }
}
