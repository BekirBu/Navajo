using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL
{
    public class TimeKeeperContext : DbContext
    {
        public TimeKeeperContext() : base("name=TimeKeeper")
        {
            if (Database.Connection.Database == "Testera")
            {
                Database.SetInitializer(new TimeInitializer<TimeKeeperContext>());
            }
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Engagement> Engagements { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                        .Map<Customer>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Day>()
                        .Map<Day>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Day>()
                        .Property(x => x.Hours)
                        .HasPrecision(4, 2);

            modelBuilder.Entity<Employee>()
                        .Map<Employee>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Employee>()
                        .Property(x => x.Salary)
                        .HasPrecision(7, 2);

            modelBuilder.Entity<Engagement>()
                        .Map<Engagement>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Engagement>()
                        .Property(x => x.Hours)
                        .HasPrecision(4, 2);

            modelBuilder.Entity<Project>()
                        .Map<Project>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Project>()
                        .Property(x => x.Amount)
                        .HasPrecision(9, 2);

            modelBuilder.Entity<Role>()
                        .Map<Role>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Role>()
                        .Property(x => x.Id)
                        .HasMaxLength(5);

            modelBuilder.Entity<Role>()
                        .Property(x => x.Hrate)
                        .HasPrecision(4, 2);

            modelBuilder.Entity<Role>()
                        .Property(x => x.Mrate)
                        .HasPrecision(7, 2);

            modelBuilder.Entity<Task>()
                        .Map<Task>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Task>()
                        .Property(x => x.Hours)
                        .HasPrecision(4, 2);

            modelBuilder.Entity<Team>()
                        .Map<Team>(x => { x.Requires("Deleted").HasValue(false); })
                        .Ignore(x => x.Deleted);

            modelBuilder.Entity<Team>()
                        .Property(x => x.Id)
                        .HasMaxLength(5);
        }
       
        public override int SaveChanges()
        {
            EntitySetBase setBase;
            string tableName, primaryKeyName;

            foreach (var entry in ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted))
            {
                setBase = GetEntitySet(entry.Entity.GetType());
                tableName = (string)setBase.MetadataProperties["Table"].Value;
                primaryKeyName = setBase.ElementType.KeyMembers[0].Name;
                Database.ExecuteSqlCommand($"UPDATE {tableName} SET Deleted=1 WHERE {primaryKeyName}='{entry.OriginalValues[primaryKeyName]}'");
                entry.State = EntityState.Unchanged;
            }
            return base.SaveChanges();
        }

        private EntitySetBase GetEntitySet(Type type)
        {
            ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;
            string typeName = ObjectContext.GetObjectType(type).Name;
            var es = octx.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)
                         .GetItems<EntityContainer>()
                         .SelectMany(c => c.BaseEntitySets.Where(e => e.Name == typeName))
                         .FirstOrDefault();
            if (es == null) throw new ArgumentException("Entity type not found in GetTableName", typeName);
            return es;
        } 

    }
}
