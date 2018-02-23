using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using KataWebDomain;
using KataWebDomain.Models;
using KataWebDomain.Services;
using NUnit.Framework.Constraints;

namespace KataWebTests
{
    // Test cases
    [TestFixture]
    public class KataDomainTests
    {
        private IList<Product> _products;
        private IDictionary<int, Product> _productsDictionary;
        private IDictionary<int, Promotion> _promotionsDictionary;
        private Checkout _checkout;
        private Promotion _skuA3For130, _skuB2For45;

        public void ScanLoop()
        {
            foreach (var product in _products)
            {
                _checkout.Scan(product);
            }
        }

        [SetUp]
        public void SetUpTests()
        {
            _checkout = new Checkout();

            _skuA3For130 = new Promotion("3 For 130", 3, "A", 130);
            _skuB2For45 = new Promotion("2 For 45", 2, "B", 45);

            _products = new List<Product>
            {
                new Product("A", 50),
                new Product("B", 30),
                new Product("C", 20),
                new Product("D", 15)
            };

            // in-memory database for tests. 
            _productsDictionary = new Dictionary<int, Product>
            {
                { 1, _products[0] },
                { 2, _products[0] },
                { 3, _products[0] },
                { 4, _products[0] },
                { 5, _products[0] },
                { 6, _products[0] },
                { 7, _products[0] },
                { 8, _products[1] },
                { 9, _products[1] },
                { 10, _products[1] }
            };

            _promotionsDictionary = new Dictionary<int, Promotion>
            {
                { 1, _skuA3For130 },
                { 2, _skuB2For45 }
            };
        }

        /// <summary>
        /// Tests if the Checkout service successfully intialized
        /// </summary>
        [Test]
        public void CheckoutSuccessfullyInitialized()
        {
            Assert.That(_checkout, Is.Not.Null);
        }

        /// <summary>
        /// Tests if the hidden products collection property of the Checkout service
        /// initialized.
        /// </summary>
        [Test]
        public void ServiceProductCollectionSuccessfullyInitialized()
        {
            Assert.That(_checkout.Products, Is.Empty);
        }

        /// <summary>
        /// Tests if the Checkout service is in fact scanning items.
        /// </summary>
        [Test]
        public void CanScanItem()
        {
            // We test to see if our service can scan an item.
            // _checkout.Scan(new Product(1, "A", 45));
            ScanLoop();


            Assert.That(_checkout.Products.Count, Is.Not.Empty);
        }

        /// <summary>
        /// Tests if a total price is being calculated.
        /// </summary>
        [Test]
        public void CanGetTotalPriceOfCheckout()
        {
            ScanLoop();

            Assert.That(_checkout.GetTotalPrice(), Is.GreaterThan(0));
        }

        [Test]
        public void DiscountServiceSuccessfullyInitialized()
        {
            ScanLoop();

            var discount = new Discount(_checkout);

            Assert.That(discount.GetProducts(), Is.Not.Empty);
        }

        [Test]
        public void ProductDictionaryIsSpecified()
        {
            foreach (var product in _productsDictionary)
            {
                _checkout.Scan(product.Value);
            }

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(180));
        }

        [Test]
        public void DiscountWasSuccessfullyApplied()
        {
            // scan all items in temp db.
            foreach (var product in _productsDictionary)
            {
                _checkout.Scan(product.Value);
            }

            // Call discount service, with the _checkout in the constructor.
            var discount = new Discount(_checkout);

            // store original price in variable
            var originalPrice = _checkout.GetTotalPrice();

            // Run promotions through discount service
            var prices = discount.GetDiscountedPrice(_promotionsDictionary);

            // Apply discount with tuples
            _checkout.ApplyDiscounts(prices);

            var discountedPrice = _checkout.GetTotalPrice();

            Assert.Multiple(() =>
            {
                Assert.That(discount, Is.Not.Null);
                Assert.That(discount.GetProducts(), Is.Not.Null);
                // Confirm that discounted price is in fact discounted
                Assert.That(discountedPrice, Is.LessThan(originalPrice));
            });
        }



        /*[Test]
        public void ProductsHaveInformation()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_products, Is.Not.Empty);

                foreach (var product in _products)
                {
                    Assert.That(product.SkuName, Is.Not.Empty);
                }
            });
        }*/
    }
}
