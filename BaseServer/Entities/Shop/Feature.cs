using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Shop
{
    public class Feature : BaseEntity<int>
    {
        public string title { get; set; }
        public string category { get; set; }
        public string itemCategory { get; set; }
    }

    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable(nameof(Feature), SchemaEnum.Shop.ToString());

            builder.Property(p => p.title).IsRequired();
            builder.Property(p => p.category).IsRequired();
            builder.Property(p => p.itemCategory).IsRequired();

            builder.HasQueryFilter(b => EF.Property<bool?>(b, "deleted") != true && EF.Property<bool?>(b, "isActive") != false);
        }
    }
}
