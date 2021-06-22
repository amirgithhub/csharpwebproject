using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {




        if (Request["cmd"] != null)
        {
            if (Request["cmd"] == "add" || Request["cmd"] =="delete")
            {

                if ((Request["cmd"] == "add") && (Request["productID"] != null))
                {
                    int tardocid = Convert.ToInt32(Request["productID"]);

                    ShoppingCartActions sca = new ShoppingCartActions();
                    sca.AddToCart(tardocid);
                    Response.Write("okadd");
                    Response.End();
                }


                if (Request["cmd"] == "delete")
                {

                    if (Request["cmd"] == "delete")
                    {
                        int deledocid = Convert.ToInt32(Request["productID"]);

                        ShoppingCartActions sca = new ShoppingCartActions();
                        sca.DeleteFromCart(deledocid);
                      
                        Response.Write("okdelete");
                        Response.End();

                    }
                
                
                
                }


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
}