using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        public List<T> Items { get; set; }

        private readonly DataContext dataContext;
        private readonly DbSet<T> dbSet;

        public SqlRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
            dbSet = dataContext.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet.AsQueryable();
        }

        public void Commit()
        {
            dataContext.SaveChanges();
        }

        public void Delete(T t)
        {
            var item = Find(t.Id);

            if(dataContext.Entry(item).State == EntityState.Detached)
            {
                dbSet.Attach(item);
            }

            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t, string Id)
        {
            var entry = Find(Id);

            dataContext.Entry(entry).CurrentValues.SetValues(t);
        }
    }
}
