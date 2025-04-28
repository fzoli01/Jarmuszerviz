using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsCostume_GUI.Models
{
    public  class Jarmuvek
    {
        public string Alvazszam { get; set; }
        public string Marka { get; set; }
        public string Tipus { get; set; }
        public int Evjarat { get; set; }
        public int UgyfelID { get; set; }
        public Ugyfelek Tulajdonos { get; set; }

    }
}
