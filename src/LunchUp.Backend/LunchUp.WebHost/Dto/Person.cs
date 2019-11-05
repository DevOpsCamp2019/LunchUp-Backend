using System;

namespace LunchUp.WebHost.Dto
{
    /// <summary>
    /// The object representing the user
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The unique identifier of the person
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The lastname of the person
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// The firstname of the person
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// The email of the person
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The base64-encoded photo of the person
        /// </summary>
        public string Photo { get; set; }
    }
}