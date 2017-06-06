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
    /// 变更管理Dao
    /// 2017/04/17(zhuguanjun)
    /// </summary>
    public class ChangeDao
    {
        /// <summary>
        /// 获取(无版本号的ID+有版本号的NAME)
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="qf"></param>
        /// <returns></returns>
        public DataTable GetDataTable(List<QueryField> qf)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select c1.ID,c2.Name from Change c1, Change c2");
            sql.Append(" where substr(c1.ID, 38) = '1' and substr(c1.ID, 1, 37) = substr(c2.ID, 1, 37)");
            sql.Append(" and c2.PID=@PID  and c2.status=@Status and c2.Type=@Type order by c2.created");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            return dt;
        }

        /// <summary>
        /// 根据版本ID获得变更实体
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public Change GetChange(List<QueryField> qlist)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from Change c");
            sql.Append(" where Status=@status and substr(c.Id,1,37)||'1'=@CID");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt == null ? new Change() : JsonHelper.TableToEntity<Change>(dt);
        }

        /// <summary>
        /// 根据版本ID获得变更附件列表
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public DataTable GetFilesList(List<QueryField> qlist)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from ChangeFiles c");
            sql.Append(" where c.Status=@status and c.ChangeID=@CID");
            sql.Append(" order by c.CREATED");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt == null ? new DataTable() : dt;
        }

    }
}
