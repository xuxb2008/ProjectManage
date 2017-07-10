namespace ProjectManagement.Forms.Report
{
    partial class Report_DifficutyDegree
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFinishStatus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtis = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.dtie = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dtis)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtie)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(44, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 30);
            this.label1.TabIndex = 10;
            this.label1.Text = "开始时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(44, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 30);
            this.label2.TabIndex = 11;
            this.label2.Text = "结束时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(702, 32);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(61, 26);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "导出";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(394, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 30);
            this.label3.TabIndex = 12;
            this.label3.Text = "完成情况";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbFinishStatus
            // 
            this.cmbFinishStatus.DisplayMember = "Text";
            this.cmbFinishStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFinishStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFinishStatus.FormattingEnabled = true;
            this.cmbFinishStatus.ItemHeight = 15;
            this.cmbFinishStatus.Location = new System.Drawing.Point(453, 3);
            this.cmbFinishStatus.Name = "cmbFinishStatus";
            this.cmbFinishStatus.Size = new System.Drawing.Size(244, 21);
            this.cmbFinishStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbFinishStatus.TabIndex = 15;
            // 
            // dtis
            // 
            // 
            // 
            // 
            this.dtis.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtis.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtis.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtis.ButtonDropDown.Visible = true;
            this.dtis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtis.IsPopupCalendarOpen = false;
            this.dtis.Location = new System.Drawing.Point(103, 3);
            // 
            // 
            // 
            this.dtis.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtis.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtis.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtis.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtis.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtis.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtis.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtis.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtis.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtis.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtis.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtis.MonthCalendar.DisplayMonth = new System.DateTime(2017, 7, 1, 0, 0, 0, 0);
            this.dtis.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtis.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtis.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtis.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtis.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtis.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtis.MonthCalendar.TodayButtonVisible = true;
            this.dtis.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtis.Name = "dtis";
            this.dtis.Size = new System.Drawing.Size(244, 21);
            this.dtis.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtis.TabIndex = 16;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(622, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 24);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gridColumn10
            // 
            this.gridColumn10.DataPropertyName = "type";
            this.gridColumn10.Name = "type";
            this.gridColumn10.Visible = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.DataPropertyName = "degree";
            this.gridColumn9.HeaderText = "难度系数";
            this.gridColumn9.Name = "efficiency";
            // 
            // gridColumn6
            // 
            this.gridColumn6.DataPropertyName = "enddate";
            this.gridColumn6.HeaderText = "结束时间";
            this.gridColumn6.Name = "enddate";
            // 
            // gridColumn5
            // 
            this.gridColumn5.DataPropertyName = "startdate";
            this.gridColumn5.HeaderText = "开始时间";
            this.gridColumn5.Name = "startedate";
            // 
            // gridColumn4
            // 
            this.gridColumn4.DataPropertyName = "Desc";
            this.gridColumn4.HeaderText = "描述";
            this.gridColumn4.Name = "desc";
            // 
            // gridColumn3
            // 
            this.gridColumn3.DataPropertyName = "name";
            this.gridColumn3.HeaderText = "名称";
            this.gridColumn3.Name = "name";
            // 
            // gridColumn2
            // 
            this.gridColumn2.DataPropertyName = "source";
            this.gridColumn2.HeaderText = "来源";
            this.gridColumn2.Name = "source";
            // 
            // gridColumn1
            // 
            this.gridColumn1.DataPropertyName = "RowNo";
            this.gridColumn1.HeaderText = "编号";
            this.gridColumn1.Name = "RowNo";
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Location = new System.Drawing.Point(702, 2);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(61, 26);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbFinishStatus, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtis, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtie, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 346);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // superGridControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.superGridControl1, 5);
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Location = new System.Drawing.Point(3, 63);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.AllowSelection = false;
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.Caption.AllowSelection = false;
            this.superGridControl1.PrimaryGrid.ColumnAutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.superGridControl1.PrimaryGrid.EnsureVisibleAfterGrouping = true;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(775, 280);
            this.superGridControl1.TabIndex = 9;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            // 
            // dtie
            // 
            // 
            // 
            // 
            this.dtie.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtie.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtie.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtie.ButtonDropDown.Visible = true;
            this.dtie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtie.IsPopupCalendarOpen = false;
            this.dtie.Location = new System.Drawing.Point(103, 33);
            // 
            // 
            // 
            this.dtie.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtie.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtie.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtie.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtie.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtie.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtie.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtie.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtie.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtie.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtie.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtie.MonthCalendar.DisplayMonth = new System.DateTime(2017, 7, 1, 0, 0, 0, 0);
            this.dtie.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtie.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtie.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtie.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtie.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtie.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtie.MonthCalendar.TodayButtonVisible = true;
            this.dtie.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtie.Name = "dtie";
            this.dtie.Size = new System.Drawing.Size(244, 21);
            this.dtie.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtie.TabIndex = 17;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.tableLayoutPanel1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(5, 1);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(787, 370);
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
            this.groupPanel1.TabIndex = 9;
            this.groupPanel1.Text = "成员贡献率";
            // 
            // Report_DifficutyDegree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 373);
            this.Controls.Add(this.groupPanel1);
            this.Name = "Report_DifficutyDegree";
            this.Text = "工作困难系数";
            ((System.ComponentModel.ISupportInitialize)(this.dtis)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtie)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFinishStatus;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtis;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtie;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
    }
}