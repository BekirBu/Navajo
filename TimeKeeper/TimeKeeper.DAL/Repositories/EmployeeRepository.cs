using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Repositories
{
    class EmployeeRepository : Repository<Employee, int>
    {
        public EmployeeRepository(TimeKeeperContext context) : base(context) { }

        public override void Update(Employee entity, int id)
        {
            Employee old = Get(id);

            if (old != null)
            {
                context.Entry(old).CurrentValues.SetValues(entity);
                old.Position = entity.Position;
                //context.SaveChanges();
            }
        }
    }
}
