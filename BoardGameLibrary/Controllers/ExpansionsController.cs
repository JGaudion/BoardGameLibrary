using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BoardGameLibrary.Models;

namespace BoardGameLibrary.Controllers
{
    public class ExpansionsController : ApiController
    {
        private BoardGameLibraryDatabaseEntities db = new BoardGameLibraryDatabaseEntities();

        // GET: api/games/{gameId:int}/expansions
        [Route("api/games/{gameId:int}/expansions")]
        public IQueryable<Models.DTO.Expansion> GetExpansionsForGame(int gameId)
        {
            //Find the expansions for this game
            var relevantExpansions = db.Expansions.Where(e=>e.ParentGameId == gameId);
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(relevantExpansions).OrderBy(e=>e.Name);
        }

        // GET: api/Expansions/5
        [ResponseType(typeof(Models.DTO.Expansion))]
        public async Task<IHttpActionResult> GetExpansion(int id)
        {
            Expansion expansion = await db.Expansions.FindAsync(id);
            if (expansion == null)
            {
                return NotFound();
            }

            return Ok(new Models.DTO.Expansion(expansion));
        }

        // PUT: api/Expansions/5
        [ResponseType(typeof(void))]        
        public async Task<IHttpActionResult> PutExpansion(int id, Expansion expansion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expansion.Id)
            {
                return BadRequest();
            }

            db.Entry(expansion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpansionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Games/{gameId}/Expansions
        [ResponseType(typeof(Models.DTO.Expansion))]
        [Route("api/games/{gameId:int}/expansions", Name = "NewExpansion")]
        public async Task<IHttpActionResult> PostExpansion(int gameId, Expansion expansion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var parentGame = db.Games.Find(gameId);
            if(parentGame == null)
            {
                return NotFound();
            }

            parentGame.Expansions.Add(expansion);

            await db.SaveChangesAsync();

            return CreatedAtRoute("NewExpansion", new { id = expansion.Id }, new Models.DTO.Expansion(expansion));
        }

        // DELETE: api/Expansions/5
        [ResponseType(typeof(Expansion))]
        public async Task<IHttpActionResult> DeleteExpansion(int id)
        {
            Expansion expansion = await db.Expansions.FindAsync(id);
            if (expansion == null)
            {
                return NotFound();
            }

            db.Expansions.Remove(expansion);
            await db.SaveChangesAsync();

            return Ok(new Models.DTO.Expansion(expansion));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExpansionExists(int id)
        {
            return db.Expansions.Count(e => e.Id == id) > 0;
        }
    }
}