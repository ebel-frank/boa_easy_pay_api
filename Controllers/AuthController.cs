using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoaEasyPay.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        public readonly IConfiguration config;
        public AuthController(IConfiguration config)
        {
            this.config = config;
        }
    }
}
