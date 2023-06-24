using Microsoft.AspNetCore.Mvc;
using VendingMachine.Protect;

namespace VendingMachine.Controllers
{
    public class AuthController : ControllerBase
    {
        public AuthController() { }

        [HttpGet]
        public void VerifyKey(string key)
        {
            if (key == AdminKeyAttribute.Key)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }
    }
}
