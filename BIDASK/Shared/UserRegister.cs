using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDASK.Shared
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [StringLength(16, ErrorMessage = "Your username is too long (16 characters max).")]
        public string Username { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Only confirmed users can play!")]
        public bool IsConfirmed { get; set; } = true;
    }
}
