using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class allsrcresult : System.Web.UI.Page
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

            int submosh = 0;
            int typemosh = 0;

            if ((Request["cmd"] != null) && (Request["subt"] != null))
            {

                submosh = Convert.ToInt32(Request["subt"]);

                mydataclassDataContext db = new mydataclassDataContext();




                if (submosh < 22)
                {
                    var res = from rows in db.Documents
                              where rows.subject.Equals(submosh)
                              select new
                              {
                                  rows.id,
                                  rows.name,
                                  rows.type,
                                  rows.subject,
                                  rows.price,
                                  rows.countdownload
                                  ,
                                  rows.countpage,
                                  rows.countview
                              };


                    int Total = res.Count();

                    if (Request["type"] != null && Request["type"] != "")
                    {
                        int tu = Int32.Parse(Request["type"]);

                        typemosh = Convert.ToInt32(Request["type"]); ;

                        res = (from rows in res
                               where rows.type.Equals(tu)
                               select rows);
                        Total = res.Count();


                    }

                    int index = 0;

                    if (Request["pagenumber"] != null)
                    {
                        string pn = Request["pagenumber"];
                        if (!String.IsNullOrWhiteSpace(pn))
                        { index = Int32.Parse(Request["pagenumber"]); }
                    }


                    int skiip = index * 10;//2

                    res = (from rows in res select rows).Skip(skiip).Take(10);

                    var resl = res.ToList();


                    ////////////////////////////link display


                    //  DocumentList.DataSource = res;
                    //   DocumentList.DataBind();

                    string _h = "<br>";//= "<table border='1px' style='dir:rtl;'>";
                    //  _h += "<tr><td></td><td></td><td></td></tr>";
                    for (int i = 0; i < resl.Count(); i++)
                    {
                        if ((i % 2) == 0)
                        {

                            _h += "<tr><th class = 'theeven' colspan='6'>{0}</th><td></td></tr><tr><td>تعداد صفحه: {1}</td><td>دفعات بازدید: {2} </td><td>قیمت: {9} ریال </td><td><a href='srcresult.aspx?cmd=search&id={8}'> مشاهده مشخصات </a></td><td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall.png'/></a></td></tr>";
                            //<td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall.png'/></a></td>
                            //<td>تعداد دریافت {3}  </td>
                            _h = _h.Replace("{0}", resl[i].name.ToString()).Replace("{1}", resl[i].countpage.ToString()).Replace("{2}", resl[i].countview.ToString()).Replace("{8}", resl[i].id.ToString()).Replace("{7}", "add").Replace("{6}", resl[i].id.ToString()).Replace("{9}", resl[i].price.ToString());

                            //.Replace("{7}", "add").Replace("{6}", resl[i].id.ToString())


                        }
                        else
                        {


                            _h += "<tr><th class = 'theodd' colspan='6'>{0}</th><td></td></tr><tr><td>تعداد صفحه: {1}</td><td>دفعات بازدید: {2} </td><td>قیمت: {9} ریال </td><td><a href='srcresult.aspx?cmd=search&id={8}'> مشاهده مشخصات </a></td><td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall2.png'/></a></td></tr>";
                            //<td><a href='' onclick='delclick(&quot;{7}&quot;,{6});return false;'> <img src='default_files/btnbuysmall.png'/></a></td>
                            //<td>تعداد دریافت {3}  </td>
                            _h = _h.Replace("{0}", resl[i].name.ToString()).Replace("{1}", resl[i].countpage.ToString()).Replace("{2}", resl[i].countview.ToString()).Replace("{8}", resl[i].id.ToString()).Replace("{7}", "add").Replace("{6}", resl[i].id.ToString()).Replace("{9}", resl[i].price.ToString());

                            //.Replace("{7}", "add").Replace("{6}", resl[i].id.ToString())





                        }



                    }




                    // _h += "</table>";

                    Label1.Text = _h;

                    ////////////////////////////////////////////////////number diplay




                    int PageCount = Total / 10;//5
                    if (PageCount * 10 < Total)//5
                        PageCount++;

                    int limt = res.ToList().Count();
                    string h = "";
                    var ji = res.ToList();
                    for (int i = 0; (i < PageCount); i++)//5
                    {

                        h += "<a  class='{5}' href=allsrcresult.aspx?cmd=search&pagenumber={1}&subt={2}&type={3}>" + "{0}" + "</a>";

                        if (i != index)
                            h = h.Replace("{5}", "resultspan");
                        else

                            h = h.Replace("{5}", "resultspanse");


                        h = h.Replace("{0}", (i + 1).ToString());

                        h = h.Replace("{1}", i.ToString());


                        h = h.Replace("{2}", submosh.ToString());




                        if (typemosh != 0)
                        {
                            h = h.Replace("{3}", typemosh.ToString());

                        }
                        else
                        {
                            h = h.Replace("{3}", "");
                            h = h.Replace("&type=", "");
                        }
                    }

                    if (PageCount > 1)
                        Label2.Text = h;

                }



                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////












            }










        }





    }

}