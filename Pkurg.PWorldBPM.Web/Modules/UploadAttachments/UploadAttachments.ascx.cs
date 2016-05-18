using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.AttachmentMan;
using Pkurg.PWorldBPM.Common;

public partial class UploadAttachments : UControlBase
{
    public string className = "UserControls_UploadFilesUC";
    public string ContractID = null;
    //public Employee CurrentEmployee = new Employee();
    public BPM_Attachment cobll = new BPM_Attachment();

    private bool isOnlyRead = false;

    public bool IsOnlyRead
    {
        get { return isOnlyRead; }
        set { isOnlyRead = value; }
    }

    //private AppDict appInfo;

    public string AppName
    {
        get
        {
            AppDict appInfo = ViewState["AppInfo"] as AppDict;
            if (appInfo == null)
            {
                DisplayMessage.ExecuteJs("alert('未设置AppId或AppId不正确');");
                return "";
            }
            return appInfo.AppName;
        }
    }

    public string AppId
    {
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                DisplayMessage.ExecuteJs("alert('AppId不正确');");
                return;
            }
            ViewState["AppInfo"] = new Pkurg.BPM.Services.AppDictService().GetByAppId(value);
        }
    }


    public TList<Attachment> rList
    {
        get
        {
            if (ViewState["rList"] != null)
            {
                return ViewState["rList"] as TList<Attachment>;
            }
            return null;
        }
        set
        {
            ViewState["rList"] = value;
        }
    }

    public string uploadFilesSize
    {
        get;
        set;
    }

    public bool IsAllowDownload
    {
        get;
        set;
    }

    public int UploadFilesNumber
    {
        get
        {
            return rList.Count;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
            BindAttachList();
        }
        if (IsOnlyRead)
        {
            upFileUpload.Visible = false;
            btnUPload.Visible = false;
        }
    }

    private void LoadData()
    {
        if (_BPMContext.ProcInst != null)
        {
            rList = cobll.GetAttachmentByFormID(_BPMContext.ProcInst.FormId);
        }
    }
    public string GetRelativePath()
    {
        return string.Format("/UploadFile/{0}/", AppName);
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        //try
        //{
        if (string.IsNullOrEmpty(AppName))
        {

            return;
        }
        
        //判断上传类型文件
        string uploadFileName = upFileUpload.FileName;
        string fileName = uploadFileName;

        if (uploadFileName == "" || uploadFileName == null)
        {
            WebCommon.Show(this.Page, Resources.Message.AttachSelect);
            return;
        }

        string forbiddenFileTypes = "|.exe|.dll|.r|.eb|.Redlof|.Happytime|.s|.c|.IRCBot|.2C|.Smibag|.f|.Killer|";

        if (forbiddenFileTypes.IndexOf("|" + Path.GetExtension(uploadFileName) + "|") != -1)
        {
            WebCommon.Show(this.Page, Resources.Message.AttachFileType);
            return;
        }

        uploadFilesSize = this.upFileUpload.PostedFile.ContentLength.ToString();
        if (int.Parse(uploadFilesSize) > 52428800)
        {
            WebCommon.Show(this.Page, Resources.Message.AttachSize);
            return;
        }


        string strFileName  = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + uploadFileName.Replace("+", "").Replace("#", "").Replace("|", "").Replace("%", "");


        string ReturnStr = string.Empty;
        if (upFileUpload.FileName.Length > 0)
        {
            string abPath = System.Web.HttpContext.Current.Request.MapPath(GetRelativePath());
            if (!Directory.Exists(abPath))
            {
                Directory.CreateDirectory(abPath);
            }
            upFileUpload.SaveAs(abPath + strFileName);
        }
        else
        {
            strFileName = ReturnStr;
        }
        

        if (rList == null)
        {
            rList = new TList<Attachment>();
        }

        Attachment ca = new Attachment();
        ca.AttachmentId = Guid.NewGuid().ToString();
        ca.AttachmentName = fileName;
        ca.Url = GetRelativePath() + strFileName;
        ca.AttachmentSize = uploadFilesSize;
        ca.IsDel = 0;
        ca.CreateAtTime = DateTime.Now;
        ca.UpdateAtTime = DateTime.Now;
        ca.CreateByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode;
        ca.CreateByUserName = _BPMContext.CurrentPWordUser.EmployeeName;
        ca.FormId = "0";
        rList.Add(ca);
        //gvAttachment.Visible = true;
        BindAttachList();

        //}
        //catch { }
    }

    #region 保存附件
    public bool SaveAttachment(string formID)
    {
        TList<Attachment> rattlist = new BPM_Attachment().GetAttachmentByFormID(formID);
        if (rList == null)
        {
            return true;
        }
        foreach (Attachment att in rList)
        {
            if (att.FormId == "0")
            {
                att.FormId = formID;
                new Pkurg.BPM.Services.AttachmentService().Save(att);
            }
        }
        foreach (Attachment att in rattlist)
        {
            Attachment info = rList.Where(x => x.AttachmentId == att.AttachmentId).FirstOrDefault();
            if (info == null)
            {
                new Pkurg.BPM.Services.AttachmentService().Delete(att.AttachmentId);
            }
        }
        return true;

    }

    public override bool SaveControlData()
    {
        return SaveAttachment(_BPMContext.ProcInst.FormId);
    }

    #endregion

    #region 下载文件

    /// <summary>
    /// 实现文件下载
    /// </summary>
    /// <param name="FullFileName"></param>
    private void FileDownload(string FullFileName)
    {
        FileInfo DownloadFile = new FileInfo(FullFileName);
        Response.Clear();
        Response.ClearHeaders();
        Response.Buffer = false;
        Response.ContentType = "application/octet-stream";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.Name, System.Text.Encoding.UTF8));
        Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
        Response.WriteFile(DownloadFile.FullName);
        Response.Flush();
        Response.End();
    }
    #endregion

    #region 附件绑定

    private void BindAttachList()
    {
        //string formId = "";
        //TList<Attachment> rList=cobll.GetAttachmentByFormID(formId);
        if (rList != null && rList.Count > 0)
        {
            //rList.Sort("AttachmentId Asc");
            //按照时间顺序排序
            rList.Sort("CreateAtTime Asc");
            gvAttachment.Visible = true;
            this.gvAttachment.DataSource = rList;
            gvAttachment.DataBind();
        }
        else
        {
            gvAttachment.Visible = false;
        }

    }

    #endregion

    #region 删除

    protected void lbDeleteAttach_Command(object sender, CommandEventArgs e)
    {
        DeletePrice(e.CommandArgument.ToString());
        BindAttachList();
    }
    private void DeletePrice(string id)
    {
        TList<Attachment> list = rList;
        foreach (Attachment price in rList)
        {
            if (price.AttachmentId.ToString() == id)
            {
                rList.Remove(price);
                break;
            }
        }
        rList = list;
    }

    #endregion
}