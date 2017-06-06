using CommonDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    public class MilestoneDAO
    {
        /// <summary>
        /// 里程碑列表
        /// Created:20170327(ChengMengjia)
        /// Updated:20170405(ChengMengjia) liuxx要求去除分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public GridData GetLCBList(int PageIndex, int PageSize, string PID)
        {
            //List<QueryField> qf = new List<QueryField>();
            //StringBuilder sqlHead = new StringBuilder();
            //sqlHead.Append(" select m.id,m.name,m.condition,m.remark,strftime('%Y-%m-%d',m.FinishDate)FinishDate,");
            //sqlHead.Append(" m.FinishStatus,strftime('%Y-%m-%d',m.CREATED)CREATED,d.Name FinishStatusName ");
            //StringBuilder sqlBody = new StringBuilder();
            //sqlBody.Append(" from Milestones m left join DictItem d on d.DictNo=" + (int)DictCategory.Milestones_FinshStatus + " and m.FinishStatus=D.No ");
            //sqlBody.Append(" where m.PID=@PID  and m.status=1 order by m.updated desc,m.created asc");
            //qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            //GridData result = NHHelper.GetGridData(PageIndex, PageSize, sqlHead.ToString(), sqlBody.ToString(), qf);
            //return result;
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql= new StringBuilder();
            sql.Append(" select m.id,m.name,m.condition,m.remark,strftime('%Y-%m-%d',m.FinishDate)FinishDate,");
            sql.Append(" m.FinishStatus,strftime('%Y-%m-%d',m.CREATED)CREATED,d.Name FinishStatusName ");
            sql.Append(" from Milestones m left join DictItem d on d.DictNo=" + (int)DictCategory.Milestones_FinshStatus + " and m.FinishStatus=D.No ");
            sql.Append(" where m.PID=@PID  and m.status=1 ");         
            sql.Append(" order by m.updated desc,m.created asc");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            GridData result = new GridData();
            result.data = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            return result;
        }
        /// <summary>
        /// 里程碑列表
        /// Created:20170421(ChengMengjia)
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetLCBList(string startDate, string endDate, string PID)
        {          
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select m.id,m.name,m.condition,m.remark,strftime('%Y-%m-%d',m.FinishDate)FinishDate,");
            sql.Append(" m.FinishStatus,strftime('%Y-%m-%d',m.CREATED)CREATED,d.Name FinishStatusName ");
            sql.Append(" from Milestones m left join DictItem d on d.DictNo=" + (int)DictCategory.Milestones_FinshStatus + " and m.FinishStatus=D.No ");
            sql.Append(" where m.PID=@PID  and m.status=1 ");
            //开始日期
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.Append(" and date(m.FinishDate) >= date(@startDate)");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = DateTime.Parse(startDate).ToString("yyyy-MM-dd") });
            }
            //结束日期
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.Append(" and (date(m.FinishDate) <= date(@endDate) or m.FinishDate is null )");
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = DateTime.Parse(endDate).ToString("yyyy-MM-dd") });
            }
            sql.Append(" order by m.updated desc,m.created asc");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            return NHHelper.ExecuteDataTable(sql.ToString(), qf);
        }
    }
}
