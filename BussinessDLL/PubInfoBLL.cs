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
    public class PubInfoBLL
    {
        PubInfoDAO dao = new PubInfoDAO();

        /// <summary>
        /// 保存信息发布报发送记录
        /// Created:20170527(ChengMengjia)
        /// </summary>
        /// <param name="risk"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public JsonResult SavePubInfo(PubInfo entity, List<DeliverablesFiles> list)
        {
            List<PubInfoFiles> listFiles = new List<PubInfoFiles>();
            entity.ID = Guid.NewGuid().ToString();
            entity.Status = 1;
            entity.CREATED = DateTime.Now;
            list.ForEach(t =>
            {
                listFiles.Add(new PubInfoFiles()
                {
                    ID = Guid.NewGuid().ToString(),
                    PubID = entity.ID,
                    Name = t.Name,
                    Path = t.Path,
                    Status = 1,
                    CREATED = DateTime.Now,
                });
            });
            return dao.AddPubInfo(entity, listFiles);
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

