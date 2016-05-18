using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.Controls;
using Pkurg.PWorldBPM.Common;

public partial class Modules_Countersign_Countersign_Group : UControlBase
{
    public Modules_Countersign_Countersign_Group()
    {
        IsCanEdit = true;
    }
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];

    protected void Page_Load(object sender, EventArgs e)
    {
        //LoggerR.logger.DebugFormat("CounterSignDeptId:{0}", CounterSignDeptId);
        if (string.IsNullOrEmpty(CounterSignDeptId))
        {
            CounterSignDeptId = PKURGICode;
        }

        if (!IsPostBack)
        {
            InitCountersignCheckboxlist();
        }


        if (string.IsNullOrEmpty(ColumnCount))
        {
            ColumnCount = "8";
        }
        chklCountersignDept.RepeatColumns = int.Parse(ColumnCount);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (_BPMContext.ProcInst == null//未提交
            || _BPMContext.ProcInst.Status == "0"//提交还未发起
            || _BPMContext.ProcInst.Status == "4")//提交被打回
        {
            this.chklCountersignDept.Enabled = true;
        }
        else
        {
            if (K2_TaskItem != null)
            {
                if (K2_TaskItem.ActivityInstanceDestination.Name == "流程审核员审核" || K2_TaskItem.ActivityInstanceDestination.Name == "发起申请" 
                    || K2_TaskItem.ActivityInstanceDestination.Name == "部门负责人意见" || K2_TaskItem.ActivityInstanceDestination.Name == "集团部门负责人意见")
                {
                    this.chklCountersignDept.Enabled = true;
                    return;
                }
            }
            this.chklCountersignDept.Enabled = false;
        }
    }

    private void InitCountersignCheckboxlist()
    {
        //LoggerR.logger.Debug("InitCountersignCheckboxlist");

        if (_BPMContext.ProcInst == null || _BPMContext.InstDataInfo == null || _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Count == 0)
        {
            //根据当前部门ID调取会签部门
            // DisplayMessage.ExecuteJs(string.Format("alert('{0}');",_BPMContext.InstDataInfo._CountsignInfo.Infos.Count));
            //LoggerR.logger.DebugFormat("_BPMContext.ProcInst == null:{0}", _BPMContext.ProcInst == null);
            if (_BPMContext.InstDataInfo == null)
            {
                _BPMContext.InstDataInfo = new Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo();
            }

            _BPMContext.InstDataInfo.GroupCountsignInfo = new Pkurg.PWorldBPM.Common.Info.CounterSignInfo()
            {
                Infos = new List<Pkurg.PWorldBPM.Common.Info.CounterSignDeptInfo>()
            };

            List<Pkurg.PWorld.Entities.Department> depts = WF_CounterSign.GetCountSignDeptInfosByCreater(CounterSignDeptId);
            foreach (var item in depts)
            {
                _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Add(new Pkurg.PWorldBPM.Common.Info.CounterSignDeptInfo()
                {
                    DeptInfo = new Pkurg.PWorldBPM.Common.Info.DepartmentInfo()
                    {
                        Id = item.DepartCode,
                        Name = item.DepartName,
                    },
                    IsChecked = IsDefaultCheckedDepartment(item.DepartName)
                });
            }

            //LoggerR.logger.Debug("CreateCountersignInfos");
        }

        BindData();
    }

    private bool IsDefaultCheckedDepartment(string departmentName)
    {
        if (string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(DefaultCheckdDepartments))
        {
            return false;
        }
        return DefaultCheckdDepartments.ToLower().Split(',').ToList().Contains(departmentName.ToLower());
    }

    private void BindData()
    {
        //LoggerR.logger.DebugFormat("BindData:{0}", _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Count);

        chklCountersignDept.Items.Clear();

        foreach (var item in _BPMContext.InstDataInfo.GroupCountsignInfo.Infos)
        {
            if (IsHiddenDepartment(item.DeptInfo.Name))
            {
                continue;
            }
            chklCountersignDept.Items.Add(new ListItem()
            {
                Text = item.DeptInfo.Name,
                Value = item.DeptInfo.Id,
                Selected = item.IsChecked,
                Enabled = IsCanEdit && IsDisableDepartment(item.DeptInfo.Name)//item.IsEnable &&
            });
        }

    }

    private bool IsDisableDepartment(string departmentName)
    {
        if (string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(DisableDepartments))
        {
            return true;
        }
        return !DisableDepartments.ToLower().Split(',').ToList().Contains(departmentName.ToLower());
    }

    private bool IsHiddenDepartment(string departmentName)
    {
        if (string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(HiddenDepartments))
        {
            return false;
        }
        return HiddenDepartments.ToLower().Split(',').ToList().Contains(departmentName.ToLower());
    }


    /// <summary>
    /// 会签数据提交
    /// </summary>
    public void Submit()
    {
        if (K2_TaskItem != null)
        {
            System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
            data.Add("CounterSignUsers_Group", GetCounterSignUsers());
            WorkflowHelper.UpdateDataFields(_BPMContext.Sn, data);
        }

    }



    /// <summary>
    /// 会签数据保存
    /// </summary>
    public void SaveData()
    {
        if (_BPMContext.InstDataInfo == null)
        {
            _BPMContext.InstDataInfo = new Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo();
        }
        _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Clear();

        foreach (ListItem item in chklCountersignDept.Items)
        {
            _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Add(new Pkurg.PWorldBPM.Common.Info.CounterSignDeptInfo()
            {
                DeptInfo = new Pkurg.PWorldBPM.Common.Info.DepartmentInfo()
                {
                    Id = item.Value,
                    Name = item.Text
                },
                IsChecked = item.Selected,
                IsEnable = item.Enabled
            });
        }

        if (!(Page is UPageBase))
        {
            _BPMContext.SaveExtend();
        }

        //同步到数据库
        SyncToDB();
    }
    /// <summary>
    /// 会签数据保存
    /// </summary>
    public void SaveData(bool isBasePage)
    {

        if (_BPMContext.InstDataInfo == null)
        {
            _BPMContext.InstDataInfo = new Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo();
        }
        _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Clear();

        foreach (ListItem item in chklCountersignDept.Items)
        {
            _BPMContext.InstDataInfo.GroupCountsignInfo.Infos.Add(new Pkurg.PWorldBPM.Common.Info.CounterSignDeptInfo()
            {
                DeptInfo = new Pkurg.PWorldBPM.Common.Info.DepartmentInfo()
                {
                    Id = item.Value,
                    Name = item.Text
                },
                IsChecked = item.Selected,
                IsEnable = item.Enabled
            });
        }

        if (isBasePage)
        {
            _BPMContext.SaveExtend();
        }

        //同步到数据库
        SyncToDB();
    }
    
    private void SyncToDB()
    {
        try
        {
            List<WF_CountersignParameter> paramInfos = new List<WF_CountersignParameter>();
            foreach (ListItem item in chklCountersignDept.Items)
            {
                if (item.Selected)
                {
                    paramInfos.Add(new WF_CountersignParameter()
                    {
                        CountersignDeptCode = item.Value,
                        CountersignDeptName = item.Text,
                        CreateByUserCode = _BPMContext.CurrentPWordUser.DepartCode,
                        CreateByUserName = _BPMContext.CurrentPWordUser.DepartName,
                        SeCode = 1//集团会签
                    });
                }
            }
            WF_CounterSign.SyncDataToDB(_BPMContext.ProcID, paramInfos);
        }
        catch (Exception ex)
        {
        }
    }

    public void SaveAndSubmit()
    {
        SaveData();
        Submit();
    }

    public string Result
    {
        get { return getResult(); }
    }

    private string getResult()
    {
        StringBuilder content = new StringBuilder();
        for (int i = 0; i < chklCountersignDept.Items.Count; i++)
        {
            ListItem item = chklCountersignDept.Items[i];
            if (item.Selected)
            {
                content.AppendFormat("{0},", item.Value);
            }
        }
        return content.ToString().TrimEnd(',');
    }

    public string GetCounterSignUsers()
    {
        //return @"founder\zhangweixing,founder\zybpmadmin";
        StringBuilder content = new StringBuilder();
        for (int i = 0; i < chklCountersignDept.Items.Count; i++)
        {
            ListItem item = chklCountersignDept.Items[i];
            if (item.Selected)
            {
                BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
                DataTable dt = bfurd.GetSelectRoleUser(item.Value, "部门负责人");
                foreach (DataRow dr in dt.Rows)
                {
                    content.AppendFormat(@"Founder\{0},", dt.Rows[0]["LoginName"].ToString());
                }
            }
        }
        return content.ToString().TrimEnd(',');
    }

    /// <summary>
    /// 会签部门Id
    /// </summary>
    public string CounterSignDeptId { get; set; }

    /// <summary>
    /// 显示列数
    /// </summary>
    public string ColumnCount { get; set; }

    protected override void LoadViewState(object savedState)
    {
        Object[] datas = savedState as Object[];

        if (datas[1] != null)
        {
            CounterSignDeptId = datas[1].ToString();
        }
        if (datas[2] != null)
        {
            ColumnCount = datas[2].ToString();
        }
        if (datas[3] != null)
        {
            DisableDepartments = datas[3].ToString();
        }
        if (datas[4] != null)
        {
            DefaultCheckdDepartments = datas[4].ToString();
        }
        base.LoadViewState(datas[0]);
    }

    protected override object SaveViewState()
    {
        return new Object[]
        {
            base.SaveViewState(),
            CounterSignDeptId,
            ColumnCount,
            DisableDepartments,
            DefaultCheckdDepartments
        };
    }

    public string DisableDepartments { get; set; }
    public string DefaultCheckdDepartments { get; set; }
    public string HiddenDepartments { get; set; }

    /// <summary>
    /// 调用后，使用户选择不同意直接返回
    /// 默认：用户选择不同意，流程等待其他用户审批完成
    /// </summary>
    public bool NoWaitOthers(int wfId)
    {
        System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
        data.Add("CounterSignNum_Group", "0");
        return WorkflowHelper.UpdateDataFieldsByWorkflowId(wfId, data);
    }

    public override bool SaveControlData()
    {
        SaveData(true);
        return true;
    }
}