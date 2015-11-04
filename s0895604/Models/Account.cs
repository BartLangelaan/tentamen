using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s0895604.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

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

        [DisplayName("Rol")]
        public AccountRole Role { get; set; }

        [DisplayName("Actief")]
        public bool Active { get; set; }

        public Account()
        {
            Role = AccountRole.User;
            Active = true;
        }

        private static Account _activeAccount;

        public static Account ActiveAccount
        {
            get { return _activeAccount; }
            set { _activeAccount = value; }
        }
    }

    public enum AccountRole
    {
        Admin = 1,
        User = 2
    }
}