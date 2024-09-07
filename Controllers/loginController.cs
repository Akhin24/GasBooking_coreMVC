using Microsoft.AspNetCore.Mvc;
using GasBooking_coreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace GasBooking_coreMVC.Controllers
{
    public class loginController : Controller
    {
        ConnectionDB dbobj = new ConnectionDB();
        public IActionResult loginpageload()
        {
            return View();
        }
        public IActionResult customerHome(int id)
        {
            
            TempData["cidfpv"] = id;
            int ide = id;
            var details = dbobj.bookedDetails(ide);
            if (details == null || !details.Any())
            {
                return View("customerHome");
            }
            else
            {
                return View(details);
            }
          //  return View();
           
        }

        public IActionResult StaffHome()
        {
           
            return View();
        }
       

        public IActionResult AdminHome()
        {
            List<Cylinderclass> cylist = new List<Cylinderclass>()
                    {
                      new Cylinderclass{cyid="Domestic", cyname="Domestic"},
                      new Cylinderclass{cyid="Commercial",cyname="Commercial"}
                    };
            ViewBag.cylist = new SelectList(cylist,"cyname","cyid");

            return View();

        }
       
        public IActionResult loginclick(Logincls clsobj)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    string locid = dbobj.loginDB(clsobj);
                    if (locid == "Login succesfull")
                    {
                        string id =dbobj.loginidDB(clsobj);                       
                        TempData["loginid"] =Convert.ToInt32(id);
                        clsobj.ID =Convert.ToInt32(id);
                        string logtype = dbobj.logintypeDB(clsobj);
                        if (logtype == "ADMIN")
                        {
                            return RedirectToAction("AdminHome");
                        }
                        else if (logtype == "STAFF")
                        {
                            return RedirectToAction("StaffHome");
                        }
                        else if (logtype == "CUSTOMER")
                        {
                            return RedirectToAction("customerHome", new {id=clsobj.ID});
                        }                       
                    }
                    else if(locid== "invalid username and password")
                    {
                        TempData["logmsg"] = "please check your username and password";
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["logmsg"] = ex.Message;
            }
            return View("loginpageload", clsobj);
           
        }
        public IActionResult cylinderadd(Cylinderinsert clyobj)
        {
            try
            {               
                if (ModelState.IsValid)
                {
                    List<Cylinderclass> cylist = new List<Cylinderclass>()
                    {
                       new Cylinderclass{cyid="Domestic", cyname="Domestic"},
                       new Cylinderclass{cyid="Commercial",cyname="Commercial"}
                    };
                    ViewBag.cylist = new SelectList(cylist, "cyname", "cyid");
                    string clin = dbobj.cylinderinsert(clyobj);
                    if (clin == "1")
                    {
                        TempData["clymsg"] = "inserted succesfully";
                    }
                    else
                    {
                        TempData["clymsg"] = "error occuerd during insertion";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["clymsg"]= ex;
            }
            return View("AdminHome", clyobj);
        }
        
    }
}
