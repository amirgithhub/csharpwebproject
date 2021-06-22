using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class paperpay : System.Web.UI.Page
{

    public string distler = "";
    int tp;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            txtPrice.Attributes.Add("readonly", "readonly");

        }
        
        
        
       
    }
    protected void shopbtn_Click(object sender, EventArgs e)
    {


       


            mydataclassDataContext db= new mydataclassDataContext();
         
       
        //moshakhasate moshtary por shavad
        Pay_Infor pi = new Pay_Infor();
      
        pi.customer_name = TextBox1.Text.Trim();
        pi.email = TextBox2.Text.Trim();
        pi.mobile = TextBox3.Text.Trim();



        int geyt = Int32.Parse(txtPrice.Text);

      


        try
        {
            PayLine Pay = new PayLine();

            //http://localhost/editedelmiran.org/peperpaycomplete.aspx
            //

            string urlcode = HttpUtility.HtmlDecode("http://www.elmiran.org/peperpaycomplete.aspx");

            //   9724c-99260-f13d7-e67f5-49bb6d28e0d2ab33e073f7de9b22

            string result = Pay.Send("http://payline.ir/payment/gateway-send", "9724c-99260-f13d7-e67f5-49bb6d28e0d2ab33e073f7de9b22", geyt, urlcode);

            //result if equal bigger that 1 is haman id_get else is error
            //result=-2 if pool kamtar az sad toman bashad


            PersianCalendar pc = new PersianCalendar();
            string sdate = "";

            if (pc.GetDayOfMonth(DateTime.Now) < 10)
                sdate += "0" + pc.GetDayOfMonth(DateTime.Now).ToString();
            else
                sdate += pc.GetDayOfMonth(DateTime.Now).ToString();

            if (pc.GetMonth(DateTime.Now) < 10)
                sdate += "/0" + pc.GetMonth(DateTime.Now).ToString();
            else
                sdate += "/" + pc.GetMonth(DateTime.Now).ToString();

            sdate += "/" + pc.GetYear(DateTime.Now).ToString();


            pi.stamp = sdate;

            pi.description = result;
            pi.totalprice = geyt;


            int j = int.Parse(result);
          
             
              


  if (j>0)
            {

              

            
  mydataclassDataContext ds = new mydataclassDataContext();
              // string cod=  HttpUtility.HtmlEncode("http://payline.ir/payment/gateway-"+ result);

               string cod = "http://payline.ir/payment/gateway-" + result;

               ds.Pay_Infors.InsertOnSubmit(pi);

               ds.SubmitChanges();
          

         
                

                   Customer_productlist cpl = new Customer_productlist();

                   cpl.Document_id_FK = 1079;
                   cpl.Customer_id_FK = pi.id;
                   db.Customer_productlists.InsertOnSubmit(cpl);
                   db.SubmitChanges();
               


      


              // Response.Write("خطا در برقراری ارتباط");
              // Labelb.Text = "خطا در برقراری ارتباط";
       
               Response.Redirect(cod,false);
             //   Response.End();
            }
            else
            {
                Response.Write("خطا در برقراری ارتباط");
                Labelb.Text="خطا در برقراری ارتباط";
                Response.Redirect("default.aspx");
                Session.RemoveAll();
                Response.End();
            }
        }
        catch(Exception rt)
        {
           string v = rt.Message;
            Response.Write("خطا در برقراری ارتباط");
            Labelb.Text="خطا در برقراری ارتباط";
            Response.End();
        }
    }


}

   
