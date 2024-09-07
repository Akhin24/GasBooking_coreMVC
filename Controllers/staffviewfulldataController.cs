using GasBooking_coreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasBooking_coreMVC.Controllers
{
    public class staffviewfulldataController : Controller
    {
        ConnectionDB dbobj = new ConnectionDB();
        public IActionResult staffviewfulldata_load()
        {
            var getdata = dbobj.viewfulldata();
            return View(getdata);
        }

        public IActionResult delivaryclick(int bid,int cid,int clyid,Deliveryclass dlsobj)
        {
            try
            {
                dlsobj.bid = bid;
                dlsobj.staffid = Convert.ToInt32(TempData["loginid"]);
                string deliveryinsert = dbobj.deliverytabinsert(dlsobj);
                if (deliveryinsert == "inserted succesfully")
                {
                    string ss = dbobj.updatebbokibgstst(dlsobj);
                    dlsobj.cylinderid = clyid;
                    int filled = dbobj.filled(dlsobj);
                    dlsobj.filled = filled;
                    int notfilled = dbobj.notfilled(dlsobj);
                    dlsobj.notfilled = notfilled;
                    dlsobj.newfilled = filled - 1;
                    dlsobj.newnotfilled = notfilled + 1;
                    string update = dbobj.updatedstock(dlsobj);
                    dlsobj.customerid = cid;
                    var getdata = dbobj.viewfulldata();
                    return View("staffviewfulldata_load", getdata);
                    
                }
                
            }
            catch(Exception ex)
            {


                TempData["dum"] = ex.Message;
            
              
            }
            return View("staffviewfulldata_load", dlsobj);
        }
    }
}
