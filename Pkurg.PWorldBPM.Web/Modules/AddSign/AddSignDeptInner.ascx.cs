using System;
using System.Data;
using System.Linq;
using Pkurg.PWorldBPM.Common;

public partial class Modules_AddSign_AddSignDeptInner : UControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name.EndsWith("会签"))
        {
            this.Visible = true;
            DeptId = GetDeptId(K2_TaskItem.ActivityInstanceDestination.Name.EndsWith("集团会签"));
        }
        else
        {
            this.Visible = false;
        }

        //DeptId = _BPMContext.CurrentPWordUser.DepartCode;

    }

    private string GetDeptId(bool isGroup)
    {
        Pkurg.PWorld.Business.Permission.BFPmsUserRoleDepartment bfurd = new Pkurg.PWorld.Business.Permission.BFPmsUserRoleDepartment();

        if (_BPMContext.InstDataInfo != null)
        {
            if (!isGroup && (_BPMContext.InstDataInfo._CountsignInfo == null || _BPMContext.InstDataInfo._CountsignInfo.Infos == null)
                || isGroup && (_BPMContext.InstDataInfo.GroupCountsignInfo == null || _BPMContext.InstDataInfo.GroupCountsignInfo.Infos == null))
            {
                return _BPMContext.CurrentPWordUser.DepartCode;
            }

        }
        else
        {
            return _BPMContext.CurrentPWordUser.DepartCode;
        }


        foreach (var item in isGroup ? _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Where(x => x.IsChecked).ToList() : _BPMContext.InstDataInfo._CountsignInfo.Infos.Where(x => x.IsChecked).ToList())
        {
            try
            {
                DataTable dt = bfurd.GetSelectRoleUser(item.DeptInfo.Id, "部门负责人");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["EmployeeCode"].ToString() == _BPMContext.CurrentPWordUser.EmployeeCode)
                        return item.DeptInfo.Id;
                }
            }
            catch
            {
                continue;
            }
        }
        return _BPMContext.CurrentPWordUser.DepartCode;
    }
    protected override object SaveControlState()
    {
        return new Object[] { base.SaveControlState(), DeptId };
    }
    protected override void LoadControlState(object savedState)
    {
        Object[] objs = savedState as Object[];
        DeptId = objs[1].ToString();
        base.LoadControlState(objs[0]);
    }
    public string DeptId { get; set; }
    protected override void OnPreRender(EventArgs e)
    {
        div_content.InnerHtml = string.Format("<a  href='#' onclick='AddSignDeptInner(\"{0}\",\"{1}\",\"{2}\")'>部门内处理</a>", ProcId, _BPMContext.Sn, DeptId);
        base.OnPreRender(e);
    }
    //public void BeginAddSign(string instId,string sn)
    //{
    //    DisplayMessage.ExecuteJs(string.Format("AddSign('{0}','{1}')", instId,sn));
    //}
}