using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fileupload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["logm"] != null)
        {




            mydataclassDataContext db = new mydataclassDataContext();


        Member mb = new Member();
            mb = ((Member)(Session["logm"]));

            var test = (from rows in db.Members
                        where ((rows.member_user.Equals(mb.member_user)) && (rows.member_pass.Equals(mb.member_pass)))
                    select rows).FirstOrDefault();

        if(test != null)
        {
        
        
        }
            else
            {
            
            Response.Redirect("default.aspx", true);
            
            }
        
        
        }
        else
        {
            Response.Redirect("default.aspx", true);
        
        
        
        }




    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        string path = Server.MapPath(".") + "\\pdffilesxaldfp\\";
        string fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);


//        application/msword===> .doc
//application/vnd.openxmlformats-officedocument.wordprocessingml.document==> .docx
        // application/pdf===> .pdf




        if ((FileUpload1.PostedFile.ContentType == "application/msword") || (FileUpload1.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document") || (FileUpload1.PostedFile.ContentType == "application/pdf"))
        {

            FileUpload1.PostedFile.SaveAs(path + fn);
        }



        else { 
        
 Label1.Text = "File Type Error. Only .doc   or .docx or .pdf";
          
            return;
        
        }



           






    }



    protected void Button2_Click(object sender, EventArgs e)
    {

        Session.Remove("logm");
        Session.Abandon();
        Response.Redirect("default.aspx",true);

       
    }
}