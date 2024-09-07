using GasBooking_coreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasBooking_coreMVC.Controllers
{
    public class staffinsertController : Controller
    {
        ConnectionDB dbobj = new ConnectionDB();
        public IActionResult staffinsert_pageload()
        {
            return View();
        }
        public IActionResult staffinserT_CLICK(StaffInsert stobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.staffinsertDB(stobj);
                   
                    TempData["msg"] = resp;
                }
            }
            catch(Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("staffinsert_pageload",stobj);
        }
    }
}
