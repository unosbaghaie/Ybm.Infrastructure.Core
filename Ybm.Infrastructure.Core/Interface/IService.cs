using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Infrastructure.Core.Interface
{
    public interface IService<T> : ISaveService<T>, IDeleteService<T>, IFetchService<T> where T : class
    {
        int RunQuery(string query, params object[] parameters);
        IList<S> RunQuery<S>(string query, params object[] parameters);
        S RunRawQuery<S>(string query, params object[] parameters);

        System.Threading.Tasks.Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
