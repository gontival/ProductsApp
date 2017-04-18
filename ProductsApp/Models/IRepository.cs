using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApp.Models
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll() ;
        T Get(int id);
        T Add(T item);
        void Remove(int id);
        bool Update(T item);

    }
}
