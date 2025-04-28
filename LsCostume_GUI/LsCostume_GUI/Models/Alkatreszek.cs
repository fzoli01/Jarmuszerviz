using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsCostume_GUI.Models
{
    public  class Alkatreszek
    {
        public int AlkatreszID { get; set; }
        public string AlkatreszNev { get; set; }
        public string Cikkszam { get; set; }
        public double Ar { get; set; }


    }
}
