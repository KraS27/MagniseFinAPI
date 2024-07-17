using MagniseFinAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagniseFinAPI.DB.Configurations
{
    public class MarketAssetConfiguration : IEntityTypeConfiguration<MarketAsset>
    {
        public void Configure(EntityTypeBuilder<MarketAsset> builder)
        {
            builder.ToTable("market_assets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Symbol).HasColumnName("symbol");
            builder.Property(x => x.Kind).HasColumnName("kind");
            builder.Property(x => x.Exchange).HasColumnName("exchange");
            builder.Property(x => x.Description).HasColumnName("descriptions");
            builder.Property(x => x.TickSize).HasColumnName("tick_size");
            builder.Property(x => x.Currency).HasColumnName("currency");

            builder.HasMany(a => a.Mappings)
                .WithMany(m => m.MarketAssets)
                .UsingEntity<MarketAssetsMapping>(
                l => l.HasOne<Mapping>().WithMany().HasForeignKey(m => m.MappingId),
                r => r.HasOne<MarketAsset>().WithMany().HasForeignKey(a => a.MarketAssetsId),
                t => t.ToTable("mapping_assets"));
        }
    }
}
