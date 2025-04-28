using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsCostume_GUI.Models
{
    public class FelhasznaltAlkatresz
    {
        public int JavitasID { get; set; }
        public int AlkatreszID { get; set; }
        public int Mennyiseg { get; set; }

        public Javitas Javitas { get; set; }
        public Alkatreszek Alkatresz { get; set; }
    }
}
