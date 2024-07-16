using MagniseFinAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagniseFinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IFintachartsService _fintachartsService;

        public AssetsController(IFintachartsService fintachartsService)
        {
            _fintachartsService = fintachartsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssets()
        {
            await _fintachartsService.GetBearerTokenAsync();

            return Ok();
        }
    }
}
