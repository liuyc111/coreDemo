using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Dtos.Request
{
    public class RegisterDto
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MinLength(10)]
        public string Password { get; set; }
        /// <summary>
        /// 验证密码
        /// </summary>
        [Required]
        [Compare(nameof(Password), ErrorMessage = "密码不一致")]
        public string ConfrimPassword { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
