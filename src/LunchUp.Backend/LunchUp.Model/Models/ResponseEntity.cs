using System;

namespace LunchUp.Model.Models
{
    public class ResponseEntity
    {
        public Guid Id { get; set; }
        public PersonEntity Origin { get; set; }
        public PersonEntity Target { get; set; }
        public bool Like { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
