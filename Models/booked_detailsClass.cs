namespace GasBooking_coreMVC.Models
{
    public class booked_detailsClass
    {
        public int bid { get; set; }
        public int clyid { get; set; }
        public int cusid { get; set; }
        public string? consnum { get; set; }
        public int stfid { get; set; }
        public int amount { get; set; }
        public string? date { get; set; }
        public string? bokmode { get; set; }
        public string? sts { get; set; }
        public int bookingid { set; get; }

    }
}
