using System;
using System.ComponentModel.DataAnnotations;

namespace RailRoadSystem.Models
{
    public class Ticket: Entity
    {
        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public int Seat { get; set; }

        [Required]
        public int RailwayCarriage { get; set; }
        
        public City CityTo { get; set; }
        
        public City CityFrom { get; set; }
    }
}