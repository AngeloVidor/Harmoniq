using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.DTOs
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountType Roles { get; set; }
    }
}