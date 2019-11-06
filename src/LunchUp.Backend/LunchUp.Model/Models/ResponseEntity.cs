using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LunchUp.Model.Models
{
    public class ResponseEntity
    {
        [Column(TypeName="varchar(36)")]
        public Guid Id { get; set; }
        public PersonEntity Origin { get; set; }
        public PersonEntity Target { get; set; }
        public bool Like { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}