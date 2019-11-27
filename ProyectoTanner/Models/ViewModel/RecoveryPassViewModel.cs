using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoTanner.Models.ViewModel
{
    public class RecoveryPassViewModel
    {
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        [Required]
        public string Password2 { get; set; }

    }
}