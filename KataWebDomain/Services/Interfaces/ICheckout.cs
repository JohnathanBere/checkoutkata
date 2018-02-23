using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataWebDomain.Models;

namespace KataWebDomain.Services.Interfaces
{
    public interface ICheckout
    {
        void Scan(Product item);
        int GetTotalPrice();
        IList<Product> Products { get; }
        void ApplyDiscounts(Tuple<int, int> prices);
        // IList<Product> GetCurrentProducts();
    }
}
