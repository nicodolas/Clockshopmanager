using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Threading.Tasks;
using SkiaSharp;
using System.Net;
using System.IO;
using System.Drawing;
namespace AdminNhapHang.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService()
        {
            var account = new Account(
                "daxwfccfx",      // cloud name
                "182111954225135",         // API key
                "Sxoh379PhMm8UpEdfzJPwZ5yab8");     // API secret

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadAndGetUrlImageAsync(string imageUrl, string fileName = null)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageUrl),
                PublicId = fileName,
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString(); // URL ảnh trên Cloudinary
            }

            return null;
        }



}
}
