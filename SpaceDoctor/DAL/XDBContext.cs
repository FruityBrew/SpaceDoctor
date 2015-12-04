using System.Data.Entity;
using SpaceDoctor.Model;

namespace SpaceDoctor.DAL
{
    public class XDBContext : DbContext
    {

        public DbSet<XClient> Clients
        { get; set; }
        public DbSet<XExam> Exams
        { get; set; }
        public DbSet<XParam> Parameters
        { get; set; }
        public DbSet<XParamType> ParamsTypes
        { get; set; }
        public DbSet<XExamType> ExamsType
        { get; set; }
        public DbSet<XDragPlan> DragPlans
        { get; set; }
        public DbSet<XDragKit> DragKits
        { get; set; }
        public DbSet<XDrag> Drags
        { get; set; }
        //public DbSet<XRegData> RegData
        //{ get; set; }

        public XDBContext(string connection) : base(connection) 
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public XDBContext() : base("SpaceDoctorDB")
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