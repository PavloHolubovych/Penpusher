using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Penpusher.Controllers;

namespace Penpusher.Models
{
    public class TestClassForApi: Iitems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        TestClassForApi[] items = new TestClassForApi[]
       {
            new TestClassForApi { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new TestClassForApi { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new TestClassForApi { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
       };

        public IEnumerable<TestClassForApi> GetAllItems()
        {
            return items;
        }
    }
    public interface Iitems
    {
        IEnumerable<TestClassForApi> GetAllItems();
    }

}