using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;



namespace SpaceDoctor.Model
{
    public class XDBContext : DbContext
    {
        public XDBContext() : base("SpaceDoctorDB") 
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<XClient> Clients { get; set; }
        public DbSet<XExam> Exams { get; set; }
        public DbSet<XParam> Parameters { get; set; }
        public DbSet<XParamsType> ParamsTypes { get; set; }
        public DbSet<XExamsType> ExamsType { get; set; }
        public DbSet<XDragPlan> DragPlans { get; set; }
        public DbSet<XDragKit> DragKits { get; set; }
        public DbSet<XDrag> Drags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}