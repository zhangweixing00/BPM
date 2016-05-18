using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
///CheckBoxForReport 的摘要说明
/// </summary>
namespace CustomControl
{
    public class CheckBoxForReport : CheckBox
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (HttpContext.Current.Request["id"] == null)
            {
                if (HttpContext.Current.Request["isReport"]!=null&&HttpContext.Current.Request["isReport"].ToUpper() == "Y")
                {
                    this.Enabled = false;
                    this.Checked = true;
                }

            }
            else
            {
                var instanceInfo = DBContext.GetSysContext().WF_WorkFlowInstance.FirstOrDefault(x => x.InstanceID == HttpContext.Current.Request["id"]);
                var context = DBContext.GetBizContext();
                switch (instanceInfo.AppID)
                {
                    case "10109":
                        var bizInfo_ContractApproval = context.ERP_ContractApproval.FirstOrDefault(x => x.FormID == instanceInfo.FormID);
                        if (bizInfo_ContractApproval.IsForceSelected.HasValue && bizInfo_ContractApproval.IsForceSelected == 1)
                        {
                            this.Enabled = false;
                            this.Checked = true;
                        }
                        break;
                    case "10107":
                        var bizInfo_Instruction = context.ERP_Instruction.FirstOrDefault(x => x.FormID == instanceInfo.FormID);
                        if (bizInfo_Instruction.IsForceSelected.HasValue && bizInfo_Instruction.IsForceSelected == 1)
                        {
                            this.Enabled = false;
                            this.Checked = true;
                        }
                        break;
                    case "2004":
                        var bizInfo_SupplementalAgreement = context.ERP_SupplementalAgreement.FirstOrDefault(x => x.FormID == instanceInfo.FormID);
                        if (bizInfo_SupplementalAgreement.IsForceSelected.HasValue && bizInfo_SupplementalAgreement.IsForceSelected == 1)
                        {
                            this.Enabled = false;
                            this.Checked = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void SaveToDB(string FormId, string AppID)
        {
            //var appInfo = DBContext.GetSysContext().WF_AppDict.FirstOrDefault(x => x.AppId == AppID);
            //string tableName = Path.GetFileNameWithoutExtension(appInfo.FormName);
            //Type lType = Assembly.GetExecutingAssembly().GetType(tableName);
            //    var context = DBContext.GetBizContext();
            //    context.(lType).FirstOrDefault(x => x.FormId == FormId);
            if (HttpContext.Current.Request["isReport"] == "Y")
            {
                var context = DBContext.GetBizContext();
                switch (AppID)
                {
                    case "10109":
                        var bizInfo_ContractApproval = context.ERP_ContractApproval.FirstOrDefault(x => x.FormID == FormId);
                        bizInfo_ContractApproval.IsForceSelected = 1;
                        break;
                    case "10107":
                        var bizInfo_Instruction = context.ERP_Instruction.FirstOrDefault(x => x.FormID == FormId);
                        bizInfo_Instruction.IsForceSelected = 1;
                        break;
                    case "2004":
                        var bizInfo_SupplementalAgreement = context.ERP_SupplementalAgreement.FirstOrDefault(x => x.FormID == FormId);
                        bizInfo_SupplementalAgreement.IsForceSelected = 1;
                        break;
                    default:
                        break;
                }
                context.SubmitChanges();
            }
        }
    }
}