using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.Controls
{
    [Serializable]
    public partial class WF_Approval_RecordInfo
    {
        #region Model
        private string _approvalid;
        private int? _wftaskid;
        private string _formid;
        private string _instanceid;
        private string _opinion;
        private int? _orderno;
        private int? _isdel;
        private string _createbyusercode;
        private string _createbyusername;
        private DateTime? _createattime;
        private string _updatebyusercode;
        private string _updatebyusername;
        private DateTime? _updateattime;
        private string _approvebyusercode;
        private string _approvebyusername;
        private string _approveresult;
        private DateTime? _approveattime;
        private string _opiniontype;
        private string _currentactivename;
        private string _issign;
        private DateTime? _receivetime;
        private DateTime? _finishedtime;
        private string _remark;
        private string _currentactiveid;
        private string _approvestatus;
        private string _delegateusername;
        private string _delegateusercode;
        private DateTime? _readtime;
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalID
        {
            set { _approvalid = value; }
            get { return _approvalid; }
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
        public string FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
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
        public string Opinion
        {
            set { _opinion = value; }
            get { return _opinion; }
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
        public string ApproveByUserCode
        {
            set { _approvebyusercode = value; }
            get { return _approvebyusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApproveByUserName
        {
            set { _approvebyusername = value; }
            get { return _approvebyusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApproveResult
        {
            set { _approveresult = value; }
            get { return _approveresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ApproveAtTime
        {
            set { _approveattime = value; }
            get { return _approveattime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OpinionType
        {
            set { _opiniontype = value; }
            get { return _opiniontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CurrentActiveName
        {
            set { _currentactivename = value; }
            get { return _currentactivename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ISSign
        {
            set { _issign = value; }
            get { return _issign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReceiveTime
        {
            set { _receivetime = value; }
            get { return _receivetime; }
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
        public string CurrentActiveID
        {
            set { _currentactiveid = value; }
            get { return _currentactiveid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApproveStatus
        {
            set { _approvestatus = value; }
            get { return _approvestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DelegateUserName
        {
            set { _delegateusername = value; }
            get { return _delegateusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DelegateUserCode
        {
            set { _delegateusercode = value; }
            get { return _delegateusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReadTime
        {
            set { _readtime = value; }
            get { return _readtime; }
        }
        #endregion Model

    }
}
