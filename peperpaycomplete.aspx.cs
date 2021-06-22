using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class peperpaycomplete : System.Web.UI.Page
{
  

    protected void Page_Load(object sender, EventArgs e)
    {
        mydataclassDataContext db = new mydataclassDataContext();



        try
        {//first try

            if ((Request.Form["trans_id"] != null) && (Request.Form["id_get"] != null))
            {//first if

                PayLine GetPayline = new PayLine();
                string trans_id = Request.Form["trans_id"];//>=1
                string id_get = Request.Form["id_get"];//>=1

                ShoppingCartActions sca = new ShoppingCartActions();
                var payavai = (from rows in db.Pay_Infors
                               where ((rows.id_get.Equals(id_get)) && rows.trans_id.Equals(trans_id))
                               select rows).FirstOrDefault();
                if (payavai == null)
                {




                }

                else
                {
                    // piz.description = " this tran_id or id_get is available in db";
                    Response.Redirect("default.aspx", false);
                    return;

                }

                Pay_Infor piz = (from rows in db.Pay_Infors
                                 where rows.description.Equals(id_get)
                                 select rows).FirstOrDefault();

                if (piz == null)
                {
                    Response.Redirect("default.aspx");
                    Response.End();
                    return;
                }

                piz.trans_id = Convert.ToInt64(trans_id);
                piz.id_get = Convert.ToInt64(id_get);



                //"9724c-99260-f13d7-e67f5-49bb6d28e0d2ab33e073f7de9b22"
                //adxcv-zzadq-polkjsad-opp13opoz-1sdf455aadzmck1244567

                string result = GetPayline.Get("http://payline.ir/payment/gateway-result-second", "9724c-99260-f13d7-e67f5-49bb6d28e0d2ab33e073f7de9b22", trans_id, id_get);

                piz.result = Convert.ToInt32(result);
               

                db.SubmitChanges();



                






       

                //switch (result)
                //{
                //    case "1": Response.Write(" pardakht anjam shod,:)"); break;
                //    case "-1": Response.Write(" sending api does not match"); break;

                //    case "-2": Response.Write(" price is not numerical or is lower that sad toman"); break;

                //    case "-3": Response.Write("cannot redirect to Null! "); break;
                //    case "-4": Response.Write("NO success has happen!:( "); break;

                //}

                if (Convert.ToInt32(result) == 1)//email send 
                {
                    if ((piz.trans_id > 0) && (piz.id_get > 0))
                       

           Label1.Text = @"از پرداخت آنلاین شما متشکریم. مقالات ISI درخواست شده بزودی به ایمیل شما فرستاده خواهد شد.
                                                        در صورت هر گونه مشکل با ما تماس بگیرید.";



                    string mailFrom = @"Info@Elmiran.org";

                    string mailTo = @"elmiran.info@yahoo.com";
                    string mailSubject = @"ISI payment by customer";
                    string smtpHost = @"webmail.elmiran.org";
                    string mailPassword = @"aD7632!Gf";
                    string smtpPort = "587";

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.To.Add(new System.Net.Mail.MailAddress(mailTo));
                    mail.From = new System.Net.Mail.MailAddress(mailFrom);
                    mail.BodyEncoding = System.Text.Encoding.UTF8;







                    mail.Subject = mailSubject;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    string middle = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
</head>

<body>
<p align='left' style='width:90%'>
	<img src='http://elmiran.org/default_files/logo.png' /></p>
<div align='right' style='width:90%; border:solid 1px #c7c7c7'>
	<br />
	<p dir='rtl' style='margin-right: 20px;'>&nbsp;
		</p>
	<p dir='rtl' style='margin-right: 20px;'>
		<span style='font-family: tahoma, geneva, sans-serif;'>مدیر محترم،<br />
		پژوهشگر زیر مبلغی برای دریافت مقالات آی اس آی واریز نموده است. با توجه به اینکه هر مقاله هزار تومان می باشد آنها ار ارسال کنید.</span></p>";

                    middle += piz.email;
                     middle+=@"<br/>";
                    middle += piz.totalprice;
                    middle += @"<br/>";
                    middle += piz.customer_name;
                    middle += @"<br/>";
                    middle+=" شماره تراکنش";
middle+= piz.trans_id;
                    middle += @"<br/>
                   


</div>
</body>
</html>";

                    mail.Body = middle;




                    //////////////////////////////////////////////////////////////////////////////////////////////////////////            
                    mail.IsBodyHtml = true;
                    mail.Priority = System.Net.Mail.MailPriority.High;

                    var smtp = new System.Net.Mail.SmtpClient
                    {
                        Host = smtpHost,
                        //EnableSsl = true,                        
                        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Timeout = 200000,
                        Credentials = new System.Net.NetworkCredential(mailFrom, mailPassword),
                    };

                    smtp.Port = Int32.Parse(smtpPort);


                    smtp.Send(mail);
                    mail.Dispose();
                    smtp.Dispose();

                    Session.RemoveAll();












                    

                }//result==1
                else
                {
                    Label1.Text = "تراکنش با مشکل مواجه شده است در صورت مشکل با ما تماس بگیرید";
                    // Session.RemoveAll();

                    //string s = "<br><a href=shoppingcart.aspx>" + "شروع مجدد فرآیند خرید</a>";




                    string s = "";

                    s += "<br>";
                    Label2.Text = s;

                }

            }//first if
            else
            {
                Response.Redirect("default.aspx", false);
                return;

            }
        }//first try
        catch (Exception e1)
        {
            Label1.Text = "اشکال در سیستم، در صورت پرداخت وجه با شما تماس خواهد گرفته شد";
            Session.RemoveAll();

            //  string s = "<br><a href=shoppingcart.aspx>" + "شروع مجدد فرآیند خرید</a>";

            string s;

            string mailFrom = @"amir.imen@elmiran.org";
            string mailTo = @"amir.imen@gmail.com";
            string mailSubject = @"Gesmate catch shopresdown";
            string smtpHost = @"webmail.elmiran.org";
            string mailPassword = @"7uRrko94Z5";
            string smtpPort = "587";

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(new System.Net.Mail.MailAddress(mailTo));
            mail.From = new System.Net.Mail.MailAddress(mailFrom);
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            mail.Subject = mailSubject;

            mail.Body = e1.Message + " error source is  " + e1.Source + e1.InnerException + " stack trace is  " + e1.StackTrace;


            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = smtpHost,
                //EnableSsl = true,                        
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Timeout = 200000,
                Credentials = new System.Net.NetworkCredential(mailFrom, mailPassword),
            };
            smtp.Port = Int32.Parse(smtpPort);


            smtp.Send(mail);
            mail.Dispose();
            smtp.Dispose();

            //s += "<br>";
            //Label2.Text = s;
            Response.Write("Not Response");
            // Response.End();
        }


    }

}