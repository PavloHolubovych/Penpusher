using System.Web.Http;



namespace Penpusher.Controllers
{

    

    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        //public TestController() : base() { }
        private readonly iTest itest;
        public TestController(iTest itest)
        {
        this.itest=itest;

        }
        [Route("get")]
        [HttpGet]
        public Product[] GetAllProducts()
        { 
            //Product[] prod = itest.GetAllProducts();
            return itest.GetAllProducts();
        }



        public interface iTest
        {
            Product[] GetAllProducts();
        }
        public class Test : iTest
        {

            public Product[] GetAllProducts()
            {
                Product[] products = new Product[]
    {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
    };

                return products;
            }
        }

        public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
}

