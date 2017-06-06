using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 2017/4/10(zhuguanjun)
    /// 供应商Dao
    /// </summary>
    public class SupplierDao
    {
        public DataTable GetSupplierList(List<QueryField> qlist)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from Supplier");
            sql.Append(" where Status=@Status and PID=@PID");
            sql.Append(" order by CREATED");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            if (dt != null)
            {
                return dt;
            }
            return new DataTable();
        }
    }
}
