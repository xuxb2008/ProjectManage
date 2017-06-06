using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    public class SettingDAO
    {
        /// <summary>
        /// 获取周报收件人列表
        ///  Created:2017.04.11(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public IList<dynamic> GetMemberList(string ProjectID, string IDs)
        {
            string sql = "select * from Stakeholders where PID=:PID and ID in ("+IDs+")";
            ISQLQuery query = NHHelper.GetCurrentSession().CreateSQLQuery(sql.ToString());
            query.SetString("PID", ProjectID);
            return query.DynamicList();
        }
    }
}
