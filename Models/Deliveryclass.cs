using Microsoft.AspNetCore.Routing.Constraints;

namespace GasBooking_coreMVC.Models
{
    public class Deliveryclass
    {
        public int bid { set; get; }
        public int staffid { set; get; }
        public string? date { set; get; }
        public int filled { set; get; }
        public int notfilled { set; get; }
        public int customerid { set; get; }
        public int cylinderid { set; get; }
        public int newfilled { set; get; }
        public int newnotfilled { set; get; }
        public string? status{ set; get; }



        public int boid { get; set; }
        public int clyid { get; set; }
        public int cusid { get; set; }
        public string? consnum { get; set; }
        public int stfid { get; set; }
        public int amount { get; set; }
        public string? daate { get; set; }
        public string? bokmode { get; set; }
        public string? sts { get; set; }
        public int bookingid { set; get; }
    }
}
