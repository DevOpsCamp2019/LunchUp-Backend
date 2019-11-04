using System;
using System.Net.Mail;

namespace LunchUp.Model
{
    public class PersonEntity
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public MailAddress Email { get; set; }
        public string Photo { get; set; }
    }
}
