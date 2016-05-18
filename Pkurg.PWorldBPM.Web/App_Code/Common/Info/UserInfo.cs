using System;
using System.Collections.Generic;

namespace Pkurg.PWorldBPM.Common.Info
{
    [Serializable]
    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }

        public string FounderLoginId { get; set; }

        public List<string> DepIds { get; set; }

        private string mainDeptId;
        public string MainDeptId
        {
            get
            {
                if (string.IsNullOrEmpty(mainDeptId))
                {
                    mainDeptId = DepIds != null && DepIds.Count > 0 ? DepIds[0] : "";
                }
                return mainDeptId;
            }
            set { mainDeptId = value; }
        }
        public Pkurg.PWorld.Entities.Employee PWordUser { get; set; }

        private string approvalUser;

        /// <summary>
        /// 审批用户：为以后审批跨越做准备
        /// </summary>
        public string ApprovalUser
        {
            get 
            {
                return "founder\\" + LoginId;
            }
            // set { approvalUser = value; }暂时不通过set方法
        }

    }
}
