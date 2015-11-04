using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s0895604.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AccountStatus Status { get; set; }

        public bool Active { get; set; }
    }

    public enum AccountStatus
    {
        Admin = 1,
        User = 2
    }
}