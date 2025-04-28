
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
        public virtual DbSet<Ugyfelek> Ugyfelek { get; set; }
        public  virtual DbSet<Alkalmazottak> Alkalmazottak { get; set; }
        public virtual DbSet<Jarmuvek> Jarmuvek { get; set; }
        public virtual DbSet<Javitasok> Javitasok { get; set; }
        public  virtual DbSet<Alkatreszek> Alkatreszek { get; set; }
        public  virtual DbSet<FelhasznaltAlkatreszek> FelhasznaltAlkatreszek { get; set; }
        public  virtual DbSet<IdopontFoglalasok> IdopontFoglalasok { get; set; }

        public JarmuSzervizContext() : base("name=JarmuSzervizContext") { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}