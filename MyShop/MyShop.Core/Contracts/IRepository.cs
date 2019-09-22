using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> Items { get; set; }

        IQueryable<T> Collection();
        T Find(string Id);
        void Insert(T t);
        void Update(T t , string Id);
        void Delete(T t);
        void Commit();
    }
}
