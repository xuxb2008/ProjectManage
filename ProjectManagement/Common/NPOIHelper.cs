using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectManagement.Common
{
    public class NPOIHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数，将一个已有Excel工作簿作为模板，并指定输出路径
        /// </summary>
        /// <param name="templetFilePath">Excel模板文件路径</param>
        /// <param name="outputFilePath">输出Excel文件路径</param>
        public NPOIHelper(string templetFilePath, string outputFilePath)
        {
            if (string.IsNullOrEmpty(templetFilePath))
                throw new Exception("Excel模板文件路径不能为空！");

            if (string.IsNullOrEmpty(outputFilePath))
                throw new Exception("输出Excel文件路径不能为空！");

            if (!File.Exists(templetFilePath))
                throw new Exception("指定路径的Excel模板文件不存在！");

            this.templetFile = templetFilePath;
            this.outputFile = outputFilePath;


            using (FileStream fs = File.OpenRead(templetFile))
            {
                // 2007版本  
                if (templetFile.IndexOf(".xlsx") > 0)
                    workBook = new XSSFWorkbook(fs);
                // 2003版本  
                else if (templetFile.IndexOf(".xls") > 0)
                    workBook = new HSSFWorkbook(fs);

                if (workBook != null)
                    workSheet = workBook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
            }


        }

        #endregion

        #region 私有变量

        private string templetFile = null;
        private string outputFile = null;
        private object missing = System.Reflection.Missing.Value;
        private IWorkbook workBook;
        private ISheet workSheet;
        private dynamic range;
        private dynamic range1;
        private dynamic range2;

        #endregion

        #region 读取模板行列

        public string ReadCell(int rowIndex, int columnIndex)
        {
            return workSheet.GetRow(rowIndex - 1).GetCell(columnIndex).StringCellValue;
        }

        #endregion

        #region 行操作

        /// <summary>
        /// 插行（在指定行上面插入指定数量行）
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        public void InsertRows(int rowIndex, int count)
        {
            try
            {
                IRow row = workSheet.GetRow(rowIndex - 1);//获取源格式行//源格式行

                #region 批量移动行
                workSheet.ShiftRows(
                    rowIndex - 2,                                 //--开始行
                    workSheet.LastRowNum,                      //--结束行
                    count,                             //--移动大小(行数)--往下移动
                    true,                                  //是否复制行高
                    false//,                               //是否重置行高
                    //true                                 //是否移动批注
                );
                #endregion

                #region 对批量移动后空出的空行插，创建相应的行，并以插入行的上一行为格式源(即：插入行-1的那一行)
                for (int i = rowIndex; i < rowIndex + count - 1; i++)
                {
                    IRow targetRow = null;
                    ICell sourceCell = null;
                    ICell targetCell = null;

                    targetRow = workSheet.CreateRow(i + 1);

                    for (int m = row.FirstCellNum; m < row.LastCellNum; m++)
                    {
                        sourceCell = row.GetCell(m);
                        if (sourceCell == null)
                            continue;
                        targetCell = targetRow.CreateCell(m);

                        //targetCell..Encoding = sourceCell.Encoding;
                        targetCell.CellStyle = sourceCell.CellStyle;
                        targetCell.SetCellType(sourceCell.CellType);
                    }
                    //CopyRow(sourceRow, targetRow);
                    //Util.CopyRow(sheet, sourceRow, targetRow);
                }

                IRow firstTargetRow = workSheet.GetRow(rowIndex - 2);
                ICell firstSourceCell = null;
                ICell firstTargetCell = null;

                for (int m = row.FirstCellNum; m < row.LastCellNum; m++)
                {
                    firstSourceCell = row.GetCell(m);
                    if (firstSourceCell == null)
                        continue;
                    firstTargetCell = firstTargetRow.CreateCell(m);

                    //firstTargetCell.Encoding = firstSourceCell.Encoding;
                    firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                    firstTargetCell.SetCellType(firstSourceCell.CellType);
                }
                #endregion
            }
            catch (Exception e)
            {
                //this.KillExcelProcess(false);
                throw e;
            }
        }


        /// <summary>
        /// 复制行（在指定行下面复制指定数量行）
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        public void CopyRows(int rowIndex, int count)
        {
            try
            {
                for (int i = 1; i <= count; i++)
                {
                    workSheet.CopyRow(rowIndex - 1, rowIndex);
                }
            }
            catch (Exception e)
            {
                //this.KillExcelProcess(false);
                throw e;
            }
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        public void DeleteRows(int rowIndex, int count)
        {
            try
            {
                workSheet.ShiftRows(rowIndex - 1, rowIndex - 1 + count, -1);
                range.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
            }
            catch (Exception e)
            {
                //this.KillExcelProcess(false);
                throw e;
            }
        }

        #endregion


        #region 静态方法

        /// <summary>
        /// 杀Excel进程
        /// </summary>
        public static void KillAllExcelProcess()
        {
            //Process[] myProcesses;
            //myProcesses = Process.GetProcessesByName("Excel");

            ////得不到Excel进程ID，暂时只能判断进程启动时间
            //foreach (Process myProcess in myProcesses)
            //{
            //    myProcess.Kill();
            //}
        }

        /// <summary>
        /// 打开相应的excel
        /// </summary>
        /// <param name="filepath"></param>
        public static void OpenExcel(string filepath)
        {
            //Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            //xlsApp.Workbooks.Open(filepath);
            //xlsApp.Visible = true;
        }

        #endregion

    }
}
