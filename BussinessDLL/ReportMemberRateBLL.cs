using DataAccessDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 成员贡献率Bll
    /// 2017/06/07(zhuguanjun)
    /// </summary>
    public class ReportMemberRateBLL
    {
        public DataTable GetMemberRate(string projectId)
        {
            return new ReportMemberRateDao().GetMemberRate(projectId);
        }
    }
}
