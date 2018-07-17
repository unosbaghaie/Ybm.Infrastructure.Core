using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ybm.Infrastructure.Core.Service;

namespace Ybm.Infrastructure.Core.Interface
{
    public interface IDeleteService<T> where T : class
    {

        event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;

        void Delete(T item);
        void DeleteAsync(T item);
        void Delete(IEnumerable<T> entities);
        void DeleteAsync(IEnumerable<T> entities);
        Task<Boolean> DeleteAsyncUoW(Expression<Func<T, bool>> predicate);
        void Delete(Expression<Func<T, bool>> predicate);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);


    }

}
