using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http.Headers;

namespace ContentManagement.Helper
{
    public class FileHelper
    {
        public static string UploadDocument(FileUploadDTO fileModel)
        {
            try
            {
                var filePath = $"{fileModel.RootPath}/{fileModel.FolderName}";
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var mediaFile = fileModel.FormFile;
                var newMedia = $"{Guid.NewGuid()}{Path.GetExtension(mediaFile.FileName)}";
                string fullPath = Path.Combine(filePath, newMedia);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    mediaFile.CopyTo(stream);
                }
                return $"/{fileModel.FolderName}/{newMedia}";
            }
            catch (Exception ex)
            {
                return "Hata oluştu: " + ex.Message;
            }
        }

        public static string UploadPassportPicture(FilePassportPictureDTO fileModel)
        {
            try
            {
                var filePath = $"{fileModel.RootPath}/{fileModel.FolderName}";
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var mediaFile = fileModel.FormFile;
                var newMedia = $"{fileModel.AA}{Path.GetExtension(mediaFile.FileName)}";
                string fullPath = Path.Combine(filePath, newMedia);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    mediaFile.CopyTo(stream);
                }

                return $"/{fileModel.FolderName}/{newMedia}";
            }
            catch (Exception ex)
            {
                return "Hata oluştu: " + ex.Message;
            }
        }
        public static string UploadFile(IFormFile file, string path, string? name = null)
        {
            Guid guid = Guid.NewGuid();
            string fileName = string.IsNullOrEmpty(name) ? guid.ToString() : name; /* Dosya adına parametreden gelen ismi veriyor eğer boşsa Guidin adını 
                                                                                      veriyor */
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); // wwwrootun dosya yolunu alıyor
            string targetFolderPath = Path.Combine(wwwrootPath, path); // wwwrootun dosya yolu ile parametreden gelen dosya yolunu birleştiriyor
            string filePath = Path.Combine(targetFolderPath, fileName + ".png"); // Birleşmiş dosya yoluna .png uzantısı ekleniyor

            if (!Directory.Exists(targetFolderPath)) // Dosya yolu var mı yok mu diye kontrol ediyor
            {
                Directory.CreateDirectory(targetFolderPath); // Dosyaya yol yapıyor
            }

            using (var stream = new FileStream(filePath, FileMode.Create)) // Oluşturma modunda dosya yolu filePath olacak şekilde bir stream oluşturuyor
            {
                file.CopyTo(stream); // Gelen içeriği kopyalar ve hedef dosya içerisine kaydeder
            }

            return fileName + ".png";
        }

        public static string UploadMemberCard(byte[] file, string subPath, string? name = null)
        {
            Guid guid = Guid.NewGuid();
            string fileName = string.IsNullOrEmpty(name) ? guid.ToString() : name;
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string targetFolderPath = Path.Combine(wwwrootPath, subPath);
            string filePath = Path.Combine(targetFolderPath, fileName + ".pdf");

            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }

            using (var memoryStream = new MemoryStream(file))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
            return fileName + ".pdf";
        }

        public static string UploadPDF(byte[] file, string subPath, string? name = null)
        {
            Guid guid = Guid.NewGuid();
            string fileName = string.IsNullOrEmpty(name) ? guid.ToString() : name;
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string targetFolderPath = Path.Combine(wwwrootPath, subPath);
            string filePath = Path.Combine(targetFolderPath, fileName + ".pdf");

            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }

            if (!File.Exists(filePath))
            {
                using (var memoryStream = new MemoryStream(file))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        memoryStream.CopyTo(fileStream);
                    }
                }
            }
            return fileName + ".pdf";
        }

        public static byte[] GetUploadFile(IFormFile src)
        {
            byte[] fileBytes = null;
            var fileName = ContentDispositionHeaderValue.Parse(src.ContentDisposition).FileName.Trim('"');

            using (var fileStream = src.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return fileBytes;
        }

        public static string GetImageSrc(byte[] fileBytes)
        {
            var base64 = Convert.ToBase64String(fileBytes);
            var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
            return imgsrc;
        }

        public static bool DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
    public class FileUploadDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public string RootPath { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string FolderName { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
    }

    public class FilePassportPictureDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public string RootPath { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public int Type { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string FolderName { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string AA { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
    }
}
