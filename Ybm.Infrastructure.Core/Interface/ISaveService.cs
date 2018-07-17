using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ybm.Infrastructure.Core.Service;

namespace Ybm.Infrastructure.Core.Interface
{
    public interface ISaveService<T> where T : class
    {

        event System.EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> SavingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> RecordSaved;

        event System.EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> RecordUpdated;

        void Create(T item);
        Task<T> CreateAsync(T item);
        Task<T> CreateAsyncUoW(T item);
        Task<bool> CreateMultiAsyncUoW(IEnumerable<T> items);

        void Update(T item);

        Task<T> UpdateAsync(T item);
        T UpdateUoW(T item);


        void CreateMulti(IEnumerable<T> items) ;
        Task<bool> CreateMultiAsync(IEnumerable<T> items);
        int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate);
        Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate);
        void UpdateWithAttach(T item);
        void UpdateWithAttachAsync(T item);
    }
}
