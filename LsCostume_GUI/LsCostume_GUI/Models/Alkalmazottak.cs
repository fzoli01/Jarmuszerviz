using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsCostume_GUI.Moduls
{
    public  class Alkalmazottak
    {
        public int AlkalmazottID { get; set; }
        public byte[] Jelszo { get; set; }
        public byte[] JelszoHash { get; set; }
        public string Rang { get; set; }
        public string Nev { get; set; }
        public string Telefonszam { get; set; }
        public string Email { get; set; }

    }
}
