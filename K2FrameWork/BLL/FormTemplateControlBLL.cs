using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;

namespace BLL
{
    public class FormTemplateControlBLL
    {
        //创建dal连接
        private static readonly IFormTemplateControlDAL dal = DALFactory.DataAccess.CreateFormTemplateControlDAL();

        public bool CreateFormTemplateControl(FormTemplateControlInfo info)
        {
            return dal.CreateFormTemplateControl(info);
        }
    }
}
