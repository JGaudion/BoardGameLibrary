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
    public class KeywordsController : ApiController
    {
        private BoardGameLibraryDatabaseEntities db = new BoardGameLibraryDatabaseEntities();

        //POST: api/games/1/keywords
        [Route("api/games/{gameId:int}/keywords", Name="AddKeywordToGame")]
        [HttpPost]
        public async Task<IHttpActionResult> AddKeywordToGame(int gameId, Keyword keyword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Look to see if the game exists
            var game = db.Games.Find(gameId);
            if (game == null)
            {
                return NotFound();
            }
            //Look to see if this keyword already exists
            if(keyword.Id != 0)
            {
                var foundKeyword = db.Keywords.Find(keyword.Id);
                if(foundKeyword != null)
                {
                    game.Keywords.Add(foundKeyword);
                    keyword = foundKeyword;
                }
            }
            else //New keyword or no id supplied
            {
                //Check to see if the name is unique by looking for any keyword with the same name (ignoring case)
                var existingName = db.Keywords.Where(k => String.Equals(k.Name, keyword.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if(existingName != null)//Ie we found a match
                {
                    game.Keywords.Add(existingName);
                    keyword = existingName;
                }
                else
                {
                    game.Keywords.Add(keyword);
                }
            }
                       

            await db.SaveChangesAsync();

            return CreatedAtRoute("AddKeywordToGame", new { id = keyword.Id }, keyword);
        }

        //DELETE: api/games/1/keywords/25
        [Route("api/games/{gameId:int}/keywords/{keywordId:int}")]
        [ResponseType(typeof(void))]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveKeywordFromGame(int gameId, int keywordId)
        {
            Game game = await db.Games.FindAsync(gameId);
            if (game == null)
            {
                return NotFound();
            }
            var keyword = game.Keywords.Where(k => k.Id == keywordId).FirstOrDefault();
            if (keyword == null)
            {
                return NotFound();
            }
            game.Keywords.Remove(keyword);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }
             
        //GET: api/keywords
        public IQueryable<Keyword> GetAllKeywords()
        {
            return db.Keywords;
        }

        // GET: api/Keywords/5
        [ResponseType(typeof(Keyword))]
        public async Task<IHttpActionResult> GetKeyword(int id)
        {
            Keyword keyword = await db.Keywords.FindAsync(id);
            if (keyword == null)
            {
                return NotFound();
            }

            return Ok(keyword);
        }

        //GET: api/games/1/keywords
        [Route("api/games/{gameId:int}/keywords")]
        public IQueryable<Keyword> GetAllKeywordsForGame(int gameId)
        {
            var game = db.Games.Find(gameId);
            if (game == null)
            {
                //I don't want to throw any errors here, so I just return an empty result.
                return Enumerable.Empty<Keyword>().AsQueryable();
            }
            else
            {
                return game.Keywords.AsQueryable();
            }
        }

        //GET: api/games/keywords/123
        [Route("api/games/keywords/{keywordId:int}")]
        public IQueryable<Models.DTO.Game> GetAllGamesWithKeyword(int keywordId)
        {
            //Search for all games which are linked to this keyword
            var games = db.Games.Where(g => g.Keywords.Any(k => k.Id == keywordId));
            return Models.DTO.Conversions.ConvertFromEntities(games).OrderBy(g=>g.Name);
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
