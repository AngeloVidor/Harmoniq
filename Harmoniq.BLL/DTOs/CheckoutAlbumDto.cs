using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Harmoniq.BLL.DTOs
{
    public class CheckoutAlbumDto
    {
        public string AlbumId { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public decimal Price { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public string Title { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public int ContentConsumerId { get; set; }


    }
}