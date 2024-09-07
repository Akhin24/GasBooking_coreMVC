using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasBooking_coreMVC.Models
{
    public class Cylinderclass
    {
        public string? cyid { set; get; }
        public string? cyname { get; set; }
        public string? modeid { set; get; }
        public string? modename { set; get; }
    }

    public class Cylinderinsert
    {
       public string? cyid { set; get; }
        public string? cyname { get; set; }
        public int stock { set; get; }
        public int filled { set; get; }
        public int notfilled { set; get; }
        public int price { set; get; }
    }
}
