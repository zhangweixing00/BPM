using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IDAL
{
    public interface IProcessRuleDAL
    {
        DataSet GetProcessNodesByProcessID(string processId);

        DataSet GetProcessNodeByProcessID(string processId);

        bool InsertProcessNode(ProcessNodeInfo pn);

        bool InsertApproveNodeRule(Guid nodeID, string displayName, string tableName, string expression, string spName);

        bool InsertRequestNode(RequestNodeInfo rn);

        DataSet GetRequestNodeByProcessID(string processId);

        bool InsertRequestNodeRule(Guid nodeID, string keyName, string tableName, string expression);

        DataSet GetApproveNodesByNodeIds(string nodeIds);

        DataSet GetRequestNodesByNodeIds(string nodeIds);

        bool InsertApproveRule(string xml, string spName, string tableName, string groupName, string  processId);

        DataSet GetApproveTableByProcessID(string processID, string groupName);

        bool InsertFormula(string processID, string groupName, string formula);

        DataSet GetRequestNodeByNodeId(Guid nodeId);

        bool UpdateRequestNode(RequestNodeInfo info);

        DataSet GetProcessNodeByNodeID(Guid nodeId);

        bool UpdateProcessNode(ProcessNodeInfo info);

        DataSet GetApproveRuleGroupNameByProcessType(string processID);

        DataSet GetFormulaByGroupName(string processId, string groupName);

        DataSet GetRequestRuleByNodeID(Guid nodeId);

        bool UpdateRequestNodeRule(Guid Id, Guid nodeId, string keyName, string tableName, string expression);

        DataSet GetApproveNodeRuleByNodeID(Guid nodeId);

        bool UpdateApproveNodeRule(Guid ID, Guid nodeID, string displayName, string tableName, string expression, string spName);

        DataSet GetApproveRuleGroupByGroupNameAndProcessID(string groupName, string processId);

        DataSet GetApproveTableByTableName(string tableName, string processId);

        string ValidateExpression(string sQLValue);

        bool DeleteProcessNode(string nodeId);

        bool DeleteRequestNode(string nodeId);

        bool DeleteRuleTableByID(Guid tableId);

        DataSet GetNodeIDsByTableID(Guid tableId);

        bool GetIsApproveByNodeIDs(Guid requestNodeId, Guid processNodeId, string ruleTableName);

        DataSet GetRuleInfoByTableID(Guid tableId);

        DataSet GetApproveTableExtendByTableName(string tableName, string  processId);

        DataSet GetPWorldDepartment();
    }
}
