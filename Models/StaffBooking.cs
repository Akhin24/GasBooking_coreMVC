using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasBooking_coreMVC.Models
{
    public class StaffBooking
    {
        public string SelectedModeId { get; set; }
        public string SelectedCylinderId { get; set; }

        public IEnumerable<SelectListItem> ModelList { get; set; }
        public IEnumerable<SelectListItem> CylinderList { get; set; }
        public int cyid { set; get; }
        public int cusid { set; get; }
        public string? consumbernumber { set; get; }
        public int stfid { set; get; }
        public int ttlamt { set; get; }
        public string? date { set; get; }

        public string? sts { set; get; }
        public int staffid { set; get; }
        public int cylinderid { set; get; }
        public string? cylindertype { set; get; }
        public int cylinderprice { set; get; }
        public int customerid { set; get; }
    }
}
