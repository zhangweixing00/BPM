using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class JC_ProjectTenderGroupInfo
    {
        private string _FormID;

        public string FormID
        {
            get { return _FormID; }
            set { _FormID = value; }
        }
        private string _SecurityLevel;

        public string SecurityLevel
        {
            get { return _SecurityLevel; }
            set { _SecurityLevel = value; }
        }
        private string _UrgenLevel;

        public string UrgenLevel
        {
            get { return _UrgenLevel; }
            set { _UrgenLevel = value; }
        }
        private string _DeptName;

        public string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _DateTime;

        public string DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
        private string _Tel;

        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Substance;

        public string Substance
        {
            get { return _Substance; }
            set { _Substance = value; }
        }
        //private string _RelateDepartment;

        //public string RelateDepartment
        //{
        //    get { return _RelateDepartment; }
        //    set { _RelateDepartment = value; }
        //}
        private string _StartDeptId;

        public string StartDeptId
        {
            get { return _StartDeptId; }
            set { _StartDeptId = value; }
        }
        private string _IsApproval;

        public string IsApproval
        {
            get { return _IsApproval; }
            set { _IsApproval = value; }
        }

        private string _GroupRealateDept;

        public string GroupRealateDept
        {
            get { return _GroupRealateDept; }
            set { _GroupRealateDept = value; }
        }

        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
    }
}
