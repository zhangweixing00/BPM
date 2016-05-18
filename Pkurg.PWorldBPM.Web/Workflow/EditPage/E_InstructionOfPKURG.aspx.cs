using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Xml;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_EditPage_E_InstructionOfPKURG : System.Web.UI.Page
{
    public string className = "Workflow_EditPage_E_InstructionOfPKURG";
    public string ContractID = null;
    //public Employee CurrentEmployee = new Employee();
    public WF_InstructionOfPKURG wf_Instruction = new WF_InstructionOfPKURG();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    Employee currentEmployee = new Employee();

    public Employee CurrentEmployee
    {
        get
        {
            return Session["CurrentEmployee"] as Employee;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    //get worklists
        //    Worklist li = WorkflowHelper.GetWorklistItem(Page.User.Identity.Name);

        //    foreach (WorklistItem lit in li)
        //    {
        //        //显示的单号
        //        //lit.ProcessInstance.Folio
        //    }
        //}

        string methodName = "Page_Load";
        string currentUser = new IdentityUser().GetEmployee().LoginId;
        ViewState["loginName"] = currentUser;
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        if (!IsPostBack)
        {
            UploadAttachments1._BPMContext.LoginId = currentUser;
           
            BFEmployee bfEmployee = new BFEmployee();
            EmployeeAdditional employeeaddInfo = bfEmployee.GetEmployeeAdditionalByLoginName(currentUser);

            currentEmployee = bfEmployee.GetEmployeeByEmployeeCode(employeeaddInfo.EmployeeCode);
            WorkflowHelper.CurrentUser = currentUser;


            Session["CurrentEmployee"] = currentEmployee;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                ViewState["FormID"] = info.FormId;
                InintData();
            }
            else
            {
                ContractID = BPMHelp.GetSerialNumber("SQ_"); 
                tbNumber.Text = ContractID;
                ViewState["FormID"] = ContractID;
                tbPerson.Text = CurrentEmployee.EmployeeName;
                tbDepartName.Text = CurrentEmployee.DepartName;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                Countersign1.CounterSignDeptId = CurrentEmployee.DepartCode;
            }
            InintLeader();
        }
        //Countersign1.SimulateUser = ViewState["loginName"].ToString();
        //FlowRelated1.SimulateUser = ViewState["loginName"].ToString();

        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }
    private void InintData()
    {
        string methodName = "InintData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            InstructionOfPkurg instructionOfPkurg = wf_Instruction.GetInstructionOfPkurgById(ViewState["FormID"].ToString());

            cblSecurityLevel.SelectedValue = instructionOfPkurg.SecurityLevel.ToString();
            cblUrgentLevel.SelectedValue = instructionOfPkurg.UrgenLevel != null ? instructionOfPkurg.UrgenLevel.ToString() : "0";
            UpdatedTextBox.Value = ((DateTime)instructionOfPkurg.Date).ToString("yyyy-MM-dd");
            tbPerson.Text = instructionOfPkurg.UserName;
            tbDepartName.Text = instructionOfPkurg.DeptName;
            tbPhone.Text = instructionOfPkurg.Mobile;
            tbTheme.Text = instructionOfPkurg.ReportTitle;
            tbContent.Text = instructionOfPkurg.ReportContent;
            instructionOfPkurg.ReportTitle = tbTheme.Text;//?
            tbNumber.Text = instructionOfPkurg.ReportCode;

            //初始化勾选框
            XmlDocument xmldoc = new XmlDocument();
            if (!string.IsNullOrEmpty(instructionOfPkurg.LeaderSelect))
            {
                xmldoc.LoadXml(instructionOfPkurg.LeaderSelect);
            }
            //经办部门负责人
            if (xmldoc.SelectSingleNode("//DeptManager") != null)
            {
                if (xmldoc.SelectSingleNode("//DeptManager").OuterXml.Length > 21)
                {
                    chkDeptManager.Checked = true;
                }
                else { chkDeptManager.Checked = false; }
            }
            else { chkDeptManager.Checked = false; }
            cbIsReport.Checked=instructionOfPkurg.IsReport==1?true:false;

            //查询已经添加的附件
            //UploadFilesUC1.BindAttachmentListByCode(ViewState["FormID"].ToString());
            WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(instructionOfPkurg.FormId);
            FlowRelated1.ProcId = workFlowInstance.InstanceId;
            Countersign1.ProcId = workFlowInstance.InstanceId;
            UploadAttachments1.ProcId = workFlowInstance.InstanceId;
            hfInstanceId.Value = workFlowInstance.InstanceId;
            #region 审批意见框
            ApproveOpinionUCDeptleader.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUCRealateDept.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUCLeader.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUCCEO.InstanceId = workFlowInstance.InstanceId;
            #endregion
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private InstructionOfPkurg SaveInstructionOfPkurg(string ID, string wfStatus)
    {
        string methodName = "SaveInstructionOfPkurg";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        bool result = false;
        InstructionOfPkurg instructionOfPkurg = null;
        try
        {

            instructionOfPkurg = wf_Instruction.GetInstructionOfPkurgById(ID);

            bool isEdit = false;
            if (instructionOfPkurg == null)
            {
                instructionOfPkurg = new InstructionOfPkurg();
                instructionOfPkurg.FormId = ViewState["FormID"].ToString();

                instructionOfPkurg.ReportCode = ViewState["FormID"].ToString();

                instructionOfPkurg.ApproveStatus = wfStatus;

                instructionOfPkurg.CreateByUserCode = CurrentEmployee.EmployeeCode;
                instructionOfPkurg.CreateAtTime = DateTime.Now;
                instructionOfPkurg.CreateByUserName = CurrentEmployee.EmployeeName;

            }
            else
            {
                isEdit = true;
                instructionOfPkurg.FormId = ViewState["FormID"].ToString();
                instructionOfPkurg.OriginalFormId = ViewState["FormID"].ToString();
                instructionOfPkurg.ApproveStatus = wfStatus;

                instructionOfPkurg.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                instructionOfPkurg.UpdateByUserName = CurrentEmployee.EmployeeName;

            }
            if (cblSecurityLevel.SelectedIndex != -1)
            {
                instructionOfPkurg.SecurityLevel = short.Parse(cblSecurityLevel.SelectedValue);
            }
            if (cblUrgentLevel.SelectedIndex != -1)
            {
                instructionOfPkurg.UrgenLevel = short.Parse(cblUrgentLevel.SelectedValue);
            }
            instructionOfPkurg.Date = DateTime.Parse(UpdatedTextBox.Value);
            instructionOfPkurg.UserName = tbPerson.Text;
            instructionOfPkurg.DeptName = tbDepartName.Text;

            instructionOfPkurg.Mobile = tbPhone.Text;
            instructionOfPkurg.ReportTitle = tbTheme.Text;
            instructionOfPkurg.ReportContent = tbContent.Text;
            instructionOfPkurg.LeaderSelect=SaveSelectedSignLeader();
            instructionOfPkurg.IsReport=(byte)(cbIsReport.Checked==true?1:0);
            if (!isEdit)
            {
                result = wf_Instruction.AddInstructionOfPkurg(instructionOfPkurg);
            }
            else
            {
                result = wf_Instruction.UpdateInstructionOfPkurg(instructionOfPkurg);
            }

        }
        catch (Exception ex)
        {

            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }

        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
        return instructionOfPkurg;
    }
    private bool SaveWorkFlowInstance(InstructionOfPkurg instructionOfPkurg, string WfStatus, DateTime? SumitTime, string WfInstanceId)
    {
        string methodName = "SaveInstructionOfPkurg";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        bool result = false;
        WorkFlowInstance workFlowInstance = null;
        try
        {
            workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(instructionOfPkurg.FormId);
            bool isEdit = false;
            if (workFlowInstance == null)
            {
                workFlowInstance = new WorkFlowInstance();
                workFlowInstance.InstanceId = Guid.NewGuid().ToString();
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "1002";
            }
            else
            {
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                isEdit = true;
            }
            workFlowInstance.FormId = instructionOfPkurg.FormId;
            workFlowInstance.FormTitle = instructionOfPkurg.ReportTitle;
            workFlowInstance.WfStatus = WfStatus;
            if (SumitTime != null)
            {
                workFlowInstance.SumitTime = SumitTime;
            }

            if (WfInstanceId != "")
            {
                workFlowInstance.WfInstanceId = WfInstanceId;
            }

            if (!isEdit)
            {
                result = wf_WorkFlowInstance.AddWorkFlowInstance(workFlowInstance);
            }
            else
            {
                result = wf_WorkFlowInstance.UpdateWorkFlowInstance(workFlowInstance);
            }
            FlowRelated1.ProcId = workFlowInstance.InstanceId;
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign1.SaveData();//会签数据保存
        }
        catch (Exception ex)
        {

            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }

        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
        return result;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();

        InstructionOfPkurg instructionOfPkurg = SaveInstructionOfPkurg(id, "00");

        if (instructionOfPkurg != null)
        {
            UploadAttachments1.SaveAttachment(id);

           

            if (SaveWorkFlowInstance(instructionOfPkurg, "0", null, ""))
            {
                WebCommon.Show(this, Resources.Message.SaveSucess);
                //Response.Redirect("~/Workflow/EditPage/InstructionList.aspx", false);
            }

        }
        else
        {
            WebCommon.Show(this, Resources.Message.SaveFail);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["BackUrl"].ToString(), false);
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        #region 工作流参数
        //get users by role
        BFEmployee employee = new BFEmployee();
        EmployeeAdditional employeeadd = employee.GetEmployeeAdditionalByLoginName("xupc");

        Employee em = employee.GetEmployeeByEmployeeCode(employeeadd.EmployeeCode);//get user info

        //get activity destination users
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDept = bfurd.GetSelectRoleUser(em.DepartCode, "部门负责人");
        DataTable dtCheck = bfurd.GetSelectRoleUser(em.CompanyCode, "流程审核人");
        //BFCountersignRoleDepartment CountersignRole= new BFCountersignRoleDepartment();
        //DataTable dtContri = CountersignRole.GetSelectCountersignDepartment(em.CompanyCode);
        DataTable dtlead = bfurd.GetSelectRoleUser(em.DepartCode, "主管总裁");
        DataTable dtCEO = bfurd.GetSelectRoleUser(em.CompanyCode, "CEO");

        //add datafields
        NameValueCollection dataFields = new NameValueCollection();
        //部门负责人
        if (chkDeptManager.Checked)
        {
            dataFields.Add("DeptManager", "K2:Founder\\" + dtDept.Rows[0]["LoginName"].ToString());
        }
        else
        {
            dataFields.Add("DeptManager", "noapprovers");
        }
        //流程审核人
        dataFields.Add("WFM", "K2:Founder\\" + dtCheck.Rows[0]["LoginName"].ToString());
        //会签
        //dataFields.Add("CounterSignUsers", "K2:Founder\\" + dtContri.Rows[0]["LoginName"].ToString());
        //部门主管领导
        dataFields.Add("leaders", "K2:Founder\\" + dtlead.Rows[0]["LoginName"].ToString());
        //CEO
        dataFields.Add("CEO", "K2:Founder\\" + dtCEO.Rows[0]["LoginName"].ToString());
        //触发新流程
        if(cbIsReport.Checked)
        {
            dataFields.Add("NewFlow_SP", "wf_usp_CreateNewForm" );
        }
        #endregion

        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        Countersign1.SaveData();//会签数据保存

        InstructionOfPkurg instructionOfPkurg = SaveInstructionOfPkurg(id, "02");
        if (instructionOfPkurg != null)
        {
            UploadAttachments1.SaveAttachment(id);
            Countersign1.SaveAndSubmit();//会签数据保存

            WorkflowHelper.StartProcess(@"K2Workflow\InstructionOfPKURG", id, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance(instructionOfPkurg, "1", DateTime.Now, wfInstanceId.ToString()))
                {
                    if (wf_Instruction.UpdateStatus(id, "02", wfInstanceId.ToString()))
                    {
                        string Opinion = "";
                        string ApproveResult = "同意";
                        string OpinionType = "";
                        string IsSign = "0";
                        string DelegateUserName = "";
                        string DelegateUserCode = "";
                        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(instructionOfPkurg.FormId);

                        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
                        {
                             ApprovalID = Guid.NewGuid().ToString(),
                
                            FormID = id,
                            InstanceID = workFlowInstance.InstanceId,
                            Opinion = Opinion,
                            ApproveAtTime = DateTime.Now,
                            ApproveByUserCode = CurrentEmployee.EmployeeCode,
                            ApproveByUserName = CurrentEmployee.EmployeeName,
                            ApproveResult = ApproveResult,
                            OpinionType = OpinionType,
                            CurrentActiveName = "拟稿",
                            ISSign = IsSign,
                
                            DelegateUserName = DelegateUserName,
                            DelegateUserCode = DelegateUserCode,
                            CreateAtTime = DateTime.Now,
                            CreateByUserCode = CurrentEmployee.EmployeeCode,
                            CreateByUserName = CurrentEmployee.EmployeeName,
                            UpdateAtTime = DateTime.Now,
                            UpdateByUserCode = CurrentEmployee.EmployeeCode,
                            UpdateByUserName = CurrentEmployee.EmployeeName,
                            FinishedTime = DateTime.Now
                        };
                        BFApprovalRecord bfApproval = new BFApprovalRecord();
                        bfApproval.AddApprovalRecord(appRecord);

                        //WebCommon.Show(this, Resources.Message.SubmitSucess);
                        //Response.Redirect("~/Workflow/ToDoWorkList.aspx", false);
                    }
                }
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功'); window.close();", true);

        //WorkflowHelper.ApproveProcess
        //insert data to business object
    }
    protected void Archive_Click(object sender, EventArgs e)
    {
        //因为Web 是多线程环境，避免甲产生的文件被乙下载去，所以档名都用唯一 
        //string fileName = ViewState["FormID"].ToString();
        //string ServerUrl = System.Configuration.ConfigurationManager.AppSettings["ServerUrl"];
        ////string ServerUrl = @"http://" + Request.Url.Authority;
        //string url = ServerUrl + @"/Workflow/ViewPage/V_InstructionOfPKURG.aspx?id=" + Request.QueryString["id"];
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "<script>alert('" + url + "')</script>");
        //string folder = System.Configuration.ConfigurationManager.AppSettings["ArchiveFolder"] + @"\InstructionOfPKURG\" + fileName + ".pdf";
        //try
        //{
        //    string dllstr = HttpContext.Current.Server.MapPath(@"/") + "wkhtmltopdf\\bin\\wkhtmltopdf.exe";
        //    Process p = System.Diagnostics.Process.Start(dllstr, url + "/ac " + " " + folder);
        //    //若不加这一行，程序就会马上执行下一句而抓不到文件发生意外：System.IO.FileNotFoundException: 找不到文件 ''。 
        //    p.WaitForExit();
            
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}

        //因为Web 是多线程环境，避免甲产生的文件被乙下载去，所以档名都用唯一 
        string fileNameWithOutExtention = "导出pdf测试" + Guid.NewGuid().ToString();

        //执行wkhtmltopdf.exe 
        string dllstr = HttpContext.Current.Server.MapPath(@"/") + "wkhtmltopdf\\bin\\wkhtmltopdf.exe";
        Process p = System.Diagnostics.Process.Start(dllstr, @"http://www.sina.com.cn/ac E:\" + fileNameWithOutExtention + ".pdf");

        //若不加这一行，程序就会马上执行下一句而抓不到文件发生意外：System.IO.FileNotFoundException: 找不到文件 ''。 
        p.WaitForExit();

        #region"档案系统调用
        //DbUtilService.DbUtilService db = new DbUtilService.DbUtilService();
        //InstructionOfPkurg Item = wf_Instruction.GetInstructionOfPkurgById(ViewState["FormID"].ToString());
        //string strDateYear = Item.Date.ToString().Substring(0, 4);//年度
        //string strDateMonth = Item.Date.ToString().Substring(4, 2);//月份
        //string strEndTime = DateTime.Now.ToString();//日期
        //string strReportTitle = Item.ReportTitle;
        //string strReportManagerName = Item.CreateByUserName;
        //string strReportContent = Item.ReportContent;

        //string strLoginId = @"founder\xupc";//域帐号
        //string strLoginIdToUpper = strLoginId.ToUpper();//1、域帐号大写转换（例如：hold\heyanfei->HOLD\HEYAFEI）
        //string[] arrConvert = strLoginIdToUpper.Split('\\'); //2、域帐号字符替换（例如：HOLD\HEYAFEI->HOLD_HEYAFEI）
        //string strLoginIdConvert = arrConvert[0] + "_" + arrConvert[1];

        //string strdeptId = db.getBMIDByADUserName(strLoginIdConvert);//传入档案系统的部门ID“根据域帐号转换”(对应：“BMID”)
        //string strQzh = "F";
        //if (strdeptId.Contains("_"))
        //{
        //    strQzh = strdeptId.Split('_')[0];//ID标识“根据档案系统的部门ID截取”(对应：“DID”)
        //}

        ////插入dfile4(第一步)
        //string strTempdfile4 = "0&;1&;0&;1&;"
        //                     + strdeptId + "&;"  //传入档案系统的部门ID(BMID)
        //    //+ "F_&;"//(测试数据)传入档案系统的部门ID(BMID)
        //                     + "-1&;"
        //                     + "0&;"
        //                     + strQzh + "&;" //ID标识(DID)
        //    //+ "F&;"//(测试数据)ID标识(DID)
        //                     + strReportTitle + "&;"
        //                     + strReportManagerName + "&;"//责任者(OA系统 经办人/发起人)
        //                     + Convert.ToDateTime(strEndTime).ToString("yyyy-MM-dd") + "&;"
        //                     + "1&;"
        //                     + DateTime.Now.ToString("yyyy-MM-dd") + "&;"
        //                     + strDateYear + "&;"
        //                     + strDateMonth + "&;"
        //                     + strReportContent + "&;"
        //                     + strLoginIdConvert;//创建者
        //int iPID = db.addField("bdzy_dfile4.xml", strTempdfile4);//pid(对应文件级返回的int值“对应：pid”)

        ////ftp上传(第二步)
        ////string formFullPath = @" E:\" + fileName + ".pdf";
        //FileStream fsForm = new FileStream(folder, FileMode.Open);
        //byte[] InputHtml = new byte[fsForm.Length]; //2进制
        //fsForm.Read(InputHtml, 0, InputHtml.Length);
        //fsForm.Close();

        //WebClient request = new WebClient();
        //System.Text.UTF8Encoding converter = new System.Text.UTF8Encoding();
        //string ftpUser = System.Configuration.ConfigurationManager.AppSettings["ftpUser"];//ftp用户名
        //string ftpPassWord = System.Configuration.ConfigurationManager.AppSettings["ftpPassWord"];//ftp密码
        //string ftpDir = System.Configuration.ConfigurationManager.AppSettings["ftpDir"];//ftp路径
        //request.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
        //string strFtpPATHNAME = "/" + "OAFiles" + "/" + DateTime.Now.Year + "/";//对应“FTP存放地址（PATHNAME）”
        //string HtmlPath = strFtpPATHNAME + Item.FormId + ".pdf";//ftp上传的html地址         
        //request.UploadData(ftpDir + HtmlPath, InputHtml);

        ////插入efile4(第三步)
        //string strTempEfile4 = "0&;0&;0&;"
        //                        + strLoginIdConvert + "&;"//创建者(默认为ROOT)
        //                        + "0&;"
        //                        + iPID + "&;"//pid(对应文件级返回的int值)
        //                        + Item.ReportCode + ".pdf" + "&;"//电子文件名(不重复文件名)
        //                        + Item.FormId.ToString() + "&;"//实际文件名不含后缀(不包含后缀的实际文件名称)" 
        //                        + "html&;"//后缀名
        //                        + "data&;"//配置名
        //                        + strFtpPATHNAME;//FTP存放地址（PATHNAME）
        //db.addField("bdzy_efile4.xml", strTempEfile4);

        //Pkurg.BPM.Entities.TList<Pkurg.BPM.Entities.Attachment> attachmentTable = new BPM_Attachment().GetAttachmentByFormID(Item.FormId);
        //if (attachmentTable.Count() > 0)
        //{
        //    foreach (Pkurg.BPM.Entities.Attachment att in attachmentTable)
        //    {
        //        //ftp上传(第一步)
        //        string fileFullPath = Server.MapPath(@"~"+att.Url);
        //        FileStream fs = new FileStream(fileFullPath, FileMode.Open);
        //        byte[] fileBinarys = new byte[fs.Length]; //2进制
        //        fs.Read(fileBinarys, 0, fileBinarys.Length);
        //        fs.Close();

        //        request.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
        //        string strFtpAttPATHNAME = "/" + "OAFiles" + "/" + DateTime.Now.Year + "/";//“对应：FTP存放地址（PATHNAME）”
        //        string[] arrAtt = att.AttachmentName.Split('.');
        //        string strPostfix = arrAtt[arrAtt.Length - 1];//截取文件后缀名称“对应：后缀名”
        //        string strAttName = Item.FormId + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + strPostfix;//“对应：电子文件名”（Item.ProcID+"-"+时间+后缀名）
        //        string AttPath = strFtpAttPATHNAME + strAttName;//ftp上传的附件地址
        //        request.UploadData(ftpDir + AttPath, fileBinarys);

        //        //插入efile4(第二步)
        //        string strfileNmae = att.AttachmentName.Substring(0, att.AttachmentName.Length - (strPostfix.Length + 1));//截取文件名称“对应：实际文件名”
        //        string strTempAttEfile4 = "0&;0&;0&;"
        //                                + strLoginIdConvert + "&;"//创建者(默认为ROOT)
        //                                + "0&;"
        //                                + iPID + "&;"//pid(对应文件级返回的int值)
        //                                + strAttName + "&;"//电子文件名(包含后缀的不重复文件名)
        //                                + strfileNmae + "&;"//实际文件名不含后缀(不包含后缀的实际文件名称)
        //                                + strPostfix + "&;"//后缀名
        //                                + "data&;"//配置名
        //                                + strFtpAttPATHNAME;//FTP存放地址（PATHNAME）
        //        db.addField("bdzy_efile4.xml", strTempAttEfile4);
        //    }
        //}
        #endregion
    }
    //protected void Print_Click(object sender, EventArgs e)
    //{
    //    string url = "/Workflow/PrintPage/P_InstructionOfPKURG.aspx?FormID=" + ViewState["FormID"].ToString() + "&InstanceId=" + hfInstanceId.Value;
    //    WebCommon.ShowUrl(this.Page, url);
    //}

    protected void InintLeader()
    {
        #region 工作流参数
        //get users by role
        BFEmployee employee = new BFEmployee();
        string currentUser = Page.User.Identity.Name.ToLower().Replace(@"founder\", "");
        EmployeeAdditional employeeadd = employee.GetEmployeeAdditionalByLoginName(currentUser);
        Employee em = employee.GetEmployeeByEmployeeCode(employeeadd.EmployeeCode);//get user info

        //get activity destination users
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDept = bfurd.GetSelectRoleUser(em.DepartCode, "部门负责人");
        DataTable dtCheck = bfurd.GetSelectRoleUser(em.CompanyCode, "流程审核人");
        DataTable dtlead = bfurd.GetSelectRoleUser(em.DepartCode, "主管总裁");
        DataTable dtCEO = bfurd.GetSelectRoleUser(em.CompanyCode, "CEO");

        lbDeptmanager.Text = dtDept.Rows[0]["EmployeeName"].ToString();
        
        #endregion
    }

    protected string SaveSelectedSignLeader()
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);

        //从“相关部门分管领导”到“集团董事长审批”中的步骤的所有参与审判的用户列表
        List<string> ApproverList = new List<string>();
        string LeaderTemp = string.Empty;
        string FinalLeaderTemp = string.Empty;

        BFEmployee employee = new BFEmployee();
        string currentUser = Page.User.Identity.Name.ToLower().Replace(@"founder\", "");
        EmployeeAdditional employeeadd = employee.GetEmployeeAdditionalByLoginName(currentUser);
        Employee em = employee.GetEmployeeByEmployeeCode(employeeadd.EmployeeCode);//get user info

        //get activity destination users
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDept = bfurd.GetSelectRoleUser(em.DepartCode, "部门负责人");

        //部门负责人
        if (chkDeptManager.Checked)
        {
            XmlElement xmleDeptManager = xmldoc.CreateElement("DeptManager");
            xmleLeaders.AppendChild(xmleDeptManager);

            LeaderTemp = dtDept.Rows[0]["LoginName"].ToString();
            if (LeaderTemp.Length > 0)
            {
                foreach (string L in LeaderTemp.Split(','))
                {
                    if (L != "")
                    {
                        ApproverList.Add(L);
                    }
                }
            }
            xmleDeptManager.SetAttribute("ID", LeaderTemp);
        }
        return xmleLeaders.OuterXml;
    }
}
