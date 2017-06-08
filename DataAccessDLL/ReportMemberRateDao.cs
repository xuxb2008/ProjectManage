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
        public DataTable GetMemberRate(string PID)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField { Name = "PID", Type = QueryFieldType.String, Value = PID });

            StringBuilder sql = new StringBuilder();
            sql.Append(@"with cte as
            (
            /*内容*/
            select * from (
            /*日常*/
            select null as RowNo,'日常' as source, r.name as name,r.Desc,r.startdate as startedate,r.enddate,r.workload,rw.workload as zhanbi,'1' as type,s.name as allname from routinework rw
            left join stakeholders s on substr(s.id,1,36) = rw.Manager
            left join routine r on rw.routineid = substr(r.id,1,36) 
            where r.status = 1 and s.pid =@pid and s.status =@status
            union
            /*交付物*/
            select null as RowNo,'交付物' as source,d.name as name,d.Desc,d.startedate,d.enddate,d.workload,dw.workload as zhanbi,'2' as type ,s.name as allname from Deliverableswork dw
            left join stakeholders s on substr(s.id,1,36) = dw.Manager
            left join DeliverablesJBXX d on dw.JBXXid = substr(d.id,1,36) 
            where d.status = 1 and s.pid =@pid and s.status =@status
            union
            /*问题*/
            select null as RowNo,'问题' as source,t.name as name ,t.Desc,t.startedate,t.enddate,t.workload,tw.workload as zhanbi,'3' as type ,s.name as allname from Troublework tw
            left join stakeholders s on substr(s.id,1,36) = tw.Manager
            left join Trouble t on tw.troubleid = substr(t.id,1,36) 
            where t.status = 1 and s.pid =@pid  and s.status =@status
            )
            )
            /*总工作量*/
            select '总工作量' as RowNo,(select sum(workload) from (select sum(workload) as workload from                       RoutineWork rw union
            select sum(workload) as workload from DeliverablesWork dw union
            select sum(workload) as workload from TroubleWork tw)) as Source,
            '天' as name,null as desc,null as startedate,null as enddate,null as workload,null as zhanbi,-2 as type,null as allname
            union
            /*人员*/
            select name as RowNo,'总工作量' as Source ,(select sum(zhanbi) from cte c where c.allname =s.name group by c.allname ) as name,'天' as desc,null as startedate,null as enddate,null as workload,null as zhanbi ,-1 as type,name as allname from stakeholders s
            where s.pid=@pid and s.status =@status
            union

            /*标题*/
            select null as RowNo,'来源' as source,'名称' as name ,'描述' as desc,'开始' as startrdate,'结束' as enddate,'工作量' as workload,'占比' as zhanbi ,0 as type,name as allname from stakeholders s
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
