using CommonDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// WBSCodeDao
    /// 2017/05/04(zhuguanjun)
    /// </summary>
    public class WBSCodeDao
    {
        /// <summary>
        /// 获取wbs代码列表
        /// 2017/05/04(zhuguanjun)
        /// </summary>
        /// <returns></returns>
        public DataTable GetWBSCodeList(List<QueryField> qlist)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select w.*,d2.Name as LengthName, d3.Name as BreakName,");
            sql.Append(" case w.Orderr when "+(int)WBSCodeOrder.Upper+" then '"+ EnumsHelper.GetDescription(WBSCodeOrder.Upper)+"'");
            sql.Append(" when " + (int)WBSCodeOrder.Lower + " then '" + EnumsHelper.GetDescription(WBSCodeOrder.Lower)+"'");
            sql.Append(" when " + (int)WBSCodeOrder.Number + " then '" + EnumsHelper.GetDescription(WBSCodeOrder.Number)+"' end as OrderName");
            sql.Append(" from WBSCode w");
            sql.Append(" left join DictItem d1 on d1.No = w.Orderr and d1.DictNo = " + (int)DictCategory.WBSCodeOrder);
            sql.Append(" left join DictItem d2 on d2.No = w.Length and d2.DictNo = " + (int)DictCategory.WBSCodeLength);
            sql.Append(" left join DictItem d3 on d3.No = w.Breakk and d3.DictNo = " + (int)DictCategory.WBSCodeBreak);
            sql.Append(" where PID=@PID ");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt;
        }

    }
}
