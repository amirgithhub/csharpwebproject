using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class home : System.Web.UI.Page
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
        mydataclassDataContext dbdoc = new mydataclassDataContext();



        int ct = dbdoc.Documents.Count();
        if (ct > 17)
        { ct = ct - 17; }
        else
        { ct = 0; }
        var query3 = (from rows in dbdoc.Documents
                      orderby rows.id ascending
                      select new
                      {
                          rows.id,
                          rows.name,
                          rows.type,
                          rows.subject,
                          rows.price,
                          rows.countdownload,
                          rows.countpage,
                          rows.countview
                      }).Skip(ct).Take(17).OrderByDescending(x => x.id);





        // string s = js.Serialize(query3);

        int limt = query3.ToList().Count();
        var ji = query3.ToList();


        string temp = "";
        string _h = "<br>";//= "<table border='1px' style='dir:rtl;'>";
        //  _h += "<tr><td></td><td></td><td></td></tr>";
        for (int i = 0; (i < 17) && (i < limt); i++)
        {
            if (ji[i].name != null) temp = ji[i].name; else temp = "بدون عنوان ";
            if ((i % 2) == 0)
            {
        
                _h += "<tr><th class = 'theeven' colspan='6'>{0}</th><td></td></tr><tr><td>تعداد صفحه: {1}</td><td>دفعات بازدید: {2} </td><td>قیمت: {9} ریال </td><td><a href='srcresult.aspx?cmd=search&id={8}'> مشاهده مشخصات </a></td><td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall.png' alt=&quot;اضافه به سبد خرید&quot;/></a></td></tr>";
                //<td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall.png'/></a></td>
                //<td>تعداد دریافت {3}  </td>
                _h = _h.Replace("{0}", temp).Replace("{1}", ji[i].countpage.ToString()).Replace("{2}", ji[i].countview.ToString()).Replace("{8}", ji[i].id.ToString()).Replace("{7}", "add").Replace("{6}", ji[i].id.ToString()).Replace("{9}", ji[i].price.ToString());

                //.Replace("{7}", "add").Replace("{6}", ji[i].id.ToString())

            }
            else
            {

                _h += "<tr><th class = 'theodd' colspan='6'>{0}</th><td></td></tr><tr><td>تعداد صفحه: {1}</td><td>دفعات بازدید: {2} </td><td>قیمت: {9} ریال </td><td><a href='srcresult.aspx?cmd=search&id={8}'> مشاهده مشخصات </a></td><td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall2.png' alt=&quot;اضافه به سبد خرید&quot;/></a></td></tr>";
                //<td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall.png'/></a></td>
                //<td>تعداد دریافت {3}  </td>
                _h = _h.Replace("{0}", temp).Replace("{1}", ji[i].countpage.ToString()).Replace("{2}", ji[i].countview.ToString()).Replace("{8}", ji[i].id.ToString()).Replace("{7}", "add").Replace("{6}", ji[i].id.ToString()).Replace("{9}", ji[i].price.ToString());

                //.Replace("{7}", "add").Replace("{6}", ji[i].id.ToString())





            }


        }

        Label1.Text = _h;

    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        mydataclassDataContext db = new mydataclassDataContext();

        string us = TextBox1.Text;
        string kalameo = TextBox2.Text;

        var pers = (from rows in db.Members
                    where ((rows.member_user.Equals(us)) && (rows.member_pass.Equals(kalameo)))
                    select rows).FirstOrDefault();
        if (pers == null)
        { Response.Redirect("default.aspx", true); }
        else
        {

            Session.Add("logm", pers);
            Response.Redirect("fileupload.aspx", true);
        }

    }
}