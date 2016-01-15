using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    /// <summary>
    /// This class is similar to Game in the Entities (BoardGameLibraryModel.edmx), but this exists as a way of passing
    /// to the client only the parts that we want to show on the client, rather than the client having all the navigation 
    /// links from the Entity Framework relationships.
    /// </summary>
    public class Game
    {
        public int Id { get; internal set; }
        public string OwnedBy { get; internal set; }
        public bool AtWork { get; internal set; }
        public int? MinNumPlayers { get; internal set; }
        public int? MaxNumPlayers { get; internal set; }
        public double PlayTimeHours { get; internal set; }
        public string DifficultyToLearn { get; internal set; }

        [Required]
        [StringLength(200, ErrorMessage = "Game name cannot exceed 200 characters")]
        public string Name { get; internal set; }

        [Required]
        public string ParentGame { get; internal set; }

        [StringLength(500, ErrorMessage = "Game description cannot exceed 500 characters")]
        public string Description { get; internal set; }

        //Better to do separately?
        public IQueryable<Comment> Comments { get; internal set; }

        /// <summary>
        /// This constructor takes a Game object from the Model and converts it into this DTO Game object
        /// </summary>
        /// <param name="game"></param>
        public Game(Models.Game game)
        {
            this.Id = game.Id;
            this.Name = game.Name;
            this.Description = game.Description;
            this.AtWork = game.AtWork;
            this.MaxNumPlayers = game.MaxNumPlayers;
            this.MinNumPlayers = game.MinNumPlayers;
            this.DifficultyToLearn = game.DifficultyToLearn;
            if (game.Owner != null)
            {
                this.OwnedBy = game.Owner.Name;
            }
            else
            {
                this.OwnedBy = "";
            }
            //Get the list of comments
            this.Comments = Conversions.ConvertFromEntities(game.Comments);

        }
    }
}
