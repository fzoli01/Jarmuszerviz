using Jarmuszerviz.Database;
using Jarmuszerviz.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Jarmuszerviz.Controllers
{
    [RoutePrefix("api/jarmuvek")]
    public class JarmuvekController : ApiController
    {
        private readonly JarmuSzervizContext ctx;

        public JarmuvekController(JarmuSzervizContext context)
        {
            ctx = context;
        }

        public JarmuvekController() : this(new JarmuSzervizContext()) { }


        // GET api/jarmuvek
        public IHttpActionResult Get()
        {
            var jarmuvek = ctx.Jarmuvek.Include(j => j.Ugyfelek).ToList();
            return Ok(jarmuvek);
        }

        // GET api/jarmuvek/{alvazszam}
        public IHttpActionResult Get(string id)
        {
            var jarmu = ctx.Jarmuvek
                .Include(j => j.Ugyfelek)
                .FirstOrDefault(j => j.Alvazszam == id);

            if (jarmu == null)
            {
                return NotFound();
            }
            return Ok(jarmu);
        }

        // POST api/jarmuvek
        public IHttpActionResult Post([FromBody] Jarmuvek ujJarmu)
        {
            if (ujJarmu == null || string.IsNullOrEmpty(ujJarmu.Alvazszam))
            {
                return BadRequest("Alvázszám kötelezõ.");
            }

            ctx.Jarmuvek.Add(ujJarmu);
            ctx.SaveChanges();
            return Created($"api/jarmuvek/{ujJarmu.Alvazszam}", ujJarmu);
        }

        // PUT api/jarmuvek/{alvazszam}
        public IHttpActionResult Put(string id, [FromBody] Jarmuvek frissitettJarmu)
        {
            if (frissitettJarmu == null || frissitettJarmu.Alvazszam != id)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            var jarmu = ctx.Jarmuvek.Find(id);
            if (jarmu == null)
            {
                return NotFound();
            }

            jarmu.Marka = frissitettJarmu.Marka;
            jarmu.Tipus = frissitettJarmu.Tipus;
            jarmu.Evjarat = frissitettJarmu.Evjarat;
            jarmu.UgyfelID = frissitettJarmu.UgyfelID;

            ctx.Entry(jarmu).State = EntityState.Modified;
            ctx.SaveChanges();
            return Ok(jarmu);
        }

        // DELETE api/jarmuvek/{alvazszam}
        public IHttpActionResult Delete(string id)
        {
            var jarmu = ctx.Jarmuvek.Find(id);
            if (jarmu == null)
            {
                return NotFound();
            }

            ctx.Jarmuvek.Remove(jarmu);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}