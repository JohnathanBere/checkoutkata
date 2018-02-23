using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataWebDomain.Models;

namespace KataWebDomain.Services.Interfaces
{
    public interface IDiscount
    {
        IList<Product> GetProducts();
        Tuple<int, int> GetDiscountedPrice(IDictionary<int, Promotion> possiblePromotions);
    }
}
