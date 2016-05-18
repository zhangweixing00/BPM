using System;
using Pkurg.PWorldBPM.Common.Context;
using SourceCode.Workflow.Client;

namespace Pkurg.PWorldBPM.Common
{
    public class UControlBase : System.Web.UI.UserControl
    {
        public UControlBase()
        {

        }

        public Pkurg.PWorldBPM.Common.Context.BPMContext _BPMContext { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ///初始化
            if (_BPMContext == null)
            {
                if (this.Page is UPageBase)
                {
                    _BPMContext = ((UPageBase)this.Page)._BPMContext;
                }
                else
                {
                    _BPMContext = new BPMContext();
                    _BPMContext.OrgService = new Pkurg.PWorldBPM.Common.Services.OrgService();
                    _BPMContext.ProcService = new Pkurg.PWorldBPM.Common.Services.BPMProcService();

                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //if (_BPMContext.InstDataInfo != null)
            //{
            //    PageParseHelper parseHelper = new PageParseHelper(_BPMContext.InstDataInfo.DataInfo);
            //    parseHelper.BindValue(this);
            //}
        }

        private WorklistItem _K2_TaskItem;

        public WorklistItem K2_TaskItem
        {
            get
            {
                if (this.Page is UPageBase)
                {
                    return ((UPageBase)this.Page).K2_TaskItem;
                }
                else
                {
                    if (_K2_TaskItem == null)
                    {

                        if (!string.IsNullOrEmpty(_BPMContext.Sn))
                        {
                            try
                            {
                                _K2_TaskItem = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, string.IsNullOrEmpty(SimulateUser) ? _BPMContext.CurrentUser.LoginId : SimulateUser);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    return _K2_TaskItem;
                }

            }
        }


        

        private string simulateUser;

        public string SimulateUser
        {
            get { return simulateUser; }
            set
            {
                simulateUser = value;
                _BPMContext.LoginId = value;
            }
        }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsCanEdit { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        private string _procId;
        public string ProcId
        {
            set
            {
                //if (_BPMContext == null)
                //{
                //    _BPMContext = new Pkurg.PWorldBPM.Common.Context.BPMContext();
                //}
                _BPMContext.ProcID = value;
                _procId = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_procId))
                {
                    _procId = _BPMContext.ProcID;
                }
                return _procId;
            }
        }

        protected override void LoadViewState(object savedState)
        {
            Object[] datas = savedState as Object[];
            IsCanEdit = bool.Parse(datas[1].ToString());

            if (datas[2] != null)
            {
                _procId = datas[2].ToString();
                _BPMContext.ProcID = datas[2].ToString();
            }
            base.LoadViewState(datas[0]);
        }
        protected override object SaveViewState()
        {
            return new Object[]
        {
            base.SaveViewState(),
            IsCanEdit,
            ProcId
        };
        }

        /// <summary>
        /// 控件数据保存
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveControlData()
        {
            return true;
        }
    }
}
