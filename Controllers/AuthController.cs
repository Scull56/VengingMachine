using Microsoft.AspNetCore.Mvc;
using VendingMachine.Data;

namespace VendingMachine.Controllers
{
    public class AuthController : ControllerBase
    {
        public AuthController() { }

        [HttpGet]
        public string VerifyKey(string key)
        {
            if (key.Length > 0)
            {
                Response.StatusCode = 200;

                return "true";
            }

            Response.StatusCode = 400;
            
            return "false";
        }
    }
}
