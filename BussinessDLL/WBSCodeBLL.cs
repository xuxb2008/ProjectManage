using CommonDLL;
using DataAccessDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// wbs代码bll
    /// 2017/05/04(zhugaunjun)
    /// </summary>
    public class WBSCodeBLL
    {
        /// <summary>
        /// 保存wbs代码
        /// 2017/05/04(zhuguanjun)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult SaveWBSCode(WBSCode entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<WBSCode>().Insert(entity, false);
                else
                    new Repository<WBSCode>().Update(entity, false);
                jsonreslut.result = true;
                jsonreslut.msg = "保存成功！";
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.BussinessDLL);
                jsonreslut.result = false;
                jsonreslut.msg = ex.Message;
            }
            return jsonreslut;
        }

        /// <summary>
        /// 获取集合
        /// 2017/05/04(zhugaunjun)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetWBSCodeList(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            return new WBSCodeDao().GetWBSCodeList(qf);
        }

        /// <summary>
        /// 物理删除
        /// 2017/05/05(zhuguanjun)
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(string id)
        {
            try
            {
                new Repository<WBSCode>().Delete(id);
                return true;
            }
            catch(Exception ex)
            {
                LogHelper.WriteException(ex, LogType.BussinessDLL);
                return false;
            }
        }

        /// <summary>
        /// 根据ASCII的到字符
        /// 2017/05/08(zhuguanjun)
        /// </summary>
        /// <param name="buf">传入的字节数组</param>
        /// <returns></returns>
        private string Ascii2Str(byte[] buf)
        {
            return Encoding.ASCII.GetString(buf);
        }

        /// <summary>
        /// 获取PNode集合并为其设置WBSCode编码
        /// 2017/05/08(zhuguanjun)
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <returns></returns>
        public List<PNode> SetWBSNo(string ProjectID)
        {
            int step = 0;//层级

            List<PNode> listNode = new WBSBLL().GetNodes(ProjectID,null);
            IEnumerable<PNode> parentNode = null;
            parentNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.No);

            DataTable dt = GetWBSCodeList(ProjectID);//WBSCode集合
            DomainDLL.WBSCode[] wbscodeArray = JsonHelper.TableToList<DomainDLL.WBSCode>(dt).OrderBy(t => t.CREATED).ToArray();//dt转array

            if (wbscodeArray == null || wbscodeArray.Count() == 0)
                return null;
            foreach (PNode parent in parentNode)
            {
                DomainDLL.WBSCode wc = wbscodeArray[step];
                byte[] array = SetStepNo(wc.LengthName, wc.Orderr, parent.No);
                parent.WBSNo = Ascii2Str(array) + wc.BreakName;
                SetChildWBSNo(listNode, parent, step, wbscodeArray);
            }
            return listNode;
        }

        /// <summary>
        /// 递归函数设置子节点的WBSCode编码
        /// 2017/05/08(zhuguanjun)
        /// </summary>
        /// <param name="listNode">节点集合</param>
        /// <param name="pnode">父节点</param>
        /// <param name="step">节点层次</param>
        /// <param name="wbscodeArray">WBSCode数组</param>
        private void SetChildWBSNo(List<PNode> listNode, PNode pnode, int step, DomainDLL.WBSCode[] wbscodeArray)
        {
            step++;
            string parentID = pnode.ID.Substring(0, 36);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentID).OrderBy(t => t.No);
            if (children.Count<PNode>() < 1)
            {
                return;
            }
            foreach (PNode child in children)
            {
                DomainDLL.WBSCode wc;
                if (step < wbscodeArray.Count())
                    wc = wbscodeArray[step];
                else
                    //wc = wbscodeArray.Last();//2017/05/22(zhuguanjun)
                    wc = new WBSCode { LengthName = 4, Orderr = (int)WBSCodeOrder.Number, BreakName = "-" };
                byte[] array = SetStepNo(wc.LengthName, wc.Orderr, child.No);
                child.WBSNo = pnode.WBSNo + Ascii2Str(array) + wc.BreakName;
                SetChildWBSNo(listNode, child, step, wbscodeArray);
            }
        }

        /// <summary>
        /// 获取ASCII字节数组
        /// 2017/05/08(zhuguanjun)
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="order">序列</param>
        /// <param name="no">WBS排序编号</param>
        /// <returns></returns>
        private byte[] SetStepNo(int length, int order, int? no)
        {
            byte[] asciiByte = new byte[length];

            switch (order)
            {
                case (int)WBSCodeOrder.Upper:
                    //超出边界
                    if (Math.Pow(26, length) < no)
                        return null;
                    for (int i = length - 1; i >= 0; i--)
                    {
                        if (no / Math.Pow(26, i) >= 1)
                        {
                            asciiByte[length - i - 1] = (byte)(no / Math.Pow(26, i) + 65);
                            no = (int)(no % Math.Pow(26, i));
                        }
                        else
                        {
                            asciiByte[length - i - 1] = 65;
                        }
                    }
                    break;
                case (int)WBSCodeOrder.Lower:
                    if (Math.Pow(26, length) < no)
                        return null;
                    for (int i = length - 1; i >= 0; i--)
                    {
                        if (no / Math.Pow(26, i) >= 1)
                        {
                            asciiByte[length - i - 1] = (byte)(no / Math.Pow(26, i) + 97);
                            no = (int)(no % Math.Pow(26, i));
                        }
                        else
                        {
                            asciiByte[length - i - 1] = 97;
                        }
                    }
                    break;
                default:
                    if (Math.Pow(10, length) < no)
                        return null;
                    for (int i = length - 1; i >= 0; i--)
                    {
                        if (no / Math.Pow(10, i) >= 1)
                        {
                            asciiByte[length - i - 1] = (byte)(no / Math.Pow(10, i) + 48);
                            no = (int)(no % Math.Pow(10, i));
                        }
                        else
                        {
                            asciiByte[length - i - 1] = 48;
                        }
                    }
                    break;
            }
            return asciiByte;
        }

        /// <summary>
        /// 保存已经赋值的PNodeList
        /// </summary>
        /// <param name="PNodeList"></param>
        /// <returns></returns>
        public JsonResult SavePNodeList(List<PNode> PNodeList)
        {
            JsonResult jsonreslut = new JsonResult();
            ISession s = NHHelper.GetCurrentSession(); ;
            s.BeginTransaction();
            try
            {
                PNodeList = PNodeList.OrderBy(t => t.ParentID).ThenBy(t => t.No).ToList();
                if (PNodeList != null && PNodeList.Count != 0)
                {
                    foreach (var item in PNodeList)
                    {
                        s.Update(item);
                    }
                }
                jsonreslut.result = true;
                jsonreslut.msg = "操作成功！";
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.BussinessDLL);
                s.Transaction.Rollback();
                jsonreslut.result = false;
                jsonreslut.msg = "操作失败！";
            }
            s.Transaction.Commit();
            s.Close();
            return jsonreslut;
        }

    }
}
