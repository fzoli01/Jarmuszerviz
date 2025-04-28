using Jarmuszerviz.Database;
using Jarmuszerviz.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Jarmuszerviz.Controllers
{
    [RoutePrefix("api/alkatreszek")]
    public class AlkatreszekController : ApiController
    {
        private readonly JarmuSzervizContext ctx;

        public AlkatreszekController(JarmuSzervizContext context)
        {
            ctx = context;
        }

        public AlkatreszekController() : this(new JarmuSzervizContext()) { }

        // GET api/alkatreszek
        public IHttpActionResult Get()
        {
            return Ok(ctx.Alkatreszek.ToList());
        }

        // GET api/alkatreszek/5
        public IHttpActionResult Get(int id)
        {
            var alkatresz = ctx.Alkatreszek.FirstOrDefault(a => a.AlkatreszID == id);
            if (alkatresz == null)
            {
                return NotFound();
            }
            return Ok(alkatresz);
        }

        // POST api/alkatreszek
        public IHttpActionResult Post([FromBody] Alkatreszek ujAlkatresz)
        {
            if (ujAlkatresz == null || string.IsNullOrEmpty(ujAlkatresz.AlkatreszNev))
            {
                return BadRequest("Név kötelezõ.");
            }

            ctx.Alkatreszek.Add(ujAlkatresz);
            ctx.SaveChanges();
            return Created($"api/alkatreszek/{ujAlkatresz.AlkatreszID}", ujAlkatresz);
        }

        // PUT api/alkatreszek/5
        public IHttpActionResult Put(int id, [FromBody] Alkatreszek frissitettAlkatresz)
        {
            if (frissitettAlkatresz == null || frissitettAlkatresz.AlkatreszID != id)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            var alkatresz = ctx.Alkatreszek.Find(id);
            if (alkatresz == null)
            {
                return NotFound();
            }

            alkatresz.AlkatreszNev = frissitettAlkatresz.AlkatreszNev;
            alkatresz.Cikkszam = frissitettAlkatresz.Cikkszam;
            alkatresz.Ar = frissitettAlkatresz.Ar;

            ctx.Entry(alkatresz).State = EntityState.Modified;
            ctx.SaveChanges();
            return Ok(alkatresz);
        }

        // DELETE api/alkatreszek/5
        public IHttpActionResult Delete(int id)
        {
            var alkatresz = ctx.Alkatreszek.Find(id);
            if (alkatresz == null)
            {
                return NotFound();
            }

            ctx.Alkatreszek.Remove(alkatresz);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}