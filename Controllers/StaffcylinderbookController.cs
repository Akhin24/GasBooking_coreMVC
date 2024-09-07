using GasBooking_coreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasBooking_coreMVC.Controllers
{
    public class StaffcylinderbookController : Controller
    {
        ConnectionDB dbobj = new ConnectionDB();
        public IActionResult staffcylinderbook_pageload()
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
            var viewModel = new StaffBooking
            {
                ModelList = modelList,
                CylinderList = cylinderList
            };
            return View(viewModel);
           
        }
        public IActionResult staffcylinderbook_click(StaffBooking stfobj)
        {
            try
            {
                
                
                    var selectedModeId = stfobj.SelectedModeId;
                    var selectedCylinderId = stfobj.SelectedCylinderId;
                    if (selectedModeId == "ONLINE")
                    {
                        string cusid = dbobj.getcustomerid(stfobj);
                        stfobj.cusid = Convert.ToInt32(cusid);
                    }
                    else if (selectedModeId == "OFLINE")
                    {
                        string cusid = dbobj.getcustomerid(stfobj);
                        stfobj.cusid = Convert.ToInt32(cusid);
                    }
                    if (selectedCylinderId == "Domestic")
                    {

                        string id = dbobj.get_cylinderidforstaffbook(stfobj);
                        stfobj.cyid = Convert.ToInt32(id);
                        string price = dbobj.get_priceforstaffbook(stfobj);
                        stfobj.ttlamt = Convert.ToInt32(price);

                    }
                    else if (selectedCylinderId == "Commercial")
                    {
                        string id = dbobj.get_cylinderidforstaffbook(stfobj);
                        stfobj.cyid = Convert.ToInt32(id);
                        string price = dbobj.get_priceforstaffbook(stfobj);
                        stfobj.ttlamt = Convert.ToInt32(price);
                    }
                    stfobj.stfid = Convert.ToInt32(TempData["loginid"]);
                    string sbc = dbobj.StaffcylinderBooking(stfobj);
                    TempData["sbmsg"] = sbc;
                
            }
            catch (Exception ex)
            {
                TempData["sbmsg"] = ex;
            }
            return View("staffcylinderbook_pageload", stfobj);
        }
    }
}
