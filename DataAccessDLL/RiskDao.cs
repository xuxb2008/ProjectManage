using CommonDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 风险管理
    /// 2017/03/31(zhuguanjun)
    /// </summary>
    public class RiskDao
    {
        /// <summary>
        /// 获取带分页和编号的数据集合
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public GridData GetGridData(int PageIndex, int PageSize, List<QueryField> qlist)
        {
            StringBuilder QueryHead = new StringBuilder();
            StringBuilder QueryBody = new StringBuilder();

            QueryHead.Append(" select r.*,d1.Name as LevelName,d2.Name as ProbabilityName,d3.Name as HandleTypeName,p1.Name as SourceName,p2.Name as DependencyName");
            QueryBody.Append(" from Risk r left join DictItem d1 on r.Level = d1.No and d1.DictNo=" + (int)DictCategory.Level);
            QueryBody.Append(" left join DictItem d2 on r.Probability = d2.No and d2.DictNo=" + (int)DictCategory.Probability);
            QueryBody.Append(" left join DictItem d3 on r.HandleType = d3.No and d3.DictNo=" + (int)DictCategory.HandType);
            QueryBody.Append(" left join PNode p1 on r.Source = p1.ID");
            //QueryBody.Append(" left join PNode p2 on r.Dependency = p2.ID");
            QueryBody.Append(" left join PNode p2 on r.Dependency = p2.ID");
            QueryBody.Append(" where r.PID=@PID  and r.status=@Status order by r.created");

            return NHHelper.GetGridData(PageIndex, PageSize, QueryHead.ToString(), QueryBody.ToString(), qlist);
        }

    }
}
