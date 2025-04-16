using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
        public string? Uploud(IFormFile file, string folderName);

        bool Delete(string filePath);
    }
}
