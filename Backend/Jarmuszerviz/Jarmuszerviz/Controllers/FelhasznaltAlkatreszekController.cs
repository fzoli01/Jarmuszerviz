using Jarmuszerviz.Database;
using Jarmuszerviz.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Jarmuszerviz.Controllers
{
    [RoutePrefix("api/felhasznalt-alkatreszek")]
    public class FelhasznaltAlkatreszekController : ApiController
    {
        private readonly JarmuSzervizContext ctx;

        public FelhasznaltAlkatreszekController(JarmuSzervizContext context)
        {
            ctx = context;
        }

        public FelhasznaltAlkatreszekController() : this(new JarmuSzervizContext()) { }


        // GET api/felhasznalt-alkatreszek
        public IHttpActionResult Get()
        {
            var felhasznaltAlkatreszek = ctx.FelhasznaltAlkatreszek
                .Include(f => f.Javitasok)
                .Include(f => f.Alkatreszek)
                .ToList();
            return Ok(felhasznaltAlkatreszek);
        }

        // GET api/felhasznalt-alkatreszek/javitas/5/alkatresz/10
        [Route("javitas/{javitasId}/alkatresz/{alkatreszId}")]
        public IHttpActionResult Get(int javitasId, int alkatreszId)
        {
            var felhasznaltAlkatresz = ctx.FelhasznaltAlkatreszek
                .Include(f => f.Javitasok)
                .Include(f => f.Alkatreszek)
                .FirstOrDefault(f => f.JavitasID == javitasId && f.AlkatreszID == alkatreszId);

            if (felhasznaltAlkatresz == null)
            {
                return NotFound();
            }
            return Ok(felhasznaltAlkatresz);
        }

        // POST api/felhasznalt-alkatreszek
        public IHttpActionResult Post([FromBody] FelhasznaltAlkatreszek ujFelhasznaltAlkatresz)
        {
            if (ujFelhasznaltAlkatresz == null)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            ctx.FelhasznaltAlkatreszek.Add(ujFelhasznaltAlkatresz);
            ctx.SaveChanges();
            return Created($"api/felhasznalt-alkatreszek/javitas/{ujFelhasznaltAlkatresz.JavitasID}/alkatresz/{ujFelhasznaltAlkatresz.AlkatreszID}", ujFelhasznaltAlkatresz);
        }

        // PUT api/felhasznalt-alkatreszek/javitas/5/alkatresz/10
        [Route("javitas/{javitasId}/alkatresz/{alkatreszId}")]
        public IHttpActionResult Put(int javitasId, int alkatreszId, [FromBody] FelhasznaltAlkatreszek frissitettFelhasznaltAlkatresz)
        {
            if (frissitettFelhasznaltAlkatresz == null ||
                frissitettFelhasznaltAlkatresz.JavitasID != javitasId ||
                frissitettFelhasznaltAlkatresz.AlkatreszID != alkatreszId)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            var felhasznaltAlkatresz = ctx.FelhasznaltAlkatreszek
                .FirstOrDefault(f => f.JavitasID == javitasId && f.AlkatreszID == alkatreszId);

            if (felhasznaltAlkatresz == null)
            {
                return NotFound();
            }

            felhasznaltAlkatresz.Mennyiseg = frissitettFelhasznaltAlkatresz.Mennyiseg;

            ctx.Entry(felhasznaltAlkatresz).State = EntityState.Modified;
            ctx.SaveChanges();
            return Ok(felhasznaltAlkatresz);
        }

        // DELETE api/felhasznalt-alkatreszek/javitas/5/alkatresz/10
        [Route("javitas/{javitasId}/alkatresz/{alkatreszId}")]
        public IHttpActionResult Delete(int javitasId, int alkatreszId)
        {
            var felhasznaltAlkatresz = ctx.FelhasznaltAlkatreszek
                .FirstOrDefault(f => f.JavitasID == javitasId && f.AlkatreszID == alkatreszId);

            if (felhasznaltAlkatresz == null)
            {
                return NotFound();
            }

            ctx.FelhasznaltAlkatreszek.Remove(felhasznaltAlkatresz);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}