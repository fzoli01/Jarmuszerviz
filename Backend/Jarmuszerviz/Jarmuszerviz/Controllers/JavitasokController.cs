using Jarmuszerviz.Database;
using Jarmuszerviz.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Jarmuszerviz.Controllers
{
    [RoutePrefix("api/javitasok")]
    public class JavitasokController : ApiController
    {
        private readonly JarmuSzervizContext ctx;

        public JavitasokController(JarmuSzervizContext context)
        {
            ctx = context;
        }

        public JavitasokController() : this(new JarmuSzervizContext()) { }


        // GET api/javitasok
        public IHttpActionResult Get()
        {
            var javitasok = ctx.Javitasok
                .Include(j => j.Jarmuvek)
                .Include(j => j.Alkalmazottak)
                .ToList();
            return Ok(javitasok);
        }

        // GET api/javitasok/5
        public IHttpActionResult Get(int id)
        {
            var javitas = ctx.Javitasok
                .Include(j => j.Jarmuvek)
                .Include(j => j.Alkalmazottak)
                .FirstOrDefault(j => j.JavitasID == id);

            if (javitas == null)
            {
                return NotFound();
            }
            return Ok(javitas);
        }

        // POST api/javitasok
        public IHttpActionResult Post([FromBody] Javitasok ujJavitas)
        {
            if (ujJavitas == null || string.IsNullOrEmpty(ujJavitas.Leiras))
            {
                return BadRequest("Leírás kötelezõ.");
            }

            ctx.Javitasok.Add(ujJavitas);
            ctx.SaveChanges();
            return Created($"api/javitasok/{ujJavitas.JavitasID}", ujJavitas);
        }

        // PUT api/javitasok/5
        public IHttpActionResult Put(int id, [FromBody] Javitasok frissitettJavitas)
        {
            if (frissitettJavitas == null || frissitettJavitas.JavitasID != id)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            var javitas = ctx.Javitasok.Find(id);
            if (javitas == null)
            {
                return NotFound();
            }

            javitas.JarmuID = frissitettJavitas.JarmuID;
            javitas.AlkalmazottID = frissitettJavitas.AlkalmazottID;
            javitas.Datum = frissitettJavitas.Datum;
            javitas.Leiras = frissitettJavitas.Leiras;
            javitas.Koltseg = frissitettJavitas.Koltseg;
            javitas.Elkeszult = frissitettJavitas.Elkeszult;

            ctx.Entry(javitas).State = EntityState.Modified;
            ctx.SaveChanges();
            return Ok(javitas);
        }

        // DELETE api/javitasok/5
        public IHttpActionResult Delete(int id)
        {
            var javitas = ctx.Javitasok.Find(id);
            if (javitas == null)
            {
                return NotFound();
            }

            ctx.Javitasok.Remove(javitas);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}