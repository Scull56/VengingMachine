using Microsoft.AspNetCore.Mvc;
using VendingMachine.Data;
using VendingMachine.Models;
using VendingMachine.Protect;

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

        [AdminKey]
        [HttpPost]
        public async Task<JsonResult> AddProducts()
        {
            Response.StatusCode = 200;

            IFormFile? file = Request.Form.Files[0];

            var extenstion = Path.GetExtension(file.FileName);

            if (file == null)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = "Undefined file" });
            }

            if (extenstion != ".xls" && extenstion != ".xlsx")
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = "the file must have extension .xls or .xlsx" });
            }

            var workbook = new Aspose.Cells.Workbook(file.OpenReadStream());

            var sheet = workbook.Worksheets[0];

            var cells = sheet.Cells;

            var products = new List<Product>();

            var i = 2;

            while (true)
            {
                var title = (string)cells[$"A{i}"].Value;
                var priceStr = (string)cells[$"B{i}"].Value;
                var countStr = (string)cells[$"C{i}"].Value;
                var image = (string)cells[$"D{i}"].Value;
                var imageExtension = Path.GetExtension((string)image);

                if (title.Length == 0 && priceStr.Length == 0 && countStr.Length == 0 && image.Length == 0)
                {
                    break;
                }

                var errors = validateProductData(title, priceStr, countStr);

                if (errors.Count > 0 || imageExtension != ".jpg" && imageExtension != ".jpeg")
                {
                    Response.StatusCode = 400;

                    if (imageExtension != ".jpg" && imageExtension != ".jpeg")
                    {
                        errors.Add("image file name has wrong extansion, it's must be .jpg or .jpeg");
                    }

                    return new JsonResult(new { message = $"Error on line {i}: " + String.Join("; ", errors) });
                }

                var product = new Product
                {
                    Title = title,
                    Price = int.Parse(priceStr),
                    Count = int.Parse(countStr),
                    Image = image
                };

                products.Add(product);

                ++i;
            }

            await _dbContext.AddRangeAsync(products);

            await _dbContext.SaveChangesAsync();

            Response.StatusCode = 200;

            return new JsonResult("");
        }

        [AdminKey]
        [HttpPost]
        public async Task<JsonResult> Add()
        {
            var title = Request.Form["title"];
            var count = Request.Form["count"];
            var price = Request.Form["price"];
            IFormFile? image = Request.Form.Files[0];

            var errors = validateProductData(title, price, count);

            if (errors.Count > 0)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = String.Join("; ", errors) });
            }

            if (image == null)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = "Undefined image" });
            }

            string imageExtension = Path.GetExtension(image.FileName);

            if (imageExtension != ".jpeg" && imageExtension != ".jpg")
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = "the image must have extension .jpg or .jpeg" });
            }

            int nextId = _dbContext.Products.LastOrDefault().Id + 1;

            string imageName = $"{nextId}{imageExtension}";

            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "assets", "images", "products", imageName);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(fs);
            }

            var product = new Product {
                Title = title,
                Count = int.Parse(count),
                Price = int.Parse(price),
                Image = imageName
            };

            await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();

            Response.StatusCode = 200;

            return new JsonResult("");
        }

        [AdminKey]
        [HttpPut]
        public async Task<JsonResult> Update()
        {
            var id = Request.Form["id"];
            var title = Request.Form["title"];
            var count = Request.Form["count"];
            var price = Request.Form["price"];

            var errors = validateProductData(title, price, count);

            if (errors.Count > 0)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = String.Join("; ", errors) });
            }

            var product = await _dbContext.Products.FindAsync((Product item) => item.Id == int.Parse(id));

            if (product == null)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = "Can't find product for update" });
            }

            product.Title = title;
            product.Count = int.Parse(count);
            product.Price = int.Parse(price);

            await _dbContext.SaveChangesAsync();

            Response.StatusCode = 200;

            return new JsonResult("");
        }

        [AdminKey]
        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var product = await _dbContext.Products.FindAsync((Product item) => item.Id == id);

            if (product == null)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { message = "Can't find product for delete" });
            }

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();

            Response.StatusCode = 200;

            return new JsonResult("");
        }

        private List<string> validateProductData(string title, string countStr, string priceStr)
        {
            var errors = new List<string>();

            if (title.Length == 0)
            {
                errors.Add("Undefined title");
            }

            if (title.Length < 2 && title.Length > 30)
            {
                errors.Add("the length of the title must be between 2 and 30 characters");
            }

            try
            {
                int count = int.Parse(countStr);

                if (count < 0)
                {
                    errors.Add("the count of products must start from 0");
                }
            }
            catch (Exception ex)
            {
                errors.Add("undefined count");
            }

            try
            {
                int price = int.Parse(priceStr);

                if (price < 1)
                {
                    errors.Add("the price must start from 1");
                }
            }
            catch (Exception ex)
            {
                errors.Add("undefined price");
            }

            return errors;
        }
    }
}
