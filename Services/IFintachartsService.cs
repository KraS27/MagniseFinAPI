﻿using MagniseFinAPI.Models;

namespace MagniseFinAPI.Services
{
    public interface IFintachartsService
    {
        public Task<string> GetBearerTokenAsync();

        public Task UpdateMarketAssetsAsync();
    }
}
