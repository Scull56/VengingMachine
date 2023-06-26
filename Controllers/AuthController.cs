using Microsoft.AspNetCore.Mvc;
using VendingMachine.Protect;

namespace VendingMachine.Controllers
{
    public class AuthController : ControllerBase
    {
        public AuthController() { }

        [AdminKey]
        [HttpGet]
        public void VerifyKey()
        {
            Response.StatusCode = 200;
        }
    }
}
