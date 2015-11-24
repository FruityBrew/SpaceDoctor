using System.Data.Entity;
using SpaceDoctor.Model;

namespace SpaceDoctor.DAL
{
    public class XDBContext : DbContext
    {
        public XDBContext(string connection) : base(connection) 
        {
            this.Configuration.LazyLoadingEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<XExam>().Property(e => e.Date).HasColumnType("datetime2");

        }

    }
}