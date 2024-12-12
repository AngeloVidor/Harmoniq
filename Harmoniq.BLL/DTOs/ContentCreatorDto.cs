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
    public class ContentCreatorDto
    {
        [Key]
        public int Id { get; set; }
        public string ContentCreatorName { get; set; }
        public string ContentCreatorDescription { get; set; }
        public string ContentCreatorCountry { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }

    }
}