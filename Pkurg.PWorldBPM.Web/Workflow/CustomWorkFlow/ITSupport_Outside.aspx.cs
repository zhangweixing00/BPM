using System;
using System.Linq;

public partial class Workflow_CustomWorkFlow_ITSupport_Outside : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvList.DataSource = DBContext.GetBizContext().V_ITSupport_Catalog.
                OrderBy(x => x.Name).
                Select(x => new 
                { 
                    Name = x.Name, 
                    Link = string.Format("http://{0}/Workflow/EditPage/E_OA_ITSupport.aspx?type={1}", 
                    Request.Url.Authority, x.Id) 
                });
            gvList.DataBind();
        }
    }
}