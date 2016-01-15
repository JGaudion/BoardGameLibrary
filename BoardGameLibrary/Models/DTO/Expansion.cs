using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLibrary.Models.DTO
{
    /// <summary>
    /// This class is similar to Expansion in the Entities (BoardGameLibraryModel.edmx), but this exists as a way of passing
    /// to the client only the parts that we want to show on the client, rather than the client having all the navigation 
    /// links from the Entity Framework relationships.
    /// </summary>
    public class Expansion
    {
        public int Id { get; internal set; }
        public string OwnedBy { get; internal set; }
        public bool AtWork { get; set; }

        [Required]
        [StringLength(200, ErrorMessage ="Expansion name cannot exceed 200 characters")]
        public string Name { get; set; }

        [Required]
        public string ParentGame { get; internal set; }

        [StringLength(500, ErrorMessage = "Expansion description cannot exceed 500 characters")]
        public string Description { get; set; }


        public Expansion(Models.Expansion expansion)
        {
            this.Id = expansion.Id;
            this.Name = expansion.Name;
            this.Description = expansion.Description;
            this.AtWork = expansion.AtWork;
            if (expansion.ParentGame != null)
            {
                this.ParentGame = expansion.ParentGame.Name;
            }
            else
            {
                this.ParentGame = ""; //We could throw an exception here, warning that no parent game was found
            }
            if (expansion.Owner != null)
            {
                this.OwnedBy = expansion.Owner.Name;
            }
            else
            {
                this.OwnedBy = "";
            }

        }
    }
}
