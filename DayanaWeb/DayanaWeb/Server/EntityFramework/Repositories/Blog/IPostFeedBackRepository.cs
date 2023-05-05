using DayanaWeb.Server.Basic.Classes;
using DayanaWeb.Server.EntityFramework.Common;
using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Server.EntityFramework.Extensions.Blog;
using DayanaWeb.Shared.Basic.Classes;
using Microsoft.EntityFrameworkCore;

namespace DayanaWeb.Server.EntityFramework.Repositories.Blog;

public interface IPostFeedBackRepository : IRepository<PostFeedBackEntity>
{
    Task<PostFeedBackEntity> GetByIdAsync(long id);
    Task<PaginatedList<PostFeedBackEntity>> GetListByFilterAsync(DefaultPaginationFilter filter);
    Task<List<PostFeedBackEntity>> GetAllAsync();
}

public class PostFeedBackRepository : Repository<PostFeedBackEntity>, IPostFeedBackRepository
{
    private readonly IQueryable<PostFeedBackEntity> _queryable;

    public PostFeedBackRepository(DataContext context) : base(context)
    {
        _queryable = DbContext.Set<PostFeedBackEntity>();
    }

    public async Task<PostFeedBackEntity> GetByIdAsync(long id) =>
         await _queryable.SingleOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();

    public async Task<List<PostFeedBackEntity>> GetAllAsync() => await _queryable.ToListAsync();

    public async Task<PaginatedList<PostFeedBackEntity>> GetListByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable.AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
        var dataTotalCount = _queryable.Count();

        return new PaginatedList<PostFeedBackEntity>()
        {
            Data = await query.Paginate(filter.Page, filter.PageSize).ToListAsync(),
            TotalCount = dataTotalCount,
            TotalPages = (int)Math.Ceiling((decimal)dataTotalCount / (decimal)filter.PageSize),
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }
}