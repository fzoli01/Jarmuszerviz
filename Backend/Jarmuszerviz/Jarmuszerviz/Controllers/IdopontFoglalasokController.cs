using Jarmuszerviz.Database;
using Jarmuszerviz.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Jarmuszerviz.Controllers
{
    [RoutePrefix("api/idopont-foglalasok")]
    public class IdopontFoglalasokController : ApiController
    {
        private readonly JarmuSzervizContext ctx;

        public IdopontFoglalasokController(JarmuSzervizContext context)
        {
            ctx = context;
        }

        public IdopontFoglalasokController() : this(new JarmuSzervizContext()) { }


        // GET api/idopont-foglalasok
        public IHttpActionResult Get()
        {
            var foglalasok = ctx.IdopontFoglalasok
                .Include(i => i.Ugyfelek)
                .ToList();
            return Ok(foglalasok);
        }

        // GET api/idopont-foglalasok/5
        public IHttpActionResult Get(int id)
        {
            var foglalas = ctx.IdopontFoglalasok
                .Include(i => i.Ugyfelek)
                .FirstOrDefault(i => i.IdoPontID == id);

            if (foglalas == null)
            {
                return NotFound();
            }
            return Ok(foglalas);
        }

        // POST api/idopont-foglalasok
        public IHttpActionResult Post([FromBody] IdopontFoglalasok ujFoglalas)
        {
            if (ujFoglalas == null || ujFoglalas.UgyfelID <= 0)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            ctx.IdopontFoglalasok.Add(ujFoglalas);
            ctx.SaveChanges();
            return Created($"api/idopont-foglalasok/{ujFoglalas.IdoPontID}", ujFoglalas);
        }

        // PUT api/idopont-foglalasok/5
        public IHttpActionResult Put(int id, [FromBody] IdopontFoglalasok frissitettFoglalas)
        {
            if (frissitettFoglalas == null || frissitettFoglalas.IdoPontID != id)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            var foglalas = ctx.IdopontFoglalasok.Find(id);
            if (foglalas == null)
            {
                return NotFound();
            }

            foglalas.UgyfelID = frissitettFoglalas.UgyfelID;
            foglalas.Datum = frissitettFoglalas.Datum;
            foglalas.Megjegyzes = frissitettFoglalas.Megjegyzes;

            ctx.Entry(foglalas).State = EntityState.Modified;
            ctx.SaveChanges();
            return Ok(foglalas);
        }

        // DELETE api/idopont-foglalasok/5
        public IHttpActionResult Delete(int id)
        {
            var foglalas = ctx.IdopontFoglalasok.Find(id);
            if (foglalas == null)
            {
                return NotFound();
            }

            ctx.IdopontFoglalasok.Remove(foglalas);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}