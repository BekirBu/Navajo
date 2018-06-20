using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    internal class TimeInitializer<T> : DropCreateDatabaseAlways<TimeKeeperContext>
    {
        public override void InitializeDatabase(TimeKeeperContext context)
        {
            try
            {
                // ensure that old database instance can be dropped
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                        $"ALTER DATABASE {context.Database.Connection.Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            }
            catch
            {
                // database does not exists - no problem ;o)
            }
            finally
            {
                base.InitializeDatabase(context);
            }
        }
    }
}
