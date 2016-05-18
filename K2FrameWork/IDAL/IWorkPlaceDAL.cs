using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL
{
    public interface IWorkPlaceDAL
    {
        DataSet GetWorkPlace();

        bool AddEditWorkPlace(Guid ID, string placeName, string placeCode);
    }
}
