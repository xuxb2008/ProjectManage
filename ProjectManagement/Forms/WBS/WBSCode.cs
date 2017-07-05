using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CommonDLL;
using BussinessDLL;
using ProjectManagement.Common;
using DevComponents.Editors;
using DomainDLL;
using DevComponents.DotNetBar.SuperGrid;

namespace ProjectManagement.Forms.WBS
{

    /// <summary>
    /// WBS代码
    /// 2017/05/04(zhuguanjun)
    /// </summary>
    public partial class WBSCode : BaseForm
    {
        #region 业务逻辑初始化
        WBSCodeBLL bll = new WBSCodeBLL();
        #endregion

        #region 画面变量
        private string orderr;//序列
        private string length;//长度
        private string breakk;//分割符
        #endregion
        public WBSCode()
        {
            InitializeComponent();
            //加载序列
            foreach (WBSCodeOrder order in Enum.GetValues(typeof(WBSCodeOrder)))
            {
                ComboItem item = new ComboItem();
                item.Text = EnumsHelper.GetDescription(order);
                item.Value = (int)order;
                cmbOrder.Items.Add(item);
            }
            //DataHelper.LoadDictItems(cmbOrder, DictCategory.WBSCodeOrder);
            DataHelper.LoadDictItems(cmbLength, DictCategory.WBSCodeLength);
            DataHelper.LoadDictItems(cmbBreak, DictCategory.WBSCodeBreak);
            DataBind();
        }

        private void DataBind()
        {
            var dt = bll.GetWBSCodeList(ProjectId);
            superGridControl1.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 添加
        /// 2017/05/05(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (cmbOrder.SelectedIndex == -1)
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "序列");
                return;
            }
            if (cmbLength.SelectedIndex == -1)
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "长度");
                return;
            }
            if (cmbBreak.SelectedIndex == -1)
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "分隔符");
                return;
            }
            #endregion

            DomainDLL.WBSCode code = new DomainDLL.WBSCode();
            ComboItem item = (ComboItem)cmbOrder.SelectedItem;
            if (item != null)
                code.Orderr = int.Parse(item.Value.ToString());
            ComboItem item1 = (ComboItem)cmbLength.SelectedItem;
            if (item1 != null)
                code.Length = int.Parse(item1.Value.ToString());
            ComboItem item2 = (ComboItem)cmbBreak.SelectedItem;
            if (item2 != null)
                code.Breakk = int.Parse(item2.Value.ToString());
            code.PID = ProjectId;
            JsonResult json = bll.SaveWBSCode(code);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                btnClear_Click(null, null);
            DataBind();
        }

        /// <summary>
        /// 清空
        /// 2017/05/05(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbLength.SelectedIndex = -1;
            cmbBreak.SelectedIndex = -1;
            cmbOrder.SelectedIndex = -1;
        }

        /// <summary>
        /// 删除按钮
        /// 2017/05/05(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Delete")
            {
                string dm = e.GridCell.GridRow.Cells["ID"].Value.ToString();
                bool IsSuccess = bll.Delete(dm);
                if (IsSuccess)
                {
                    MessageBox.Show("删除成功");
                    DataBind();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }

            }
        }

        /// <summary>
        /// 应用按钮
        /// 2017/05/05(zhugaunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUse_Click(object sender, EventArgs e)
        {
            var list = bll.SetWBSNo(ProjectId);
            if (list != null)
            {
                var result = bll.SavePNodeList(list);
                MessageBox.Show(result.msg);
            }
            else
            {
                MessageBox.Show("请先添加WBS代码！");
            }
        }

        /// <summary>
        /// 行点击
        /// 2017/05/08(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            
        }

        #region 方法
        //private byte[] str2ASCII(String xmlStr)
        //{
        //    return Encoding.Default.GetBytes(xmlStr);
        //}

        //private string Ascii2Str(byte[] buf)
        //{
        //    return Encoding.ASCII.GetString(buf);
        //}

        ///// <summary>
        ///// 设定WBSNo
        ///// 2017/05/08(zhuguanjun)
        ///// </summary>
        ///// <param name="ProjectID"></param>
        //private void SetWBSNo(string ProjectID)
        //{
        //    int step = 0;//层级

        //    List<PNode> listNode = new WBSBLL().GetNodes(ProjectID);
        //    IEnumerable<PNode> parentNode = null;
        //    parentNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.No);

        //    DataTable dt = bll.GetWBSCodeList(ProjectId);//WBSCode集合
        //    DomainDLL.WBSCode[] wbscodeArray = JsonHelper.TableToList<DomainDLL.WBSCode>(dt).OrderBy(t=>t.CREATED).ToArray();//dt转array

        //    if (wbscodeArray == null || wbscodeArray.Count() == 0)
        //        return;
        //    foreach (PNode parent in parentNode)
        //    {
        //        DomainDLL.WBSCode wc = wbscodeArray[step];
        //        byte[] array = SetStepNo(wc.LengthName, wc.Orderr, parent.No);
        //        parent.WBSNo = Ascii2Str(array) + wc.BreakName;
        //        SetChildWBSNo(listNode, parent,step, wbscodeArray);
        //    }
        //}

        //private void SetChildWBSNo(List<PNode> listNode, PNode pnode,int step,DomainDLL.WBSCode[] wbscodeArray)
        //{
        //    step++;
        //    string parentID = pnode.ID.Substring(0, 36);
        //    IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentID).OrderBy(t => t.No);
        //    if (children.Count<PNode>() < 1)
        //    {
        //        return;
        //    }
        //    foreach (PNode child in children)
        //    {
        //        DomainDLL.WBSCode wc;
        //        if (step < wbscodeArray.Count())
        //            wc = wbscodeArray[step];
        //        else
        //            wc = wbscodeArray.Last();
        //        byte[] array = SetStepNo(wc.LengthName, wc.Orderr, child.No);
        //        child.WBSNo = pnode.WBSNo + Ascii2Str(array) + wc.BreakName;
        //        SetChildWBSNo(listNode, child, step, wbscodeArray);
        //    }
        //}

        ///// <summary>
        ///// 获取ASCII字节数组
        ///// 2017/05/08(zhuguanjun)
        ///// </summary>
        ///// <param name="length">长度</param>
        ///// <param name="order">序列</param>
        ///// <param name="no">WBS排序编号</param>
        ///// <returns></returns>
        //private byte[] SetStepNo(int length,int order,int? no)
        //{
        //    byte[] asciiByte = new byte[length];

        //    switch (order)
        //    {
        //        case (int)WBSCodeOrder.Upper:
        //            //超出边界
        //            if (Math.Pow(26, length) < no)
        //                return null;
        //            for (int i = length - 1; i >= 0; i--)
        //            {
        //                if (no / Math.Pow(26, i) >= 1)
        //                {
        //                    asciiByte[length - i - 1] = (byte)(no / Math.Pow(26, i) + 65);
        //                    no = (int)(no % Math.Pow(26, i));
        //                }
        //                else
        //                {
        //                    asciiByte[length - i - 1] = 65;
        //                }
        //            }
        //            break;
        //        case (int)WBSCodeOrder.Lower:
        //            if (Math.Pow(26, length) < no)
        //                return null;
        //            for (int i = length - 1; i >= 0; i--)
        //            {
        //                if (no / Math.Pow(26, i) >= 1)
        //                {
        //                    asciiByte[length - i - 1] = (byte)(no / Math.Pow(26, i) + 97);
        //                    no = (int)(no % Math.Pow(26, i));
        //                }
        //                else
        //                {
        //                    asciiByte[length - i - 1] = 97;
        //                }
        //            }
        //            break;
        //        default:
        //            if (Math.Pow(10, length) < no)
        //                return null;
        //            for (int i = length - 1; i >= 0; i--)
        //            {
        //                if (no / Math.Pow(10, i) >= 1)
        //                {
        //                    asciiByte[length - i - 1] = (byte)(no / Math.Pow(10, i) + 48);
        //                    no = (int)(no % Math.Pow(10, i));
        //                }
        //                else
        //                {
        //                    asciiByte[length - i - 1] = 48;
        //                }
        //            }
        //            break;
        //    }
        //    return asciiByte;
        //}
        #endregion

    }
}
