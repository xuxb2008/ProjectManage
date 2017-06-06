using DomainDLL;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDLL;

namespace DataAccessDLL
{
    public class Repository<T> : IRepository<T> where T : PersistenceEntity 
    {
        private ISession _session;
        protected virtual ISession Session
        {

            get
            {
                if (_session == null)
                {
                    return NHHelper.GetCurrentSession();
                }
                else
                {
                    return _session;
                }
            }
        }

        public Repository()
        { }

        public Repository(ISession session)
        {
            _session = session;
        }


        /// <summary>
        /// 项目更新时间
        /// Created:20170526(ChengMengjia)
        /// </summary>
        private void UpdateProject(ISession s)
        {
            string pid = CacheHelper.GetProjectID();
            if (string.IsNullOrEmpty(pid))
                return;
            Project project = new Repository<Project>().Get(pid);
            if (project == null)
                return;
            project.ProjectLastUpdate = DateTime.Now;
            s.Update(project);
        }

        #region IRepository<T> 成员


        /// <summary>
        /// 延迟加载，返回的是一个代理对象，没有立即命中数据库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Load(string id)
        {
            try
            {
                return Session.Load<T>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("获取实体失败", ex);
            }
        }
        /// <summary>
        /// 立即从数据库中加载该对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(string id)
        {
            try
            {
                return Session.Get<T>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("获取实体失败", ex);
            }
        }

        /// <summary>
        /// 根据条件获取单条记录
        /// </summary>
        /// <param name="queryList"></param>
        /// <returns></returns>
        public virtual T FindSingle(List<QueryField> queryList)
        {
            ICriteria crit = Session.CreateCriteria<T>();
            if (queryList != null)
            {
                foreach (var query in queryList)
                {
                    crit.Add(GetExpression(query));
                }
            }
            return crit.UniqueResult<T>();
        }

        /// <summary>
        /// 获取所有数据（单表）
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetAll()
        {
            IList<T> ret = new List<T>();
            try
            {
                ret = Session.CreateCriteria<T>()
                .AddOrder(Order.Asc("CREATED"))
                .List<T>();
            }
            catch (Exception ex)
            {

            }

            return ret;

        }

        public virtual void Insert(T entity)
        {
            ISession s = Session;
            try
            {
                s.BeginTransaction();
                if (entity.Status == null)
                    entity.Status = 1;
                entity.ID = Guid.NewGuid().ToString();
                entity.CREATED = DateTime.Now;
                s.Save(entity);
                UpdateProject(s);//更新项目
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("插入实体失败", ex);
            }
        }


        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="hisFlg"></param>
        public virtual void Insert(T entity, bool hisFlg)
        {
            ISession s = Session;
            try
            {
                s.BeginTransaction();
                entity.ID = hisFlg ? Guid.NewGuid().ToString() + "-1" : Guid.NewGuid().ToString();
                if (entity.Status == null)
                    entity.Status = 1;
                entity.CREATED = DateTime.Now;
                s.Save(entity);
                UpdateProject(s);//更新项目
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("插入实体失败", ex);
            }
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="hisFlg"></param>
        /// <param name="id"></param>
        public virtual void Insert(T entity, bool hisFlg, out string id)
        {
            id = string.Empty;
            ISession s = Session;
            try
            {
                s.BeginTransaction();
                entity.ID = hisFlg ? Guid.NewGuid().ToString() + "-1" : Guid.NewGuid().ToString();
                if (entity.Status == null)
                    entity.Status = 1;
                id = entity.ID;
                entity.CREATED = DateTime.Now;
                s.Save(entity);
                UpdateProject(s);//更新项目
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("插入实体失败", ex);
            }

        }

        public virtual void Update(T entity)
        {
            ISession s = Session;
            try
            {
                s.BeginTransaction();
                entity.UPDATED = DateTime.Now;
                s.Update(entity);
                UpdateProject(s);//更新项目
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("更新实体失败", ex);
            }
        }


        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="hisFlg"></param>
        public virtual void Update(T entity, bool hisFlg)
        {
            ISession s = Session;
            try
            {
                s.BeginTransaction();
                if (hisFlg)
                {
                    T old = new Repository<T>().Get(entity.ID);
                    old.UPDATED = DateTime.Now;
                    old.Status = 0;
                    s.Update(old);
                    string hisNo = entity.ID.Substring(37);
                    entity.ID = entity.ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                    entity.Status = 1;
                    entity.CREATED = old.CREATED;
                    entity.UPDATED = DateTime.Now;
                    s.Save(entity);
                }
                else
                {
                    entity.UPDATED = DateTime.Now;
                    s.Update(entity);
                }
                UpdateProject(s);//更新项目
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("更新实体失败", ex);
            }


        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="hisFlg"></param>
        /// <param name="id"></param>
        public virtual void Update(T entity, bool hisFlg, out string id)
        {
            ISession s = Session;
            try
            {
                s.BeginTransaction();
                if (hisFlg)
                {
                    T old = new Repository<T>().Get(entity.ID);
                    old.UPDATED = DateTime.Now;
                    old.Status = 0;
                    s.Update(old);
                    string hisNo = entity.ID.Substring(37);
                    entity.ID = entity.ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                    id = entity.ID;
                    entity.Status = 1;
                    entity.CREATED = old.CREATED;
                    s.Save(entity);
                }
                else
                {
                    id = entity.ID;
                    entity.UPDATED = DateTime.Now;
                    s.Update(entity);
                }
                UpdateProject(s);//更新项目
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("更新实体失败", ex);
            }

        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(string id)
        {
            try
            {
                ISession s = Session;
                var entity = Get(id);
                s.Delete(entity);
                s.Flush();
            }
            catch (System.Exception ex)
            {
                throw new Exception("物理删除实体失败", ex);
            }
        }

        /// <summary>
        /// 获取翻页一览
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="queryList"></param>
        /// <param name="sort"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public virtual IList<T> GetList(int pageSize, int pageIndex, List<QueryField> queryList, SortField sort, out int recordCount)
        {
            int startIndex = pageSize * (pageIndex - 1);
            ICriteria crit = Session.CreateCriteria<T>();

            if (queryList != null)
            {
                foreach (var query in queryList)
                {
                    crit.Add(GetExpression(query));
                }
            }



            // Copy current ICriteria instance to the new one for getting the pagination records.
            ICriteria pageCrit = CriteriaTransformer.Clone(crit);

            // Get the total record count
            recordCount = Convert.ToInt32(pageCrit.SetProjection(Projections.RowCount()).UniqueResult());

            if (sort != null)
            {
                crit.AddOrder(GetOrder(sort));
            }

            return crit.SetFirstResult(startIndex)
                .SetMaxResults(pageSize)
                .List<T>();

        }

        /// <summary>
        /// 获取一览
        /// </summary>
        /// <param name="queryList"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IList<T> GetList(List<QueryField> queryList, SortField sort)
        {
            ICriteria crit = Session.CreateCriteria<T>();
            if (queryList != null)
            {
                foreach (var query in queryList)
                {
                    crit.Add(GetExpression(query));
                }
            }
            // Copy current ICriteria instance to the new one for getting the pagination records.
            ICriteria pageCrit = CriteriaTransformer.Clone(crit);
            if (sort != null)
            {
                crit.AddOrder(GetOrder(sort));
            }
            return crit.List<T>();
        }

        /// <summary>
        /// 获取排序
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public Order GetOrder(SortField sort)
        {
            if (sort.Direction == SortDirection.Asc)
            {
                return Order.Asc(sort.Name).IgnoreCase();
            }
            else
            {
                return Order.Desc(sort.Name).IgnoreCase();
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ICriterion GetExpression(QueryField query)
        {
            if (query.Type == QueryFieldType.String)
            {
                if (query.Comparison == QueryFieldComparison.like)
                    return Expression.Like(query.Name, query.Value.ToString(), MatchMode.Anywhere);
                else
                    return Expression.Eq(query.Name, query.Value.ToString());
            }
            if (query.Type == QueryFieldType.Boolean)
            {
                return Expression.Eq(query.Name, query.Value);
            }

            if (query.Type == QueryFieldType.Numeric || query.Type == QueryFieldType.DateTime)
            {

                switch (query.Comparison)
                {
                    case QueryFieldComparison.eq:
                        return Expression.Eq(query.Name, query.Value);
                    case QueryFieldComparison.ge:
                        return Expression.Ge(query.Name, query.Value);
                    case QueryFieldComparison.gt:
                        return Expression.Gt(query.Name, query.Value);
                    case QueryFieldComparison.le:
                        return Expression.Le(query.Name, query.Value);
                    case QueryFieldComparison.lt:
                        return Expression.Lt(query.Name, query.Value);
                    default:
                        return null;
                }
            }
            return null;
        }

        #endregion




    }
}
