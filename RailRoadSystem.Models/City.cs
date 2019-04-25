using System.ComponentModel.DataAnnotations;

namespace RailRoadSystem.Models
{
    public class City: Entity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}