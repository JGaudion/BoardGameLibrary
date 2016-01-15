using BoardGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BoardGameLibrary.Controllers
{
    public class GamesController : ApiController
    {
       
        ///This is the database db and points to the EntityFramework dbdb in the Models folder. We use this to call the database.
        public BoardGameLibraryDatabaseEntities db = new BoardGameLibraryDatabaseEntities();

        /// <summary>
        /// Returns all of the games
        /// GET api/games
        /// The Method maps to GET because it starts with Get. The method name format is Get[any name here]
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.DTO.Game> GetGames()
        {
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(db.Games);
        }

        /// <summary>
        /// Get a specific game using the id.
        /// GET api/games/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Models.DTO.Game))]
        public IHttpActionResult GetGame(int id) 
        {
            var game = db.Games.Where(g => g.Id == id).FirstOrDefault();
            if(game == null)
            {
                return NotFound();
            }
            var dtoGame = new Models.DTO.Game(game);
            return Ok(dtoGame);
        }

       
        /// <summary>
        /// Create a new game by passing in the game object
        /// POST api/games
        /// The method maps to POST because it starts with Post. We could explicitly set this method using the [httpPost] attribute
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Games.Add(game);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = game.Id }, new Models.DTO.Game(game));

        }
        
        /// <summary>
        /// Update a game by passing in the id of the game and the game object with the new values
        /// PUT api/games/1
        /// The method maps to PUT because it starts with Put. The method name format is Put[any name here]
        /// </summary>
        /// <param name="id"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> PutGame (int id, Game game)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != game.Id)
            {
                return BadRequest();
            }

            db.Entry(game).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            
            return StatusCode(HttpStatusCode.NoContent);
        }
       

        /// <summary>
        /// Deletes a game with the specified id
        /// DELETE api/games/1
        /// The method maps to DELETE because it starts with Delete. The method name format is Delete[Any name here]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IHttpActionResult>  DeleteGame (int id)
        {
            Game game = await db.Games.FindAsync(id);
            if(game == null)
            {
                return NotFound();
            }
            //Remove all expansions if we are deleting the game
            db.Comments.RemoveRange(game.Comments);
            db.Expansions.RemoveRange(game.Expansions);
            db.Games.Remove(game);
            await db.SaveChangesAsync();

            return Ok(new Models.DTO.Game(game));
        }
    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.Id == id) > 0;
        }

     
    }
}
