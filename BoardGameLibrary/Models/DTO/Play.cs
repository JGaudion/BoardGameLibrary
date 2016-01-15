using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    public class Play
    {
        public int Id { get; internal set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int? GameId { get; set; }
        [StringLength(200, ErrorMessage = "Game name cannot exceed 200 characters")]
        public string GameName { get; set; }
        public int NumPlayers { get; set; }
        public IEnumerable<int> PlayerIds { get; set; }
        public IEnumerable<string> PlayerNames { get; set; }
                
        public Play(Models.Play play)
        {
            this.Id = play.Id;
            this.Date = play.Date;
            this.GameId = play.GameId;

            this.PlayerIds = play.Players.Select(p => p.Id); //Selecting just the Id from each of the players
            this.PlayerNames = play.Players.Select(p => p.Name); //Selecting just the Name from each of the players
            this.NumPlayers = play.Players.Count();

            ///This is shorthand for an "if" statement. The "?" is the "if" and the format is:
            ///[condition] ? [value if true] : [value if false]
            this.GameName = play.Game != null ? play.Game.Name : "";


        }
    }
}
