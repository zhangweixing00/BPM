using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL
{
    public interface IPositionDAL
    {
        /// <summary>
        /// 取得职位信息
        /// </summary>
        /// <returns></returns>
        DataSet GetPosition();
    }
}
