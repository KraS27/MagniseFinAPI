using MagniseFinAPI.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MagniseFinAPI.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<MarketAsset> MarketAssets { get; set; }
        
        public DbSet<Mapping> Mappings { get; set; }
    }
}
