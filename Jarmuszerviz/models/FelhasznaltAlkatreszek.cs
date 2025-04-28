using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarmuszerviz.Models
{

    public class FelhasznaltAlkatreszek
    {
        [Key, Column(Order = 1)] 
        [ForeignKey("Javitasok")]
        public int JavitasID { get; set; }

        [Key, Column(Order = 2)] 
        [ForeignKey("Alkatreszek")] 
        public int AlkatreszID { get; set; }

        public int Mennyiseg { get; set; }

        public virtual Javitasok Javitasok { get; set; }
        public virtual Alkatreszek Alkatreszek { get; set; }

        public FelhasznaltAlkatreszek() { }
        public FelhasznaltAlkatreszek(int javitasID, int alkatreszID, int mennyiseg)
        {
            JavitasID = javitasID;
            AlkatreszID = alkatreszID;
            Mennyiseg = mennyiseg;
        }
    }
}