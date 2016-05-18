using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserProfileInfo
    {
        public string ID { get; set; }
        public string CHName { get; set; }
        public string ENName { get; set; }
        public string ADAccount { get; set; }
        public string Email { get; set; }
        public string EmailOrig { get; set; }
        public string OfficePhone { get; set; }
        public string CellPhone { get; set; }
        public string WorkPlace { get; set; }
        public string HireDate { get; set; }
        public string Birthdate { get; set; }
        public Guid PositionGuid { get; set; }
        public string PositionName { get; set; }
        public string ManagerAccount { get; set; }
        public string EmployeeAccount { get; set; }
        public string CostCenter { get; set; }
        public int State { get; set; }
        public string FAX { get; set; }
        public string BlackBerry { get; set; }
        public string GraduateFrom { get; set; }
        public string OAC { get; set; }
        public string PoliticalAffiliation { get; set; }
        public string Gender { get; set; }
        public string EducationalBackground { get; set; }
        public string WorkExperienceBefore { get; set; }
        public string WorkExperienceNow { get; set; }
        public int OrderNo { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
