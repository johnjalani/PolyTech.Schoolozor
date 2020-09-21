using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schoolozor.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Schoolozor.Model
{
    public interface IDataManager<T> where T : BaseContextFields, new()
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> RemoveAsync(T item);
        Task Transaction(params Func<Task>[] func);
    }

    public class DataManager<T> : IDataManager<T> where T : BaseContextFields, new()
    {
        private readonly SchoolContext _ctx;
        public DataManager(SchoolContext ctx)
        {
            _ctx = ctx;
        }
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = _ctx.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<T>();

            if (list == null)
            {
                return new List<T>();
            }

            return list;
        }

        public IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = _ctx.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();

            if (list == null)
            {
                return new List<T>();
            }

            return list;
        }

        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = _ctx.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause
            return item;
        }

        public async Task<T> AddAsync(T item)
        {

            item.InsertedDateTime = DateTime.Now;
            item.UpdatedDateTime = DateTime.Now;
            _ctx.Entry(item).State = EntityState.Added;

            await _ctx.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            var old = GetSingle(o => o.Id == item.Id);

            item.UpdatedDateTime = DateTime.Now;
            item.InsertedDateTime = old.InsertedDateTime;
            item.DeletedDateTime = old.DeletedDateTime;
            _ctx.Entry(item).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
            return item;
        }

        public async Task<T> RemoveAsync(T item)
        {
            var old = GetSingle(o => o.Id == item.Id);

            item.UpdatedDateTime = old.InsertedDateTime;
            item.InsertedDateTime = old.InsertedDateTime;
            item.DeletedDateTime = DateTime.Now;
            _ctx.Entry(item).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
            return item;
        }

        public async Task Transaction(params Func<Task>[] func)
        {
            using (var trans = _ctx.Database.BeginTransaction())
            {
                for (int i = 0; i < func.Length; i++)
                {
                    await func[i]();
                }
            }
        }
    }
}
