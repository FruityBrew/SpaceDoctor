﻿using System.Data.Entity;


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
        public DbSet<XParamType> ParamsTypes { get; set; }
        public DbSet<XExamType> ExamsType { get; set; }
        public DbSet<XDragPlan> DragPlans { get; set; }
        public DbSet<XDragKit> DragKits { get; set; }
        public DbSet<XDrag> Drags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<XExam>().Property(e => e.Date).HasColumnType("datetime2");

        }

    }
}