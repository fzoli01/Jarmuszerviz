using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarmuszerviz.Models
{
    public class Alkatreszek
    {
        [Key]
        public int AlkatreszID { get; set; }
        [Required]
        public string AlkatreszNev { get; set; }
        public string Cikkszam { get; set; }
        public decimal? Ar { get; set; }
        

        public Alkatreszek() { }
        public Alkatreszek(string alkatresznev, decimal? ar, string cikkszam)
        {
            AlkatreszNev = alkatresznev;
            Ar = ar;
            Cikkszam = cikkszam;
        }

    }
}