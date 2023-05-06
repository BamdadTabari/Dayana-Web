using DayanaWeb.Server.Basic.Classes;
using DayanaWeb.Server.EntityFramework.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayanaWeb.Server.EntityFramework.Entities.Blog;
public class PostCategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PostEntity> Posts { get; set; }
}

public class PostCategoryEntityConfiguration : IEntityTypeConfiguration<PostCategoryEntity>
{
    public void Configure(EntityTypeBuilder<PostCategoryEntity> builder)
    {
        #region Properties features

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(DefaultNumbers.NameLength);

        #endregion
    }
}