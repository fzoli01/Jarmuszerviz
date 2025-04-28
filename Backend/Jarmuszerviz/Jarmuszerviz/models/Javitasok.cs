using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarmuszerviz.Models
{
    public class Javitasok
    {
        [Key]
        public int JavitasID { get; set; }

        [ForeignKey("Jarmuvek")]
        public  string JarmuID { get; set; } 
        public virtual Jarmuvek Jarmuvek { get; set; }

        [ForeignKey("Alkalmazottak")]
        public int AlkalmazottID { get; set; }
        public virtual Alkalmazottak Alkalmazottak { get; set; }

        public DateTime? Datum { get; set; }
        public string Leiras { get; set; }
        public decimal? Koltseg { get; set; }
        public bool Elkeszult { get; set; }




        public Javitasok() { }
        public Javitasok(string jarmuID, int alkalmazottID, DateTime? datum, string leiras, decimal? koltseg,bool elkeszult)
        {
            JarmuID = jarmuID;
            AlkalmazottID = alkalmazottID;
            Datum = datum;
            Leiras = leiras;
            Koltseg = koltseg;
            Elkeszult = elkeszult;
        }
    }
}