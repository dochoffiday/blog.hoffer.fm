using System;
using System.ComponentModel.DataAnnotations;

namespace BC.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public String Username { get; set; }

        [Required]
        public String Password { get; set; }
    }
}