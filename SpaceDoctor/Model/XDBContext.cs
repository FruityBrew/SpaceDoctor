using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;



namespace SpaceDoctor.Models
{
    public class XDBContext : DbContext
    {
        public XDBContext() : base("Doctor") { }

        public DbSet<XClient> clients { get; set; }
        public DbSet<XExam> exams { get; set; }
        public DbSet<XParams> parameters { get; set; }

    }
}