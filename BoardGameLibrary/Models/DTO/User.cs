using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    public class User
    {
        public int Id { get; internal set; }
        [Required]
        [StringLength(100, ErrorMessage = "User name cannot exceed 100 characters")]
        public string Name { get; set; }
        public DateTime? LastPlayed { get; set; }
        public int NumPlays { get; set; }
        public int NumGames { get; set; }
        public int NumExpansions { get; set; }
        public int NumOpinions { get; set; }
        public int NumOpenRequests { get; set; }

        public User(Models.User user)
        {
            this.Id = user.Id;
            this.LastPlayed = user.LastPlayed;
            this.Name = user.Name;
            this.NumPlays = user.Plays.Count();
            this.NumOpinions = user.UserOpinions.Count();
            this.NumGames = user.Games.Count();
            this.NumExpansions = user.Expansions.Count();
            this.NumOpenRequests = user.Requests.Where(r => !r.Date.HasValue || r.Date.Value.Date >= DateTime.Today).Count();
        }
    }
}
