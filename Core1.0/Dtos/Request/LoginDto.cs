using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Dtos.Request
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Pwd { get; set; }
    }
}
