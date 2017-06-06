using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using CommonDLL;
using System.IO;
using DomainDLL;
using DataAccessDLL;

namespace BussinessDLL
{
    public class ReportBLL
    {
        ReportDAO dao = new ReportDAO();


        #region 周报

        /// <summary>
        /// 本周完成工作
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetRoutineList(string PID)
        {
            DateTime now = DateTime.Now;  //当前时间  
            DateTime startWeek = now.AddDays(1 - Convert.ToInt32(now.DayOfWeek.ToString("d")));  //本周周一  
            DateTime endWeek = startWeek.AddDays(6);  //本周周日  
            return new RoutineBLL().GetRoutinList(PID, startWeek.ToString(), endWeek.ToString(), "");
        }

        #endregion

        #region 周报生成内容查询

        /// <summary>
        /// 本周完成工作查询 
        /// Created:20170508(ChengMengjia)
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <param name="NeedWork"></param>
        /// <param name="NeedLCB"></param>
        /// <param name="NeedJFW"></param>
        /// <returns></returns>
        public DataTable GetFinishedWork(string ProjectId, DateTime startDay, DateTime endDay, bool NeedWork, bool NeedLCB, bool NeedJFW)
        {
            StringBuilder sql = new StringBuilder();
            List<QueryField> qf = new List<QueryField>();
            sql.Append(" select '' Type,'' Name,'' Desc,'' Result,'' Person,'' Status ");
            if (NeedWork)
            {
                //本周工作
                sql.Append(" union  all ");
                sql.Append(" select p.Name Type,r.Name,r.Desc,r.DealResult Result,'' Person,d.Name Status from Routine r ");
                sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1 ");
                sql.Append(" left join DictItem d on r.FinishStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.WorkHandleStatus);
                sql.Append(" where r.status = 1 and r.FinishStatus=2 and p.PID=@PID and date(r.StartDate) >= date(@startDate) and (date(r.EndDate) <= date(@endDate) or r.EndDate is null )");
            }
            if (NeedLCB)
            {
                //本周里程碑
                sql.Append(" union  all ");
                sql.Append(" select '里程碑' Type,m.name,m.Condition Desc,m.Remark Result,'' Person,d.Name Status from Milestones m  ");
                sql.Append(" left join DictItem d on d.DictNo=" + (int)DictCategory.Milestones_FinshStatus + " and m.FinishStatus=D.No ");
                sql.Append(" where m.status=1 and m.PID=@PID and  date(m.FinishDate) >= date(@startDate) and date(m.FinishDate) <= date(@endDate) ");
            }
            if (NeedJFW)
            {
                //本周交付物
                sql.Append(" union  all ");
                sql.Append(" select parent.Name Type,j.name,j.Desc,'' Result,j.Manager Person,'' Status ");
                sql.Append(" from DeliverablesJBXX j inner join PNode n on j.NodeID=substr(n.ID,1,36) and n.status=1");
                sql.Append(" left join PNode parent on n.ParentID=substr(parent.ID,1,36) and parent.status=1");
                sql.Append(" where j.status=1 and n.PID=@PID and date(j.StarteDate) >= date(@startDate) and (date(j.EndDate) <= date(@endDate) or j.EndDate is null ) ");
            }
            qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = startDay.ToString("yyyy-MM-dd") });
            qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = endDay.ToString("yyyy-MM-dd") });
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectId });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }


        /// <summary>
        /// 下周计划完成工作查询
        /// Created:20170508(ChengMengjia)
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <param name="NeedWork"></param>
        /// <param name="NeedLCB"></param>
        /// <param name="NeedJFW"></param>
        /// <returns></returns>
        public DataTable GetUnFinishWork(string ProjectId, DateTime startDay, DateTime endDay, bool NeedWork, bool NeedLCB, bool NeedJFW)
        {
            StringBuilder sql = new StringBuilder();
            List<QueryField> qf = new List<QueryField>();
            sql.Append(" select '' Type,'' Name,'' Desc,'' Result,'' Person,'' Status ");
            if (NeedWork)
            {
                //下周工作
                sql.Append(" union  all ");
                sql.Append(" select p.Name Type,r.Name,r.Desc,r.DealResult Result,'' Person,d.Name Status from Routine r ");
                sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1 ");
                sql.Append(" left join DictItem d on r.FinishStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.WorkHandleStatus);
                sql.Append(" where r.status = 1 and r.FinishStatus=1 and p.PID=@PID and (date(r.StartDate) >= date(@startDate) or date(r.EndDate) <= date(@endDate) )");
            }
            if (NeedLCB)
            {
                //下周里程碑
                sql.Append(" union  all ");
                sql.Append(" select '里程碑' Type,m.name,m.Condition Desc,m.Remark Result,'' Person,d.Name Status from Milestones m  ");
                sql.Append(" left join DictItem d on d.DictNo=" + (int)DictCategory.Milestones_FinshStatus + " and m.FinishStatus=D.No ");
                sql.Append(" where m.status=1 and m.PID=@PID and  date(m.FinishDate) >= date(@startDate) and date(m.FinishDate) <= date(@endDate) ");
            }
            if (NeedJFW)
            {
                //下周交付物
                sql.Append(" union  all ");
                sql.Append(" select parent.Name Type,j.name,j.Desc,'' Result,j.Manager Person,'' Status ");
                sql.Append(" from DeliverablesJBXX j inner join PNode n on j.NodeID=substr(n.ID,1,36) and n.status=1");
                sql.Append(" left join PNode parent on n.ParentID=substr(parent.ID,1,36) and parent.status=1");
                sql.Append(" where j.status=1 and n.PID=@PID and (date(j.StarteDate) >= date(@startDate) or date(j.EndDate) <= date(@endDate) ) ");
            }
            qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = startDay.ToString("yyyy-MM-dd") });
            qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = endDay.ToString("yyyy-MM-dd") });
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectId });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }


        /// <summary>
        /// 存在的问题查询
        /// Created:20170508(ChengMengjia)
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="startDay"></param>
        /// <param name="NeedFinished"></param>
        /// <param name="NeedUnfinish"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string ProjectId, DateTime startDay, bool NeedFinished, bool NeedUnfinish)
        {
            StringBuilder sql = new StringBuilder();
            List<QueryField> qf = new List<QueryField>();
            sql.Append(" select '' Name,'' Desc,'' Result,'' Person,'' Status,'' HandleDate ");
            if (NeedFinished)
            {
                sql.Append(" union all ");
                sql.Append(" select r.Name,r.Desc,r.HandleResult Result,s.Name Person,d.Name Status,strftime('%Y-%m-%d',r.HandleDate) HandleDate from Trouble r ");
                sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1 ");
                sql.Append(" left join Stakeholders s on r.HandleMan = substr(s.ID,1,36) ");
                sql.Append(" left join DictItem d on r.HandleStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.TroubleHandleStatus);
                sql.Append(" where r.status = 1 and p.PID=@PID and r.HandleStatus=2 ");
                sql.Append(" and date(r.HandleDate) >= date(@startDate) and date(r.HandleDate) <= date(@endDate) ");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = startDay.ToString("yyyy-MM-dd") });
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = startDay.AddDays(6).ToString("yyyy-MM-dd") });
            }
            if (NeedUnfinish)
            {
                sql.Append(" union all ");
                sql.Append(" select r.Name,r.Desc,r.HandleResult Result,s.Name Person,d.Name Status,strftime('%Y-%m-%d',r.HandleDate) HandleDate from Trouble r ");
                sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1 ");
                sql.Append(" left join Stakeholders s on r.HandleMan = substr(s.ID,1,36) ");
                sql.Append(" left join DictItem d on r.HandleStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.TroubleHandleStatus);
                sql.Append(" where r.status = 1 and p.PID=@PID and r.HandleStatus=1 ");
                sql.Append(" and date(r.EndDate) <= date(@endDate2) ");
                qf.Add(new QueryField() { Name = "endDate2", Type = QueryFieldType.String, Value = startDay.AddDays(13).ToString("yyyy-MM-dd") });
            }
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectId });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }

        #endregion

        /// <summary>
        /// 保存周报发送记录
        /// Created:20170508(ChengMengjia)
        /// </summary>
        /// <param name="risk"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public JsonResult SaveWeeklyHistory(Report_Weekly entity, List<Report_WeeklyFiles> list)
        {
            return dao.Add_RptWeekly(entity, list);
        }

        /// <summary>
        /// 获取周报发送记录
        /// Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable getWeeklyHistory(string ProjectID, string start, string end, string query)
        {
            List<QueryField> qf = new List<QueryField>();

            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Report_Weekly where PID=@ProjectID and status=1 ");
            qf.Add(new QueryField() { Name = "ProjectID", Type = QueryFieldType.String, Value = ProjectID });
            if (!string.IsNullOrEmpty(start))
            {
                sql.Append(" and  CREATED>=@start");
                qf.Add(new QueryField() { Name = "start", Type = QueryFieldType.String, Value = DateTime.Parse(start).ToString("yyyy-MM-dd 00:00:00") });
            }
            if (!string.IsNullOrEmpty(end))
            {
                sql.Append(" and  CREATED<=@end");
                qf.Add(new QueryField() { Name = "end", Type = QueryFieldType.String, Value = DateTime.Parse(end).ToString("yyyy-MM-dd 23:59:59") });
            }
            if (!string.IsNullOrEmpty(query))
            {
                sql.Append(" and  Title+Content like @query ");
                qf.Add(new QueryField() { Name = "query", Type = QueryFieldType.String, Value = "%" + query + "%" });
            }
            sql.Append(" order by CREATED ");

            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }

        /// <summary>
        /// 获取周报发送记录
        /// Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Report_Weekly getWeeklyHistory(string id)
        {
            return new Repository<Report_Weekly>().Get(id);
        }

        public List<Report_WeeklyFiles> getWeeklyHistoryFiles(string ReportID)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            qf.Add(new QueryField() { Name = "ReportID", Type = QueryFieldType.String, Value = ReportID });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            return new Repository<Report_WeeklyFiles>().GetList(qf, sf) as List<Report_WeeklyFiles>;
        }
    }
}

