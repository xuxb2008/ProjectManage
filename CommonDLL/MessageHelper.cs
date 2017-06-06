using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonDLL
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        Alert,
        Confirm
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageID
    {
        /// <summary>
        /// 错误：保存失败
        /// </summary>
        E000000001,
        /// <summary>
        /// 错误：系统失误
        /// </summary>
        E000000002,
        /// <summary>
        /// 上传失败
        /// </summary>
        E000000003,
        /// <summary>
        /// 关闭前询问是否保存
        /// </summary>
        I000000001,
        /// <summary>
        /// 保存成功
        /// </summary>
        I000000002,
        /// <summary>
        /// 项目切换询问
        /// </summary>
        I000000003,
        /// <summary>
        /// 节点切换询问
        /// </summary>
        I000000004,
        /// <summary>
        /// 节点树刷新询问
        /// </summary>
        I000000005,
        /// <summary>
        /// 文件已存在询问
        /// </summary>
        I000000006,
        /// <summary>
        /// 文件下载成功
        /// </summary>
        I000000007,
        /// <summary>
        /// 进度更改询问
        /// </summary>
        I000000008,
        /// <summary>
        /// 数据切换询问
        /// </summary>
        I000000009,
        /// <summary>        
        /// 警告为空
        /// </summary>
        W000000001,
        /// <summary>
        /// 警告未选择
        /// </summary>
        W000000002,
        /// <summary>
        /// 上传的文件不存在
        /// </summary>
        W000000003,
        /// <summary>
        /// 工作目录不存在
        /// </summary>
        W000000004,
        /// <summary>
        /// 提示先上传
        /// </summary>
        W000000005,
        /// <summary>
        /// 主表数据不存在提示
        /// </summary>
        W000000006,
        /// <summary>
        /// 有子节点的节点不能转为交付物
        /// </summary>
        W000000007
    }


    /// <summary>
    /// 类名：消息处理类
    /// 创建：2017/03/23(xuxb)
    /// </summary>
    public class MessageHelper
    {
        #region 修改人:ChengMengjia 时间:2017.3.24 内容:增加返回值DialogResult
        public static DialogResult ShowMsg(MessageID messageId, MessageType msgType, params string[] args)
        {
            XmlHelper.XmlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Message.xml";
            string msg = XmlHelper.GetProValueByValue("Messagers", "message", "code", messageId.ToString());

            msg = String.Format(msg, args);

            if (msgType == MessageType.Alert)
            {
                return MessageBox.Show(msg, "提示消息");
            }
            else
            {
                return MessageBox.Show(msg, "确认消息", MessageBoxButtons.OKCancel);
            }

        }
        #endregion

        /// <summary>
        /// 保存后的结果消息
        /// </summary>
        /// <param name="result"></param>
        public static void ShowRstMsg(bool result)
        {
            if (result)
            {
                ShowMsg(MessageID.I000000002, MessageType.Alert);
            }
            else
            {
                ShowMsg(MessageID.E000000001, MessageType.Alert);
            }

        }

    }
}
