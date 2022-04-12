using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyMonitorLibrary.Tests
{
    [TestClass()]
    public class EnergyConsumptionValidatorTests
    {
        /*
        [TestMethod()]
        public void ExistsPriceBetweenTestOnePrice()
        {
            EMValidator v = new();
            DatabaseOfPrices prices = new();
            DateTime a, b;

            a = new (2000, 1, 1);
            b = new (2000, 12, 31);
            prices.Add(new Price (a, b, 10.0));

            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2000, 1, 1);
            b = new (2000, 6, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2000, 6, 1);
            b = new (2000, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));
        }

        [TestMethod()]
        public void ExistsPriceBetweenTestMorePrices()
        {
            EMValidator v = new();
            DatabaseOfPrices prices = new();
            DateTime a, b;

            a = new (2000, 1, 1);
            b = new (2000, 12, 31);
            prices.Add(a, b, 10.0);

            a = new (2001, 1, 1);
            b = new (2001, 12, 31);
            prices.Add(a, b, 20.0);

            a = new (2000, 1, 1);
            b = new (2001, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2000, 1, 1);
            b = new (2000, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2001, 1, 1);
            b = new (2001, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2000, 12, 31);
            b = new (2001, 1, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2000, 11, 30);
            b = new (2001, 2, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2000, 1, 1);
            b = new (2000, 1, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2000, 12, 31);
            b = new (2000, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2001, 1, 1);
            b = new (2001, 1, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new (2001, 12, 31);
            b = new (2001, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));


            a = new (1999, 12, 31);
            b = new (2000, 2, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));

            a = new (1999, 12, 31);
            b = new (2000, 1, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));

            a = new (2001, 11, 1);
            b = new (2002, 1, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));

            a = new (2001, 12, 31);
            b = new (2002, 1, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));


        }
        */

        [TestMethod()]
        public void OverlapsTest()
        {
            
            List<Price> pricesList = new();
            List<Price> tmpPricesList;
            DateTime a, b;


            Assert.IsFalse(EMValidator.ContainsOverlapingPrices(pricesList));
            
            a = new (2000, 1, 1);
            b = new (2000, 12, 31);
            pricesList.Add(new Price(a, b, 10));
            pricesList.Sort(new Price.Comparer());
            Assert.IsFalse(EMValidator.ContainsOverlapingPrices(pricesList));
            
            a = new (1999, 12, 31);
            b = new (1999, 12, 31);
            pricesList.Add(new Price(a, b, 10));
            pricesList.Sort(new Price.Comparer());
            Assert.IsFalse(EMValidator.ContainsOverlapingPrices(pricesList));


            a = new (1999, 1, 1);
            b = new (1999, 12, 30);
            pricesList.Add(new Price(a, b, 10));
            pricesList.Sort(new Price.Comparer());
            Assert.IsFalse(EMValidator.ContainsOverlapingPrices(pricesList));

            a = new (2002, 1, 1);
            b = new (2002, 1, 1);
            pricesList.Add(new Price(a, b, 10));
            pricesList.Sort(new Price.Comparer());
            Assert.IsFalse(EMValidator.ContainsOverlapingPrices(pricesList));

            a = new (2002, 1, 2);
            b = new (2002, 12, 31);
            pricesList.Add(new Price(a, b, 10));
            pricesList.Sort(new Price.Comparer());
            Assert.IsFalse(EMValidator.ContainsOverlapingPrices(pricesList));


            //překryv začaátk i konec těsně
            a = new (2000, 1, 1);
            b = new (2000, 12, 31);
            tmpPricesList = new(pricesList);
            tmpPricesList.Add(new Price(a, b, 10));
            tmpPricesList.Sort(new Price.Comparer());
            Assert.IsTrue(EMValidator.ContainsOverlapingPrices(tmpPricesList));

            //překryv začátek více dní
            a = new (2000, 1, 1);
            b = new (2001, 12, 31);
            tmpPricesList = new(pricesList);
            tmpPricesList.Add(new Price(a, b, 10));
            tmpPricesList.Sort(new Price.Comparer());
            Assert.IsTrue(EMValidator.ContainsOverlapingPrices(tmpPricesList));

            //překryv začátek 1 den
            a = new (2000, 12, 31);
            b = new (2001, 1, 1);
            tmpPricesList = new(pricesList);
            tmpPricesList.Add(new Price(a, b, 10));
            tmpPricesList.Sort(new Price.Comparer());
            Assert.IsTrue(EMValidator.ContainsOverlapingPrices(tmpPricesList));

            //překryv začátek i konec 1 den
            a = new (2002, 12, 31);
            b = new (2002, 12, 31);
            tmpPricesList = new(pricesList);
            tmpPricesList.Add(new Price(a, b, 10));
            tmpPricesList.Sort(new Price.Comparer());
            Assert.IsTrue(EMValidator.ContainsOverlapingPrices(tmpPricesList));

            //překryv konec více dní
            a = new(2001, 10, 15);
            b = new(2002, 12, 31);
            tmpPricesList = new(pricesList);
            tmpPricesList.Add(new Price(a, b, 10));
            tmpPricesList.Sort(new Price.Comparer());
            Assert.IsTrue(EMValidator.ContainsOverlapingPrices(tmpPricesList));

            //překryv konec jeden den
            a = new(2001, 10, 15);
            b = new(2002, 1, 1);
            tmpPricesList = new(pricesList);
            tmpPricesList.Add(new Price(a, b, 10));
            tmpPricesList.Sort(new Price.Comparer());
            Assert.IsTrue(EMValidator.ContainsOverlapingPrices(tmpPricesList));


            a = new (2001, 1, 1);
            b = new (2001, 12, 31);
            pricesList.Add(new Price(a, b, 10));
            pricesList.Sort(new Price.Comparer());
            Assert.IsFalse(EMValidator.ContainsOverlapingPrices(pricesList));
        }
    }
}
