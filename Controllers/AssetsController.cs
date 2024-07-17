using MagniseFinAPI.Models;
using MagniseFinAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagniseFinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IMarketAssetsService _marketAssetsService;

        public AssetsController(IMarketAssetsService marketAssetsService)
        {
            _marketAssetsService = marketAssetsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssets([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var assets = await _marketAssetsService.GetAllAsync(new Pagination<MarketAsset>(page, pageSize));

                return Ok(assets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }

        }
    }
}
