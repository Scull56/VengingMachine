using Microsoft.AspNetCore.Mvc;
using VendingMachine.Data;
using VendingMachine.Models;
using VendingMachine.Protect;

namespace VendingMachine.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly VendingDbContext _dbContext;

        private readonly string imagesPath = Path.Combine(Environment.CurrentDirectory, "products_images");

        private readonly string[] supportedTableContentTypes = new string[] { "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
        private readonly string[] supportedImageContentTypes = new string[] { "image/jpg", "image/jpeg" };
        private readonly string[] supportedImageExtensions = new string[] { ".jpg", ".jpeg" };

        public ProductsController(VendingDbContext db) => _dbContext = db;

        [HttpGet]
        public JsonResult GetProducts()
        {
            Response.StatusCode = 200;

            return new JsonResult(_dbContext.Products.OrderByDescending(product => product.Id).ToList());
        }

        [HttpPut]
        public async Task<JsonResult> Buy(int id, int count)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                Response.StatusCode = 400;

                return new JsonResult(new {message = "Can't find product" });
            }

            if (product.Count - count < 0)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = "The quantity of the product is less than the requested count" });
            }

            product.Count -= count;

            await _dbContext.SaveChangesAsync();

            Response.StatusCode = 200;

            return new JsonResult("");
        }

        [AdminKey]
        [HttpPost]
        public async Task<JsonResult> AddProducts()
        {   
            IFormFile? table = null;

            var images = new List<IFormFile>();

            if (Request.Form.Files.Count == 0)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = "Undefined table file and images" });
            }

            foreach (var file in Request.Form.Files)
            {
                if (supportedTableContentTypes.Contains(file.ContentType))
                {
                    table = file;

                    continue;
                }
                
                if (supportedImageContentTypes.Contains(file.ContentType))
                {
                    images.Add(file);

                    continue;
                }

                Response.StatusCode = 400;

                return new JsonResult(new { message = "Was detected file with unsupported content type. Table file must have .xls or .xlsx extension. Images must have .jpg or .jpeg extenstions" });
            }

            if (table == null)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = "Undefined table file" });
            }

            if (images.Count == 0)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = "Undefined any images" });
            }

            var workbook = new Aspose.Cells.Workbook(table.OpenReadStream());

            var sheet = workbook.Worksheets[0];

            var cells = sheet.Cells;

            var products = new List<Product>();

            var i = 2;

            while (true)
            {
                var title = cells[$"A{i}"].Value != null ? cells[$"A{i}"].Value.ToString() : "";
                var priceStr = cells[$"B{i}"].Value != null ? cells[$"B{i}"].Value.ToString() : "";
                var countStr = cells[$"C{i}"].Value != null ? cells[$"C{i}"].Value.ToString(): "";
                var image = cells[$"D{i}"].Value != null ? cells[$"D{i}"].Value.ToString(): "";

                if (title.Length == 0 && priceStr.Length == 0 && countStr.Length == 0 && image.Length == 0)
                {
                    break;
                }

                var imageExtension = Path.GetExtension(image);

                var errors = validateProductData(title, countStr, priceStr);

                if (!supportedImageExtensions.Contains(imageExtension))
                {
                    errors.Add("the image must have extension .jpg or .jpeg");
                }

                var imageFileIndex = images.FindIndex(file => file.FileName == image);

                if (imageFileIndex == -1)
                {
                    errors.Add("cant find image file by defined name");
                }

                if (errors.Count > 0)
                {
                    Response.StatusCode = 400;

                    return new JsonResult(new { message = $"Error on line {i}: " + String.Join("; ", errors) });
                }

                string tempImageName = $"_{i - 1}{imageExtension}";

                var product = new Product
                {
                    Title = title,
                    Price = int.Parse(priceStr),
                    Count = int.Parse(countStr),
                    Image = imageFileIndex.ToString()
                };

                products.Add(product);

                ++i;
            }

            await _dbContext.AddRangeAsync(products);

            await _dbContext.SaveChangesAsync();

            products.ForEach(product =>
            {
                var imageIndex = int.Parse(product.Image);

                var imageFile = images[imageIndex];

                var newImageName = $"{product.Id}{Path.GetExtension(imageFile.FileName)}";

                product.Image = newImageName;

                string path = Path.Combine(imagesPath, newImageName);

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(fs);
                }
            });

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
            IFormFile? image;

            if (Request.Form.Files.Count == 0)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = "Undefined image" });
            }

            image = Request.Form.Files[0];

            var errors = validateProductData(title, count, price);

            if (errors.Count > 0)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = String.Join("; ", errors) });
            }

            string imageContentType = image.ContentType;

            if (!supportedImageContentTypes.Contains(imageContentType))
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = "the image must have extension .jpg or .jpeg" });
            }

            var product = new Product {
                Title = title,
                Count = int.Parse(count),
                Price = int.Parse(price),
                Image = ""
            };

            await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();

            string imageExtension = Path.GetExtension(image.FileName);

            string imageName = $"{product.Id}{imageExtension}";

            product.Image = imageName;

            string path = Path.Combine(imagesPath, imageName);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(fs);
            }

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

            var errors = validateProductData(title, count, price);

            if (errors.Count > 0)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = String.Join("; ", errors) });
            }

            var product = await _dbContext.Products.FindAsync(int.Parse(id));

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
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                Response.StatusCode = 400;

                return new JsonResult(new { message = "Can't find product for delete" });
            }

            _dbContext.Products.Remove(product);

            try
            {
                System.IO.File.Delete(Path.Combine(imagesPath, product.Image));
            }
            catch { 
            }

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
