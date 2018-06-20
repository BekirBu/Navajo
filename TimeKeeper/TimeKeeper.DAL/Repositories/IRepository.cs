using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Repositories
{
    //generic interface
    public interface IRepository<T, K>
    {
        IQueryable<T> Get();
        T Get(K id);
        List<T> Get(Func<T, bool> where);

        void Insert(T entity);
        void Update(T entity, K Id);
        void Delete(T entity);
    }
}
