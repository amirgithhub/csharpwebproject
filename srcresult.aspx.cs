using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class srcresult : System.Web.UI.Page
{
    public string distler = " ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            if (Session["DocList"] != null)
            {


                distler = "block";
            }
            else
            { distler = "none"; }

            if ((Request["cmd"] != null) && (Request["id"] != null))
            {

                mydataclassDataContext db = new mydataclassDataContext();
                int doc_id = Convert.ToInt32(Request["id"]);

                var doc = (from rows in db.Documents
                           where rows.id.Equals(doc_id)
                           select rows).FirstOrDefault();

                ///////////////////////////////////// ezafe kardane bazdid


                if (doc != null)
                {

                    int? has = doc.countview;


                    if (has == null)
                    {
                        doc.countview = 1;

                        doc.countdownload = 1;

                    }





                    else
                    {

                        doc.countview++;
                    }

                    db.SubmitChanges();



                }
                else
                {

                    Response.Redirect("default.aspx", true);
                }




                string ty = "";

                ty = doc.type.ToString();
                switch (ty)
                {
                    case "1": ty="پایان نامه"; break;
                    case "2":ty="مقاله";break;
                    case "3": ty="تحقیق";break;
                    case "4":ty="ترجمه";break;
                    default: ty = "--"; break;

                }





                /////////////////////////////////////////////////////////////////////////

                string z = "<a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuy.png'/></a><br />";
                z = z.Replace("{7}", "add").Replace("{6}", doc.id.ToString());
                string h = " ";
                for (int i = 0; i < 9; i++)
                {


                    h += "<p class='pager'>>{0}<</p>";
                    h = string.Format(h, i + ":");

                }

                h = h.Replace(">0:<", "<h2>" + doc.name + "</h2>").Replace(">1:<", "<h3>" + z + "</h3>").Replace(">2:<", "<b>" + "چکیده" + "</b>" + "<br>" + doc.chekide).Replace(">3:<", "<b>" + "فهرست مطالب" + "</b>" + "<br>" + doc.fehrest + "</br>").Replace(">4:<", "<b>" + "  تعداد صفحات: " + "</b>" + doc.countpage.ToString()).Replace(">5:<", "<b>" + "  تعدادبازدید: " + "</b>" + doc.countview.ToString()).Replace(">6:<", "<b>" + " تعداد دانلود:    " + "</b>" + doc.countdownload.ToString()).Replace(">7:<", "<b>" + " قیمت:  " + "</b>" + doc.price.ToString() + " ریال  ").Replace(">8:<", "<b>" + " نوع پژوهش:  " + "</b>" + ty);

                Label1.Text = h;


              

                Label3.Text = z;
            }
        }
    }
    protected void shopbtn_Click(object sender, EventArgs e)
    {
            
          
            Server.Transfer("shops.aspx");
         
    }
}