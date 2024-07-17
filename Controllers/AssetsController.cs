using MagniseFinAPI.Models;
using MagniseFinAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

                return Ok(new {Count = assets.Count(), data = assets});
            }
            catch (PaginationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }

        }
    }
}
