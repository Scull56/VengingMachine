using Microsoft.AspNetCore.Mvc;
using VendingMachine.Data;

namespace VendingMachine.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly VendingDbContext _dbContext;

        public ProductsController(VendingDbContext db) => _dbContext = db;

        [HttpGet]
        public JsonResult GetProducts()
        {
            Response.StatusCode = 200;

            return new JsonResult(_dbContext.Products.ToList());
        }

        [HttpPost]
        public JsonResult AddProducts(string key)
        {
            Response.
        }

        [HttpPost]
        public JsonResult AddProduct(string key)
        {

        }

        [HttpPut]
        public JsonResult UpdateProduct(string key)
        {

        }

        [HttpDelete]
        public JsonResult DeleteProduct(string key, int id)
        {

        }
    }
}
