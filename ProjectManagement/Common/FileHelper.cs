
using BussinessDLL;
using CommonDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ProjectManagement
{
    /// <summary>
    /// 上传文件类型
    /// Created：2017.3.28(xuxb)
    /// Updated：2017.3.28(ChengMengjia)
    /// </summary>
    public enum UploadType
    {
        /// <summary>
        /// 项目-合同扫描件
        /// </summary>
        ContractHTSMJ,
        /// <summary>
        /// 项目-合同电子档
        /// </summary>
        ContractHTDZD,
        /// <summary>
        /// 项目-工作说明书扫描件
        /// </summary>
        ContractGZSMJ,
        /// <summary>
        /// 项目-工作说明书电子档
        /// </summary>
        ContractGZDZD,
        /// <summary>
        /// 项目-招标文件电子档
        /// </summary>
        ContractZBDZD,
        /// <summary>
        /// 项目-投标文件电子档
        /// </summary>
        ContractTBDZD,
        /// <summary>
        /// 项目-合同其他附件
        /// </summary>
        ContractQTFJ,

        /// <summary>
        /// 项目文件
        /// </summary>
        Project,
        /// <summary>
        /// WBS文件
        /// </summary>
        WBS,
        /// <summary>
        /// 分包合同文件
        /// </summary>
        SubContract,
        /// <summary>
        /// 收入管理文件
        /// </summary>
        InCome,
        /// <summary>
        /// 变更管理文件
        /// </summary>
        Change,
        /// <summary>
        /// 日常工作文件
        /// </summary>
        Routine,
        /// <summary>
        /// 问题管理文件
        /// </summary>
        Trouble,
        /// <summary>
        /// 问题管理文件(问题原因)
        /// </summary>
        TroubleReason,
        /// <summary>
        /// 问题管理文件(问题分析)
        /// </summary>
        TroubleAnalyse,
        /// <summary>
        /// 问题管理文件(解决方案)
        /// </summary>
        TroubleSolution,
        /// <summary>
        /// 周报模板
        /// </summary>
        WeeklyModel,
        /// <summary>
        /// WBS工作结构模板
        /// </summary>
        WBSModel,
        /// <summary>
        /// 供应商文件
        /// </summary>
        Supplier,
        /// <summary>
        /// 周报报表
        /// </summary>
        Report_Weekly,
        /// <summary>
        /// 项目计划报表
        /// </summary>
        Report_Plan,
        /// <summary>
        /// 收入报表
        /// </summary>
        Report_Earning,
        /// <summary>
        /// 成本报表
        /// </summary>
        Report_Cost,
        /// <summary>
        /// 收款报表
        /// </summary>
        Report_Receivables,
        /// <summary>
        /// 动态列数报表（可以设置增列或降列）
        /// </summary>
        Report_Dynamic,
        /// <summary>
        /// 模板上传
        /// </summary>
        Templet
    }




    /// <summary>
    /// 类名：文件处理类
    /// Created：2017.3.28(xuxb)
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 上传文件
        /// Created：2017.3.28(xuxb)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static string UploadFile(string filePath, UploadType type, string projectId, string nodeId)
        {
            try
            {
                //判断上传文件是否存在
                if (!File.Exists(filePath))
                {
                    LogHelper.WriteLog("上传的文件不存在。", LogType.CommonDLL);
                    MessageHelper.ShowMsg(MessageID.W000000003, MessageType.Alert);
                    return string.Empty;
                }

                //判断工作目录是否存在
                string workDir = GetWorkdir();
                if (string.IsNullOrEmpty(workDir))
                {
                    LogHelper.WriteLog("工作目录不存在。", LogType.CommonDLL);
                    MessageHelper.ShowMsg(MessageID.W000000004, MessageType.Alert);
                    return string.Empty;
                }

                string toPath = workDir + GetUploadPath(type, projectId, nodeId);
                if (!Directory.Exists(toPath))
                {
                    Directory.CreateDirectory(toPath);
                }
                string fileName = Path.GetFileName(filePath);
                fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileName;
                File.Copy(filePath, toPath + "\\" + fileName);

                return fileName;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.ProjectManagement);
                MessageHelper.ShowMsg(MessageID.E000000003, MessageType.Alert);
                return string.Empty;
            }
        }


        ///// <summary>
        ///// 上传模板
        ///// Created：2017.5.18(zhuguanjun)
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <param name="type"></param>
        ///// <param name="projectId"></param>
        ///// <param name="nodeId"></param>
        ///// <returns></returns>
        //public static string UploadFileWithNewName(string filePath, UploadType type, string projectId, string nodeId,string newName)
        //{
        //    try
        //    {
        //        //判断上传文件是否存在
        //        if (!File.Exists(filePath))
        //        {
        //            LogHelper.WriteLog("上传的文件不存在。", LogType.CommonDLL);
        //            MessageHelper.ShowMsg(MessageID.W000000003, MessageType.Alert);
        //            return string.Empty;
        //        }

        //        //判断工作目录是否存在
        //        string workDir = GetWorkdir();
        //        if (string.IsNullOrEmpty(workDir))
        //        {
        //            LogHelper.WriteLog("工作目录不存在。", LogType.CommonDLL);
        //            MessageHelper.ShowMsg(MessageID.W000000004, MessageType.Alert);
        //            return string.Empty;
        //        }

        //        string toPath = workDir +  GetUploadPath(type, projectId, nodeId);
        //        if (!Directory.Exists(toPath))
        //        {
        //            Directory.CreateDirectory(toPath);
        //        }

        //        File.Copy(filePath, toPath + "\\" + newName, true);

        //        return newName;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteException(ex, LogType.ProjectManagement);
        //        MessageHelper.ShowMsg(MessageID.E000000003, MessageType.Alert);
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// 取得工作目录
        /// Created：2017.3.28(xuxb)
        /// </summary>
        /// <returns></returns>
        public static string GetWorkdir()
        {
            //取得工作目录
            string workDir = CommonHelper.GetConfigValue(ConstHelper.Config_WorkDir);
            if (!Directory.Exists(workDir))
            {
                Directory.CreateDirectory(workDir);
            }

            return workDir;
        }

        /// <summary>
        /// 获得上传文件路径
        /// Created：2017.3.28(xuxb)
        /// Updated：2017.3.28(ChengMengjia)
        /// Updated：2017.4.12(zhuguanjun)
        /// Updated：2017.6.9(ChengMengjia) 日常工作的上传路径修改
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static string GetUploadPath(UploadType type, string projectId, string nodeId)
        {
            ProjectBLL projectBll = new ProjectBLL();
            WBSBLL wbsBll = new WBSBLL();
            string baseNodeId = string.Empty;
            switch (type)
            {
                case UploadType.ContractHTSMJ:
                    //上传项目合同扫描件
                    return projectBll.GetProjectPath(projectId) + "合同信息\\合同扫描件\\"; ;
                case UploadType.ContractHTDZD:
                    //上传项目合同电子档
                    return projectBll.GetProjectPath(projectId) + "合同信息\\合同电子档\\"; ;
                case UploadType.ContractGZSMJ:
                    //上传项目工作说明书扫描件
                    return projectBll.GetProjectPath(projectId) + "合同信息\\工作说明书扫描件\\";
                case UploadType.ContractGZDZD:
                    //上传项目工作说明书电子档
                    return projectBll.GetProjectPath(projectId) + "合同信息\\工作说明书电子档\\";
                case UploadType.ContractZBDZD:
                    //上传项目招标文件电子档
                    return projectBll.GetProjectPath(projectId) + "合同信息\\招标文件电子档\\";
                case UploadType.ContractTBDZD:
                    //上传项目投标文件电子档
                    return projectBll.GetProjectPath(projectId) + "合同信息\\投标文件电子档\\";
                case UploadType.ContractQTFJ:
                    //上传项目其他附件
                    return projectBll.GetProjectPath(projectId) + "合同信息\\其他附件\\";
                case UploadType.Project:
                    //上传项目文件时
                    return projectBll.GetProjectPath(projectId);

                case UploadType.WBS:
                    //上传WBS文件时
                    return wbsBll.GetWBSPath(projectId, nodeId);
                case UploadType.WeeklyModel:
                case UploadType.WBSModel:
                    //周报模板
                    return Directory.GetCurrentDirectory() + "\\Template\\";
                case UploadType.Change:
                    //上传变更文件时
                    return projectBll.GetProjectPath(projectId) + "变更管理\\";

                case UploadType.InCome:
                    //上传收款文件时
                    return projectBll.GetProjectPath(projectId) + "营收管理\\收入\\";

                case UploadType.SubContract:
                    //上传分包文件时
                    return projectBll.GetProjectPath(projectId) + "分包管理\\分包\\";
                case UploadType.Supplier:
                    //上传供应商文件时
                    return projectBll.GetProjectPath(projectId) + "分包管理\\供应商\\";

                case UploadType.Routine:
                    PNode rnode = wbsBll.GetNode(nodeId);//日常工作节点
                    //上传日常工作文件时
                    return projectBll.GetProjectPath(projectId) + wbsBll.GetWBSPath(projectId, rnode.ParentID, false)
                        + "日常工作\\" + rnode.Name + "\\";
                case UploadType.Trouble:
                case UploadType.TroubleReason:
                case UploadType.TroubleAnalyse:
                case UploadType.TroubleSolution:
                    PNode tnode = wbsBll.GetNode(nodeId);//问题节点
                    //上传问题管理文件时
                    return projectBll.GetProjectPath(projectId) + wbsBll.GetWBSPath(projectId, tnode.ParentID, false)
                         + "问题管理\\" + tnode.Name + "\\";
                case UploadType.Report_Plan:
                    return "报表\\项目计划\\";
                case UploadType.Report_Earning:
                    return "报表\\收入情况\\";
                case UploadType.Report_Cost:
                    return "报表\\成本分配\\";
                case UploadType.Report_Receivables:
                    return "报表\\收款情况\\";
                case UploadType.Report_Weekly:
                    return projectBll.GetProjectPath(projectId) + "报表\\周报\\";
                case UploadType.Report_Dynamic:
                    return "报表\\动态报表\\";
                case UploadType.Templet:
                    return "模板";
                default:
                    //上传项目其他文件时
                    return projectBll.GetProjectPath(projectId);
            }
        }

        /// <summary>
        /// 获取文件完整路径
        /// Created：2017.3.30(xuxb)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePath(UploadType type, string projectId, string nodeId, string fileName)
        {
            return GetWorkdir() + GetUploadPath(type, projectId, nodeId) + fileName;
        }

        /// <summary>
        /// 打开文件
        /// Created：2017.3.30(xuxb)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <param name="fileName"></param>
        public static void OpenFile(UploadType type, string projectId, string nodeId, string fileName)
        {
            string path = GetFilePath(type, projectId, nodeId, fileName);
            if (!File.Exists(path))
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //定义一个ProcessStartInfo实例
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            //设定打开文件的目录
            info.WorkingDirectory = GetWorkdir() + GetUploadPath(type, projectId, nodeId);
            //设定打开文件名
            info.FileName = @fileName;
            //设定打开参数
            info.Arguments = "";

            try
            {
                //打开文件
                System.Diagnostics.Process.Start(info);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                LogHelper.WriteException(ex, LogType.ProjectManagement);
                MessageHelper.ShowMsg(MessageID.E000000002, MessageType.Alert);
            }
        }

        /// <summary>
        /// 打开文件
        /// Created：2017.4.11(ChengMengjia)
        /// </summary>
        /// <param name="Path"></param>
        public static void OpenFile(string Path, string Name)
        {
            if (!File.Exists(Path + Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //定义一个ProcessStartInfo实例
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            //设定打开文件的目录
            info.WorkingDirectory = Path;
            //设定打开文件名
            info.FileName = Name;
            //设定打开参数
            info.Arguments = "";

            try
            {
                //打开文件
                System.Diagnostics.Process.Start(info);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                LogHelper.WriteException(ex, LogType.ProjectManagement);
                MessageHelper.ShowMsg(MessageID.E000000002, MessageType.Alert);
            }
        }

        /// <summary>
        /// 下载文件
        /// Created：2017.3.30(xuxb)
        /// Updated：2017.3.31(ChengMengjia) 增加文件是否存在的判断
        /// Updated：2017.5.25(ChengMengjia) 由OpenFileDialog改为SaveFileDialog
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <param name="fileName"></param>
        public static void DownLoadFile(UploadType type, string projectId, string nodeId, string fileName)
        {
            string filePath = string.Empty;
            filePath = FileHelper.GetFilePath(type, projectId, nodeId, fileName);
            if (!File.Exists(filePath))
            {
                //文件已不存在
                MessageHelper.ShowMsg(MessageID.W000000003, MessageType.Alert);
                return;
            }
            try
            {
                //打开文件夹下载
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    string oldExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString(); //扩展名
                    dialog.CheckPathExists = true;//是否检查文件是否存在
                    dialog.Title = "文件保存";
                    dialog.Filter = "所有文件(*.*)|*.*";
                    dialog.FileName = fileName;
                    dialog.DefaultExt = oldExtName;
                    if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName.Length > 0)
                    {
                        string newfileName = dialog.FileName.ToString();
                        //用户自行更改扩展名
                        if (newfileName.Substring(newfileName.LastIndexOf(".") + 1).ToString() != oldExtName)
                        {
                            DialogResult result = MessageBox.Show("扩展名不可更改，将恢复默认！", "警告");
                            if (result == DialogResult.OK)
                            {
                                File.Copy(filePath, newfileName.Split('.')[0] + "." + oldExtName, true);
                                MessageHelper.ShowMsg(MessageID.I000000007, MessageType.Alert);
                            }
                        }
                        else
                        {
                            File.Copy(filePath, newfileName, true);
                            MessageHelper.ShowMsg(MessageID.I000000007, MessageType.Alert);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.ProjectManagement);
                MessageHelper.ShowMsg(MessageID.E000000002, MessageType.Alert);
            }
        }

        /// <summary>
        /// WBS节点文件路径下所有文件的移动
        /// Created：20170330(ChengMengjia)
        /// Updated：20170607(ChengMengjia)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="CurrentNodeID">最新NodeID</param>
        public static void WBSMoveFloder(UploadType type, string CurrentNodeID)
        {
            try
            {
                WBSBLL wbsBll = new WBSBLL();
                if (CurrentNodeID.Length <= 36 || CurrentNodeID.Substring(37).Equals("1"))
                    return;//没有最新版本号
                string oldNodeID = CurrentNodeID.Substring(0, 36) + "-" + (int.Parse(CurrentNodeID.Substring(37)) - 1).ToString();
                string oldPath = GetWorkdir() + wbsBll.GetWBSPath(oldNodeID);
                string newPath = GetWorkdir() + wbsBll.GetWBSPath(CurrentNodeID);
                if (oldPath.Equals(newPath) || !Directory.Exists(oldPath))
                    return;
                if (Directory.Exists(newPath))
                    //需要先将目标路径删除否则无法移动
                    Directory.Delete(newPath, true);
                string[] newP = newPath.Split('\\');
                string s = newPath.Substring(0, newPath.Length - 2 - newP[newP.Count() - 2].Length);
                if (!Directory.Exists(s))
                    Directory.CreateDirectory(s);
                Directory.Move(oldPath, newPath);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.CommonDLL);
            }
        }
        /// <summary>
        /// 文件路径下所有文件的移动
        /// Created：20170607(ChengMengjia)
        /// </summary>

        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void MoveFloder(string oldPath, string newPath)
        {
            if (oldPath.Equals(newPath) || !Directory.Exists(oldPath))//路径相同或原路径不存在
                return;
            if (Directory.Exists(newPath))
                //需要先将目标路径删除否则无法移动
                Directory.Delete(newPath, true);
            string[] newP = newPath.Split('\\');
            string s = newPath.Substring(0, newPath.Length - 2 - newP[newP.Count() - 2].Length);
            if (!Directory.Exists(s))
                Directory.CreateDirectory(s);
            Directory.Move(oldPath, newPath);
        }

        /// <summary>
        /// 文件路径下所有文件的复制
        /// Created：2017.3.30(ChengMengjia)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="oldNodeID"></param>
        /// <param name="newNodeID"></param>
        public static void CopyFloder(UploadType type, string projectId, string oldNodeID, string newNodeID)
        {
            string oldPath = GetWorkdir() + GetUploadPath(type, projectId, oldNodeID);
            string newPath = GetWorkdir() + GetUploadPath(type, projectId, newNodeID);
            if (!Directory.Exists(oldPath))
                return;
            CopyFolder(oldPath, newPath);
        }

        /// <summary>
        /// 循环移动
        /// Created：2017.3.30(ChengMengjia)
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        private static void CopyFolder(string oldPath, string newPath)
        {
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);
            foreach (var item in Directory.EnumerateFiles(oldPath))
                File.Copy(item, Path.Combine(newPath, Path.GetFileName(item)), true);
            foreach (var item in Directory.EnumerateDirectories(oldPath))
                CopyFolder(item, Path.Combine(newPath, Path.GetFileName(item)));
        }

        /// <summary>
        /// copy文件到指定目录
        /// Created：2017.4.6(xuxb)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CopyFile(string filePath, UploadType type, string projectId, string nodeId, string fileName)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    //文件已不存在
                    MessageHelper.ShowMsg(MessageID.W000000003, MessageType.Alert);
                    return false;
                }
                else
                {
                    string path = GetWorkdir() + GetUploadPath(type, projectId, nodeId);
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    File.Copy(filePath, path + fileName, true);

                    File.Delete(filePath);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.ProjectManagement);
                MessageHelper.ShowMsg(MessageID.E000000002, MessageType.Alert);
                return false;
            }

        }
    }
}
