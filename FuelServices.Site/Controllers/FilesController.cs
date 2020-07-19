using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Controllers;

namespace FuelServices.Site.Controllers
{
    public class FilesController : Controller
    {
        public FileResult Banner()
        {

            return PhysicalFile($"G:/VS Projects/FuelServicesV1/FuelServices.Site/wwwroot/uploads/video.mp4", "application/octet-stream", enableRangeProcessing: true);
        }
    }
}
