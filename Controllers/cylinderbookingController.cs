using GasBooking_coreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Sockets;
using System.Reflection;

namespace GasBooking_coreMVC.Controllers
{
    public class cylinderbookingController : Controller
    {
        ConnectionDB dbobj = new ConnectionDB();
        public IActionResult book_pageload(Boookingclass bokobj)
        {
            var modelList = new List<SelectListItem>
                {
                     new SelectListItem { Value = "ONLINE", Text = "ONLINE" },
                     new SelectListItem { Value = "OFLINE", Text = "OFLINE" }
                };




            var cylinderList = new List<SelectListItem>
                {
                        new SelectListItem { Value = "Domestic", Text = "Domestic" },
                        new SelectListItem { Value = "Commercial", Text = "Commercial" }
                };
            var viewModel = new Boookingclass
            {
                ModelList = modelList,
                CylinderList = cylinderList
            };
            return View(viewModel);

        }
        public IActionResult book_click(Boookingclass bokobj)
        {
            try
            {

                var selectedModeId = bokobj.SelectedModeId;
                var selectedCylinderId = bokobj.SelectedCylinderId;



                if (selectedModeId == "ONLINE")
                {
                    string staffid = dbobj.get_staffid(bokobj);
                    bokobj.stfid = Convert.ToInt32(staffid);
                }


                if (selectedCylinderId == "Domestic")
                {
                    
                    string id = dbobj.get_cylinderid(bokobj);
                    bokobj.cyid = Convert.ToInt32(id);
                    string price = dbobj.get_price(bokobj);
                    bokobj.ttlamt = Convert.ToInt32(price);

                }
                else if (selectedCylinderId == "Commercial")
                {
                    string id = dbobj.get_cylinderid(bokobj);
                    bokobj.cyid = Convert.ToInt32(id);
                    string price = dbobj.get_price(bokobj);
                    bokobj.ttlamt = Convert.ToInt32(price);
                }

                    bokobj.cusid = Convert.ToInt32(TempData["loginid"]);
                    string s = dbobj.cylinderbooking(bokobj);
                    TempData["bmsg"] = s;
                
            }
            catch (Exception ex)
            {
                TempData["bmsg"] = ex;
            }
            return View ("book_pageload", bokobj);

        }
    }
}
