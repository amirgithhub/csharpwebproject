using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contactus : System.Web.UI.Page
{
    public string distler = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DocList"] != null)
        {


            distler = "block";
        }
        else
        { distler = "none"; }





















    }
    protected void shopbtn_Click(object sender, EventArgs e)
    {
      











        try
        {


            string mailFrom = @"amir.imen@elmiran.org";
            string mailTo = @"elmiran.info@yahoo.com";
            string mailSubject = @"Darkhast Tamas from Elmiran.Org";
            string smtpHost = @"webmail.elmiran.org";
            string mailPassword = @"7uRrko94Z5";
            string smtpPort = "587";

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(new System.Net.Mail.MailAddress(mailTo));
            mail.From = new System.Net.Mail.MailAddress(mailFrom);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            
            
         
         
          
            mail.Subject = mailSubject;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           string middle= @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
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
		<span style='font-family: tahoma, geneva, sans-serif;'>مدیر محترم سایت علم ایران،<br />
		با شما تماس زیر انجام شده است.</span></p>";




            middle +=
	

	@"<p dir='rtl' style='margin-right: 20px;'>
		<span style='font-family: tahoma, geneva, sans-serif;'>"+ " نام،<br />"
+TextBox1.Text+"</span></p>";

              middle +=
	

	@"<p dir='rtl' style='margin-right: 20px;'>
		<span style='font-family: tahoma, geneva, sans-serif;'>"+ " ایمیل،<br />"
+TextBox2.Text+"</span></p>";
              middle +=
	

	@"<p dir='rtl' style='margin-right: 20px;'>
		<span style='font-family: tahoma, geneva, sans-serif;'>"+ " موبایل،<br />"
+TextBox3.Text+"</span></p>";
              middle +=
	

	@"<p dir='rtl' style='margin-right: 20px;'>
		<span style='font-family: tahoma, geneva, sans-serif;'>"+ " متن،<br />"
+TextBox5.Text+"</span></p>";

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


            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";

            TextBox5.Text = "";


        }
        catch (Exception e1)
        {

            Response.Write(e1.Message);
        }























    }
}