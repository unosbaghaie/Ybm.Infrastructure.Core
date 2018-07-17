using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ybm.Infrastructure.Core.Pagination
{
  
    public static class IQueryableExtensions
    {
        //public static PagedList<T> ApplyPaging<T>(this IQueryable<T> query , QueryModel queryModel)
        //{

        //    var take = queryModel.PageSize;
        //    var skip = (queryModel.Page > 0 ? queryModel.Page - 1 : 0) * take;

        //    if (queryModel.Page <= 0)
        //    {
        //        queryModel.Page = 1;
        //    }

        //    if (queryModel.PageSize <= 0)
        //    {
        //        queryModel.PageSize = 10;
        //    }

        //    var countOfAll = query.Count();

        //    var result = new PagedList<T>();

        //    result.CurrentPage = queryModel.Page;

        //    var NumberOfPages = Math.Ceiling((double)countOfAll / (double)queryModel.PageSize);

        //    result.NumberOfPages = Convert.ToInt32(NumberOfPages);

        //    result.TotalItems = countOfAll;

        //    result.Items = query.Skip((queryModel.Page - 1) * queryModel.PageSize).Take(queryModel.PageSize).ToHashSet();

        //    if (result.NumberOfPages < 10)
        //        result.MaxSize = result.NumberOfPages;
        //    else
        //        result.MaxSize = 10;

        //    return result;
        //}




        //public static IQueryable<T> ApplyPaging<T>(
        //  this IQueryable<T> query, IPagedQueryModel model)
        //{
        //    if (model.Page <= 0)
        //    {
        //        model.Page = 1;
        //    }

        //    if (model.PageSize <= 0)
        //    {
        //        model.PageSize = 10;
        //    }

        //    return query.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize);
        //}


        //public static IQueryable<T> ApplyOrdering<T>(
        //  this IQueryable<T> query,
        //  QueryModel model,
        //  IDictionary<string, Expression<Func<T, object>>> columnsMap)
        //{
        //    if (string.IsNullOrWhiteSpace(model.SortBy) || !columnsMap.ContainsKey(model.SortBy))
        //    {
        //        return query;
        //    }

        //    if (model.IsAscending)
        //    {
        //        return query.OrderBy(columnsMap[model.SortBy]);
        //    }
        //    else
        //    {
        //        return query.OrderByDescending(columnsMap[model.SortBy]);
        //    }
        //}


    }
}
