using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    public class Comment
    {
        public int Id { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as 0
        public int UserId { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as 0
        public int? GameId { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as 0
        public int? RequestId { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as 0
        public int? PlayId { get; internal set; }
        [Required] //Setting required on a nullable value stops a missing value being interpreted as 0
        public int? UserOpinionId { get; internal set; }
        public DateTime Date { get; internal set; }
        [StringLength(500, ErrorMessage = "Details cannot exceed 500 characters")]
        public string CommentDetail { get; set; }
        public string UserName { get; set; }
        

        public Comment(Models.Comment comment)
        {
            this.Id = comment.Id;
            this.UserId = comment.UserId;
            this.GameId = comment.GameId;
            this.RequestId = comment.RequestId;
            this.PlayId = comment.PlayId;
            this.UserOpinionId = comment.UserOpinionId;
            this.Date = comment.Date;
            this.CommentDetail = comment.CommentDetail;
            this.UserName = comment.User.Name;
           

        }
        
    }
}
