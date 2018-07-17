using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Z.EntityFramework.Plus;
using System.Threading.Tasks;
using System.Threading;
using Ybm.Infrastructure.Core.Interface;

namespace Ybm.Infrastructure.Core.Service
{
    public class Service<T> : IService<T> where T : class
    {
        #region [Eventing Section]
        Interface.IService<T> implementation;
        #region [Events]
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> SavingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordSaved;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordUpdated;
        #endregion
        public void PopulateEvents(Interface.IService<T> _implementation)
        {
            implementation = _implementation;

            implementation.BeforeSavingRecord += new EventHandler<EntitySavingEventArgs<T>>(this.OnBeforeSavingRecord);
            implementation.SavingRecord += new EventHandler<EntitySavingEventArgs<T>>(this.OnSavingRecord);
            implementation.RecordSaved += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordSaved);


            implementation.BeforeDeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnBeforeDeletingRecord);
            implementation.DeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnDeletingRecord);
            implementation.RecordDeleted += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnRecordDeleted);


            implementation.UpdatingRecord += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnUpdatingRecord);
            implementation.RecordUpdated += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordUpdated);


        }
        #region [Virtual Mothods]
        public virtual void OnBeforeSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordSaved(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnBeforeDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnRecordDeleted(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnUpdatingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordUpdated(object sender, EntitySavingEventArgs<T> e)
        {
        }
        #endregion
        #endregion
        private readonly DbContext _dbContext;
        public Service(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual void Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (BeforeSavingRecord != null)
                BeforeSavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
            _dbContext.Set<T>().Add(item);

            if (SavingRecord != null)
                SavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });


            _dbContext.SaveChanges();

            if (RecordSaved != null)
                RecordSaved.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
        }

        public virtual async Task<T> CreateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (BeforeSavingRecord != null)
                BeforeSavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
            await _dbContext.Set<T>().AddAsync(item);

            if (SavingRecord != null)
                SavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });


            await _dbContext.SaveChangesAsync();

            if (RecordSaved != null)
                RecordSaved.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            return item;

        }

        public virtual async Task<T> CreateAsyncUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            await _dbContext.Set<T>().AddAsync(item);

            return item;
        }

        /// <summary>
        /// It doesn't support Creating Events. Instead use Create(T model)
        /// </summary>
        /// <param name="items"></param>
        public virtual void CreateMulti(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().AddRange(items);
            _dbContext.SaveChanges();
        }
        public virtual async Task<bool> CreateMultiAsync(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            if (!items.Any())
                throw new ArgumentNullException("item");

            await _dbContext.Set<T>().AddRangeAsync(items);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> CreateMultiAsyncUoW(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            if (!items.Any())
                throw new ArgumentNullException("item");

            await _dbContext.Set<T>().AddRangeAsync(items);

            return true;
        }

        public virtual void Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            //item.Datestamp = DateTime.Now;
            //_dbContext.Set(item.GetType()).Attach(item);

            _dbContext.Entry(item).State = EntityState.Modified;
            if (UpdatingRecord != null)
                UpdatingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            _dbContext.SaveChanges();
            if (RecordUpdated != null)
                RecordUpdated.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

        }
        public virtual async Task<T> UpdateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            //item.Datestamp = DateTime.Now;
            //_dbContext.Set(item.GetType()).Attach(item);

            _dbContext.Entry(item).State = EntityState.Modified;
            if (UpdatingRecord != null)
                UpdatingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            await _dbContext.SaveChangesAsync();
            if (RecordUpdated != null)
                RecordUpdated.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            return item;
        }

        public virtual T UpdateUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            //_dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.Set<T>().Update(item);

            return item;
        }
        public virtual void SaveChanges()
        {
            //Console.WriteLine("_dbContext : " + _dbContext.GetHashCode() + "  sender : " + sender);
            try
            {
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
            }

        }
        public virtual async System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            //Console.WriteLine("_dbContext : " + _dbContext.GetHashCode() + "  sender : " + sender);
            return await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// It doesn't support deleting events. Instead Use Delete(T item)
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            _dbContext.Set<T>().Where(predicate).Delete();
            _dbContext.SaveChanges();
        }
        public virtual async Task<Boolean> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                await _dbContext.Set<T>().Where(predicate).DeleteAsync();
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<Boolean> DeleteAsyncUoW(Expression<Func<T, bool>> predicate)
        {
            try
            {
                await _dbContext.Set<T>().Where(predicate).DeleteAsync();
                return default(Boolean);
            }
            catch (Exception ex)
            {
                return default(Boolean);
            }
        }
        public virtual void Delete(T item)
        {
            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });

            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            if (DeletingRecord != null)
                DeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
            _dbContext.SaveChanges();

            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
        }
        public virtual async void DeleteAsync(T item)
        {
            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });

            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            if (DeletingRecord != null)
                DeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
            await _dbContext.SaveChangesAsync();

            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
        }
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }
            _dbContext.SaveChanges();

        }
        public virtual async void DeleteAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }
        public virtual IQueryable<T> FetchAll()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }
        public virtual IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().AsNoTracking() :
                 _dbContext.Set<T>().AsNoTracking().Where(predicate);
        }



        //public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken);

        public virtual Boolean Any(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().Any(predicate);
        }
        public virtual async Task<Boolean> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
        }
        public virtual IQueryable<T> FetchAll<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate, Expression<Func<T, S>> orderByExpression, bool ascending = true)
        {
            pageIndex--;
            if (pageIndex < 0)
                throw new ArgumentException("Page index must be greater or equal to 0", "pageIndex");
            if (pageSize <= 0)
                throw new ArgumentException("Page size must be greater then 0", "pageSize");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression");

            var objectSet = _dbContext.Set<T>();

            var items = (ascending)
                ? objectSet
                    .OrderBy(orderByExpression)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                : objectSet
                    .OrderByDescending(orderByExpression)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);

            pageIndex++;
            return items;
        }
        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().FirstOrDefault() : _dbContext.Set<T>().FirstOrDefault(predicate);
        }
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await _dbContext.Set<T>().FirstOrDefaultAsync() : await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public virtual T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbContext.Set<T>().FirstOrDefault(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual async Task<T> LastOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbContext.Set<T>().LastOrDefaultAsync(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }
        public virtual T LastOrDefault(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? _dbContext.Set<T>().LastOrDefault() : _dbContext.Set<T>().FirstOrDefault(predicate);
        }
        public virtual async Task<T> FirstOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }
        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? await _dbContext.Set<T>().SingleOrDefaultAsync() : await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }
        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ?
            _dbContext.Set<T>().Count() :
            _dbContext.Set<T>().Count(predicate);

        }
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await _dbContext.Set<T>().CountAsync() : await _dbContext.Set<T>().CountAsync(predicate);
        }
        public virtual int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate)
        {
            return _dbContext.Set<T>().Where(predicate).Update(updatePredicate);
        }
        public virtual async Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate)
        {
            return await _dbContext.Set<T>().Where(predicate).UpdateAsync(updatePredicate);
        }
        //public virtual int RunQuery(string query, params object[] parameters)
        //{
        //    var result = _dbContext.Database.SqlQuery<int>(query, parameters).ToList();
        //    return result[0];

        //}
        public void ExecuteSqlQuery(SqlCommand sqlCommand)
        {
            var entityConnection = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection con = new SqlConnection(entityConnection))
            {
                using (SqlCommand cmd = sqlCommand)
                {
                    try
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public DataSet ExecuteSqlQueryWithResult(SqlCommand sqlCommand)
        {
            DataSet ds = new DataSet();
            var entityConnection = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection con = new SqlConnection(entityConnection))
            {
                using (SqlCommand cmd = sqlCommand)
                {
                    try
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds;
        }
        public T FetchFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }
        public async Task<T> FetchFirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        public void UpdateWithAttach(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public async void UpdateWithAttachAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public int RunQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }
        public IList<S> RunQuery<S>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }
        public S RunRawQuery<S>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}
