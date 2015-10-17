using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;



namespace SpaceDoctor.Model
{
    public class XDBContext : DbContext
    {
        public XDBContext() : base("SpaceDoctorDB") { }

        public DbSet<XClient> Clients { get; set; }
        public DbSet<XExam> Exams { get; set; }
        public DbSet<XParam> Parameters { get; set; }
        public DbSet<XParamsTypes> ParamsTypes { get; set; }

    }
}