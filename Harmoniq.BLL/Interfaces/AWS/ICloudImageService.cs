using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Harmoniq.BLL.Interfaces.AWS
{
    public interface ICloudImageService
    {
        Task<string> UploadImageFileAsync(IFormFile imageFile);

    }
}