using Microsoft.AspNetCore.Mvc;
using VendingMachine.Data;
using VendingMachine.Models;
using VendingMachine.Protect;

namespace VendingMachine.Controllers
{
    public class AvailabilityController : ControllerBase
    {
        private readonly VendingDbContext _dbContext;

        public AvailabilityController(VendingDbContext db) => _dbContext = db;

        [HttpGet]
        public JsonResult Get()
        {
            var list = _dbContext.Availabilities.Select((Availability item) => new object[] {item.Denomination, item.State}).ToList();

            Response.StatusCode = 200;

            return new JsonResult(list);
        }

        [AdminKey]
        [HttpPut]
        public void Set(int denomination, bool state)
        {
            var availability = _dbContext.Availabilities.First((Availability item) => item.Denomination == denomination);

            if (availability == null)
            {
                Response.StatusCode = 400;

                return;
            }

            if (availability.State != state)
            {
                availability.State = state;

                _dbContext.SaveChanges();
            }

            Response.StatusCode = 200;
        }
    }
}
