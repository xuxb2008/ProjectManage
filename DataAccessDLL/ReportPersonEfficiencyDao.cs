using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 人员执行效率报表
    /// 2017/07/06(zhuguanjun)
    /// </summary>
    public class ReportPersonEfficiencyDao
    {
        public DataTable GetPersonEfficiency(string PID, DateTime Startedate, DateTime Enddate, int FinishStatus)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qlist.Add(new QueryField { Name = "StarteDate", Type = QueryFieldType.DateTime, Value = Startedate });
            qlist.Add(new QueryField { Name = "EndDate", Type = QueryFieldType.DateTime, Value = Enddate });
            qlist.Add(new QueryField { Name = "FinishStatus", Type = QueryFieldType.Numeric, Value = FinishStatus });
            StringBuilder sql = new StringBuilder();

            sql.Append(@"
            select * from (
            /*内容*/
            select * from (
            /*日常*/
            select * from (select 
            (select count(*)+1 from routinework rin where rw.Manager = rin.Manager and rin.created<rw.created) as RowNo,
            '日常' as source, r.name as name,r.Desc,date(r.startdate) as startedate,date(r.enddate) as enddate,rw.workload,rw                                .actualworkload ,'1' as type,s.name as allname,
            cast(round(rw.workload*1.0/rw.actualworkload*100,3) as varchar(20)) || '%' as efficiency,r.finishstatus from  routinework rw
            inner join stakeholders s on substr(s.id,1,36) = rw.Manager and r.status = @status 
            inner join routine r on rw.routineid = substr(r.id,1,36) and s.status =@status  
            where 1=1 and s.pid =@pid  
            order by rw.manager,rw.created)
            union
            /*交付物*/
            select * from(select 
            (select (select count(*) from routinework where manager = dw.manager)+(select count(*) from troublework where manager = dw.manager)
            + count(*)+1 from deliverableswork d where dw.Manager = d.Manager and d.created<dw.created)as rowno,
            '交付物' as source,d.name as name,d.Desc,date(d.startedate) as startdate,date(d.enddate) as enddate,dw.workload,dw.actualworkload ,
            '3' as type ,s.name as allname,
            cast(round(dw.workload*1.0/dw.actualworkload*100,3) as varchar(20)) || '%' as efficiency,
            (case when pg.ptype = 5 then 3 else 2 end) as finishstatus from Deliverableswork dw
            inner join stakeholders s on substr(s.id,1,36) = dw.Manager and s.status =@status 
            inner join DeliverablesJBXX d on dw.JBXXid = substr(d.id,1,36) and d.status = @status  
            inner join PNode  p on substr(p.id,1,36) = d.nodeid and p.status = @status    
            inner join NodeProgress pg on pg.nodeid = substr(p.id,1,36) and pg.status = @status       
            where 1=1 and s.pid =@pid  
            )
            union
            /*问题*/
            select * from(select 
            (select (select count(*) from routinework where manager = tw.manager)+ count(*)+1 from Troublework t where tw.Manager = t.Manager and            t.created<tw.created)as rowno,
            '问题' as source,t.name as name ,t.Desc,date(t.startedate) as startdate,date(t.enddate) as enddate,tw.workload,tw.actualworkload,'2'             as type ,s.name as allname,
            cast(round((tw.workload*1.0/tw.actualworkload),3)*100 as varchar(20)) || '%' as efficiency,t.handlestatus as finishstatus from Troublework tw
            inner join stakeholders s on substr(s.id,1,36) = tw.Manager and s.status =@status 
            inner join Trouble t on tw.troubleid = substr(t.id,1,36) and t.status = @status  
            where 1=1 and s.pid =@pid   
            order by tw.manager,tw.created)

            ) where " +
             (Startedate != DateTime.MinValue ? "date(startedate)>=date(@StarteDate) " : "1=1 ") +
            (Enddate != DateTime.MinValue ? "and date(enddate)<=date(@EndDate) " : "and 1=1 ") +
            (FinishStatus != 0 ? (FinishStatus == 3 ? "and finishstatus=3 " : "and finishstatus!=3 ") : "and 1=1 ") +
            //date(startedate)>=date(@staretdate) 
            //and date(enddate)<=date(@enddate)
            //and finishstatus = @finishstatus
            @"
            
            union
            /*标题*/
            select name as RowNo,'来源' as source,'名称' as name ,'描述' as desc,'开始' as startedate,'结束' as enddate,'工作量' as workload,                '实际工作量' as actualworkload ,0 as type,name as allname,'效率系数' as efficiency,null as finishstatus from stakeholders s
            where s.pid=@pid and s.status =@status
            union

            /*签字行*/
            select null as RowNo,null as source,null as name ,null as desc,null as startedate,null as enddate,null as workload,'平均系数:' as                       actualworkload ,-3 as type,name || '1' as allname,
            (select cast(round((ifnull((select sum(workload) from troublework where substr(s.id,1,36)=manager ),0)+
            ifnull((select sum(workload) from routinework where substr(s.id,1,36)=manager ),0)+
            ifnull((select sum(workload) from deliverableswork where substr(s.id,1,36)=manager ),0)
            )*1.0/(ifnull((select sum(actualworkload) from troublework where substr(s.id,1,36)=manager ),0)+
            ifnull((select sum(actualworkload) from routinework where substr(s.id,1,36)=manager ),0)+
            ifnull((select sum(actualworkload) from deliverableswork where substr(s.id,1,36)=manager ),0))*100.0,1) as varchar(20)) ||'%' ) 
            as efficiency,
            null as finishstatus from stakeholders s
            
            where s.pid=@pid and s.status =@status
            ) order by allname,type,rowno

            ");
            DataTable dt = NHHelper.ExecuteDataTableNoRow(sql.ToString(), qlist);
            return dt;
        }

    }
}
