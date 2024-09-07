using GasBooking_coreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasBooking_coreMVC.Controllers
{
    
    public class CancelController : Controller
    {
        ConnectionDB dbobj = new ConnectionDB();
        public IActionResult Cancel_detail_pageload(int id)
        {
            TempData["cid"] = id;
            booked_detailsClass datalist = dbobj.detailsforcancel(id);
            return View(datalist);
        }
        public IActionResult Cancel_click(booked_detailsClass bokobj)
        {
            try
            {

                bokobj.bookingid = Convert.ToInt32(TempData["cid"]);
                int s = dbobj.cancelbooking(bokobj);
               
                TempData["cmsg"] = s;
            }
            catch (Exception ex)
            {
                TempData["cmsg"] = ex;
            }
            return View("Cancel_detail_pageload", bokobj);
            
        }       
    }
}
