using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Harmoniq.BLL.DTOs
{
    public class UserRegisterDto
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountType Roles { get; set; }




    }
}