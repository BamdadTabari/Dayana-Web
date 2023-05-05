using DayanaWeb.Server.Basic.Classes;
using DayanaWeb.Server.EntityFramework.Common;
using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Server.EntityFramework.Extensions.Blog;
using DayanaWeb.Shared.Basic.Classes;
using Microsoft.EntityFrameworkCore;

namespace DayanaWeb.Server.EntityFramework.Repositories.Blog;
public interface IPostCategoryRepository : IRepository<PostCategoryEntity>
{
    Task<PostCategoryEntity> GetByIdAsync(long id);
    Task<PaginatedList<PostCategoryEntity>> GetListByFilterAsync(DefaultPaginationFilter filter);
    Task<List<PostCategoryEntity>> GetAllAsync();
}

public class PostCategoryRepository : Repository<PostCategoryEntity>, IPostCategoryRepository
{
    private readonly IQueryable<PostCategoryEntity> _queryable;

    public PostCategoryRepository(DataContext context) : base(context)
    {
        _queryable = DbContext.Set<PostCategoryEntity>();
    }

    public async Task<PostCategoryEntity> GetByIdAsync(long id) =>
         await _queryable.SingleOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();

    public async Task<List<PostCategoryEntity>> GetAllAsync() => await _queryable.ToListAsync();

    public async Task<PaginatedList<PostCategoryEntity>> GetListByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable.AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
        var dataTotalCount = _queryable.Count();

        return new PaginatedList<PostCategoryEntity>()
        {
            Data = await query.Paginate(filter.Page, filter.PageSize).ToListAsync(),
            TotalCount = dataTotalCount,
            TotalPages = (int)Math.Ceiling((decimal)dataTotalCount / (decimal)filter.PageSize),
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }
}