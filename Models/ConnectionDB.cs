using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GasBooking_coreMVC.Models

{
    public class ConnectionDB
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-IEB5SN8\SQLEXPRESS;database=GasBookingDB;Integrated security=true");
      
        public string staffinsertDB(StaffInsert stfobj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_max_regid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                int cid = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                int mid = cid;
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }
                stfobj.sid = regid;
                stfobj.rid = regid;
                stfobj.sstatus = "ACTIVE";
                SqlCommand cmd1 = new SqlCommand("sp_staffinserts", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@sid", stfobj.sid);
                cmd1.Parameters.AddWithValue("@name", stfobj.sname);
                cmd1.Parameters.AddWithValue("@phn", stfobj.sphone);
                cmd1.Parameters.AddWithValue("@emil", stfobj.semail);
                cmd1.Parameters.AddWithValue("@staffsts", stfobj.sstatus);
           
                stfobj.logtype = "STAFF";
                stfobj.sts = "ACTIVE";
                cmd1.Parameters.AddWithValue("@rid", stfobj.rid);
                cmd1.Parameters.AddWithValue("@unam", stfobj.uname);
                cmd1.Parameters.AddWithValue("@pass", stfobj.pass);
                cmd1.Parameters.AddWithValue("@logtype", stfobj.logtype);
                cmd1.Parameters.AddWithValue("@logsts", stfobj.sts);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                return ("inserted succesfullly");
               
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }          
        }
        

        public string customerinsertDB(Customerinsert clsobj)
        {
            try
            {                             
                SqlCommand cmd = new SqlCommand("sp_max_regid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                int cid = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                int mid = cid;
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }
                clsobj.cid = regid;
                clsobj.csts = "ACTIVE";
                SqlCommand cmd1 = new SqlCommand("sp_customer_insert", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@cid", clsobj.cid);
                cmd1.Parameters.AddWithValue("@nam", clsobj.cname);
                cmd1.Parameters.AddWithValue("@connum", clsobj.cnumber);
                cmd1.Parameters.AddWithValue("@phn", clsobj.cphone);
                cmd1.Parameters.AddWithValue("@addr", clsobj.caddr);
                cmd1.Parameters.AddWithValue("@photo", clsobj.cpho);
                cmd1.Parameters.AddWithValue("@emil", clsobj.cemil);
                cmd1.Parameters.AddWithValue("@sts", clsobj.csts);


                clsobj.rid = regid;
                clsobj.logtype = "CUSTOMER";
                clsobj.sts = "ACTIVE";
                cmd1.Parameters.AddWithValue("@rid", clsobj.rid);
                cmd1.Parameters.AddWithValue("@unam", clsobj.uname);
                cmd1.Parameters.AddWithValue("@pass", clsobj.pass);
                cmd1.Parameters.AddWithValue("@logtype", clsobj.logtype);
                cmd1.Parameters.AddWithValue("@logsts", clsobj.sts);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                return ("inserted succesfully");
                
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }           
        }
        public string loginDB(Logincls logobj)
        {
            try
            {              
                SqlCommand cmd = new SqlCommand("sp_logincountofid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", logobj.username);
                cmd.Parameters.AddWithValue("@password", logobj.password);
                SqlParameter status = new SqlParameter("@status", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(status);
                con.Open();
                int cid = cmd.ExecuteNonQuery();
                con.Close();
                if (status.Value.ToString() == "1")
                {

                    return "Login succesfull";
                   
                   // return id;
                    //msg = "success";
                }
                else
                {
                    return  "invalid username and password";

                }                              
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
        public string loginidDB(Logincls logobj)
        {
            try
            {
                string id = "";
                SqlCommand cmd1 = new SqlCommand("sp_logingetid", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@username", logobj.username);
                cmd1.Parameters.AddWithValue("@password", logobj.password);
                con.Open();
                id = cmd1.ExecuteScalar().ToString();
                con.Close();
                    return id;
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
           
        }

        public string logintypeDB(Logincls logobj)
        {
            try
            {
                string logtye = "";
                SqlCommand cmd2 = new SqlCommand("sp_logtype", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@username", logobj.username);
                cmd2.Parameters.AddWithValue("@password", logobj.password);
                con.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    logtye = dr["log_type"].ToString();
                }
                con.Close();
                return logtye;
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
        }
        public string cylinderinsert(Cylinderinsert clyobj)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("sp_cylinder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type",clyobj.cyname);
                cmd.Parameters.AddWithValue("@stock", clyobj.stock);
                cmd.Parameters.AddWithValue("@filled", clyobj.filled);
                cmd.Parameters.AddWithValue("@notfilled", clyobj.notfilled);
                cmd.Parameters.AddWithValue("@price", clyobj.price);
                con.Open();
                int i=cmd.ExecuteNonQuery();
                con.Close();
                return i.ToString();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
        public string cylinderbooking(Boookingclass bokobj)
        {
            try
            {
                bokobj.date= DateTime.Now.ToString("yyyy-MM-dd");
               
                bokobj.sts = "Booked";
               
                SqlCommand cmd = new SqlCommand("sp_booking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cylinder_id", bokobj.cyid);
                cmd.Parameters.AddWithValue("@customer_id", bokobj.cusid);
                cmd.Parameters.AddWithValue("@consumernumber", bokobj.consumbernumber);
                cmd.Parameters.AddWithValue("@staff_id", bokobj.stfid);
                cmd.Parameters.AddWithValue("@total_amount", bokobj.ttlamt);
                cmd.Parameters.AddWithValue("@date", bokobj.date);
                cmd.Parameters.AddWithValue("@booking_mode", bokobj.SelectedModeId);
                cmd.Parameters.AddWithValue("@status", bokobj.sts);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "BOOKING DONE ";

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
        public string get_cylinderid(Boookingclass bokobj)
        {
            try
            {
                int id = 0;
                SqlCommand cmd = new SqlCommand("sp_selcylinderid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cyid", bokobj.cylinderid);
                cmd.Parameters.AddWithValue("@type", bokobj.SelectedCylinderId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["cylinder_id"].ToString());
                }
                con.Close();
                return (id).ToString();
                
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }

        }
        public string get_price(Boookingclass bokobj)
        {
            try
            {
                int price = 0;
                SqlCommand cmd = new SqlCommand("sp_selcylinderprice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@price", bokobj.cylinderprice);
                cmd.Parameters.AddWithValue("@type", bokobj.SelectedCylinderId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    price = Convert.ToInt32(dr["price"].ToString());
                }
                con.Close();
                return (price).ToString();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }//sp_selstaffid
        public string get_staffid(Boookingclass stffsobj)
        {
            try
            {
                int staffid = 0;
                SqlCommand cmd = new SqlCommand("sp_selstaffid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@staffid", stffsobj.staffid);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    staffid = Convert.ToInt32(dr["staff_id"].ToString());
                }
                con.Close();
                return (staffid).ToString();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
    
        }
        public string StaffcylinderBooking(StaffBooking sfboobj)
        {
            try
            {
                sfboobj.date = DateTime.Now.ToString("yyyy-MM-dd");

                sfboobj.sts = "Booked";

                SqlCommand cmd = new SqlCommand("sp_booking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cylinder_id", sfboobj.cyid);
                cmd.Parameters.AddWithValue("@customer_id", sfboobj.cusid);
                cmd.Parameters.AddWithValue("@consumernumber", sfboobj.consumbernumber);
                cmd.Parameters.AddWithValue("@staff_id", sfboobj.stfid);
                cmd.Parameters.AddWithValue("@total_amount", sfboobj.ttlamt);
                cmd.Parameters.AddWithValue("@date", sfboobj.date);
                cmd.Parameters.AddWithValue("@booking_mode", sfboobj.SelectedModeId);
                cmd.Parameters.AddWithValue("@status", sfboobj.sts);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "BOOKING DONE ";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }//sp_selcustomerid
        public string getcustomerid(StaffBooking sfboobj)
        {
            try
            {
                int customer_id = 0;
                SqlCommand cmd = new SqlCommand("sp_selcustomerid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@consumernumber", sfboobj.consumbernumber);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    customer_id =Convert.ToInt32(dr["customer_id"].ToString());
                }
                con.Close();
                return (customer_id.ToString());
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message);
            }
        }
        public string get_cylinderidforstaffbook(StaffBooking stfbokobj)
        {
            try
            {
                int id = 0;
                SqlCommand cmd = new SqlCommand("sp_selcylinderid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cyid", stfbokobj.cylinderid);
                cmd.Parameters.AddWithValue("@type", stfbokobj.SelectedCylinderId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["cylinder_id"].ToString());
                }
                con.Close();
                return (id).ToString();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }

        }
        public string get_priceforstaffbook(StaffBooking stffbokobj)
        {
            try
            {
                int price = 0;
                SqlCommand cmd = new SqlCommand("sp_selcylinderprice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@price", stffbokobj.cylinderprice);
                cmd.Parameters.AddWithValue("@type", stffbokobj.SelectedCylinderId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    price = Convert.ToInt32(dr["price"].ToString());
                }
                con.Close();
                return (price).ToString();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
        public List<booked_detailsClass> bookedDetails(int ide)
        {
            var boid_items = new List<booked_detailsClass>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selbookeditems", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerid",ide);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var item = new booked_detailsClass()
                    {
                        bid =Convert.ToInt32( dr["booking_id"].ToString()),
                        clyid= Convert.ToInt32(dr["cylinder_id"].ToString()),
                        cusid= Convert.ToInt32(dr["customer_id"].ToString()),
                        consnum=dr["consumer_number"].ToString(),
                        stfid= Convert.ToInt32(dr["staff_id"].ToString()),
                        amount= Convert.ToInt32(dr["total_amount"].ToString()),
                        date= dr["date"].ToString(),
                        bokmode= dr["booking_mode"].ToString(),
                        sts= dr["status"].ToString(),
                    };
                    boid_items.Add(item);
                }
                con.Close();
                return (boid_items);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public int cancelbooking(booked_detailsClass bokobj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_cancelBooking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookingid", bokobj.bookingid);
                con.Open();
               int s=  cmd.ExecuteNonQuery();
                con.Close();
                return (s);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public booked_detailsClass detailsforcancel(int id)
        {
            var getdata = new booked_detailsClass();
            try
            {
               
                SqlCommand cmd = new SqlCommand("sp_selectbookdetailforcancel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bid",id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    getdata = new booked_detailsClass()
                    {
                        bid = Convert.ToInt32(dr["booking_id"].ToString()),
                        clyid = Convert.ToInt32(dr["cylinder_id"].ToString()),
                        cusid = Convert.ToInt32(dr["customer_id"].ToString()),
                        consnum = dr["consumer_number"].ToString(),
                        stfid = Convert.ToInt32(dr["staff_id"].ToString()),
                        amount = Convert.ToInt32(dr["total_amount"].ToString()),
                        date = dr["date"].ToString(),
                        bokmode = dr["booking_mode"].ToString(),
                        sts = dr["status"].ToString(),
                    };

                }
                con.Close();
                return (getdata);
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public List<Deliveryclass> viewfulldata()
        {
            var fulldatas = new List<Deliveryclass>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_bookeddetailsforstaff", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var item = new Deliveryclass()
                    {
                        boid = Convert.ToInt32(dr["booking_id"].ToString()),
                        clyid = Convert.ToInt32(dr["cylinder_id"].ToString()),
                        cusid = Convert.ToInt32(dr["customer_id"].ToString()),
                        consnum = dr["consumer_number"].ToString(),
                        stfid = Convert.ToInt32(dr["staff_id"].ToString()),
                        amount = Convert.ToInt32(dr["total_amount"].ToString()),
                        daate = dr["date"].ToString(),
                        bokmode = dr["booking_mode"].ToString(),
                        sts = dr["status"].ToString(),
                    };
                    fulldatas.Add(item);
                }
                return (fulldatas);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public string deliverytabinsert(Deliveryclass dlsobj)
        {
            try
            {
                dlsobj.date= DateTime.Now.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand("sp_delivery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bid", dlsobj.bid);
                cmd.Parameters.AddWithValue("@staffid", dlsobj.staffid);
                cmd.Parameters.AddWithValue("@date", dlsobj.date);
                con.Open();
               int s= cmd.ExecuteNonQuery();
                con.Close();
                return "inserted succesfully";
                
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
           
        }
        public int filled(Deliveryclass dlsobj)
        {
            try
            {
                int filled = 0;
                SqlCommand cmd = new SqlCommand("sp_filled", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cyid", dlsobj.cylinderid);
                con.Open();
               SqlDataReader dr= cmd.ExecuteReader();
                while (dr.Read())
                {
                    filled =Convert.ToInt32(dr["filled"].ToString());
                }
                con.Close();
                return filled;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public int notfilled(Deliveryclass dlsobj)
        {
            try
            {
                int notfilled = 0;
                SqlCommand cmd = new SqlCommand("sp_notfilled", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cyid", dlsobj.cylinderid);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    notfilled = Convert.ToInt32(dr["not_filled"].ToString());
                }
                con.Close();
                return notfilled;
                
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }//sp_notfilled
        public string updatedstock(Deliveryclass dlsobj)
        {
            try
            {
                //dlsobj.filled =1- dlsobj.newfilled;
              //  dlsobj.notfilled =1+ dlsobj.newnotfilled;
                SqlCommand cmd = new SqlCommand("sp_updatestock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clyid", dlsobj.clyid);
                cmd.Parameters.AddWithValue("@filled", dlsobj.newfilled);
                cmd.Parameters.AddWithValue("@notfilled", dlsobj.newnotfilled);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Done";
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }//sp_bookingtableupdate
        public string updatebbokibgstst(Deliveryclass dlsobj)
        {
            try
            {
                dlsobj.status = "DELIVERED";
                SqlCommand cmd = new SqlCommand("sp_bookingtableupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bid ", dlsobj.bid);
                cmd.Parameters.AddWithValue("@sts", dlsobj.status);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "done";

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
            
        }
    }
}
