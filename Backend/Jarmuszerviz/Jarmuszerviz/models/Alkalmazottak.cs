using Jarmuszerviz.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jarmuszerviz.Models
{

        public class Alkalmazottak
        {
            [Key]
            public int AlkalmazottID { get; set; }

            [Required]
            public byte[] Jelszo { get; set; } // Salt

            [Required]
            public byte[] Jelszo_Hash { get; set; } // Hash

            public string Rang { get; set; }

            [Required]
            public string Nev { get; set; }


            public string Telefonszam { get; set; }
            public string Email { get; set; }


            public Alkalmazottak() { }

            public Alkalmazottak(string nev, string jelszo, string rang = null, string telefonszam = null, string email = null)
            {
                Nev = nev;
                Rang = rang;
                Telefonszam = telefonszam;
                Email = email;

                ApiValidator.CreatePasswordHash(jelszo, out byte[] hash, out byte[] salt);
                Jelszo_Hash = hash;
                Jelszo = salt;
            }
        
    }
}