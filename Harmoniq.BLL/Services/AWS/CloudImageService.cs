using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Harmoniq.BLL.Interfaces.AWS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Harmoniq.BLL.Services.AWS
{
    public class CloudImageService : ICloudImageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public CloudImageService(IConfiguration configuration)
        {
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];
            var region = configuration["AWS:Region"];
            _bucketName = configuration["AWS:BucketName"];

            _s3Client = new AmazonS3Client
            (
                accessKey,
                secretKey,
                Amazon.RegionEndpoint.GetBySystemName(region)
            );
        }

        public async Task<string> UploadImageFileAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Image file is empty");
            }

            var fileTransferUtility = new TransferUtility(_s3Client);

            using (var stream = imageFile.OpenReadStream())
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = $"{Guid.NewGuid()}_{imageFile.FileName}",
                    BucketName = _bucketName,
                    CannedACL = S3CannedACL.NoACL
                };
                await fileTransferUtility.UploadAsync(uploadRequest);
                return $"https://{_bucketName}.s3.us-east-2.amazonaws.com/{uploadRequest.Key}";
            }
        }
    }
}