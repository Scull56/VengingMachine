using Microsoft.EntityFrameworkCore;
using VendingMachine.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VendingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VendingDb"));
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapFallbackToFile("index.html");

app.Run();
