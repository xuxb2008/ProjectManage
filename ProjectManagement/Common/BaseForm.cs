using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DomainDLL;
using ProjectManagement.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonDLL;

namespace ProjectManagement.Common
{
    public partial class BaseForm : Office2007RibbonForm
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get
            {
                return CacheHelper.GetProjectID();
            }
            set
            {
                CacheHelper.SetProjectID(value);
            }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public static string ProjectName;
        /// <summary>
        /// 项目编号
        /// </summary>
        public static string ProjectNo;

        /// <summary>
        /// 节点信息
        /// </summary>
        public static PNode CurrentNode;

        /// <summary>
        /// 首页
        /// </summary>
        public static StartPage startPage;

        public BaseForm()
        {
            InitializeComponent();
        }
    }
}
