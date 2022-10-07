using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyTicket.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password length is at least 8 char")]
        public string Password { get; set; }
    }
}