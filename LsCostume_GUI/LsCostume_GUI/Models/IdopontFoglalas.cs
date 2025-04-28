using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsCostume_GUI.Models
{
    public class IdopontFoglalas
    {
        public int IdoPontID { get; set; }
        public string Datum { get; set; }
        public string Megjegyzes { get; set; }
        public int UgyfelId { get; set; }

        public Ugyfelek Ugyfel { get; set; }

        public IdopontFoglalas(DateTime date)
        {
            Datum = date.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
