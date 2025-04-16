using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        const int maxLength = 2_097_152;
        List<string> allowedExtensions = [".png",".jpg" ,".jpeg"];
        public string? Uploud(IFormFile file, string folderName)
        {

            // 1.Check Extension
            var extentension =Path.GetExtension(file.FileName);
            if(! allowedExtensions.Contains(extentension)) return null;

            //2.Check Size
            if(file.Length ==0|| file.Length>maxLength) return null;
            //3.Get Located Folder Path
            var folderPath =Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", folderName);
            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            //5.Get File Path
            var filePath=Path.Combine(folderPath,fileName);
            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream fs = new FileStream(filePath, FileMode.Create);
            //7.Use Stream To Copy File
            file.CopyTo(fs);
            //8.Return FileName To Store In Database
            return fileName;
        }
        public bool Delete(string filePath)
        {
           if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
         }

    }
}
