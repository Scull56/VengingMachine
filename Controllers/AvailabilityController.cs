using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Data;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class AvailabilityController : ControllerBase
    {
        private readonly VendingDbContext _dbContext;

        public AvailabilityController(VendingDbContext db) => _dbContext = db;

        [HttpGet]
        public JsonResult GetAvailability()
        {
            var list = _dbContext.Availabilities.Select((Availability item) => new object[] {item.Denomination, item.State}).ToList();

            Response.StatusCode = 200;

            return new JsonResult(list);
        }

        [HttpPut]
        public async void SetAvailability(string key, int denomination, bool state)
        {

            var availability = _dbContext.Availabilities.Find((Availability item) => item.Denomination == denomination);

            if (availability == null)
            {
                Response.StatusCode = 400;

                return;
            }

            if (availability.State != state)
            {
                availability.State = state;

                await _dbContext.SaveChangesAsync();
            }

            Response.StatusCode = 200;
        }
    }
}
