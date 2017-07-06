using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 成员贡献率
    /// </summary>
    public class ReportMemberRateDao
    {
        /// <summary>
        /// 获取贡献率
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetMemberRate(string PID,DateTime Startedate,DateTime Enddate,int FinishStatus)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qlist.Add(new QueryField { Name = "StarteDate", Type = QueryFieldType.DateTime, Value = Startedate });
            qlist.Add(new QueryField { Name = "EndDate", Type = QueryFieldType.DateTime, Value = Enddate });
            qlist.Add(new QueryField { Name = "FinishStatus", Type = QueryFieldType.Numeric, Value = FinishStatus });

            StringBuilder sql = new StringBuilder();
            sql.Append(@"with cte as
            (
            /*内容*/
            select * from (
            /*日常*/
            select * from (select 
           (select count(*)+1 from routinework rin where rw.Manager = rin.Manager and rin.created<rw.created) as RowNo,
           '日常' as source, r.name as name,r.Desc,date(r.startdate) as startedate,date(r.enddate),r.workload,rw.workload as zhanbi,'1' as type,s.name as allname from routinework rw
            left join stakeholders s on substr(s.id,1,36) = rw.Manager
            left join routine r on rw.routineid = substr(r.id,1,36) 
            where r.status = @status and s.pid =@pid and s.status =@status "+
            //日常 时间、完成情况
            (Startedate != DateTime.MinValue?"and date(r.startdate)>=date(@StarteDate) ":"and 1=1 ")+ 
            (Enddate != DateTime.MinValue?"and date(r.enddate )<=date(@EndDate)":"and 1=1 ")+
            (FinishStatus != 0 ? (FinishStatus == 3 ? "and r.finishstatus=3 " : "and r.finishstatus!=3") : "and 1=1 ") +
            @"
            order by rw.manager,rw.created)
            union
            /*交付物*/
            select * from(
            select (select (select count(*) from routinework where manager = dw.manager)+
            (select count(*) from troublework where manager = dw.manager)
            + count(*)+1 from deliverableswork d             
            where dw.Manager = d.Manager and d.created<dw.created)as rowno,
            '交付物' as source,d.name as name,d.Desc,date(d.startedate),date(d.enddate),d.workload,dw.workload as zhanbi,'3' as type ,s.name as allname from Deliverableswork dw
            left join stakeholders s on substr(s.id,1,36) = dw.Manager
            left join DeliverablesJBXX d on dw.JBXXid = substr(d.id,1,36) 
            left join PNode  p on substr(p.id,1,36) = d.nodeid   
            left join NodeProgress pg on pg.nodeid = substr(p.id,1,36)      
            where d.status = @status and s.pid =@pid and s.status =@status and pg.status = @status and p.status = 1 " +
            //交付物 时间、完成情况
            (Startedate != DateTime.MinValue ? "and date(d.startedate)>=date(@StarteDate) " : "and 1=1 ") +
            (Enddate != DateTime.MinValue ? "and date(d.enddate)<=date(@EndDate) " : "and 1=1 ") +
            (FinishStatus != 0 ? ( FinishStatus ==5?"and pg.ptype=5 ": "and pg.ptype!=5 ") : "and 1=1 ") +
            @"order by dw.manager,dw.created)
            union
            /*问题*/
            select * from(
            select (select (select count(*) from routinework where manager = tw.manager)+ count(*)+1 from Troublework t where tw.Manager = t.Manager and t.created<tw.created)as rowno,
            '问题' as source,t.name as name ,t.Desc,date(t.startedate),date(t.enddate),t.workload,tw.workload as zhanbi,'2' as type ,s.name as allname from Troublework tw
            left join stakeholders s on substr(s.id,1,36) = tw.Manager
            left join Trouble t on tw.troubleid = substr(t.id,1,36) 
            where t.status = 1 and s.pid =@pid  and s.status =@status " +
            //问题 时间、完成情况
            (Startedate != DateTime.MinValue ? "and date(t.startedate)>=date(@StarteDate) " : "and 1=1 ") +
            (Enddate != DateTime.MinValue ? "and date(t.enddate)<=date(@EndDate) " : "and 1=1 ") +
            (FinishStatus != 0 ? (FinishStatus == 5 ? "and t.handlestatus=3 " : "and t.handlestatus!=3 ") : "and 1=1 ") +
            @"order by tw.manager,tw.created)

            )
            )
            /*总工作量*/
            select '总工作量' as RowNo,
            (
            select sum(workload) from (
             select sum(rw.workload) as workload from routinework rw 
             left join stakeholders s on substr(s.id,1,36) = rw.Manager
             left join  routine r on substr(r.id,1,36) = rw.routineid  
             where s.PID =@pid and s.status=1 "+
             //日常
            (Startedate != DateTime.MinValue ? "and date(r.startdate)>=date(@StarteDate) " : "and 1=1 ") +
            (Enddate != DateTime.MinValue ? "and date(r.enddate )<=date(@EndDate)" : "and 1=1 ") +
            (FinishStatus != 0 ? (FinishStatus == 5 ? "and r.finishstatus=5 " : "and r.finishstatus!=5 ") : "and 1=1 ") +
              @"union
            select sum(tw.workload) as workload from troublework tw 
            left join stakeholders s on substr(s.id,1,36) = tw.Manager 
            left join trouble t on substr(t.id,1,36) = tw.troubleid 
            where s.PID =@pid and s.status=1 "+
            //问题 时间、完成情况
            (Startedate != DateTime.MinValue ? "and date(t.startedate)>=date(@StarteDate) " : "and 1=1 ") +
            (Enddate != DateTime.MinValue ? "and date(t.enddate)<=date(@EndDate) " : "and 1=1 ") +
            (FinishStatus != 0 ? (FinishStatus == 5 ? "and t.handleresult=5 " : "and t.handleresult!=5 ") : "and 1=1 ") +
              @"union
            select sum(dw.workload) as workload from deliverableswork dw  
            left join stakeholders s on substr(s.id,1,36) = dw.Manager  
            left join DeliverablesJBXX d on dw.JBXXid = substr(d.id,1,36)  
            left join PNode  p on substr(p.id,1,36) = d.nodeid  
            left join NodeProgress pg on pg.nodeid = substr(p.id,1,36)    
            where s.PID =@pid and s.status=1 and pg.status = 1 and p.status = 1 " +
            //交付物 时间、完成情况
            (Startedate != DateTime.MinValue ? "and date(d.startedate)>=date(@StarteDate) " : "and 1=1 ") +
            (Enddate != DateTime.MinValue ? "and date(d.enddate)<=date(@EndDate) " : "and 1=1 ") +
            (FinishStatus != 0 ? (FinishStatus == 5 ? "and pg.ptype=5 " : "and pg.ptype!=5 ") : "and 1=1 ") +
              @"))
            as Source,
            '天' as name,null as desc,null as startedate,null as enddate,null as workload,null as zhanbi,-2 as type,null as allname
            union
            /*人员*/
            select name as RowNo,'总工作量' as Source ,ifnull((select sum(zhanbi) from cte c where c.allname =s.name group by c.allname ),0) as name,'天' as desc,
            '占比' as startedate,
            cast(round(
            (select sum(zhanbi) from cte c where c.allname =s.name group by c.allname )*1.0/
            (
            select sum(workload) from (
              select sum(workload) as workload from routinework rw 
              left join stakeholders s on substr(s.id,1,36) = rw.Manager 
              where s.PID =@pid and s.status=1
              union
              select sum(workload) as workload from troublework tw 
              left join stakeholders s on substr(s.id,1,36) = tw.Manager 
              where s.PID =@pid and s.status=1
              union
              select sum(workload) as workload from deliverableswork dw 
              left join stakeholders s on substr(s.id,1,36) = dw.Manager 
              where s.PID =@pid and s.status=1
              )
             ),3
            )*100 as varchar(20))  ||'%' 
            as enddate,null as workload,null as zhanbi ,-1 as type,name as allname from stakeholders s
            where s.pid=@pid and s.status =@status
            union

            /*标题*/
            select '编号' as RowNo,'来源' as source,'名称' as name ,'描述' as desc,'开始' as startrdate,'结束' as enddate,'工作量' as workload,'占比' as zhanbi ,0 as type,name as allname from stakeholders s
            where s.pid=@pid and s.status =@status
            union

            /*签字行*/
            select null as RowNo,null as source,null as name ,null as desc,null as startrdate,null as enddate,null as workload,null as zhanbi ,-3 as type,name as allname from stakeholders s
            where s.pid=@pid and s.status =@status
            union

            select * from cte

            /*排序*/
            order by allname,type");
            
            DataTable dt = NHHelper.ExecuteDataTableNoRow(sql.ToString(), qlist);
            return dt;
        }
    }
}
