using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Linq.Expressions;
using LinqKit;
using System.Text.RegularExpressions;
public partial class process : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        mydataclassDataContext dbdoc = new mydataclassDataContext();


        string cm = Request["cmd"];
    
        string rawsearch = Request["txtsearch"];
        string doc_type = Request["tydoc"];
        string doc_subject = Request["subj"];
        if (doc_type == "0")
        { doc_type = null; }
        if (doc_subject == "0")
        { doc_subject = null; }
   

        if (rawsearch.Trim().Length > 1)
        {
            string[] srchar = Regex.Split(rawsearch, " +");

            srchar = srchar.Where(p => p.Length > 2).ToArray();
       

            if (srchar.Length > 0)
            {
                if (Request["cmd"] == "document")
                {



                    var newKids = xc.ContainsInDescription(srchar);


                    var query3 =
                             (from p in dbdoc.Documents.Where(newKids)
                              select new { p.id, p.name, p.subject, p.type ,p.countdownload,p.countpage,p.countview});


                    if (!string.IsNullOrEmpty(doc_subject))
                    {
                        query3 = from p in query3
                                 where p.subject.Equals(doc_subject)
                                 select p;
                    }

                    if (!string.IsNullOrEmpty(doc_type))
                    {
                        query3 = from q in query3
                                 where q.type.Equals(doc_type)
                                 select q;



                    }

                    var q3 = query3.ToList();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    if (query3 != null)
                    {
                        string s = js.Serialize(q3);
                        Response.Write(s);
                        Response.End();
                    }
                    else
                    {
                        Response.Write("errore akhar");
                        Response.End();

                    }
                }
            }
            else
            {

                Response.Write("errore akhar");
                Response.End();



            }

        }
    }

    public class xc
    {
              public static Expression<Func<Document, bool>> ContainsInDescription(
                                                      params string[] keywords)
        {
            var predicate = PredicateBuilder.False<Document>();
            foreach (string keyword in keywords)
            {
                string temp = keyword;
                predicate = predicate.Or(p => p.name.Contains(temp));
            }
            return predicate;
        }
    }

}













