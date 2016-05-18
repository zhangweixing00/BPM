using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace IDAL
{
    public interface IFormDesignDAL
    {
        bool CreateControl(ControlInfo info);

        DataTable GetAllCountrol();

        DataTable GetControlById(Guid Id);

        bool DelControlById(Guid Id);

        bool UpdateControl(ControlInfo info);

        DataTable GetALLFormTemplate();

        bool UpdateFormTemplate(FormTemplateInfo info);

        bool UpdateFormTemplateHtml(FormTemplateInfo info);

        bool CreateFormTemplate(FormTemplateInfo info);

        DataTable GetFormTemplateById(Guid Id);

        bool DelFormTemplateById(Guid Id);
    }
}
