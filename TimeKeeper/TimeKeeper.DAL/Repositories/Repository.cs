using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Repositories
{
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        protected TimeKeeperContext context;
        protected DbSet<T> dbSet;

        public Repository(TimeKeeperContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            Utility.Log($"REPOSOTORY: Get data.", "INFO");
            return dbSet;
        }

        public T Get(K id)
        {
            Utility.Log($"REPOSOTORY: Get data with entered id.", "INFO");
            return dbSet.Find(id);
        }

        public List<T> Get(Func<T, bool> where)
        {
            Utility.Log($"REPOSOTORY: Get data where function.", "INFO");
            return dbSet.Where(where).ToList();
        }

        public virtual void Insert(T entity)
        {
            Utility.Log($"REPOSOTORY: Insert data.", "INFO");

            //Type EntityType = typeof(T);

            //if(EntityType == typeof(Customer))
            //{
            //}

            dbSet.Add(entity);
        }

        public virtual void Update(T entity, K Id)
        {
            Utility.Log($"REPOSOTORY: Update data.", "INFO");
            T old = Get(Id);

            if (old != null)
            {
                context.Entry(old).CurrentValues.SetValues(entity);
            }
        }

        public void Delete(T entity)
        {
            Utility.Log($"REPOSOTORY: Delete data.", "INFO");
            dbSet.Remove(entity);
        }

    }
}
