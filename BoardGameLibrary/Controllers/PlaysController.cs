using BoardGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BoardGameLibrary.Controllers
{
    public class PlaysController : ApiController
    {
        private BoardGameLibraryDatabaseEntities db = new BoardGameLibraryDatabaseEntities();

        /// <summary>
        /// Returns all of the plays
        /// GET api/plays
        /// The Method maps to GET because it starts with Get. The method name format is Get[any name here]
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.DTO.Play> GetPlays()
        {
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(db.Plays);
        }

        /// <summary>
        ///  GET: api/games/{gameId:int}/plays
        ///  Get all the plays linked to a specific game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [Route("api/games/{gameId:int}/plays")]
        public IQueryable<Models.DTO.Play> GetPlaysForGame(int gameId)
        {
            //Find the plays for this game
            var relevantPlays = db.Plays.Where(p => p.GameId == gameId);
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(relevantPlays).OrderByDescending(r => r.Date); 
        }

       
        /// <summary>
        /// GET: api/users/{userId:int}/plays
        /// Get all of the plays where one of the players was a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("api/users/{userId:int}/plays")]
        public IQueryable<Models.DTO.Play> GetPlaysForUser(int userId)
        {
            //Find the plays which involve this user
            var relevantPlays = db.Plays.Where(p => p.Players.Any(u=>u.Id == userId));
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(relevantPlays).OrderByDescending(r => r.Date);
        }

        /// <summary>
        /// GET: api/users/plays/?userId=1&userId=2
        /// Get all of the plays which contain all of the provided user ids.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("api/users/plays/userId")]
        public IQueryable<Models.DTO.Play> GetPlaysForUsers([FromUri] int[] userId)
        {
            //Find the plays which involve all of the users provided
            var relevantPlays = db.Plays.Where(p => userId.All(u => p.Players.Select(s=>s.Id).Contains(u)));
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(relevantPlays).OrderByDescending(r => r.Date); 
        }

        /// <summary>
        ///  GET: api/plays/5
        ///  Get a specific play using the Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Models.DTO.Play))]
        public async Task<IHttpActionResult> GetPlay(int id)
        {
            Play play = await db.Plays.FindAsync(id);
            if (play == null)
            {
                return NotFound();
            }

            return Ok(new Models.DTO.Play(play));
        }

        /// <summary>
        /// Record a new play by passing in the play object
        /// POST api/plays
        /// </summary>
        /// <param name="play"></param>
        /// <returns></returns>
        [HttpPost] //Define this method as a POST
        public async Task<IHttpActionResult> RecordNewPlay(Play play)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plays.Add(play);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = play.Id }, new Models.DTO.Play(play));
        }

        /// <summary>
        /// Edit a recorded play by passing in the id of the play and the play object with new values
        /// PUT api/plays/1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="play"></param>
        /// <returns></returns>
        [HttpPut] //Define this method as a PUT
        public async Task<IHttpActionResult> EditPlay(int id, Play play)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != play.Id)
            {
                return BadRequest();
            }

            db.Entry(play).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes a game with the specified id
        /// DELETE api/plays/1
        /// The method maps to DELETE because it starts with Delete. The method name format is Delete[Any name here]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> DeletePlay(int id)
        {
            Play play = await db.Plays.FindAsync(id);
            if (play == null)
            {
                return NotFound();
            }
            //Remove all comments if we are deleting the play
            db.Comments.RemoveRange(play.Comments);
            db.Plays.Remove(play);
            await db.SaveChangesAsync();

            return Ok(new Models.DTO.Play(play));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
