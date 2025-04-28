using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarmuszerviz.Models
{


    public class IdopontFoglalasok
    {
        [Key]
        public int IdoPontID { get; set; }

        [ForeignKey("Ugyfelek")]
        public int UgyfelID{ get; set; } 
        public virtual Ugyfelek Ugyfelek { get; set; }
        public DateTime? Datum { get; set; }
        public string Megjegyzes { get; set; }




        public IdopontFoglalasok() { }
        public IdopontFoglalasok(int ugyfelid, DateTime? datum, string megjegyzes)
        {
            UgyfelID = ugyfelid;
            Datum = datum;
            Megjegyzes = megjegyzes;
        }
    }
}