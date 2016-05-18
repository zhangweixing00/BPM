using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CustomFlow
    {
        public CustomFlow()
        { }
        #region Model
        private int _id;
        private string _formid;
        private string _processid;
        private string _submitid;
        private DateTime? _submitdate;
        private string _applicant;
        private string _priority;
        private string _urgent;
        private bool _isemail;
        private string _bbcategorycode;
        private string _bscategorycode;
        private string _appreason;
        private string _attachids;
        private string _approvexml;
        private int? _state;
        private string _operator;
        private DateTime? _createdon;
        private string _createdby;
        private string _processState;
        private string _appexplain;
        /// <summary>
        /// 自增
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 表单号
        /// </summary>
        public string FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 流程ID
        /// </summary>
        public string ProcessId
        {
            set { _processid = value; }
            get { return _processid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubmitId
        {
            set { _submitid = value; }
            get { return _submitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SubmitDate
        {
            set { _submitdate = value; }
            get { return _submitdate; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string Applicant
        {
            set { _applicant = value; }
            get { return _applicant; }
        }
        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority
        {
            set { _priority = value; }
            get { return _priority; }
        }
        /// <summary>
        /// 紧急度
        /// </summary>
        public string Urgent
        {
            set { _urgent = value; }
            get { return _urgent; }
        }
        /// <summary>
        /// 是否Email
        /// </summary>
        public bool IsEmail
        {
            set { _isemail = value; }
            get { return _isemail; }
        }
        /// <summary>
        /// 是否短信
        /// </summary>
        public bool IsSMS { get; set; }
        /// <summary>
        /// 业务大类
        /// </summary>
        public string BBCategoryCode
        {
            set { _bbcategorycode = value; }
            get { return _bbcategorycode; }
        }
        /// <summary>
        /// 业务小类
        /// </summary>
        public string BSCategoryCode
        {
            set { _bscategorycode = value; }
            get { return _bscategorycode; }
        }
        /// <summary>
        /// 申请原因
        /// </summary>
        public string AppReason
        {
            set { _appreason = value; }
            get { return _appreason; }
        }
        /// <summary>
        /// 附件ID
        /// </summary>
        public string AttachIds
        {
            set { _attachids = value; }
            get { return _attachids; }
        }
        /// <summary>
        /// 审批XML
        /// </summary>
        public string jqFlowChart
        {
            set { _approvexml = value; }
            get { return _approvexml; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int? State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatedOn
        {
            set { _createdon = value; }
            get { return _createdon; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy
        {
            set { _createdby = value; }
            get { return _createdby; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string ProcessState
        {
            set { _processState = value; }
            get { return _processState; }
        }
        /// <summary>
        /// 申请说明
        /// </summary>
        public string AppExplain
        {
            set { _appexplain = value; }
            get { return _appexplain; }
        }
        /// <summary>
        /// 申请部门
        /// </summary>
        public string DeptCode { get; set; }

        public string ApplicantName { get; set; }

        #endregion Model

        #region 适用于打印
        public string ApproveXML
        { get; set; }
        public string EmployeeId
        { get; set; }
        public string EmployeeCode
        { get; set; }
        public string EmployeeName
        { get; set; }
        public string ContractTerms { get; set; }
        public string SupervisorEmployeeCode { get; set; }
        public string SUPERISOR { get; set; }
        public DateTime? Date_Join { get; set; }
        public DateTime? Service_Start_Date { get; set; }
        public DateTime? Probation_Complete { get; set; }
        public DateTime? Contract_Start { get; set; }
        public DateTime? Contract_End { get; set; }
        public DateTime? Date_of_Birth { get; set; }
        public string LocationName { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string AC_Location { get; set; }
        public string Address_Line_3 { get; set; }
        public string Political_Status { get; set; }
        public string Correspondence_1 { get; set; }
        public string Correspondence_2 { get; set; }
        public string Place_of_Birth { get; set; }
        public string Home_Tel_No { get; set; }
        public string Nationality { get; set; }
        public string Pager_No { get; set; }
        public string Passport_no_and_Cnty { get; set; }
        public string Mobile_Phone { get; set; }
        public string Passport_Place_of_Issue { get; set; }
        public string Education_Level { get; set; }
        public string Language { get; set; }
        public string Marital_Status { get; set; }
        public string Spouse_Name { get; set; }
        public string Spouse_Contact_Tel { get; set; }
        public string No_of_children { get; set; }
        public string Emergency_cont_Name { get; set; }
        public string Emergency_contact_No { get; set; }
        public string Pay_Terms { get; set; }
        public string Base_Currency { get; set; }
        public string Bank_AC_no { get; set; }
        public string Bank_AC_Holder { get; set; }
        public string Bank_name { get; set; }
        public string Benefit_Location { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public string A4 { get; set; }
        public string A11 { get; set; }
        public string EXPR1 { get; set; }
        public string Email { get; set; }
        public string Email_Part { get; set; }
        public string EXPR2 { get; set; }
        public string Post { get; set; }
        public DateTime? EXPR3 { get; set; }
        public DateTime? EXPR4 { get; set; }
        public string Tel { get; set; }
        public string Sex { get; set; }
        public string BrDivisionCode { get; set; }
        public string BrDivisionName { get; set; }
        public string DivisionNo { get; set; }
        public string FirstDeptName { get; set; }
        public string FirstDeptCode { get; set; }
        public string SecondDeptName { get; set; }
        public string SecondDeptCode { get; set; }
        public string ThirdDeptName { get; set; }
        public string PostLevelName { get; set; }
        public string Card { get; set; }
        public string EXPR5 { get; set; }
        public string EXPR6 { get; set; }
        public string Category { get; set; }
        public string Last_Working_Date { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
        public string PostLevel { get; set; }
        public string PostType { get; set; }
        public int State1 { get; set; }
        public DateTime? StateDate { get; set; }
        public string ClassName { get; set; }
        public string SubClassName { get; set; }
        #endregion
    }
}
