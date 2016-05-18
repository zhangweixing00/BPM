using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Portal_AddressBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDepts();
            BindList(); 
        }
    }



    private void BindDepts()
    {
        DataTable dt = new Pkurg.PWorldBPM.Business.Portal.AddressList().GetCompanyList();
        TreeNode root = new TreeNode { Text = "北大资源", Value = "-1", ToolTip = "北大资源" };
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            root.ChildNodes.Add(new TreeNode { Text = dt.Rows[i]["DepartName"].ToString(), Value = dt.Rows[i]["DepartCode"].ToString(), ToolTip = dt.Rows[i]["Remark"].ToString() });
        }
        tvDepts.Nodes.Add(root);
    }

    private void BindList()
    {
        int pageIndex = this.AspNetPager1.CurrentPageIndex;
        int pageSize = this.AspNetPager1.PageSize;
        int count = 0;
        string employeeName = txtEmployeeName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string companyCode = txtCompanyCode.Text.Trim();
        string departCode = txtDeptCode.Text.Trim();
        string telephone = txtTelephone.Text.Trim();
        string mobilePhone = txtMobilePhone.Text.Trim();


        DataTable dt = new Pkurg.PWorldBPM.Business.Portal.AddressList().GetAddressList(pageIndex, pageSize, out count,
            companyCode, departCode, employeeName, email, telephone, mobilePhone);
        this.AspNetPager1.RecordCount = count;
        lblCount.Text = count.ToString();
        rptList.DataSource = dt;
        rptList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindList();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = 1;
        BindList();
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblOfficePhone = (Label)e.Item.FindControl("lblOfficePhone");
            Label lblMobilePhone = (Label)e.Item.FindControl("lblMobilePhone");
            Label lblDepartName = (Label)e.Item.FindControl("lblDepartName");
            Label lblCompanyName = (Label)e.Item.FindControl("lblCompanyName");

            DataRowView row = (DataRowView)e.Item.DataItem;

            if (row["EmployeeCode"].ToString() == "D0002" || row["EmployeeCode"].ToString() == "D0001")
            {
                lblOfficePhone.Text = "";
                lblMobilePhone.Text = "";
            }

            if (row["EmployeeCode"].ToString() == "D0001")
            {
                lblDepartName.Text = "";
            }

            //员工名字
            //王天娇、余兰、亢伟不显示公司和职位
            string employeeCode = row["EmployeeCode"].ToString();
            if (employeeCode == "D0120" || employeeCode == "D0011" || employeeCode == "D0004")
            {
                lblCompanyName.Text = "";
                lblDepartName.Text = "";
            }
        }
    }

    protected void tvDepts_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode selectedNode = this.tvDepts.SelectedNode;
        string code = selectedNode.Value;

        if (selectedNode.Depth == 0)
        {
            txtCompanyCode.Text = "";
            txtDeptCode.Text = "";
        }
        else if (selectedNode.Depth == 1 || selectedNode.Depth == 2)
        {
            txtCompanyCode.Text = code;
            txtDeptCode.Text = "";
        }
        else if (selectedNode.Depth > 2)
        {
            txtDeptCode.Text = code;
        }

        lblCompanyName.Text = selectedNode.ToolTip;
        if (selectedNode.ChildNodes.Count == 0)
        {
            DataTable dt = new Pkurg.PWorldBPM.Business.Portal.AddressList().GetDeptList(code);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectedNode.ChildNodes.Add(new TreeNode { Text = dt.Rows[i]["DepartName"].ToString(), Value = dt.Rows[i]["DepartCode"].ToString(), ToolTip = dt.Rows[i]["Remark"].ToString() });
            }
        }
        //BindList
        this.AspNetPager1.CurrentPageIndex = 1;
        BindList();
    }

    /// <summary>
    /// 获取职位
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected string GetPosition(object obj)
    {
        
        DataRowView row = (DataRowView)obj;

        //员工名字
        //王天娇、余兰、亢伟不显示公司和职位
        string employeeCode = row["EmployeeCode"].ToString();
        if (employeeCode == "D0120" || employeeCode == "D0011" || employeeCode == "D0004")
        {
            return "";
        }

        //公司
        string companyName = row["CompanyName"].ToString();
        //编制类型
        string payroll_type_desc = row["payroll_type_desc"] == null ? "" : row["payroll_type_desc"].ToString();
        //职级
        string positionLevel = row["PositionLevel"] == null ? "" : row["PositionLevel"].ToString();

        string position_desc = row["position_desc"].ToString();
        string str = row["position_desc"].ToString();

        //“二级公司”为“北大资源集团控股有限公司”、“北大资源集团有限公司”、“北大资源集团商业有限公司”
        bool company = companyName == "北大资源集团投资有限公司" || companyName == "北大资源集团控股有限公司" || companyName == "北大资源集团有限公司" || companyName == "北大资源集团商业有限公司" || companyName == "北大资源集团文化艺术传播（北京）有限公司";
        //“编制类型”为“正式”、“临时（退休返聘）”、“临时（外包）”、、“临时（其他）”
        bool type = payroll_type_desc == "正式" || payroll_type_desc == "临时（退休返聘）" || payroll_type_desc == "临时（外包）" || payroll_type_desc == "临时（其他）";
        //“职级”为“助理 专员 主管 业务经理 部门副经理 部门经理 部门副总监”
        bool level = positionLevel == "助理" || positionLevel == "专员" || positionLevel == "主管" || positionLevel == "业务经理" || positionLevel == "部门副经理" || positionLevel == "部门经理" || positionLevel == "部门副总监";

        //职员
        if (company && type && level)
        {
            return "职员";
        }

        //E-HR里本身的职位名称
        string leads = "部门总监,部门副总经理,部门助理总经理,部门总经理,公司助理总经理,公司副总经理,公司助理总裁,公司副总裁,公司常务副总经理,公司常务副总裁,公司总经理,公司总裁,公司副董事长,公司董事长,董事,";
        bool lead = leads.Contains(positionLevel + ",");
        if (company && type && lead)
        {
            //“总监”
            if (positionLevel == "部门总监")
            {
                return "总监";
            }
            return position_desc;
        }
        //实习生
        bool shixisheng = payroll_type_desc == "临时（实习）";
        if (shixisheng)
        {
            return "实习生";
        }
        return str;
    }
}