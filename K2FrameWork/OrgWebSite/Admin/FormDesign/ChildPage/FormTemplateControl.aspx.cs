using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrgWebSite.Admin.FormDesign.ChildPage
{
    public partial class FormTemplateControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfFormTemplateID.Value = Request.QueryString["ID"];

            //Response.Write("<TABLE style='WIDTH: 100%' border=1 jQuery17208094799997630725='87'><TBODY><TR><TD id=cell00 class='drag droppable' jQuery17208094799997630725='88' entered='false'><INPUT class=easyui-validatebox width=120 height=20 validType='text'></TD><TD id=cell01 class='drag droppable' jQuery17208094799997630725='89' entered='false'><INPUT class=easyui-validatebox width=120 height=20 validType='text'></TD><TD id=cell02 class='drag droppable' jQuery17208094799997630725='90' entered='false'><INPUT class=easyui-validatebox width=120 height=20 validType='text'></TD></TR><TR><TD id=cell10 class='drag droppable' jQuery17208094799997630725='91' entered='false'><INPUT class=easyui-validatebox width=120 height=20 validType='number'></TD><TD id=cell11 class='drag droppable' jQuery17208094799997630725='92' entered='false'><INPUT class=easyui-validatebox value=hgjgyugy width=120 height=20 validType='email'></TD><TD id=cell12 class='drag droppable' jQuery17208094799997630725='93' entered='false'></TD></TR></TBODY></TABLE>");
        }
    }
}