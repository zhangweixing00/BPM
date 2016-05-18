using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace IDAL
{
    public interface IFormTemplateControlPropertyDAL
    {
        bool CreateFormTemplateControlProperty(FormTemplateControlPropertyInfo info);
    }
}
