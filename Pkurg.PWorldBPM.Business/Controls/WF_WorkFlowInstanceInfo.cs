using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_WorkFlowInstanceInfo
    {

        #region Model
        private string _instanceid;
        private string _appid;
        private string _formid;
        private string _wfinstanceid;
        private int? _orderno;
        private int? _isdel;
        private string _createbyusercode;
        private string _createbyusername;
        private DateTime? _createattime;
        private string _updatebyusercode;
        private string _updatebyusername;
        private DateTime? _updateattime;
        private string _createdeptcode;
        private string _createdeptname;
        private string _workitemcode;
        private string _workitemname;
        private int? _wftaskid;
        private DateTime? _finishedtime;
        private string _remark;
        private string _formtitle;
        private string _wfstatus;
        private DateTime? _sumittime;
        private string _formdata;
        private string _processname;
        /// <summary>
        /// 
        /// </summary>
        public string InstanceID
        {
            set { _instanceid = value; }
            get { return _instanceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AppID
        {
            set { _appid = value; }
            get { return _appid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WFInstanceId
        {
            set { _wfinstanceid = value; }
            get { return _wfinstanceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateByUserCode
        {
            set { _createbyusercode = value; }
            get { return _createbyusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateByUserName
        {
            set { _createbyusername = value; }
            get { return _createbyusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateAtTime
        {
            set { _createattime = value; }
            get { return _createattime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateByUserCode
        {
            set { _updatebyusercode = value; }
            get { return _updatebyusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateByUserName
        {
            set { _updatebyusername = value; }
            get { return _updatebyusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateAtTime
        {
            set { _updateattime = value; }
            get { return _updateattime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateDeptCode
        {
            set { _createdeptcode = value; }
            get { return _createdeptcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateDeptName
        {
            set { _createdeptname = value; }
            get { return _createdeptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkItemCode
        {
            set { _workitemcode = value; }
            get { return _workitemcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkItemName
        {
            set { _workitemname = value; }
            get { return _workitemname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WFTaskID
        {
            set { _wftaskid = value; }
            get { return _wftaskid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FinishedTime
        {
            set { _finishedtime = value; }
            get { return _finishedtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormTitle
        {
            set { _formtitle = value; }
            get { return _formtitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WFStatus
        {
            set { _wfstatus = value; }
            get { return _wfstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SumitTime
        {
            set { _sumittime = value; }
            get { return _sumittime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormData
        {
            set { _formdata = value; }
            get { return _formdata; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProcessName
        {
            set { _processname = value; }
            get { return _processname; }
        }
        #endregion Model

    }
}
