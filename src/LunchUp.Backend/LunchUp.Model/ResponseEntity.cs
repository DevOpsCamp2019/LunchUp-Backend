using System;

namespace LunchUp.Model
{
    public class ResponseEntity
    {
        public PersonEntity Origin { get; set; }
        public PersonEntity Target { get; set; }
        public bool Like { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
