using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    class Entity
    {
    }

    public class Department
    {
        public Department(string _deptCode)
        {
            this.code = "";
            this.deptCode = _deptCode;
            this.department = "";
            this.parentCode = "";
            this.parentDepartment = "";
            this.levels = 0;
            this.state = 1;
            this.description = "";
            this.abbreviation = "";
            this.deptTypeCode = "";
            this.deptType = "";
            this.orderNO = "1";
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private string deptCode;

        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        private string parentCode;

        public string ParentCode
        {
            get { return parentCode; }
            set { parentCode = value; }
        }

        private string parentDepartment;

        public string ParentDepartment
        {
            get { return parentDepartment; }
            set { parentDepartment = value; }
        }
        private string department;

        public string DepartmentName
        {
            get { return department; }
            set { department = value; }
        }
        private string abbreviation;

        public string Abbreviation
        {
            get { return abbreviation; }
            set { abbreviation = value; }
        }
        private int levels;

        public int Levels
        {
            get { return levels; }
            set { levels = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int state;

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        private string deptType;

        public string DeptType
        {
            get { return deptType; }
            set { deptType = value; }
        }

        private string deptTypeCode;

        public string DeptTypeCode
        {
            get { return deptTypeCode; }
            set { deptTypeCode = value; }
        }

        private string orderNO;

        public string OrderNO
        {
            get { return orderNO; }
            set { orderNO = value; }
        }
    }

    public class UserProfile
    {
        public UserProfile()
        {
            this.iD = "";
            this.cHName = "";
            this.eNName = "";
            this.aDAccount = "";
            this.email = "";
            this.officePhone = "";
            this.cellPhone = "";
            this.workPlace = "";
            this.deptCode = "";
            this.hireDate = "1900-1-1";
            this.birthdate = "1900-1-1";
            this.costCenter = "";
            this.employeeID = "";
            this.managerAccount = "";
            this.positionGuid = "";

            this.fAX = "";
            this.blackBerry = "";
            this.graduateFrom = "";
            this.oAC = "";
            this.politicalAffiliation = "";
            this.gender = "";
            this.positionDesc = "";
            this.educationalBackground = "";
            this.workExperienceBefore = "";
            this.workExperienceNow = "";
            this.orderNo = "";
            this.photoUrl = "";
            
        }

        private string iD;

        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string cHName;

        public string CHName
        {
            get { return cHName; }
            set { cHName = value; }
        }
        private string eNName;

        public string ENName
        {
            get { return eNName; }
            set { eNName = value; }
        }
        private string aDAccount;

        public string ADAccount
        {
            get { return aDAccount; }
            set { aDAccount = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string officePhone;

        public string OfficePhone
        {
            get { return officePhone; }
            set { officePhone = value; }
        }
        private string cellPhone;

        public string CellPhone
        {
            get { return cellPhone; }
            set { cellPhone = value; }
        }
        private string workPlace;

        public string WorkPlace
        {
            get { return workPlace; }
            set { workPlace = value; }
        }
        private string deptCode;

        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        private string hireDate;

        public string HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }
        private string birthdate;

        public string Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }
        private string positionGuid;

        public string PositionGuid
        {
            get { return positionGuid; }
            set { positionGuid = value; }
        }
        private string managerAccount;

        public string ManagerAccount
        {
            get { return managerAccount; }
            set { managerAccount = value; }
        }
        private string employeeID;

        public string EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        private string costCenter;

        public string CostCenter
        {
            get { return costCenter; }
            set { costCenter = value; }
        }

        private string fAX;

        public string FAX
        {
            get { return fAX; }
            set { fAX = value; }
        }
        private string blackBerry;

        public string BlackBerry
        {
            get { return blackBerry; }
            set { blackBerry = value; }
        }
        private string graduateFrom;

        public string GraduateFrom
        {
            get { return graduateFrom; }
            set { graduateFrom = value; }
        }
        private string oAC;

        public string OAC
        {
            get { return oAC; }
            set { oAC = value; }
        }
        private string politicalAffiliation;

        public string PoliticalAffiliation
        {
            get { return politicalAffiliation; }
            set { politicalAffiliation = value; }
        }
        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private string positionDesc;

        public string PositionDesc
        {
            get { return positionDesc; }
            set { positionDesc = value; }
        }
        private string educationalBackground;

        public string EducationalBackground
        {
            get { return educationalBackground; }
            set { educationalBackground = value; }
        }
        private string workExperienceBefore;

        public string WorkExperienceBefore
        {
            get { return workExperienceBefore; }
            set { workExperienceBefore = value; }
        }
        private string workExperienceNow;

        public string WorkExperienceNow
        {
            get { return workExperienceNow; }
            set { workExperienceNow = value; }
        }
        private string orderNo;

        public string OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }
        private string photoUrl;

        public string PhotoUrl
        {
            get { return photoUrl; }
            set { photoUrl = value; }
        }
    }

    public class ProcessSettingsEntity
    {
        #region fields
        private int _procSetId;
        private string _startPage;
        private string _viewPage;
        private string _description;
        private string _editPage;
        private string _processName;
        private string _processFullName;
        private string _folder;

        #endregion

        public ProcessSettingsEntity()
        {
            this._procSetId = 0;
            this._startPage = string.Empty;
            this._viewPage = string.Empty;
            this._description = string.Empty;
            this._processName = string.Empty;
            this._processFullName = string.Empty;
            this._folder = string.Empty;
        }

        public string ProcessFullName
        {
            get { return this._processFullName; }
            set { this._processFullName = value; }
        }

        public string Folder
        {
            get { return this._folder; }
            set { this._folder = value; }
        }

        public string ProcessName
        {
            get { return this._processName; }
            set { this._processName = value; }
        }

        public int ProcSetId
        {
            get
            {
                return this._procSetId;
            }
            set
            {
                this._procSetId = value;
            }
        }

        public string StartPage
        {
            get
            {
                return this._startPage;
            }
            set
            {
                this._startPage = value;
            }
        }

        public string ViewPage
        {
            get
            {
                return this._viewPage;
            }
            set
            {
                this._viewPage = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string EditPage
        {
            get
            {
                return this._editPage;
            }
            set
            {
                this._editPage = value;
            }
        }
    }

    public class DCP
    {
        public DCP()
        {
            this.iD = "-1";
            this.mDAccount = "";
            this.processCode = "";
            this.workPlace = "";
            this.deptCode = "";
            this.chargePersonAccount = "";
        }

        private string iD;

        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string deptCode;

        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        private string processCode;

        public string ProcessCode
        {
            get { return processCode; }
            set { processCode = value; }
        }
        private string chargePersonAccount;

        public string ChargePersonAccount
        {
            get { return chargePersonAccount; }
            set { chargePersonAccount = value; }
        }
        private string mDAccount;

        public string MDAccount
        {
            get { return mDAccount; }
            set { mDAccount = value; }
        }
        private string workPlace;

        public string WorkPlace
        {
            get { return workPlace; }
            set { workPlace = value; }
        }

        private string chargePersonName;

        public string ChargePersonName
        {
            get { return chargePersonName; }
            set { chargePersonName = value; }
        }
        private string mDName;

        public string MDName
        {
            get { return mDName; }
            set { mDName = value; }
        }
    }

    public class HotelInfo
    {
        #region 酒店信息
        private int _id;
        private string _hotelname;
        private string _region;
        private string _address;
        private string _phone;
        private string _chamber;
        private string _negotiatedprice;
        private string _breakfast;
        private string _broadband;
        private string _preordain;
        private string _evaluation;
        private string _remark;
        private string _hotelgroup;
        private string _sale;
        private DateTime? _createdon;
        private string _createdby;
        private string _hotellevel;
       
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName
        {
            set { _hotelname = value; }
            get { return _hotelname; }
        }
        /// <summary>
        /// 地区
        /// </summary>
        public string Region
        {
            set { _region = value; }
            get { return _region; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 酒店电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 房型
        /// </summary>
        public string Chamber
        {
            set { _chamber = value; }
            get { return _chamber; }
        }
        /// <summary>
        /// 协议价格
        /// </summary>
        public string NegotiatedPrice
        {
            set { _negotiatedprice = value; }
            get { return _negotiatedprice; }
        }
        /// <summary>
        /// 早餐
        /// </summary>
        public string Breakfast
        {
            set { _breakfast = value; }
            get { return _breakfast; }
        }
        /// <summary>
        /// 宽带
        /// </summary>
        public string Broadband
        {
            set { _broadband = value; }
            get { return _broadband; }
        }
        /// <summary>
        /// 预定名义
        /// </summary>
        public string Preordain
        {
            set { _preordain = value; }
            get { return _preordain; }
        }
        /// <summary>
        /// 弘毅内部评价
        /// </summary>
        public string Evaluation
        {
            set { _evaluation = value; }
            get { return _evaluation; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 酒店集团
        /// </summary>
        public string HotelGroup
        {
            set { _hotelgroup = value; }
            get { return _hotelgroup; }
        }
        /// <summary>
        /// 酒店销售代表
        /// </summary>
        public string Sale
        {
            set { _sale = value; }
            get { return _sale; }
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

        public string HotelLevel
        {
            set { _hotellevel = value; }
            get { return _hotellevel; }
        }
        #endregion
    }

    public class SealProfileInfo
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string sealName;
        /// <summary>
        /// 印章名称
        /// </summary>
        public string SealName
        {
            get { return sealName; }
            set { sealName = value; }
        }
        private string sealGuid;
        /// <summary>
        /// 印章Guid
        /// </summary>
        public string SealGuid
        {
            get { return sealGuid; }
            set { sealGuid = value; }
        }

        private string createdBy;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        private DateTime createdOn;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
    }
}
