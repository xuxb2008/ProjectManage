using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 工作困难系数Dao
    /// 2017/07/07(zhuguanjun)
    /// </summary>
    public class ReportDefficutyDegreeDao
    {
        public DataTable GetDefficutyDegree(string PID, DateTime Startedate, DateTime Enddate, int FinishStatus)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qlist.Add(new QueryField { Name = "StarteDate", Type = QueryFieldType.DateTime, Value = Startedate });
            qlist.Add(new QueryField { Name = "EndDate", Type = QueryFieldType.DateTime, Value = Enddate });
            qlist.Add(new QueryField { Name = "FinishStatus", Type = QueryFieldType.Numeric, Value = FinishStatus });
            StringBuilder sql = new StringBuilder();

            //sql.Append(@"
            //select * from (
            ///*内容*/
            //select * from (
            ///*日常*/
            //select * from (select 
            //(select count(*)+1 from routinework rin where rin.created<rw.created) as RowNo,
            //'日常' as source, r.name as name,r.Desc ,date(r.startdate) as startedate,date(r.enddate) as enddate,'1' as type,s.name as allname,
            //round((r.enddate-r.startdate+1)*1.0/rw.actualworkload,1) as efficiency,r.finishstatus from  routinework rw
            //inner join stakeholders s on substr(s.id,1,36) = rw.Manager and r.status = @status 
            //inner join routine r on rw.routineid = substr(r.id,1,36) and s.status =@status  
            //where 1=1 and s.pid =@pid  
            //order by rw.manager,rw.created)
            //union
            ///*交付物*/
            //select * from(select 
            //(select (select count(*) from routinework )+(select count(*) from troublework )
            //+ count(*)+1 from deliverableswork d where d.created<dw.created)as rowno,
            //'交付物' as source,d.name as name,d.Desc,date(d.startedate) as startdate,date(d.enddate) as enddate,
            //'3' as type ,s.name as allname,
            //round((d.enddate-d.startedate+1)*1.0/dw.actualworkload,1) as efficiency,
            //(case when pg.ptype = 5 then 3 else 2 end) as finishstatus from Deliverableswork dw
            //inner join stakeholders s on substr(s.id,1,36) = dw.Manager and s.status =@status 
            //inner join DeliverablesJBXX d on dw.JBXXid = substr(d.id,1,36) and d.status = @status  
            //inner join PNode  p on substr(p.id,1,36) = d.nodeid and p.status = @status    
            //inner join NodeProgress pg on pg.nodeid = substr(p.id,1,36) and pg.status = @status       
            //where 1=1 and s.pid =@pid  
            //)
            //union
            ///*问题*/
            //select * from(select 
            //(select (select count(*) from routinework )+ count(*)+1 from Troublework t where t.created<tw.created)as rowno,
            //'问题' as source,t.name as name ,t.Desc,date(t.startedate) as startdate,date(t.enddate) as enddate,'2' as type ,s.name as allname,
            //round((t.enddate-t.startedate+1)*1.0/tw.actualworkload,1) as efficiency,t.handlestatus as finishstatus from Troublework tw
            //inner join stakeholders s on substr(s.id,1,36) = tw.Manager and s.status =@status 
            //inner join Trouble t on tw.troubleid = substr(t.id,1,36) and t.status = @status  
            //where 1=1 and s.pid =@pid   
            //order by tw.manager,tw.created)

            //) where " +
            // (Startedate != DateTime.MinValue ? "date(startedate)>=date(@StarteDate) " : "1=1 ") +
            //(Enddate != DateTime.MinValue ? "and date(enddate)<=date(@EndDate) " : "and 1=1 ") +
            //(FinishStatus != 0 ? (FinishStatus == 3 ? "and finishstatus=3 " : "and finishstatus!=3 ") : "and 1=1 ") +
            //@"            

            //) order by type,rowno

            //");
            sql.Append(@"
                with cte as (select 
                '日常' as source, t.name as name,t.Desc ,date(t.startdate) as startdate,date(t.enddate) as enddate,'1' as type,workload,
                (select sum(tw.actualworkload) from routinework tw where substr(t.id,1,36)=tw.routineid) as actualworkload,
                round(((julianday(enddate)-julianday(startdate)+1)*1.0/(select sum(tw.actualworkload) from routinework tw where substr(t.id,1,36)=tw.routineid)),1) as degree 
                from routine t 
                inner join pnode pn on t.nodeid=substr(pn.id,1,36) 
                inner join project p on pn.pid=substr(p.id,1,36) and p.id=@PID
                where t.status=1 
                union
                select 
                '问题' as source, t.name as name,t.Desc ,date(t.startedate) as startdate,date(t.enddate) as enddate,'2' as type,workload,
                (select sum(tw.actualworkload) from troublework tw where substr(t.id,1,36)=tw.troubleid) as actualworkload ,
                round(((julianday(enddate)-julianday(startedate)+1)*1.0/(select sum(tw.actualworkload) from troublework tw where substr(t.id,1,36)=tw.troubleid)*1.0),1) as degree  
                from trouble t 
                inner join pnode pn on t.nodeid=substr(pn.id,1,36) 
                inner join project p on pn.pid=substr(p.id,1,36) and p.id=@PID 
                where t.status=1 
                union 
                select 
                '交付物' as source, t.name as name,t.Desc ,date(t.startedate) as startdate,date(t.enddate) as enddate,'3' as type,workload,
                (select sum(tw.actualworkload) from deliverableswork tw where substr(t.id,1,36)=tw.jbxxid) as actualworkload ,
                round(((julianday(enddate)-julianday(startedate)+1)*1.0/(select sum(tw.actualworkload) from deliverableswork tw where substr(t.id,1,36)=tw.jbxxid)),1) as degree 
                from deliverablesjbxx t 
                inner join pnode pn on t.nodeid=substr(pn.id,1,36) 
                inner join project p on pn.pid=substr(p.id,1,36) and p.id=@PID 
                where t.status=1) 

                select * from(select null as source, null as name,null as Desc ,null as startdate,null as enddate,'4' as type,null as workload, 
                '平均系数' as actualworkload ,
                round(sum(degree)/count(1),1) as degree from cte 
                union 
                select * from cte) order by type
            ");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            if (dt != null && dt.Rows.Count > 0)
                dt.Rows[dt.Rows.Count - 1]["RowNo"] = "";
            return dt;
        }
    }
}
