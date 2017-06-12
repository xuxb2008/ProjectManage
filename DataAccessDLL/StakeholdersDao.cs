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
    /// 干系人管理
    /// 2017/03/28(zhugj)
    /// </summary>
    public class StakeholdersDao
    {

        /// <summary>
        /// 获取带分页和编号的干系人列表集合
        /// Updated:20170601(ChengMengjia)  干系人类别需要LeftJoin查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public GridData GetGridData(int PageSize, int PageIndex, List<QueryField> qlist)
        {
            StringBuilder QueryHead = new StringBuilder();
            StringBuilder QueryBody = new StringBuilder();

            QueryHead.Append(" select s.*, s.companyname || '-' || s.name as truename, d1.Name as SendTypeName,d2.Name as TypeName");
            QueryBody.Append(" from Stakeholders s left join DictItem d1 on s.SendType = d1.No and d1.DictNo=" + (int)DictCategory.SendType);
            QueryBody.Append(" left join DictItem d2 on s.Type = d2.No and d2.DictNo=" + (int)DictCategory.StakehoderType);
            QueryBody.Append(" where s.PID=@PID  and s.status=@Status order by s.updated desc,s.created desc");

            return NHHelper.GetGridData(PageIndex, PageSize, QueryHead.ToString(), QueryBody.ToString(), qlist);
        }

    }
}
