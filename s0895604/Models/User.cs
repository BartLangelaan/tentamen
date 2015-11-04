using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace s0895604.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Gebruikersnaam")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Wachtwoord")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Achternaam")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Rol")]
        public UserRole Role { get; set; }

        [DisplayName("Actief")]
        public bool Active { get; set; }

        public User()
        {
            Role = UserRole.User;
            Active = true;
        }
    }

    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}