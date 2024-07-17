using MagniseFinAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagniseFinAPI.DB.Configurations
{
    public class MappingConfiguration : IEntityTypeConfiguration<Mapping>
    {
        public void Configure(EntityTypeBuilder<Mapping> builder)
        {
            builder.ToTable("mappings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Symbol).HasColumnName("symbol");
            builder.Property(x => x.Exchange).HasColumnName("exchange");          
            builder.Property(x => x.DefaultOrderSize).HasColumnName("defaulrOrderSize");          
        }
    }
}
