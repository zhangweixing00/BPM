using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pkurg.PWorldBPM.Services.BLL;
using Pkurg.PWorldBPM.Services.DAL;
using System.Net;
using System.IO;

namespace Pkurg.PWorldBPM.Services.WinForm
{
    public partial class FormArchive : Form
    {
        #region Private Fields

        private BPMArchiveBLL bpmArchiveBll = new BPMArchiveBLL();

        private ArchiveLogBLL archiveLogBll = new ArchiveLogBLL();

        private PWorldUserInfoBLL pWorldUserInfoBll = new PWorldUserInfoBLL();

        private AttachmentBLL attachmentBll = new AttachmentBLL();

        #endregion Private Fields

        #region Events

        public FormArchive()
        {
            InitializeComponent();
        }

        private void FormArchive_Load(object sender, EventArgs e)
        {
            this.FlowArchive();
            System.Threading.Thread.Sleep(10000);// 10s后关闭程序
            System.Environment.Exit(0);
        }

        #endregion Events

        #region Private Methods

        /// <summary>
        /// 流程归档
        /// </summary>
        /// <returns></returns>
        private void FlowArchive()
        {
            this.WriteLog("开始归档。");
            // 默认正常，如果发生异常则为false
            bool b = true;
            int n = 0;
            string strInstanceID = string.Empty;
            try
            {
                // 获取需要归档的数据
                List<V_BPM_Archive> lstBPMArchive = this.bpmArchiveBll.GetList();
                n = lstBPMArchive.Count;
                if (lstBPMArchive.Count > 0)
                {
                    // BPM主机地址
                    string strBPMHostUrl = System.Configuration.ConfigurationManager.AppSettings["BPMHostUrl"];
                    // WrokFlow前缀地址
                    string strWFPrefixUrl = System.Configuration.ConfigurationManager.AppSettings["WFPrefixUrl"];

                    foreach (var item in lstBPMArchive)
                    {
                        strInstanceID = item.InstanceID;
                        // 组合出流程页面url 【统一在FormName前加V_】 
                        string strWFUrl = strBPMHostUrl + strWFPrefixUrl + "V_" + item.FormName.Trim() + "?ID=" + item.InstanceID;
                        // 1.读取页面信息
                        string strHtml = this.ReadPage(strWFUrl);
                        if (!string.IsNullOrEmpty(strHtml))
                        {
                            // 物理文件名称
                            string strHtmlFileName = item.FormID + ".html";
                            // 生成本地的html物理文件(暂时无需生成)
                            //string strFullPath = this.CreateFile(strHtml, strHtmlFileName);

                            // 2.调用档案系统接口进行归档
                            bool bRet = this.CallFileSysWebService(item, strHtmlFileName, strHtml);
                            if (bRet)
                            {
                                // 3.更新Archive_Log表
                                Archive_Log model = new Archive_Log();
                                model.FormID = item.FormID;
                                model.InstanceID = item.InstanceID;
                                // 如果调用归档接口成功为1
                                model.IsSuccess = 1;
                                this.archiveLogBll.Operate(model);
                            }
                            else
                            {
                                n--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                b = false;
                this.WriteLog("发生异常:InstanceID:[" + strInstanceID + "],错误信息：[ " + ex.Message + "];代码追踪： [" + ex.StackTrace + "]");
            }
            if (b)
            {
                this.WriteLog("归档完毕,共归档 " + n + " 条数据。");
            }
        }

        /// <summary>
        /// 读取页面信息
        /// </summary>
        /// <param name="url">页面Url</param>
        /// <returns></returns>
        private string ReadPage(string url)
        {
            string strRet = string.Empty;
            // 读取流程页面信息
            WebRequest request = WebRequest.Create(url);
            request.Timeout = 10 * 60 * 1000;// 10分钟
            //NetworkCredential credential = new NetworkCredential("test", "test", "Founder");
            //request.Credentials = credential;
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            strRet = reader.ReadToEnd();
            #region 将其中的相对路径修改为绝对路径

            if (!string.IsNullOrEmpty(strRet))
            {
                // BPM主机地址
                string strBPMHostUrl = System.Configuration.ConfigurationManager.AppSettings["BPMHostUrl"];

                string strOld1 = "=\"/Resource/";
                string strNew1 = "=\"" + strBPMHostUrl + "Resource/";
                strRet = strRet.Replace(strOld1, strNew1);

                string strOld2 = "=\'/Resource/";
                string strNew2 = "=\'" + strBPMHostUrl + "Resource/";
                strRet = strRet.Replace(strOld2, strNew2);

                string strOld3 = "=\"/Files/";
                string strNew3 = "=\"" + strBPMHostUrl + "Files/";
                strRet = strRet.Replace(strOld3, strNew3);

                string strOld4 = "=\'/Files/";
                string strNew4 = "=\'" + strBPMHostUrl + "Files/";
                strRet = strRet.Replace(strOld4, strNew4);
            }

            #endregion 将其中的相对路径修改为绝对路径
            return strRet;

        }

        /// <summary>
        /// 生成物理文件
        /// </summary>
        /// <param name="htmlContent">html页面信息、文件流信息</param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        private string CreateFile(string htmlContent, string fileName)
        {
            string strRet = string.Empty;
            // 保存为物理文件
            string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "ArchiveFiles\\";
            // 文件全路径
            string strFileFullPath = strFilePath + fileName;
            //如果没有该路径就创建一个
            if (!System.IO.Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            using (FileStream fs = new FileStream(strFileFullPath, FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(htmlContent);
                sw.Flush();
                sw.Close();
            }
            return strFileFullPath;
        }

        /// <summary>
        /// 生成Efile4类型的字符串（Efile4为档案系统要求的）
        /// </summary>
        /// <param name="createUserID">创建者</param>
        /// <param name="pID">pid(对应文件级返回的int值)</param>
        /// <param name="strEFileName">电子文件名</param>
        /// <param name="strFileFullName">上传文件的名称包含文件类型 如test.html</param>
        /// <param name="ftpPathName">ftp路径</param>
        /// <returns></returns>
        private string CreateEfile4(string createUserID, int pID, string strEFileName, string strFileFullName, string ftpPathName)
        {
            string strRet = string.Empty;
            string[] fileArray = strFileFullName.Split('.');
            // 截取文件后缀名称“对应：后缀名”
            string strPostfix = fileArray[fileArray.Length - 1];
            // 截取文件名称“对应：实际文件名”
            string strFileName = strFileFullName.Substring(0, strFileFullName.Length - (strPostfix.Length + 1));
            strRet = "0&;0&;0&;"
                   + createUserID + "&;"//创建者(默认为ROOT)
                   + "0&;"
                   + pID + "&;"//pid(对应文件级返回的int值)
                   + strEFileName + "&;"//电子文件名(不重复文件名)
                   + strFileName + "&;"//实际文件名不含后缀(不包含后缀的实际文件名称)" 
                   + strPostfix + "&;"//后缀名
                   + "data&;"//配置名
                   + ftpPathName;//FTP存放地址（PATHNAME）
            return strRet;
        }

        /// <summary>
        /// 调用档案系统接口进行归档
        /// </summary>
        /// <param name="item">BPM归档项</param>
        /// <param name="htmlFileName">生成的html名称</param>
        /// <param name="htmlContent">html内容</param>
        private bool CallFileSysWebService(V_BPM_Archive item, string htmlFileName, string htmlContent)
        {
            bool bRet = false;
            try
            {
                // 声明WebService
                WSArchive.DbUtilService wsDb = new WSArchive.DbUtilService();

                #region 1. 插入dfile4(dfile4是档案系统要求传入的)
                // 年度
                string strDateYear = item.CreateAtTime.Value.Year.ToString();
                // 月份
                string strDateMonth = item.CreateAtTime.Value.Month.ToString();
                // 流程完成日期
                string strEndTime = item.FinishedTime.ToString();
                // 表单Title
                string strTitle = item.FormTitle;
                string strManagerName = item.CreateByUserName;
                // 
                string strContent = string.Empty;

                // 登录用户
                V_Pworld_UserInfo_All userInfo = this.pWorldUserInfoBll.GetUserInfo(item.CreateByUserCode);
                if (userInfo == null || string.IsNullOrEmpty(userInfo.LoginName))
                {
                    this.WriteLog("InstanceID：[" + item.InstanceID + "]，找不到用户：" + item.CreateByUserCode);
                    return bRet;
                }
                string strLoginName = userInfo.LoginName;
                // 根据档案系统要求格式： Founder_admin
                string strDomainAccount = ("Founder_" + strLoginName).ToUpper();
                string strDeptId = wsDb.getBMIDByADUserName(strDomainAccount);//传入档案系统的部门ID“根据域帐号转换”(对应：“BMID”)
                // 全宗号（档案系统要求的）
                string strQzh = "F";
                if (strDeptId.Contains("_"))
                {
                    strQzh = strDeptId.Split('_')[0];//ID标识“根据档案系统的部门ID截取”(对应：“DID”)
                }
                string strTempdfile4 = "0&;1&;0&;1&;"
                                   + strDeptId + "&;"  //传入档案系统的部门ID(BMID)
                                   + "-1&;"
                                   + "0&;"
                                   + strQzh + "&;" //ID标识(DID)
                                   + strTitle + "&;"
                                   + strManagerName + "&;"//责任者(OA系统 经办人/发起人)
                                   + Convert.ToDateTime(strEndTime).ToString("yyyy-MM-dd") + "&;"
                                   + "1&;"
                                   + DateTime.Now.ToString("yyyy-MM-dd") + "&;"
                                   + strDateYear + "&;"
                                   + strDateMonth + "&;"
                                   + strContent + "&;"
                                   + strDomainAccount;//创建者
                int iPID = wsDb.addField("bdzy_dfile4.xml", strTempdfile4);//pid(对应文件级返回的int值“对应：pid”)

                #endregion 1. 插入dfile4

                #region 2. ftp上传html

                WebClient wc = new WebClient();
                // 2进制数据
                byte[] InputHtml = Encoding.GetEncoding("GB2312").GetBytes(htmlContent);
                // FTP用户
                string strFtpUser = System.Configuration.ConfigurationManager.AppSettings["ftpUser"];
                // ftp密码
                string strFtpPassWord = System.Configuration.ConfigurationManager.AppSettings["ftpPassWord"];
                // ftp路径
                string strFtpDir = System.Configuration.ConfigurationManager.AppSettings["ftpDir"];
                wc.Credentials = new NetworkCredential(strFtpUser, strFtpPassWord);
                // 对应“FTP存放地址（PATHNAME）”
                string strFtpPathName = "/" + "OAFiles" + "/" + DateTime.Now.Year + "/";
                // ftp上传的html地址        
                string strHtmlPath = strFtpPathName + htmlFileName;
                wc.UploadData(strFtpDir + strHtmlPath, InputHtml);

                //  插入efile4(efile4是档案系统要求传入的)
                string strTempEfile4 = this.CreateEfile4(strDomainAccount, iPID, htmlFileName.Replace(".html", ""), htmlFileName, strFtpPathName);
                wsDb.addField("bdzy_efile4.xml", strTempEfile4);

                #endregion  2. ftp上传html

                #region 3. ftp上传该流程相关的附件

                // 获取附件
                List<BPM_Attachment> lstAttachment = this.attachmentBll.GetList(item.FormID);
                if (lstAttachment.Count() > 0)
                {
                    int nIndex = 1;
                    foreach (var attachment in lstAttachment)
                    {
                        // 限制50M以下的物理文件才进行上传
                        int nLimitSize = 50 * 1024 * 1024;// 单位bytes  1M=1024kb   1kb=1024bytes
                        if (!string.IsNullOrEmpty(attachment.AttachmentSize) && int.Parse(attachment.AttachmentSize) <= nLimitSize)
                        {
                            //ftp上传(第一步)
                            // BPM主机地址
                            string strBPMHostUrl = System.Configuration.ConfigurationManager.AppSettings["BPMHostUrl"];

                            // 附件全路径(网络地址uri)
                            string strAttachmentFullPath = strBPMHostUrl.TrimEnd('/') + attachment.URL;
                            // 网络读取
                            WebClient wc1 = new WebClient();
                            try
                            {
                                byte[] fileBinarys = wc1.DownloadData(strAttachmentFullPath);
                                #region 本地读取的代码 （已弃用的代码）
                                //  FileStream fs = new FileStream(fullPath, FileMode.Open);
                                //byte[] fileBinarys = new byte[fs.Length]; //2进制
                                //fs.Read(fileBinarys, 0, fileBinarys.Length);
                                //fs.Close();
                                #endregion 本地读取的代码
                                wc.Credentials = new NetworkCredential(strFtpUser, strFtpPassWord);
                                string[] fileArray = attachment.AttachmentName.Split('.');
                                // 截取文件后缀名称“对应：后缀名”
                                string strPostfix = fileArray[fileArray.Length - 1];
                                string strAttName = item.FormID + "-" + nIndex;//“对应：电子文件名”
                                string strAttPath = strFtpPathName + item.FormID + "-" + attachment.AttachmentName;//ftp上传的附件地址
                                wc.UploadData(strFtpDir + strAttPath, fileBinarys);

                                //插入efile4(第二步)
                                string strTempAttEfile4 = this.CreateEfile4(strDomainAccount, iPID, strAttName, attachment.AttachmentName, strFtpPathName);
                                wsDb.addField("bdzy_efile4.xml", strTempAttEfile4);
                                nIndex++;
                            }
                            catch (Exception ex)
                            {
                                this.WriteLog("【上传附件】发生异常:InstanceID：[" + item.InstanceID + "],错误信息：[ " + ex.Message + "];代码追踪： [" + ex.StackTrace + "]");
                            }
                        }
                    }
                }
                #endregion 3. ftp上传该流程相关的附件
            }
            catch (Exception ex)
            {
                // 发生异常
                this.WriteLog("【调用档案系统接口进行归档】发生异常:InstanceID：[" + item.InstanceID + "],错误信息：[ " + ex.Message + "];代码追踪： [" + ex.StackTrace + "]");
                return bRet;
            }
            bRet = true;
            return bRet;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logInfo">日志</param>
        /// <param name="logParams">参数</param>
        private void WriteLog(string logInfo, params string[] logParams)
        {
            // 保存为物理文件
            string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            string logFileName = string.Format(@"{0}\{1}.log",
                strFilePath,
                DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo));

            string log = string.Format(logInfo, logParams);
            FileStream fsFile = new FileStream(logFileName, FileMode.Append);
            StreamWriter swWriter = new StreamWriter(fsFile);
            swWriter.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), log));
            swWriter.Close();
            fsFile.Close();
        }


        #endregion
    }
}
