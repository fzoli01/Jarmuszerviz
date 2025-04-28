using Jarmuszerviz.Database;
using Jarmuszerviz.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Security;
using Jarmuszerviz.Security;


namespace Jarmuszerviz.Controllers
{
    [RoutePrefix("api/alkalmazottak")]
    public class AlkalmazottakController : ApiController
    {
        private readonly JarmuSzervizContext ctx;

        public AlkalmazottakController(JarmuSzervizContext context)
        {
            ctx = context;
        }

        public AlkalmazottakController() : this(new JarmuSzervizContext()) { }
        public class AlkalmazottakRequest
        {
            public string Nev { get; set; }
            public string Jelszo { get; set; }
            public string Rang { get; set; }
            public string Telefonszam { get; set; }
            public string Email { get; set; }
        }

        // POST api/alkalmazottak
        public IHttpActionResult Post([FromBody] AlkalmazottakRequest ujAlkalmazottRequest)
        {
            if (string.IsNullOrEmpty(ujAlkalmazottRequest.Nev) ||
                string.IsNullOrEmpty(ujAlkalmazottRequest.Jelszo))
            {
                return BadRequest("Név és jelszó kötelező.");
            }

            var alkalmazott = new Alkalmazottak(
                nev: ujAlkalmazottRequest.Nev,
                jelszo: ujAlkalmazottRequest.Jelszo,
                rang: ujAlkalmazottRequest.Rang,
                telefonszam: ujAlkalmazottRequest.Telefonszam,
                email: ujAlkalmazottRequest.Email
            );

            ctx.Alkalmazottak.Add(alkalmazott);
            ctx.SaveChanges();

            return Created($"api/alkalmazottak/{alkalmazott.AlkalmazottID}", alkalmazott);
        }


        public IHttpActionResult Put(int id, [FromBody] AlkalmazottakRequest frissitettAlkalmazott)
        {
            var alkalmazott = ctx.Alkalmazottak.FirstOrDefault(a => a.AlkalmazottID == id);
            if (alkalmazott == null)
                return NotFound();

            if (!string.IsNullOrEmpty(frissitettAlkalmazott.Jelszo))
            {
                ApiValidator.CreatePasswordHash(frissitettAlkalmazott.Jelszo,
                    out byte[] hash,
                    out byte[] salt
                );
                alkalmazott.Jelszo_Hash = hash;
                alkalmazott.Jelszo = salt;
            }

            alkalmazott.Nev = frissitettAlkalmazott.Nev;
            alkalmazott.Rang = frissitettAlkalmazott.Rang;
            alkalmazott.Telefonszam = frissitettAlkalmazott.Telefonszam;
            alkalmazott.Email = frissitettAlkalmazott.Email;

            ctx.SaveChanges();
            return Ok(alkalmazott);
        }


        // GET api/alkalmazottak
        public IHttpActionResult Get()
        {
            return Ok(ctx.Alkalmazottak.ToList());
        }

        // GET api/alkalmazottak/5
        public IHttpActionResult Get(int id)
        {
            var alkalmazott = ctx.Alkalmazottak.FirstOrDefault(a => a.AlkalmazottID == id);
            if (alkalmazott == null)
            {
                return NotFound();
            }
            return Ok(alkalmazott);
        }


        // DELETE api/alkalmazottak/5
        public IHttpActionResult Delete(int id)
        {
            var alkalmazott = ctx.Alkalmazottak.FirstOrDefault(a => a.AlkalmazottID == id);
            if (alkalmazott == null)
                return NotFound();

            ctx.Alkalmazottak.Remove(alkalmazott);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}