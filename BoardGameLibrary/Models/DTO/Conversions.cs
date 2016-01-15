using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    /// <summary>
    /// This class exists as a place to store my converters. There are more elegant ways of doing this, such as having a single method
    /// that uses generics to work on any type passed to it.
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        /// This turns a collection of Games from the model into a collection of DTO (Data Transfer Object) Games
        /// </summary>
        /// <param name="Games"></param>
        /// <returns></returns>
        public static IQueryable<Game> ConvertFromEntities(IEnumerable<Models.Game> Games)
        {
            List<Game> result = new List<Game>(); //List of the DTO games
            foreach (var game in Games)
            {
                result.Add(new Game(game));
            }
            return result.AsQueryable();
        }

        /// <summary>
        /// This turns a collection of Expansions from the model into a collection of DTO (Data Transfer Object) Expansions
        /// </summary>
        /// <param name="Expansions"></param>
        /// <returns></returns>
        public static IQueryable<Expansion> ConvertFromEntities(IEnumerable<Models.Expansion> Expansions)
        {
            List<Expansion> result = new List<Expansion>(); //List of the DTO expansions
            foreach (var expansion in Expansions)
            {
                result.Add(new Expansion(expansion));
            }
            return result.AsQueryable();
        }

        /// <summary>
        /// This turns a collection of Plays from the model into a collection of DTO (Data Transfer Object) Plays
        /// </summary>
        /// <param name="Plays"></param>
        /// <returns></returns>
        public static IQueryable<Play> ConvertFromEntities(IEnumerable<Models.Play> Plays)
        {
            List<Play> result = new List<Play>(); //List of the DTO plays
            foreach (var play in Plays)
            {
                result.Add(new Play(play));
            }
            return result.AsQueryable();
        }

        /// <summary>
        /// This turns a collection of Requests from the model into a collection of DTO (Data Transfer Object) Requests
        /// </summary>
        /// <param name="Plays"></param>
        /// <returns></returns>
        public static IQueryable<Request> ConvertFromEntities(IEnumerable<Models.Request> Requests)
        {
            List<Request> result = new List<Request>(); //List of the DTO requests
            foreach (var request in Requests)
            {
                result.Add(new Request(request));
            }
            return result.AsQueryable();
        }

        /// <summary>
        /// This turns a collection of Plays from the model into a collection of DTO (Data Transfer Object) Plays
        /// </summary>
        /// <param name="Plays"></param>
        /// <returns></returns>
        public static IQueryable<Opinion> ConvertFromEntities(IEnumerable<Models.UserOpinion> Opinions)
        {
            List<Opinion> result = new List<Opinion>(); //List of the DTO opinions
            foreach (var opinion in Opinions)
            {
                result.Add(new Opinion(opinion));
            }
            return result.AsQueryable();
        }

        /// <summary>
        /// This turns a collection of Comments from the model into a collection of DTO (Data Transfer Object) Comments
        /// </summary>
        /// <param name="Comments"></param>
        /// <returns></returns>
        public static IQueryable<Comment> ConvertFromEntities(IEnumerable<Models.Comment> Comments)
        {
            List<Comment> results = new List<Comment>(); //List of the DTO comments
            foreach (var comment in Comments)
            {
                results.Add(new Comment(comment));
            }
            return results.AsQueryable();
        }
    }
}
