using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Ybm.Infrastructure.Core
{
    public static class IQueryableExtension
    {

        public static IQueryable<T> PerformOrderBy<T>(this IQueryable<T> query)
        {
            return query.OrderBy("Id");
        }

        public static IQueryable<T> PerformPaging<T>(this IQueryable<T> query, QueryModel queryModel)
        {
            if (queryModel == null)
                return query.Skip(0).Take(10);

            if (queryModel.pageSize == 0)
                return query.Skip(0).Take(10);

            return query.Skip(queryModel.skip).Take(queryModel.pageSize);
        }

        public static string GetRawQuery(this IQueryable query)
        {
            return query.ToString();
        }
    }
}
