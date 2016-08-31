using System;
using System.Text;
using System.Collections.Generic;
using problem_solving_followup;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTestProject
{
    [TestFixture]
    public class LinqUnitTests
    {
        private List<User> users;
        private List<User> originalUsers;
        private List<Product> products;
        private List<Product> originalProducts;
        private List<Transaction> transactions;
        private List<Transaction> originalTransactions;

        [SetUp]
        public void Test_Initialize()
        {
            // Load users from data file
            originalUsers = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Data/Users.json"));
            users = DeepCopy<List<User>>(originalUsers);

            // Load products from data file
            originalProducts = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"Data/Products.json"));
            products = DeepCopy<List<Product>>(originalProducts);

            // Load transactions from data file
            originalTransactions = JsonConvert.DeserializeObject<List<Transaction>>(File.ReadAllText(@"Data/Transactions.json"));
            transactions = DeepCopy<List<Transaction>>(originalTransactions);
        }

        [TearDown]
        public void Test_Cleanup()
        {
            // Restore users
            string json = JsonConvert.SerializeObject(originalUsers, Formatting.Indented);
            File.WriteAllText(@"Data/Users.json", json);
            users = DeepCopy<List<User>>(originalUsers);

            // Restore products
            string json2 = JsonConvert.SerializeObject(originalProducts, Formatting.Indented);
            File.WriteAllText(@"Data/Products.json", json2);
            products = DeepCopy<List<Product>>(originalProducts);

            // Restore transactions
            string json3 = JsonConvert.SerializeObject(originalTransactions, Formatting.Indented);
            File.WriteAllText(@"Data/Transactions.json", json3);
            transactions = DeepCopy<List<Transaction>>(originalTransactions);
        }

        [Test]
        public void Test_WhatIsTheTotalNumberOfTransactions()
        {
            var result = transactions.Count();

            Assert.AreEqual(2000, result);
        }

        [Test]
        public void Test_WhatIsTheTotalQuantityPurchased()
        {
            var result = transactions.Sum( t => t.Quantity); // TODO

            Assert.AreEqual(3001, result);
        }

        [Test]
        public void Test_WhatIsTheTotalQuantityPurchasedIn2016()
        {
            var result = transactions.Where(d => d.Date.Year == 2016).Sum( t => t.Quantity); // TODO

            Assert.AreEqual(1160, result);
        }
        
        [Test]
        public void Test_WhatIsTheTotalQuantityPurchasedInThePast7Days()
        {
            var result = transactions.Where(d => d.Date >= (DateTime.Today.AddDays( -8)) ).Sum(t => t.Quantity); // TODO

            Assert.AreEqual(32, result);
        }

        [Test]
        public void Test_HowManyTransactionsBoughtMoreThan1Quantity()
        {
            var result = transactions.GroupBy(t => t).Where(t => t.Count() > 1).Count();
                
 
               

            Assert.AreEqual(1001, result);
        }

        [Test]
        public void Test_HowManyTransactionsOccuredOnSundays()
        {
            var result = transactions.Where(d => d.Date.DayOfWeek == DayOfWeek.Sunday).Count(); // TODO

            Assert.AreEqual(267, result);
        }

        [Test]
        public void Test_WhatIsTheAverageQuantityPurchased()
        {
            var result = transactions.Select( t => t.Quantity).Average(); // TODO

            Assert.AreEqual(1.5005, result, 0.0001);
        }

        [Test]
        public void Test_HowManyBagsOfChipsHaveBeenBought()
        {
            var result = transactions.Where( p => p.ProductName == "Chips").Sum( q => q.Quantity); // TODO

            Assert.AreEqual(390, result);
        }

        [Test]
        public void Test_HowManyBagsOfChipsHasJasonBought()
        {
            var result = transactions.Where(p => p.ProductName == "Chips"  && p.UserName == "Jason").Sum(q => q.Quantity); // TODO

            Assert.AreEqual(44, result);
        }

        [Test]
        public void Test_HowManyBagsOfChipsDidJasonBuyIn2015()
        {
            var result = transactions.Where(d => d.Date.Year == 2015).Where(p => p.ProductName == "Chips" && p.UserName == "Jason").Sum(q => q.Quantity); // TODO

            Assert.AreEqual(33, result);
        }

        [Test]
        public void Test_HowManyBagsOfChipsDidJasonBuyInMay2016()
        {
            var result = transactions.Where(d => d.Date.Year == 2016 && d.Date.Month == 5).Where(p => p.ProductName == "Chips" && p.UserName == "Jason").Sum(q => q.Quantity); ; // TODO

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Test_WhatProductSellsTheMostBetween12And1PM()
        {
            var result = ""; // TODO

            Assert.AreEqual("Candy", result);
        }

        [Test]
        public void Test_WhatProductSellsTheLeast()
        {
            var result = ""; // TODO

            Assert.AreEqual("Cookies", result);
        }

        [Test]
        public void Test_WhoBoughtTheMostCandy()
        {
            var result = transactions.Where(p => p.ProductName == "Candy")
                .GroupBy(g => g.UserName, q => q.Quantity)
            //   .Select( k => k.Key )
           // .Max(t => t.Sum(f => f.Quantity))
          //  .Select(g => new { Key = g.Key, Value =  g.Max(f => g.Quantity))

               ; // TODO

            Assert.AreEqual("David", result);
        }

        [Test]
        public void Test_WhatIsTheTotalDollarValueOfAllTransactions()
        {
            var result = 0; // TODO

            Assert.AreEqual(3168.45, result, 0.001);
        }

        [Test]
        public void Test_WhoSpentTheMostMoney()
        {
            var result = ""; // TODO

            Assert.AreEqual("Rod", result);
        }

        [Test]
        public void Test_WhatIsThePasswordOfThePersonWhoSpentTheMostMoney()
        {
            var result = ""; // TODO

            Assert.AreEqual("optx", result);
        }

        private static T DeepCopy<T>(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
