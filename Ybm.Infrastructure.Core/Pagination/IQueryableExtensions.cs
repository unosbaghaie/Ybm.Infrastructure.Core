using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ybm.Infrastructure.Core.ExpressionHelper;


namespace Ybm.Infrastructure.Core.Pagination
{
  

        public static class IQueryableExtensions
        {
            public static GridDataResult<T> ApplyPaging<T>(this IQueryable<T> query, GridState queryModel)
            {
                if (queryModel == null)
                    queryModel = new GridState() { skip = 0, take = 10 };

                var count = query.Count();

                if (queryModel == null || (queryModel.take == 0 && queryModel.skip == 0))
                    queryModel = new GridState()
                    {
                        skip = 0,
                        take = 10
                    };

                query = query.Skip(queryModel.skip).Take(queryModel.take);

                var result = new GridDataResult<T>();
                result.total = count;
                result.data = query.ToList();

                return result;
            }

            public async static Task<GridDataResult<T>> ApplyPagingAsync<T>(this IQueryable<T> query, GridState queryModel)
            {
                var count = query.Count();

                if (queryModel == null)
                    queryModel = new GridState()
                    {
                        skip = 0,
                        take = 10
                    };

                query = query.Skip(queryModel.skip).Take(queryModel.take);

                var result = new GridDataResult<T>();
                result.total = count;
                result.data = await query.ToListAsync();

                return result;
            }



            public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, GridState model)
            {
                if (model == null || model.sort == null || !model.sort.Any())
                {
                    var type = typeof(T);
                    var field = type.GetProperties().Where(q => q.Name.Contains("Id")).FirstOrDefault();

                    if (field == null)
                        throw new Exception("حداقل یک فیلد شامل کلمه ی Id برای مرتب سازی داده ها نیاز می باشد");

                    return query.OrderBy(field.Name);
                }
                bool isFirstTime = true;
                foreach (var sort in model.sort)
                {
                    if (string.IsNullOrWhiteSpace(sort.field))
                        continue;

                    if (sort.dir == "asc" && isFirstTime)
                    {
                        query = query.OrderBy(sort.field);
                    }
                    else
                    if (sort.dir == "asc" && !isFirstTime)
                    {
                        query = query.ThenBy(sort.field);
                    }
                    else
                    if (sort.dir == "des" && isFirstTime)
                    {
                        query = query.OrderByDescending(sort.field);
                    }
                    else
                    if (sort.dir == "des" && !isFirstTime)
                    {
                        query = query.ThenByDescending(sort.field);
                    }
                    isFirstTime = false;
                }
                return query;
            }

        }
    }

