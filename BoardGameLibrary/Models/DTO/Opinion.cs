using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    public class Opinion
    {
        public int Id { get; internal set; }
        [Required]
        public int UserId { get; internal set; }
        [Required]
        public int GameId { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as 0
        public int? Rating { get; set; }
        [StringLength(500, ErrorMessage ="Details cannot exceed 500 characters")]
        public string Details { get; set; }

        public string UserName { get; set; }
        public string GameName { get; set; }

        public Opinion(Models.UserOpinion opinion)
        {
            this.Id = opinion.Id;
            this.UserId = opinion.UserId;
            this.GameId = opinion.GameId;
            this.Rating = opinion.Rating;
            this.UserName = opinion.User.Name;
            this.GameName = opinion.Game.Name;
            this.Details = opinion.Details;
        }
    }
}
