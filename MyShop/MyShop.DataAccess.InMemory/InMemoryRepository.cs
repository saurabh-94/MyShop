using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;

        public string ClassName { get; set; }

        public List<T> Items { get; set; }


        public InMemoryRepository()
        {
            ClassName = typeof(T).Name;

            Items = cache[ClassName] as List<T>;

            if(Items == null)
            {
                Items = new List<T>();
            }

        }

        public IQueryable<T> Collection()
        {
            return Items.AsQueryable();
        }

        public void Commit()
        {
            cache[ClassName] = Items;
        }

        public void Delete(T t)
        {
            Items.Remove(t);
        }

        public T Find(string Id)
        {
            return Items.Find(i => i.Id == Id);
        }

        public void Insert(T t)
        {
            Items.Add(t);
        }

        public void Update(T t, string Id)
        {
            var Item = Items.Find(i => i.Id == Id);

            if(Item == null)
            {
                throw new Exception(string.Format("{0} Not Found", ClassName));
            }

            else
            {
                Item = t;
            }
        }
    }
}
