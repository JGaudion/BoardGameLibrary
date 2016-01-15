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
    public class RequestsController : ApiController
    {
        private BoardGameLibraryDatabaseEntities db = new BoardGameLibraryDatabaseEntities();

        /// <summary>
        /// Returns all of the requests
        /// GET api/requests
        /// The Method maps to GET because it starts with Get. The method name format is Get[any name here]
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.DTO.Request> GetRequests()
        {
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(db.Requests).OrderByDescending(r => r.Date); 
        }

        /// <summary>
        ///  GET: api/games/{gameId:int}/requests
        ///  Get all the requests linked to a specific game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [Route("api/games/{gameId:int}/requests")]
        public IQueryable<Models.DTO.Request> GetRequestsForGame(int gameId)
        {
            //Find the plays for this game
            var relevantRequests = db.Requests.Where(r => r.GameId == gameId);
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(relevantRequests).OrderByDescending(r=> r.Date);
        }


        /// <summary>
        /// GET: api/users/{userId:int}/requests
        /// Get all of the requests requested by a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("api/users/{userId:int}/requests")]
        public IQueryable<Models.DTO.Request> GetRequestsForUser(int userId)
        {
            //Find the plays which involve this user
            var relevantRequests = db.Requests.Where(r => r.UserId == userId);
            //Convert into the data transfer objects (DTO)
            return Models.DTO.Conversions.ConvertFromEntities(relevantRequests).OrderByDescending(r => r.Date); 
        }
              

        /// <summary>
        ///  GET: api/requests/5
        ///  Get a specific request using the Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Models.DTO.Request))]
        public async Task<IHttpActionResult> GetRequest(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(new Models.DTO.Request(request));
        }

        /// <summary>
        /// Record a new request by passing in the request object
        /// POST api/requests
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost] //Define this method as a POST
        public async Task<IHttpActionResult> RecordNewRequest(Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Requests.Add(request);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = request.Id }, new Models.DTO.Request(request));
        }

        /// <summary>
        /// Edit a recorded request by passing in the id of the request and the request object with new values
        /// PUT api/requests/1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="play"></param>
        /// <returns></returns>
        [HttpPut] //Define this method as a PUT
        public async Task<IHttpActionResult> EditRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != request.Id)
            {
                return BadRequest();
            }

            db.Entry(request).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes a game with the specified id
        /// DELETE api/requests/1
        /// The method maps to DELETE because it starts with Delete. The method name format is Delete[Any name here]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> DeleteRequest(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            //Remove all comments if we are deleting the play
            db.Comments.RemoveRange(request.Comments);
            db.Requests.Remove(request);
            await db.SaveChangesAsync();

            return Ok(new Models.DTO.Request(request));
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
