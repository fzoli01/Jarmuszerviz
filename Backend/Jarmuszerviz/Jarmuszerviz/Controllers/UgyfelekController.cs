using Jarmuszerviz.Database;
using Jarmuszerviz.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Jarmuszerviz.Controllers
{
    [RoutePrefix("api/ugyfelek")]
    public class UgyfelekController : ApiController
    {
        private readonly JarmuSzervizContext ctx;

        public UgyfelekController(JarmuSzervizContext context)
        {
            ctx = context;
        }

        public UgyfelekController() : this(new JarmuSzervizContext()) { }


        // GET api/ugyfelek
        public IHttpActionResult Get()
        {
            return Ok(ctx.Ugyfelek.ToList());
        }

        // GET api/ugyfelek/5
        public IHttpActionResult Get(int id)
        {
            var ugyfel = ctx.Ugyfelek.FirstOrDefault(u => u.UgyfelID == id);
            if (ugyfel == null)
            {
                return NotFound();
            }
            return Ok(ugyfel);
        }

        // POST api/ugyfelek
        public IHttpActionResult Post([FromBody] Ugyfelek ujUgyfel)
        {
            if (ujUgyfel == null || string.IsNullOrEmpty(ujUgyfel.Nev))
            {
                return BadRequest("Név kötelezõ.");
            }

            ctx.Ugyfelek.Add(ujUgyfel);
            ctx.SaveChanges();
            return Created($"api/ugyfelek/{ujUgyfel.UgyfelID}", ujUgyfel);
        }

        // PUT api/ugyfelek/5
        public IHttpActionResult Put(int id, [FromBody] Ugyfelek frissitettUgyfel)
        {
            if (frissitettUgyfel == null || frissitettUgyfel.UgyfelID != id)
            {
                return BadRequest("Érvénytelen adatok.");
            }

            var ugyfel = ctx.Ugyfelek.Find(id);
            if (ugyfel == null)
            {
                return NotFound();
            }

            ugyfel.Nev = frissitettUgyfel.Nev;
            ugyfel.Telefonszam = frissitettUgyfel.Telefonszam;
            ugyfel.Email = frissitettUgyfel.Email;
            ugyfel.Cim = frissitettUgyfel.Cim;

            ctx.Entry(ugyfel).State = EntityState.Modified;
            ctx.SaveChanges();
            return Ok(ugyfel);
        }

        // DELETE api/ugyfelek/5
        public IHttpActionResult Delete(int id)
        {
            var ugyfel = ctx.Ugyfelek.Find(id);
            if (ugyfel == null)
            {
                return NotFound();
            }

            ctx.Ugyfelek.Remove(ugyfel);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}