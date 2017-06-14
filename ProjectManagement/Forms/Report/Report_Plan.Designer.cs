namespace ProjectManagement.Forms.Report
{
    partial class Report_Plan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtClear = new DevComponents.DotNetBar.ButtonX();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.Namee = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.WBSNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Workload = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.StarteDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.EndDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Manager = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Progress = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.cmbFinishStatus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbManager = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtiEndDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dtiStartDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiStartDate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtClear);
            this.groupPanel1.Controls.Add(this.treeList1);
            this.groupPanel1.Controls.Add(this.btnExport);
            this.groupPanel1.Controls.Add(this.btnSearch);
            this.groupPanel1.Controls.Add(this.cmbFinishStatus);
            this.groupPanel1.Controls.Add(this.cmbManager);
            this.groupPanel1.Controls.Add(this.dtiEndDate);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.dtiStartDate);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(5, 1);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(791, 485);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "计划预览";
            // 
            // txtClear
            // 
            this.txtClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.txtClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.txtClear.Location = new System.Drawing.Point(512, 3);
            this.txtClear.Margin = new System.Windows.Forms.Padding(2);
            this.txtClear.Name = "txtClear";
            this.txtClear.Size = new System.Drawing.Size(56, 18);
            this.txtClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtClear.TabIndex = 8;
            this.txtClear.Text = "清空";
            this.txtClear.Click += new System.EventHandler(this.txtClear_Click);
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.Namee,
            this.WBSNo,
            this.Workload,
            this.StarteDate,
            this.EndDate,
            this.Manager,
            this.Progress,
            this.ID});
            this.treeList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeList1.Location = new System.Drawing.Point(-3, 62);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.Size = new System.Drawing.Size(796, 447);
            this.treeList1.TabIndex = 7;
            // 
            // Namee
            // 
            this.Namee.Caption = "名称";
            this.Namee.FieldName = "Name";
            this.Namee.Name = "Namee";
            this.Namee.Visible = true;
            this.Namee.VisibleIndex = 0;
            this.Namee.Width = 135;
            // 
            // WBSNo
            // 
            this.WBSNo.Caption = "WBS代码";
            this.WBSNo.FieldName = "WBSNo";
            this.WBSNo.Name = "WBSNo";
            this.WBSNo.Visible = true;
            this.WBSNo.VisibleIndex = 1;
            this.WBSNo.Width = 130;
            // 
            // Workload
            // 
            this.Workload.Caption = "工作量";
            this.Workload.FieldName = "Workload";
            this.Workload.Name = "Workload";
            this.Workload.Visible = true;
            this.Workload.VisibleIndex = 2;
            this.Workload.Width = 129;
            // 
            // StarteDate
            // 
            this.StarteDate.Caption = "开始时间";
            this.StarteDate.FieldName = "StarteDate";
            this.StarteDate.Name = "StarteDate";
            this.StarteDate.Visible = true;
            this.StarteDate.VisibleIndex = 3;
            this.StarteDate.Width = 128;
            // 
            // EndDate
            // 
            this.EndDate.Caption = "结束时间";
            this.EndDate.FieldName = "EndDate";
            this.EndDate.Name = "EndDate";
            this.EndDate.Visible = true;
            this.EndDate.VisibleIndex = 4;
            this.EndDate.Width = 128;
            // 
            // Manager
            // 
            this.Manager.Caption = "负责人";
            this.Manager.FieldName = "Manager";
            this.Manager.Name = "Manager";
            // 
            // Progress
            // 
            this.Progress.Caption = "完成比例";
            this.Progress.FieldName = "Progress";
            this.Progress.Name = "Progress";
            this.Progress.Visible = true;
            this.Progress.VisibleIndex = 5;
            this.Progress.Width = 128;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(590, 32);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(56, 18);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "导出计划";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(590, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(56, 18);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "生成计划";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbFinishStatus
            // 
            this.cmbFinishStatus.DisplayMember = "Text";
            this.cmbFinishStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFinishStatus.FormattingEnabled = true;
            this.cmbFinishStatus.ItemHeight = 19;
            this.cmbFinishStatus.Location = new System.Drawing.Point(338, 32);
            this.cmbFinishStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFinishStatus.Name = "cmbFinishStatus";
            this.cmbFinishStatus.Size = new System.Drawing.Size(151, 25);
            this.cmbFinishStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbFinishStatus.TabIndex = 4;
            // 
            // cmbManager
            // 
            this.cmbManager.DisplayMember = "Text";
            this.cmbManager.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbManager.FormattingEnabled = true;
            this.cmbManager.ItemHeight = 19;
            this.cmbManager.Location = new System.Drawing.Point(338, 1);
            this.cmbManager.Margin = new System.Windows.Forms.Padding(2);
            this.cmbManager.Name = "cmbManager";
            this.cmbManager.Size = new System.Drawing.Size(151, 25);
            this.cmbManager.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbManager.TabIndex = 2;
            // 
            // dtiEndDate
            // 
            // 
            // 
            // 
            this.dtiEndDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiEndDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiEndDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiEndDate.ButtonDropDown.Visible = true;
            this.dtiEndDate.IsPopupCalendarOpen = false;
            this.dtiEndDate.Location = new System.Drawing.Point(82, 32);
            this.dtiEndDate.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.dtiEndDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiEndDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiEndDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiEndDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiEndDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiEndDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiEndDate.MonthCalendar.DisplayMonth = new System.DateTime(2017, 3, 1, 0, 0, 0, 0);
            this.dtiEndDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtiEndDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiEndDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiEndDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiEndDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiEndDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiEndDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiEndDate.Name = "dtiEndDate";
            this.dtiEndDate.Size = new System.Drawing.Size(150, 21);
            this.dtiEndDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiEndDate.TabIndex = 3;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(260, 34);
            this.labelX4.Margin = new System.Windows.Forms.Padding(2);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(73, 18);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "完成比例：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(5, 34);
            this.labelX2.Margin = new System.Windows.Forms.Padding(2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(73, 18);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "结束时间：";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(260, 2);
            this.labelX3.Margin = new System.Windows.Forms.Padding(2);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(73, 18);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "负责人：";
            // 
            // dtiStartDate
            // 
            this.dtiStartDate.AccessibleDescription = "";
            // 
            // 
            // 
            this.dtiStartDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiStartDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiStartDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiStartDate.ButtonDropDown.Visible = true;
            this.dtiStartDate.IsPopupCalendarOpen = false;
            this.dtiStartDate.Location = new System.Drawing.Point(82, 0);
            this.dtiStartDate.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.dtiStartDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiStartDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiStartDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiStartDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiStartDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiStartDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiStartDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiStartDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiStartDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiStartDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiStartDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiStartDate.MonthCalendar.DisplayMonth = new System.DateTime(2017, 3, 1, 0, 0, 0, 0);
            this.dtiStartDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtiStartDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiStartDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiStartDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiStartDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiStartDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiStartDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiStartDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiStartDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiStartDate.Name = "dtiStartDate";
            this.dtiStartDate.Size = new System.Drawing.Size(150, 21);
            this.dtiStartDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiStartDate.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(5, 2);
            this.labelX1.Margin = new System.Windows.Forms.Padding(2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(73, 18);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "开始时间：";
            // 
            // gridColumn1
            // 
            this.gridColumn1.HeaderText = "编号";
            this.gridColumn1.Name = "RowNo";
            // 
            // gridColumn2
            // 
            this.gridColumn2.DataPropertyName = "WBSNo";
            this.gridColumn2.HeaderText = "WBS代码";
            this.gridColumn2.Name = "No";
            // 
            // gridColumn3
            // 
            this.gridColumn3.HeaderText = "任务名称";
            this.gridColumn3.Name = "Name";
            // 
            // gridColumn4
            // 
            this.gridColumn4.DataPropertyName = "Workload";
            this.gridColumn4.HeaderText = "工作量";
            this.gridColumn4.Name = "WorkLoad";
            // 
            // gridColumn5
            // 
            this.gridColumn5.HeaderText = "开始时间";
            this.gridColumn5.Name = "StarteDate";
            // 
            // gridColumn6
            // 
            this.gridColumn6.HeaderText = "结束时间";
            this.gridColumn6.Name = "EndDate";
            // 
            // gridColumn8
            // 
            this.gridColumn8.DataPropertyName = "Progress";
            this.gridColumn8.HeaderText = "完成比例";
            this.gridColumn8.Name = "FinishRatio";
            // 
            // gridColumn7
            // 
            this.gridColumn7.DataPropertyName = "Manager";
            this.gridColumn7.HeaderText = "负责人";
            this.gridColumn7.Name = "Manager";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Name = "ID";
            this.gridColumn9.Visible = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Name = "Status";
            this.gridColumn10.Visible = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Name = "CREATED";
            this.gridColumn11.Visible = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Name = "UPDATED";
            this.gridColumn12.Visible = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.DataPropertyName = "NodeID";
            this.gridColumn13.Name = "NodeID";
            this.gridColumn13.Visible = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.DataPropertyName = "Desc";
            this.gridColumn14.Name = "Desc";
            this.gridColumn14.Visible = false;
            // 
            // gridColumn15
            // 
            this.gridColumn15.DataPropertyName = "Weight";
            this.gridColumn15.Name = "Weight";
            this.gridColumn15.Visible = false;
            // 
            // Report_Plan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 488);
            this.Controls.Add(this.groupPanel1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "Report_Plan";
            this.Text = "项目计划报表";
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiStartDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiEndDate;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiStartDate;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFinishStatus;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbManager;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Namee;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Workload;
        private DevExpress.XtraTreeList.Columns.TreeListColumn StarteDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn EndDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Manager;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Progress;
        private DevExpress.XtraTreeList.Columns.TreeListColumn WBSNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ID;
        private DevComponents.DotNetBar.ButtonX txtClear;
    }
}