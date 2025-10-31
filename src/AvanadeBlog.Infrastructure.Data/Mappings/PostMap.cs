using AvanadeBlog.Domain.Entities;
using AvanadeBlog.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvanadeBlog.Infrastructure.Data.Mappings
{
    public class PostMap : BaseEntityMap<PostEntity>
    {

        public override void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Content)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(4000);

            builder.HasIndex(x => x.Title)
                .IsUnique();
        }
    }
}
