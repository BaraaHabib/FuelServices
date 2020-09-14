using Microsoft.AspNetCore.Mvc;

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