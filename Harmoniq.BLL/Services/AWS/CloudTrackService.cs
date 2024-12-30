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
    public class CloudTrackService : ICloudTrackService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public CloudTrackService(IConfiguration configuration)
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

        public async Task<string> UploadAudioFileAsync(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                throw new ArgumentException("Audio file is empty");
            }

            var allowedExtensions = new[] { ".mp3", ".wav" };
            var fileExtension = Path.GetExtension(audioFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid file extension");
            }

            var fileTransferUtility = new TransferUtility(_s3Client);

            using (var stream = audioFile.OpenReadStream())
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = $"{Guid.NewGuid()}_{audioFile.FileName}",
                    BucketName = _bucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                await fileTransferUtility.UploadAsync(uploadRequest);
                return $"https://{_bucketName}.s3.us-east-2.amazonaws.com/{uploadRequest.Key}";
            }

        }
    }
}