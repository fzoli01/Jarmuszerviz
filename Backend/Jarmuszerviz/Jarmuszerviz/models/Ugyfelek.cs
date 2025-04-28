using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarmuszerviz.Models
{
    public class Ugyfelek
    {
        [Key]
        public int UgyfelID { get; set; }
        [Required]
        public string Nev { get; set; }
        public string Telefonszam { get; set; }
        public string Email { get; set; }
        public string Cim { get; set; }

        public Ugyfelek() { }
        public Ugyfelek(string nev, string telefonszam, string email, string cim)
        {
            Nev = nev;
            Telefonszam = telefonszam;
            Email = email;
            Cim = cim;
        }

    }
}