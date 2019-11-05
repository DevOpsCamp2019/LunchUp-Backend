using System;

namespace LunchUp.Model.Models
{
    public class PersonEntity
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
    }
}
