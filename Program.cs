using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using VendingMachine.Data;
using VendingMachine.Protect;

var builder = WebApplication.CreateBuilder(args);

AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

builder.Services.AddDbContext<VendingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VendingDb"));
});

builder.Services.AddControllers();

var adminKey = builder.Configuration.GetValue<string>("AdminKey");

var app = builder.Build();

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "products_images")),
    RequestPath = "/products_images"
});

app.UseRouting();
app.UseAdminKey(adminKey);
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapFallbackToFile("index.html");

app.Run();
