using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RailRoadSystem.Models
{
    public class User: Entity
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
