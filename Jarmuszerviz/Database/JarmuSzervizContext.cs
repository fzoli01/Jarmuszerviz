
using Jarmuszerviz.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Jarmuszerviz.Database
{
    public class JarmuSzervizContext : DbContext
    {
        public DbSet<Ugyfelek> Ugyfelek { get; set; }
        public DbSet<Alkalmazottak> Alkalmazottak { get; set; }
        public DbSet<Jarmuvek> Jarmuvek { get; set; }
        public DbSet<Javitasok> Javitasok { get; set; }
        public DbSet<Alkatreszek> Alkatreszek { get; set; }
        public DbSet<FelhasznaltAlkatreszek> FelhasznaltAlkatreszek { get; set; }
        public DbSet<IdopontFoglalasok> IdopontFoglalasok { get; set; }

        public JarmuSzervizContext() : base("name=JarmuSzervizContext") { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}