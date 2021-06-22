using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddToCart : System.Web.UI.Page
{
    public string distler = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["DocList"] != null)
            {


                distler = "block";
            }
            else
            { distler = "none"; }

            mydataclassDataContext db = new mydataclassDataContext();

            // List<int> cartlist = new List<int>();

            // List<int> DocItems_ids2 = new List<int> { 2, 3, 45, 1010,1009 };


            List<int> DocItems_ids2 = ((List<int>)Session["Doclist"]);


            if (DocItems_ids2 != null)
            {
                var DocItem = (from rows in db.Documents
                               where DocItems_ids2.Contains(rows.id)
                               select new { rows.id, rows.name, rows.price }).ToList();


                //<a href="#" onclick="functiongoeshere(); return false;">click</a>
                if (DocItem != null)
                {
                    int totalprice = 0;
                    string _h = "";
                    _h += "<tr style='text-align: right;'><th></th><th>کد پژوهش</th><th> عنوان </th><th>قیمت(ریال)</th></tr>";
                    for (int i = 0; i < DocItem.Count(); i++)
                    {
                        _h += "<tr><td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'>حذف</a></td><td>{0}</td><td>{1}</td><td>{2}</td></tr>";
                        _h = _h.Replace("{0}", DocItem[i].id.ToString()).Replace("{1}", DocItem[i].name).Replace("{2}", DocItem[i].price.ToString()).Replace("{7}", "delete").Replace("{6}", DocItem[i].id.ToString());

                        if (DocItem[i].price != null)
                            totalprice += (int)DocItem[i].price;

                    }


                    Label1.Text = _h;



                    string resl = "  قیمت کل مجموعه برابر  ";

                    resl += "<strong>" + totalprice.ToString() + "</strong>";
                    resl += " ریال می باشد  ";

                    Label2.Text = resl;
                    //}

                }

            }


        }



    }









    protected void shopbtn_Click(object sender, EventArgs e)
    {

        Server.Transfer("shops.aspx");

    }




}