using LsCostume_GUI.Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsCostume_GUI.Models
{
    public class Javitas
    {
        public int JavitasID { get; set; }
        public string JarmuID { get; set; }
        public int AlkalmazottID { get; set; }
        public string Datum { get; set; }
        public string Leiras { get; set; }
        public double Koltseg { get; set; }
        public string Elkeszult {  get; set; }
        public Jarmuvek Jarmu { get; set; }
        public Alkalmazottak Alkalmazott { get; set; }
        public List<FelhasznaltAlkatresz> FelhasznaltAlkatreszek { get; set; }

        public Javitas(bool kesz, DateTime date)
        {
            
            if (kesz) Elkeszult = "Kész";
            else Elkeszult = "Folyamatban";
            Datum = date.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
