using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace LunchUp.WebHost.Dto
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
    }
}