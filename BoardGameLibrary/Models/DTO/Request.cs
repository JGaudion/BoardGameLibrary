using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    public class Request
    {
        public int Id { get; internal set; }
        [Required]
        public int UserId { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as the default date
        public DateTime? Date { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as 0
        public int? GameId { get; internal set; }
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; internal set; }
        public string GameName { get; internal set; }
        public string UserName { get; internal set; }

        public Request (Models.Request request)
        {
            this.Id = request.Id;
            this.UserId = request.UserId;
            this.Date = request.Date;
            this.GameId = request.GameId;
            this.Description = request.Description;
            this.GameName = request.Game != null? request.Game.Name : "";
            this.UserName = request.User != null? request.User.Name : "";

        }

    }
}
