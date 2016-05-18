using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;

namespace BLL
{
    public class FormTemplateControlPropertyBLL
    {
        //创建dal连接
        private static readonly IFormTemplateControlPropertyDAL dal = DALFactory.DataAccess.CreateFormTemplateControlPropertyDAL();

        public bool CreateFormTemplateControlProperty(FormTemplateControlPropertyInfo info)
        {
            return dal.CreateFormTemplateControlProperty(info);
        }
    }
}
