using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class JC_FinalistApprovalInfo
    {
        private string _FormID;

        public string FormID
        {
            get { return _FormID; }
            set { _FormID = value; }
        }
        private string _ProjectName;

        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }
        private string _ReportDept;

        public string ReportDept
        {
            get { return _ReportDept; }
            set { _ReportDept = value; }
        }
        private string _ReportDate;

        public string ReportDate
        {
            get { return _ReportDate; }
            set { _ReportDate = value; }
        }
        private string _IsAccreditByGroup;

        public string IsAccreditByGroup
        {
            get { return _IsAccreditByGroup; }
            set { _IsAccreditByGroup = value; }
        }
        private string _CheckStatus;

        public string CheckStatus
        {
            get { return _CheckStatus; }
            set { _CheckStatus = value; }
        }
        private string _ArchiveDate;

        public string ArchiveDate
        {
            get { return _ArchiveDate; }
            set { _ArchiveDate = value; }
        }
        private string _ArchiveName;

        public string ArchiveName
        {
            get { return _ArchiveName; }
            set { _ArchiveName = value; }
        }
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

        private string _GroupPurchaseDept;

        public string GroupPurchaseDept
        {
            get { return _GroupPurchaseDept; }
            set { _GroupPurchaseDept = value; }
        }
        private string _GroupLegalDept;

        public string GroupLegalDept
        {
            get { return _GroupLegalDept; }
            set { _GroupLegalDept = value; }
        }
    }
}
