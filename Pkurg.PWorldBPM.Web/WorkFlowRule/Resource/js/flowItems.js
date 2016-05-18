
/*********************流程数据数组*********************/
/*房地产业务流程*/
var flowItems1 = [

{ id: 1101, row: 1, flowId: 124, page: "ItemDetail", name: "项目投资立项申请（城市公司）" },
{ id: 1102, row: 1, flowId: 125, page: "ItemDetail", name: "项目投资立项申请（北大资源）" },
{ id: 1103, row: 1, flowId: 126, page: "ItemDetail", name: "项目立项考核指标变更（城市公司）" },
{ id: 1104, row: 1, flowId: 127, page: "ItemDetail", name: "项目立项考核指标变更（北大资源）" },
{ id: 1105, row: 1, flowId: 128, page: "AffairDetail", name: "项目立项通知书下发（北大资源）" },

{ id: 1201, row: 2, flowId: 0, page: "", name: "招标需求申请单", url: "/Workflow/EditPage/E_JC_ProjectTenderCityCompany.aspx"},
{ id: 1202, row: 2, flowId: 0, page: "", name: "入围投标审批单", url: "/Workflow/EditPage/E_JC_FinalistApproval.aspx" },
{ id: 1203, row: 2, flowId: 0, page: "", name: "招标定标审批单", url: "/Workflow/EditPage/E_JC_BidScaling.aspx" },
{ id: 1204, row: 2, flowId: 2005, page: "bpm", name: "招标需求(集团内部)", url: "/Workflow/EditPage/E_JC_ProjectTenderGroup.aspx" },
{ id: 1205, row: 2, flowId: 0, page: "", name: "招标特殊事项申请单", url: "/Workflow/EditPage/E_JC_TenderSpecialItem.aspx" },
{ id: 1206, row: 3, flowId: 0, page: "", name: "外墙涂料采购申请单", url: "/Workflow/EditPage/E_JC_PurchasePaint.aspx" }
];

/*总部管理流程*/
var flowItems2 = [
{ id: 2101, row: 1, flowId: 129, page: "AffairDetail", name: "IT支持申请单（北大资源）" },
{ id: 2102, row: 1, flowId: 3001, page: "bpm", name: "流程单（资源投资）", url: "/Workflow/EditPage/E_OA_InstructionOfPKURGI.aspx" },
{ id: 2103, row: 1, flowId: 3003, page: "bpm", name: "流程单（资源集团）", url: "/Workflow/EditPage/E_OA_InstructionOfGroup.aspx" },
{ id: 2105, row: 1, flowId: 3005, page: "bpm", name: "合同流程单（资源投资）", url: "/Workflow/EditPage/E_OA_ContractAuditOfPKURGI.aspx" },
{ id: 2106, row: 1, flowId: 3007, page: "bpm", name: "合同流程单（资源集团）", url: "/Workflow/EditPage/E_OA_ContractAuditOfGroup.aspx" },
{ id: 2112, row: 1, flowId: 0, page: "", name: "自定义流程（新版）", url: "/Workflow/EditPage/E_OA_CustomWorkflow.aspx" },


{ id: 2201, row: 2, flowId: 70, page: "AffairDetail", name: "名片印制申请（北大资源）" },
{ id: 2204, row: 2, flowId: 1001, page: "bpm", name: "制度发文审批单", url: "/Workflow/EditPage/E_OA_SystemDispatch.aspx" },
{ id: 2205, row: 2, flowId: 3009, page: "bpm", name: "印章申请（资源投资）", url: "/Workflow/EditPage/E_OA_SealOfPKURGI.aspx" },
{ id: 2206, row: 2, flowId: 3010, page: "bpm", name: "印章申请（资源集团）", url: "/Workflow/EditPage/E_OA_SealOfGroup.aspx" },

{ id: 2301, row: 3, flowId: 41, page: "AffairDetail", name: "人员需求申请（北大资源）" },
{ id: 2303, row: 3, flowId: 57, page: "AffairDetail", name: "员工录用审批（北大资源）" },
{ id: 2304, row: 3, flowId: 71, page: "AffairDetail", name: "实习生录用审批（北大资源）" },
{ id: 2305, row: 3, flowId: 46, page: "AffairDetail", name: "转正审批申请（北大资源）" },
{ id: 2306, row: 3, flowId: 130, page: "AffairDetail", name: "干部任免审批（北大资源）" },
{ id: 2307, row: 3, flowId: 43, page: "AffairDetail", name: "薪酬调整审批（北大资源）" },
{ id: 2308, row: 3, flowId: 53, page: "AffairDetail", name: "薪酬调整审批（所属企业）" },
{ id: 2309, row: 3, flowId: 94, page: "AffairDetail", name: "员工流动审批（北大资源）" },
{ id: 2310, row: 3, flowId: 54, page: "AffairDetail", name: "员工离职转单（北大资源）" },

{ id: 2401, row: 4, flowId: 0, page: "bpm", name: "人员需求申请（资源投资）", url: "/Workflow/EditPage/E_HR_EmployeeNeed.aspx" },
{ id: 2402, row: 4, flowId: 0, page: "bpm", name: "员工录用审批（资源投资）", url: "/Workflow/EditPage/E_HR_Employment.aspx" },

{ id: 2403, row: 4, flowId: 0, page: "bpm", name: "转正审批申请（资源投资）", url: "/Workflow/EditPage/E_HR_EmployeeRegular.aspx" },
{ id: 2404, row: 4, flowId: 0, page: "bpm", name: "员工流动审批（资源投资）", url: "/Workflow/EditPage/E_HR_EmployeeTransfer.aspx" },
{ id: 2405, row: 4, flowId: 0, page: "bpm", name: "薪酬调整审批（资源投资）", url: "/Workflow/EditPage/E_HR_SalaryAdjust.aspx" },
{ id: 2406, row: 4, flowId: 0, page: "bpm", name: "薪酬调整审批（资源投资统管干部）", url: "/Workflow/EditPage/E_HR_SalaryAdjust.aspx?ref=EI" },
{ id: 2407, row: 4, flowId: 0, page: "bpm", name: "干部任免审批（资源投资）", url: "/Workflow/EditPage/E_HR_CadresOrRemoval.aspx" },
{ id: 2408, row: 4, flowId: 0, page: "bpm", name: "实习生录用审批", url: "/Workflow/EditPage/E_HR_InternEmploy.aspx" },
{ id: 2409, row: 4, flowId: 0, page: "bpm", name: "员工离职审批", url: "/Workflow/EditPage/E_HR_EmployeeLeft.aspx" },

{ id: 2501, row: 5, flowId: 0, page: "bpm", name: "人员需求申请（资源集团）", url: "/Workflow/EditPage/E_HR_EmployeeNeed.aspx?ref=group" },
{ id: 2502, row: 5, flowId: 0, page: "bpm", name: "员工录用审批（资源集团）", url: "/Workflow/EditPage/E_HR_Employment.aspx?ref=group" },
{ id: 2503, row: 5, flowId: 0, page: "bpm", name: "转正审批申请（资源集团）", url: "/Workflow/EditPage/E_HR_EmployeeRegular.aspx?ref=group" },
{ id: 2504, row: 5, flowId: 0, page: "bpm", name: "员工流动审批（资源集团）", url: "/Workflow/EditPage/E_HR_EmployeeTransfer.aspx?ref=group" },
{ id: 2505, row: 5, flowId: 0, page: "bpm", name: "薪酬调整审批（资源集团）", url: "/Workflow/EditPage/E_HR_SalaryAdjust.aspx?ref=group" },
{ id: 2506, row: 5, flowId: 0, page: "bpm", name: "薪酬调整审批（资源集团统管干部）", url: "/Workflow/EditPage/E_HR_SalaryAdjust.aspx?ref=EG" },
{ id: 2507, row: 5, flowId: 0, page: "bpm", name: "干部任免审批（资源集团）", url: "/Workflow/EditPage/E_HR_CadresOrRemoval.aspx?ref=group" }
];

/*城市公司流程*/

var flowItems3 = [

{ id: 3101, row: 1, flowId: 0, page: "", name: "流程单（资源投资所属企业）", url: "/Workflow/EditPage/E_OA_InstructionOfEToI.aspx"},
{ id: 3102, row: 1, flowId: 0, page: "", name: "流程单（资源集团所属企业）", url: "/Workflow/EditPage/E_OA_InstructionOfEToG.aspx" },
{ id: 3103, row: 1, flowId: 0, page: "", name: "合同流程单（资源投资所属企业）", url: "/Workflow/EditPage/E_OA_ContractAuditOfEToI.aspx" },
{ id: 3104, row: 1, flowId: 0, page: "", name: "合同流程单（资源集团所属企业）", url: "/Workflow/EditPage/E_OA_ContractAuditOfEToG.aspx" },
{ id: 3106, row: 1, flowId: 0, page: "", name: "自定义流程（新版）", url: "/Workflow/EditPage/E_OA_CustomWorkflow.aspx" },

{ id: 3301, row: 2, flowId: 131, page: "ItemDetail", name: "请示单（湖南）" },
{ id: 3302, row: 2, flowId: 132, page: "ItemDetail", name: "合同审批单（湖南）" },
{ id: 3303, row: 2, flowId: 138, page: "ItemDetail", name: "发文审批单（湖南）" },
{ id: 3304, row: 2, flowId: 139, page: "AffairDetail", name: "请假申请（湖南）" },
{ id: 3305, row: 2, flowId: 144, page: "AffairDetail", name: "派车申请单（湖南）" },
{ id: 3306, row: 2, flowId: 145, page: "AffairDetail", name: "转正审批单（湖南）" },
{ id: 3307, row: 2, flowId: 146, page: "AffairDetail", name: "员工离职转单（湖南）" },

{ id: 3201, row: 3, flowId: 133, page: "AffairDetail", name: "固定资产购置申请（贵阳地产）" },
{ id: 3202, row: 3, flowId: 148, page: "ItemDetail", name: "印章申请（重庆）" },
{ id: 3203, row: 3, flowId: 150, page: "ItemDetail", name: "加班年假请示单（重庆）" },
{ id: 3204, row: 3, flowId: 135, page: "ItemDetail", name: "请示单（重庆）" },
{ id: 3205, row: 3, flowId: 140, page: "AffairDetail", name: "派车单（天津）" },
{ id: 3206, row: 4, flowId: 115, page: "AffairDetail", name: "请示单" },
{ id: 3207, row: 4, flowId: 149, page: "AffairDetail", name: "请款单/报销单" },
{ id: 3208, row: 4, flowId: 151, page: "AffairDetail", name: "请款单/报销单（诊所）" },


{ id: 3501, row: 5, flowId: 0, page: "", name: "合同单（物业）", url: "/Workflow/EditPage/E_OA_ContractOfWY.aspx" },
{ id: 3502, row: 5, flowId: 0, page: "", name: "合同单（物业集团分公司）", url: "/Workflow/EditPage/E_OA_ContractOfWY.aspx" },
{ id: 3503, row: 5, flowId: 0, page: "", name: "请示单（物业）", url: "/Workflow/EditPage/E_OA_InstructionOfWY.aspx" },
{ id: 3504, row: 5, flowId: 0, page: "", name: "请示单（物业集团分公司）", url: "/Workflow/EditPage/E_OA_InstructionOfWY.aspx" },
{ id: 3505, row: 5, flowId: 0, page: "", name: "印章申请（物业集团）", url: "/Workflow/EditPage/E_OA_SealOfWY.aspx" },
{ id: 3506, row: 5, flowId: 0, page: "", name: "印章申请（物业集团分公司）", url: "/Workflow/EditPage/E_OA_SealOfWY.aspx" },
{ id: 3507, row: 5, flowId: 0, page: "", name: "自定义流程（新版）", url: "/Workflow/EditPage/E_OA_CustomWorkflow.aspx" },


{ id: 3601, row: 6, flowId: 158, page: "ItemDetail", name: "请示单（方亚海泰）" },
{ id: 3602, row: 6, flowId: 161, page: "ItemDetail", name: "印章审批（方亚海泰）" },
{ id: 3603, row: 6, flowId: 163, page: "ItemDetail", name: "合同审批（方亚海泰）" },
{ id: 3604, row: 6, flowId: 166, page: "ItemDetail", name: "发文审批（方亚海泰）" },
{ id: 3605, row: 6, flowId: 167, page: "AffairDetail", name: "派车单（方亚海泰）" },
{ id: 3606, row: 6, flowId: 170, page: "AffairDetail", name: "请假单（方亚海泰）" },
{ id: 3607, row: 6, flowId: 168, page: "AffairDetail", name: "证照、档案调阅单（方亚海泰）" },
{ id: 3608, row: 6, flowId: 160, page: "ItemDetail", name: "请示单（海分）" },
{ id: 3609, row: 6, flowId: 165, page: "ItemDetail", name: "合同审批（海分）" },
{ id: 3610, row: 6, flowId: 159, page: "ItemDetail", name: "请示单（所属公司）" },
{ id: 3611, row: 6, flowId: 164, page: "ItemDetail", name: "合同审批（所属公司）" },
{ id: 3612, row: 6, flowId: 162, page: "ItemDetail", name: "印章审批（所属公司）" },
{ id: 3613, row: 6, flowId: 169, page: "AffairDetail", name: "证照、档案调阅单（所属公司）" },

{ id: 3701, row: 7, flowId: 98, page: "ItemDetail", name: "请示单（科技园）" },
{ id: 3702, row: 7, flowId: 99, page: "ItemDetail", name: "请示单（科技园所属企业）" },
{ id: 3703, row: 7, flowId: 152, page: "ItemDetail", name: "合同（科技园）" },
{ id: 3704, row: 7, flowId: 153, page: "ItemDetail", name: "合同（科技园所属企业）" },
{ id: 3705, row: 7, flowId: 154, page: "AffairDetail", name: "请假单（科技园）" },
{ id: 3705, row: 7, flowId: 156, page: "AffairDetail", name: "请假单（北大科技园所属企业）" },
{ id: 3705, row: 7, flowId: 155, page: "AffairDetail", name: "员工录用审批（科技园）" },
{ id: 3705, row: 7, flowId: 157, page: "AffairDetail", name: "名片印制申请（科技园）" }


];

