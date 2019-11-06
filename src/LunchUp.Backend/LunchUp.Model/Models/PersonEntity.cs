using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LunchUp.Model.Models
{
    public class PersonEntity
    {
        [Column(TypeName="varchar(36)")]
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Company { get; set; }
        public DateTime? OptIn { get; set; }
        
        public virtual ICollection<ResponseEntity> Responses { get; set; }

    }
}