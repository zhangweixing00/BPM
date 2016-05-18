using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace IDAL
{
    public interface ICustomFlowDAL
    {
        bool AddCustomFlow(CustomFlow cf);
        DataTable GetBusinessClass();
        DataTable GetBusinessSubClass(string classId);
        DataTable GetDocBySubClassId(string subClassCode);
        DataTable GetCustomFlowByFormId(string FormId);
        DataTable GetCustomFlowByProcessId(string processId);
        string GetApproveXMLByProcInsID(string procInsId);
        bool UpdateCustomFlowByFormId(CustomFlow cf);
        bool UpdateCustomFlowStatusByAttachIds(string attachIds, string formId);
        bool UpdateAttachStatusByAttachAttachCodes(string attachcodes);
        bool UpdateCostomFlowStatusByFormId(string formId, string processStatus);
    }
}
