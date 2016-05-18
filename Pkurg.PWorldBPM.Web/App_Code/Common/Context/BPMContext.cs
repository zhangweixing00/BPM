using System;
using System.IO;
using System.Xml.Serialization;
using Pkurg.PWorldBPM.Common.Info;
using Pkurg.PWorldBPM.Common.IServices;

namespace Pkurg.PWorldBPM.Common.Context
{
    [Serializable]
    public class BPMContext
    {
        #region Services

        private IOrgService _orgService;
        public IOrgService OrgService
        {
            set { _orgService = value; }
            get
            {
                return _orgService;
            }
        }

        private IProcService _procService;
        public IProcService ProcService
        {
            set { _procService = value; }
            get
            {
                return _procService;
            }
        }

        #endregion

        /// <summary>
        /// 流程SN
        /// </summary>
        private string _sn;
        public string Sn
        {
            get
            {
                if (string.IsNullOrEmpty(_sn))
                {
                    _sn = System.Web.HttpContext.Current.Request["sn"];
                }
                return _sn;
            }
        }

        private string workflowId;
        public string WorkflowId
        {
            get
            {
                if (!string.IsNullOrEmpty(workflowId))
                {
                    return workflowId;
                }
                if (!string.IsNullOrEmpty(Sn) && Sn.Split('_').Length == 2)
                {
                    workflowId = Sn.Split('_')[0];
                }

                return workflowId;

            }
            set { workflowId = value; }
        }

        private string procID;
        public string ProcID
        {
            get
            {
                if (!string.IsNullOrEmpty(procID))
                {
                    return procID;
                }
                procID = System.Web.HttpContext.Current.Request["id"];
                if (string.IsNullOrEmpty(procID))
                {
                    if (!string.IsNullOrEmpty(WorkflowId))
                    {
                        var info = ProcService.GetInfoByWFId(workflowId);//new Pkurg.BPM.Services.ProcInstService().Find(string.Format("workflowId='{0}'",workflowId));
                        if (info != null)
                        {
                            procID = info.ProcId;
                        }
                    }
                }
                return procID;
            }
            set { procID = value; }
        }

        public string ProcName { get; set; }

        private ContextProcInst procInst;
        public ContextProcInst ProcInst
        {
            get
            {
                if (procInst != null)
                {
                    return procInst;
                }
                if (string.IsNullOrEmpty(ProcID))
                {
                    return null;
                }
                if (procInst == null)
                {
                    procInst = ProcService.GetInfo(ProcID);

                    ProcID = procInst.ProcId;
                    if (string.IsNullOrEmpty(WorkflowId))
                    {
                        WorkflowId = procInst.WorkflowId;
                    }

                }
                return procInst;
            }
            set
            {
                procInst = value;
            }
        }


        private Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo instDataInfo;

        public Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo InstDataInfo
        {
            get
            {

                if (instDataInfo == null && ProcInst != null && !string.IsNullOrEmpty(ProcInst.FormData))
                {
                   // LoggerR.logger.DebugFormat("ProcInst.FormData:{0}", ProcInst.FormData);
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo));
                        using (StringReader sr = new StringReader(ProcInst.FormData))
                        {
                            instDataInfo = serializer.Deserialize(sr) as Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo;
                        }
                    }
                    catch(Exception ex)
                    {
                        LoggerR.logger.DebugFormat("ex:{1}\r\nProcInst.FormData:{0}", ProcInst.FormData,ex.StackTrace);
                    }
                }
                return instDataInfo;
            }


            set { instDataInfo = value; }
        }

        private string loginId;

        public string LoginId
        {
            set
            {
                loginId = value;
                CurrentUser = _orgService.GetUserInfo(loginId);
            }
        }

        public Pkurg.PWorldBPM.Common.Info.UserInfo CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session==null)
                {
                    return new IdentityUser().GetEmployee();
                }
                object userInfo = System.Web.HttpContext.Current.Session["BPM_User"];
                if (userInfo == null)
                {
                     //updated 2014-12-23 by yanghechun
                     //如果Session丢失，需要重新获取
                     userInfo= new IdentityUser().GetEmployee();
                     System.Web.HttpContext.Current.Session["BPM_User"] = userInfo;

                    //Pkurg.PWorldBPM.Common.Info.UserInfo currentUser = _orgService.GetCurrentUser();
                    //System.Web.HttpContext.Current.Session["BPM_User"] = currentUser;
                    //return currentUser;
                }
                return userInfo as Pkurg.PWorldBPM.Common.Info.UserInfo;
            }
            set
            {
                System.Web.HttpContext.Current.Session["BPM_User"] = value;
            }
        }


        public Pkurg.PWorld.Entities.Employee CurrentPWordUser
        {
            get
            {
                return CurrentUser.PWordUser;
            }
        }

        public void SaveExtend()
        {
            if (ProcInst == null)
            {
                return;
            }
            if (InstDataInfo == null)
            {

            }
            XmlSerializer serializer = new XmlSerializer(typeof(Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo));

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, instDataInfo);
                ProcInst.FormData = sw.ToString();
            }

            Pkurg.PWorldBPM.Business.Controls.WF_Instance.UpdateFormDataByProcID(ProcInst.ProcId, ProcInst.FormData);
        }

        public void Save()
        {
            if (ProcInst == null)
            {
                return;
            }
            if (InstDataInfo == null)
            {

            }
            XmlSerializer serializer = new XmlSerializer(typeof(Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo));

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, instDataInfo);
                ProcInst.FormData = sw.ToString();
            }
            ProcService.UpdateProcInst(ProcInst);
        }
    }
}
