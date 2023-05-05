using DayanaWeb.Server.Basic.Classes;
using DayanaWeb.Server.EntityFramework.Common;
using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Server.EntityFramework.Extensions.Blog;
using DayanaWeb.Shared.Basic.Classes;
using Microsoft.EntityFrameworkCore;

namespace DayanaWeb.Server.EntityFramework.Repositories.Blog;
public interface IPostRepository : IRepository<PostEntity>
{
    Task<PostEntity> GetByIdAsync(long id);
    Task<PaginatedList<PostEntity>> GetListByFilterAsync(DefaultPaginationFilter filter);
    Task<List<PostEntity>> GetAllAsync();
}

public class PostRepository : Repository<PostEntity>, IPostRepository
{
    private readonly IQueryable<PostEntity> _queryable;

    public PostRepository(DataContext context) : base(context)
    {
        _queryable = DbContext.Set<PostEntity>();
    }

    public async Task<PostEntity> GetByIdAsync(long id) =>
         await _queryable.SingleOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();

    public async Task<List<PostEntity>> GetAllAsync() => await _queryable.ToListAsync();

    public async Task<PaginatedList<PostEntity>> GetListByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable.AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
        var dataTotalCount = _queryable.Count();

        return new PaginatedList<PostEntity>()
        {
            Data = await query.Paginate(filter.Page, filter.PageSize).ToListAsync(),
            TotalCount = dataTotalCount,
            TotalPages = (int)Math.Ceiling((decimal)dataTotalCount / (decimal)filter.PageSize),
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }
}