using CommonDLL;
using DataAccessDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 模板分类
    /// 2017/05/18(zhuguanjun)
    /// </summary>
    public class TempletTypeBLL
    {
        /// <summary>
        /// 获取模板分类
        /// </summary>
        /// <returns></returns>
        public List<TempletType> GetTempletTypeList()
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            List<TempletType> li = new Repository<TempletType>().GetList(qlist, null) as List<TempletType>;
            return li;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <returns></returns>
        public List<Templet> GetTempletList()
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            List<Templet> li = new Repository<Templet>().GetList(qlist, null) as List<Templet>;
            return li;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        public List<Templet> GetTempletList(string keywords)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            if (!string.IsNullOrEmpty(keywords))
            {
                qlist.Add(new QueryField() { Name = "Name", Type = QueryFieldType.String, Value = keywords, Comparison = QueryFieldComparison.like });
            }
            List<Templet> li = new Repository<Templet>().GetList(qlist, null) as List<Templet>;
            return li;
        }

        //保存分类
        public JsonResult SaveTempletType(TempletType entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string id;
                jsonreslut.result = false;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<TempletType>().Insert(entity, true, out id);
                else
                    new Repository<TempletType>().Update(entity, true, out id);
                jsonreslut.result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.BussinessDLL);
                jsonreslut.result = false;
                jsonreslut.msg = ex.Message;
            }
            return jsonreslut;
        }

        //保存模板
        public JsonResult SaveTemplet(Templet entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string id;
                jsonreslut.result = false;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Templet>().Insert(entity, true, out id);
                else
                    new Repository<Templet>().Update(entity, true, out id);
                jsonreslut.result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.BussinessDLL);
                jsonreslut.result = false;
                jsonreslut.msg = ex.Message;
            }
            return jsonreslut;
        }
    }
}
