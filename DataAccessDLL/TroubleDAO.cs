using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 类名：项目问题数据处理类
    /// Created：2017.04.06(xuxb)
    /// </summary>
    public class TroubleDAO
    {
        /// <summary>
        /// 项目问题查询
        /// Created:2017.04.06(xuxb)
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string PID, string startDate, string endDate, string key)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.ID,r.Name,r.Desc,r.HandleResult,s.Name HandleManName, ");
            sql.Append(" strftime('%Y-%m-%d',r.StarteDate) as StartDate,strftime('%Y-%m-%d',r.EndDate) as EndDate,d.Name as HandleStatus,d1.Name as Level from Trouble r ");
            sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1");
            sql.Append(" left join Stakeholders s on r.HandleMan = substr(s.ID,1,36) ");
            sql.Append(" left join DictItem d on r.HandleStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.TroubleHandleStatus);
            sql.Append(" left join DictItem d1 on r.Level = d1.No and d1.DictNo = " + (int)CommonDLL.DictCategory.TroubleLevel);
            sql.Append(" where r.status = 1 and p.PID = @PID ");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });

            //开始日期
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.Append(" and date(r.StarteDate) >= date(@startDate)");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = DateTime.Parse(startDate).ToString("yyyy-MM-dd") });
            }

            //结束日期
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.Append(" and date(r.EndDate) <= date(@endDate) )");
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = DateTime.Parse(endDate).ToString("yyyy-MM-dd") });
            }
            //关键字
            if (!string.IsNullOrEmpty(key))
            {
                sql.Append(" and (r.Name like '%' || @key || '%' or r.Desc like '%' || @key || '%' or r.DealResult like '%' || @key || '%') ");
                qf.Add(new QueryField() { Name = "key", Type = QueryFieldType.String, Value = key });
            }

            sql.Append(" order by r.StarteDate Desc  ");

            DataSet ds = NHHelper.ExecuteDataset(sql.ToString(), qf);

            if (ds != null && ds.Tables.Count > 0)
            {
                return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
            }
            else
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 项目问题查询
        /// Created:2017.04.21(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string PID, string startDate, string endDate, int? Status)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.ID,r.Name,p.Name NodeName,r.Desc,r.HandleResult,s.Name HandleMan,strftime('%Y-%m-%d',r.HandleDate) as HandleDate, ");
            sql.Append(" strftime('%Y-%m-%d',r.StarteDate) as StartDate,strftime('%Y-%m-%d',r.EndDate) as EndDate,d.Name as HandleStatus,d1.Name as Level from Trouble r ");
            sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1");
            sql.Append(" left join Stakeholders s on r.HandleMan = substr(s.ID,1,36) ");
            sql.Append(" left join DictItem d on r.HandleStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.TroubleHandleStatus);
            sql.Append(" left join DictItem d1 on r.Level = d1.No and d1.DictNo = " + (int)CommonDLL.DictCategory.TroubleLevel);
            sql.Append(" where r.status = 1 and p.PID = @PID ");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });

            //开始日期
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.Append(" and date(r.HandleDate) >= date(@startDate)");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = DateTime.Parse(startDate).ToString("yyyy-MM-dd") });
            }

            //结束日期
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.Append(" and date(r.HandleDate) <= date(@endDate) )");
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = DateTime.Parse(endDate).ToString("yyyy-MM-dd") });
            }
            //解决状态
            if (Status != null)
            {
                sql.Append(" and r.HandleStatus =@status ");
                qf.Add(new QueryField() { Name = "status", Type = QueryFieldType.Numeric, Value = Status });
            }

            sql.Append(" order by r.StarteDate Desc  ");
            return NHHelper.ExecuteDataTable(sql.ToString(), qf);
        }
    }
}
