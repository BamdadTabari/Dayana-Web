using DayanaWeb.Server.Basic.Classes;
using DayanaWeb.Server.EntityFramework.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayanaWeb.Server.EntityFramework.Entities.Blog;
public class PostEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }

    public long PostCategoryId { get; set; }
    public PostCategoryEntity PostCategory { get; set; }
    public List<PostFeedBackEntity> PostFeedBacks { get; set; }
}

public class PostEntityConfiguration : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        #region Properties features

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(DefaultNumbers.NameLength);

        builder.Property(e => e.Description).IsRequired().HasMaxLength(DefaultNumbers.ShortDescriptionLength);

        builder.Property(e => e.Author).IsRequired().HasMaxLength(DefaultNumbers.NameLength);

        #endregion

        builder.HasOne(x => x.PostCategory).WithMany(x => x.Posts)
            .HasForeignKey(x => x.PostCategoryId).OnDelete(DeleteBehavior.Cascade);
    }
}