using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarmuszerviz.Models
{
    public class Jarmuvek
    {
        [Key]

        public string Alvazszam { get; set; }

        public string Marka { get; set; }
        public string Tipus { get; set; }
        public int? Evjarat { get; set; }

        [ForeignKey("Ugyfelek")]
        public int UgyfelID { get; set; }

        public virtual Ugyfelek Ugyfelek { get; set; }

        public Jarmuvek() { }
        public Jarmuvek(string alvazszam, string marka, string tipus, int? evjarat, int ugyfelID)
        {
            Alvazszam = alvazszam;
            Marka = marka;
            Tipus = tipus;
            Evjarat = evjarat;
            UgyfelID = ugyfelID;
        }
    }
}