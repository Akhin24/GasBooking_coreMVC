using GasBooking_coreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasBooking_coreMVC.Controllers
{
    public class CustomerinsertController : Controller
    {
        ConnectionDB dbobj = new ConnectionDB();
        private readonly IWebHostEnvironment _WebHost;
        public CustomerinsertController(IWebHostEnvironment webhost)
        {
            _WebHost = webhost;
        }

        public IActionResult Customerinsert_pageload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Customerinsert_click(Customerinsert cusobj,IFormFile singlefile)
        {
            try
            {
                
                    if (singlefile != null && singlefile.Length > 0)
                    {
                        string uploadfolder = Path.Combine(_WebHost.WebRootPath, "photos");
                        string uniquefilename = Guid.NewGuid().ToString() + "_" + Path.GetFileName(singlefile.FileName);
                        string filepath = Path.Combine(uploadfolder, uniquefilename);
                        using (var stream = System.IO.File.Create(filepath))
                        {
                        singlefile.CopyTo(stream);
                        }
                        cusobj.cpho = "/photos/" + uniquefilename;
                    }
                    string resp = dbobj.customerinsertDB(cusobj);
                   
                    TempData["msg1"] = resp;
                
            }
            catch (Exception ex)
            {
                TempData["msg1"] = ex.Message;
            }
            return View("Customerinsert_pageload", cusobj);
           
        }
    }
}
