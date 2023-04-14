﻿using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Shared.Basic.Classes;

namespace DayanaWeb.Server.EntityFramework.Extensions.Blog;

public static class PostCategoryQueryableExtension
{
    public static IQueryable<PostCategory> ApplyFilter(this IQueryable<PostCategory> query, DefaultPaginationFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Description.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Title.ToLower().Trim()));

        return query;
    }

    public static IQueryable<PostCategory> ApplySort(this IQueryable<PostCategory> query, SortByEnum? sortBy)
    {
        return sortBy switch
        {
            SortByEnum.CreationDate => query.OrderBy(x => x.CreateDate),
            SortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreateDate),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}

