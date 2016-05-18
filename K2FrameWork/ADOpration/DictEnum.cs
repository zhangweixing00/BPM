using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ADOpration
{
    public enum enumDictCache
    {
        [Description("员工")]
        Employee,

        [Description("合同签署公司")]
        Company,

        [Description("大部门")]
        FirstDept,

        [Description("中部门")]
        SecondDept,

        [Description("小部门")]
        ThirdDept,

        [Description("员工类型")]
        EmployeeType,

        [Description("离职原因")]
        LeaveReason,

        [Description("工作地点")]
        Location,

        [Description("职位")]
        Post,

        [Description("职位明细")]
        PostDetail,

        [Description("职位级别")]
        PostLevel,

        [Description("招聘渠道")]
        RecruitChannel,

        [Description("电话机")]
        Telephone,

        [Description("试用期")]
        TrialPeriod,

        [Description("电脑")]
        Computer,

        [Description("办理入职地点")]
        OnboardLocation,

        [Description("AdUsers")]
        AdUsers,

        [Description("AdGroups")]
        AdGroups,

        [Description("学历")]
        Degree,

        [Description("民族")]
        Nationality,

        [Description("政治面貌")]
        Political,

        [Description("合约条件")]
        ContractCondition,

        [Description("Payroll")]
        Payroll,

        [Description("婚姻状况")]
        MaritalStatus,

        [Description("离职表单")]
        LeaveForm,

        [Description("支付类型")]
        FinancialMoney,
        [Description("角色类别")]
        RoleType,
        [Description("流程编码")]
        FlowCode,
        [Description("证件类型")]
        CardType,
        [Description("假期类型")]
        VacationCategory,
        [Description("规则类型")]
        RuleCategory,
        [Description("采购配置状态")]
        PoState,
        [Description("采购配置发布状态")]
        PoPublish,
        [Description("参数")]
        Parameter,
        [Description("业务线类型")]
        BusinessLine,
        [Description("服务类型")]
        ServiceType,
        [Description("上线方式")]
        OperateType,
        [Description("申请类型")]
        ApplyType,
        [Description("合同模板类型")]
        ContractTemplate,
        [Description("导入Excel数据状态")]
        ExcelStates,
        [Description("请假统计类别")]
        HolidayCategory,
        [Description("加班统计类别")]
        OverWorkCategory,
        [Description("导入Excel类型")]
        FileType,
        [Description("配置类型")]
        ConfigCategory,
        [Description("特殊日期类型")]
        SPDayType,
        [Description("加班类型")]
        OverWorkType
    }

    public enum enumInterfaceState
    {
        [Description("AD")]
        AD = 1,
        [Description("HRplus")]
        HRplus = 2,
        [Description("Email")]
        Email = 4
    }
    public enum enumLeaveFormInterfaceState
    {
        [Description("AD")]
        AD = 1,
        [Description("EmailGroup")]
        EmailGroup = 2,
        [Description("Email")]
        Email = 4,
        [Description("PR")]
        PR = 8,
        /// <summary>
        /// 释放分机 
        /// </summary>
        [Description("Tel")]
        Tel = 16,
        /// <summary>
        /// 转移分机
        /// </summary>
        [Description("ShiftTel")]
        ShiftTel = 32
    }
    /// <summary>
    /// 分配状态
    /// </summary>
    public enum enumIsAssign
    {
        [Description("已分配")]
        IsAssign,
        [Description("未分配")]
        IsNotAssign
    }

    /// <summary>
    /// 保留状态
    /// </summary>
    public enum enumIsRetain
    {
        [Description("保留")]
        IsRetain,
        [Description("非保留")]
        IsNotRetain
    }

    /// <summary>
    /// 批状态
    /// </summary>
    public enum BatchSateString
    {
        [Description("未处理")]
        UnProcess,
        [Description("处理中")]
        Processing,
        [Description("完成")]
        Complete,
        [Description("删除")]
        Delete,
        [Description("请选择")]
        Select
    }

    /// <summary>
    /// 离职表单审批状态
    /// </summary>
    public enum enumLeaveForm
    {
        [Description("草稿")]
        Draft,
        [Description("已提交")]
        SubmitHRCB,
        [Description("提交")]
        Submit,
        [Description("处理中")]
        Running,
        [Description("完成")]
        Finished,

    }

    /// <summary>
    /// 离职表单审批角色 
    /// </summary>
    public enum enumLeaveFormRole
    {

        /// <summary>
        /// 部门确认
        /// </summary>
        [Description("部门确认")]
        Dept,
        /// <summary>
        /// 部门领导确认
        /// </summary>
        [Description("部门领导确认")]
        DeptLeader,
        /// <summary>
        /// ES资产确认
        /// </summary>
        [Description("ES资产确认")]
        ES,
        /// <summary>
        /// 法务确认
        /// </summary>
        [Description("法务确认")]
        Legal,
        /// <summary>
        /// 薪酬确认
        /// </summary>
        [Description("薪酬确认")]
        Salary,
        /// <summary>
        /// 股票期权确认
        /// </summary>
        [Description("股票期权确认")]
        Options,
        /// <summary>
        /// 财务单据确认
        /// </summary>
        [Description("财务单据确认")]
        Finance,
        /// <summary>
        /// 内部IT确认
        /// </summary>
        [Description("内部IT确认")]
        IT,
        /// <summary>
        /// 内部IT确认
        /// </summary>
        [Description("分机确认")]
        TelConrifm,
        /// <summary>
        /// 出纳确认
        /// </summary>
        [Description("出纳确认")]
        Teller,
        /// <summary>
        /// HRBP
        /// </summary>
        [Description("HRBP")]
        HRBP,
        /// <summary>
        /// HRC&B
        /// </summary>
        [Description("HR C&B确认")]
        HRCB,
        [Description("办公用品归还")]
        PublicgoodsConfirm,
        [Description("专利确认")]
        PatentConfirm
    }
    /// <summary>
    /// 离职表单节点名称
    /// </summary>
    public enum enumLeaveActivityName
    {

        /// <summary>
        /// 部门助理确认
        /// </summary>
        [Description("部门助理确认")]
        Dept,
        /// <summary>
        /// 部门确认
        /// </summary>
        [Description("部门确认")]
        DeptLeader,
        /// <summary>
        /// ES资产确认
        /// </summary>
        [Description("ES资产确认")]
        ES,
        /// <summary>
        /// 法务确认
        /// </summary>
        [Description("法务确认")]
        Legal,
        /// <summary>
        /// 薪酬确认
        /// </summary>
        [Description("薪酬确认")]
        Salary,
        /// <summary>
        /// 股票期权确认
        /// </summary>
        [Description("股票期权确认")]
        Options,
        /// <summary>
        /// 财务单据确认
        /// </summary>
        [Description("内部IT系统确认")]
        Finance,
        /// <summary>
        /// 内部IT确认
        /// </summary>
        [Description("内部IT账号确认")]
        IT,
        /// <summary>
        /// 分机
        /// </summary>
        [Description("分机确认")]
        TelConfirm,
        /// <summary>
        /// 出纳确认
        /// </summary>
        [Description("出纳确认")]
        Teller,
        /// <summary>
        /// HR BP离职审批
        /// </summary>
        [Description("HR BP离职审批")]
        HRBP,
        /// <summary>
        /// HR C&B确认
        /// </summary>
        [Description("HR C&B确认")]
        HRCB,
        [Description("办公用品归还")]
        PublicgoodsConfirm,
        [Description("专利确认")]
        PatentConfirm
    }
    /// <summary>
    /// FinancialMoney
    /// 应付员工
    /// 员工应返
    /// </summary>
    public enum enumFinancialMoney
    {
        [Description("应付员工")]
        应付员工,
        [Description("员工应返")]
        员工应返,

    }

    /// <summary>
    /// workFlow
    /// </summary>
    public enum enumWorkFlow
    {
        [Description("入职流程申请表单")]
        OAF = 1,
        [Description("离职流程申请")]
        TAF = 2,
        [Description("自定义流程")]
        Other = 3
    }

    /// <summary>
    /// TemplateType
    /// </summary>
    public enum enumTemplateType
    {
        [Description("打印")]
        Print = 1,
        [Description("邮件")]
        Email,
        [Description("短信")]
        Message,
        [Description("导入")]
        Import,
        [Description("导出")]
        Export,
    }

    /// <summary>
    /// 假期或加班的状态
    /// </summary>
    public enum ApplyWorkFlowState
    {
        [Description("草稿")]
        Draft = 0,
        [Description("发起申请")]
        Submit,
        [Description("同意请求")]
        Approve,
        [Description("驳回请求")]
        Refuse,
    }

    /// <summary>
    /// 考勤包状态
    /// </summary>
    public enum SeniorBatchState
    {
        [Description("发起申请")]
        Submit = 1,
        [Description("HR通过")]
        HRApprove,
        [Description("领导通过")]
        LeaderApprove,
        [Description("HRC&B通过")]
        HRCBApprove,
        [Description("驳回")]
        Decline,

    }

    /// <summary>
    /// 在线请假流程审批动作
    /// </summary>
    public enum ApplyWorkFlowOperate
    {
        [Description("发起")]
        Submit,
        [Description("同意")]
        Approve,
        [Description("驳回")]
        Decline
    }


    /// <summary>
    /// 月底考勤解释一级流程审批动作
    /// </summary>
    public enum EditFlowOperate
    {
        [Description("发起")]
        Submit,
        [Description("编辑")]
        Edit,
        [Description("不做编辑")]
        NoEdit,
        [Description("同意")]
        Approve,
        [Description("驳回")]
        Decline
    }

    /// <summary>
    /// 月底考勤解释二级流程审批动作
    /// </summary>
    public enum EditSeniorOperate
    {
        [Description("发起")]
        Submit,
        [Description("同意")]
        Approve,
        [Description("驳回")]
        Refuse
    }




    /// <summary>
    /// 上传文件的状态
    /// Create By lihua
    /// 2011/11/16
    /// </summary>
    public enum ExcelStatus
    {
        [Description("未开始")]
        NoStarted = 1,
        [Description("进行中")]
        Doing,
        [Description("导入成功")]
        ImportSuccess,
        [Description("导入失败")]
        ImportFailed
    }


    /// <summary>
    /// 考勤节点
    /// </summary>
    public enum ADFActiveCode
    {
        [Description("发起申请")]
        EmployeeSubmit = 1,
        [Description("领导审批")]
        LeaderApprove = 2
    }


    /// <summary>
    /// 在线请假模块节点名称
    /// </summary>
    public enum ADF_ApplyActiveName
    {
        [Description("发起申请")]
        EmployeeSubmit,
        [Description("领导审批")]
        LeaderApprove
    }

    /// <summary>
    /// 月底考勤说明模块节点名称
    /// </summary>
    public enum ADF_EditActiveName
    {
        [Description("助理发起")]
        AssistantSubmit,
        [Description("考勤说明")]
        EmployeeEdit,
        [Description("领导审批")]
        FirstApprove
    }

    /// <summary>
    /// 二级领导审批模块节点名称
    /// </summary>
    public enum ADF_SeniorActiveName
    {
        [Description("助理发起")]
        AssistantSummarySubmit,
        [Description("HR审批")]
        HRApprove,
        [Description("领导汇总审批")]
        SeniorLeaderApprove,
        [Description("HRC&B审批")]
        HRCBApproveAndEdit

    }

    /// <summary>
    /// 流程编码
    /// </summary>
    public enum ADF_WorkFlowCode
    {
        [Description("在线请假流程")]
        APF,
        [Description("一级领导审核流程")]
        PLF,
        [Description("二级领导审核流程")]
        SLF
    }

    /// <summary>
    /// 考勤包状态
    /// </summary>
    public enum SeniorBatchStates
    {
        [Description("草稿")]
        Draft = 0,

        [Description("一级审批流程提交中")]
        Submite = 1,

        [Description("冻结流程时间成功")]
        FrozenSuccess = 2,

        [Description("匹配打卡记录成功")]
        CreateRecordSuccess = 3,

        [Description("统计信息成功")]
        StatisticsSuccess = 4,

        [Description("一级领导流程发起")]
        PrimaryFlowSuccess = 5,

        [Description("二级领导审核流程发起")]
        SeniorFlowSuccess = 6,
        [Description("流程完结")]
        FlowEnd = 7,

        [Description("一级审批流程提交中失败")]
        SubmiteFailed = -1,

        [Description("冻结流程时间成功失败")]
        FrozenFailed = -2,

        [Description("匹配打卡记录成功失败")]
        CreateRecordFailed = -3,

        [Description("统计信息成功失败")]
        StatisticsFailed = -4,

        [Description("一级领导流程发起失败")]
        PrimaryFlowFailed = -5,

        [Description("二级领导审核流程发起失败")]
        SeniorFlowFailed = -6,


    }

    /// <summary>
    /// 特殊日期类型
    /// </summary>
    public enum SpecialDayType
    {
        [Description("工作日")]
        WorkDay = 1,
        [Description("休息日")]
        FreeDay,
        [Description("公假日")]
        PublicDay,
        [Description("节假日")]
        Holiday
    }
}
