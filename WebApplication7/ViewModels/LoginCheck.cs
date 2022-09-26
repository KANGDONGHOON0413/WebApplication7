using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.ViewModels
{
    public class LoginCheck
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string UserPW { get; set; }
    }
}
