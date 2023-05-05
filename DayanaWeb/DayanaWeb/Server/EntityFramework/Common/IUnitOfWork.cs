using DayanaWeb.Server.EntityFramework.Repositories.Blog;

namespace DayanaWeb.Server.EntityFramework.Common;
public interface IUnitOfWork : IDisposable
{
    IPostRepository Posts { get; }
    IPostCategoryRepository PostCategories { get; }
    IPostFeedBackRepository PostFeedBacks { get; }
    Task<bool> CommitAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public IPostRepository Posts { get; }
    public IPostCategoryRepository PostCategories { get; }
    public IPostFeedBackRepository PostFeedBacks { get; }
    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public UnitOfWork(DataContext context)
    {
        _context = context;
        Posts = new PostRepository(_context);
        PostCategories = new PostCategoryRepository(_context);
        PostFeedBacks = new PostFeedBackRepository(_context);
    }
}
