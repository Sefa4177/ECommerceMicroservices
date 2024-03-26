using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLib.ControllerBases;
using SharedLib.Dtos;

namespace ECommerce.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult RecievePayment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }

    }
}
