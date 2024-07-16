﻿using MagniseFinAPI.Services;
using Microsoft.AspNetCore.Http;
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
            return Ok(new int[] {1, 2, 3} );
        }
    }
}
