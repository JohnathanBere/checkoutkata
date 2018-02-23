using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataWebDomain.Models
{
    public class Product
    {
        public Product(/*int? skuId,*/ string skuName, int unitPrice)
        {
            // SkuId = skuId.GetValueOrDefault();
            SkuName = skuName;
            UnitPrice = unitPrice;
        }

        // public int SkuId { get; set; }
        public string SkuName { get; set; }
        public int UnitPrice { get; set; }
    }
}
