using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.IO;
using Kendo.Mvc;
using System.Linq;

namespace DigitalLearningDataImporter.TelWebApp.Controllers
{
    public class UploadController : Controller
    {
        public IWebHostEnvironment HostingEnvironment { get; set; }

        public UploadController(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }
        public IActionResult BasicUsage()
        {
            return View();
        }


        public ActionResult Submit(IEnumerable<IFormFile> files)
        {
            var fileInfos = Enumerable.Empty<DigitalLearningIntegration.Application.Utils.FileInfo>();

            if (files != null)
            {
                fileInfos = GetFileInfo(files);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, "App_Data", fileName);

                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(physicalPath))
                    {
                        file.CopyTo(stream);
                    }
                }
            }

            return View("Result", fileInfos);
        }


        public ActionResult Result()
        {
            return View();
        }

        private IEnumerable<DigitalLearningIntegration.Application.Utils.FileInfo> GetFileInfo(IEnumerable<IFormFile> files)
        {
            var fileInfos = new List<DigitalLearningIntegration.Application.Utils.FileInfo>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));

                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, "App_Data", fileName);

                    fileInfos.Add(new DigitalLearningIntegration.Application.Utils.FileInfo()
                    {
                        Name = fileName,
                        ContentDisposition = file.ContentDisposition,
                        Size = file.Length,
                        FullPath = physicalPath
                    });
                }
            }

            return fileInfos;
        }
    }
}
