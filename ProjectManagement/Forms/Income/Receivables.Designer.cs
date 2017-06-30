namespace ProjectManagement.Forms.Income
{
    partial class FormReceivables
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
            this.gridSK = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtExplanation = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtSBatchNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbSFinishStatus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnSSave = new DevComponents.DotNetBar.ButtonX();
            this.btnSClear = new DevComponents.DotNetBar.ButtonX();
            this.dtSInDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtSRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSCondition = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX54 = new DevComponents.DotNetBar.LabelX();
            this.labelX53 = new DevComponents.DotNetBar.LabelX();
            this.labelX52 = new DevComponents.DotNetBar.LabelX();
            this.labelX51 = new DevComponents.DotNetBar.LabelX();
            this.labelX50 = new DevComponents.DotNetBar.LabelX();
            this.intSRatio = new DevComponents.Editors.IntegerInput();
            this.labelX49 = new DevComponents.DotNetBar.LabelX();
            this.labelX48 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtSInDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.gridSK);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(5, 1);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(834, 252);
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
            this.groupPanel1.Text = "收款信息";
            // 
            // gridSK
            // 
            this.gridSK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSK.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gridSK.Location = new System.Drawing.Point(0, 0);
            this.gridSK.Margin = new System.Windows.Forms.Padding(2);
            this.gridSK.Name = "gridSK";
            // 
            // 
            // 
            this.gridSK.PrimaryGrid.AllowEdit = false;
            this.gridSK.PrimaryGrid.AutoGenerateColumns = false;
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.gridSK.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.gridSK.PrimaryGrid.MultiSelect = false;
            this.gridSK.PrimaryGrid.ReadOnly = true;
            this.gridSK.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.gridSK.Size = new System.Drawing.Size(828, 228);
            this.gridSK.TabIndex = 0;
            this.gridSK.Text = "superGridControl1";
            this.gridSK.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.gridSK_RowClick);
            // 
            // gridColumn9
            // 
            this.gridColumn9.Name = "ID";
            this.gridColumn9.Visible = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.HeaderText = "编号";
            this.gridColumn1.Name = "RowNo";
            // 
            // gridColumn10
            // 
            this.gridColumn10.HeaderText = "收款批次";
            this.gridColumn10.MinimumWidth = 150;
            this.gridColumn10.Name = "BatchNo";
            this.gridColumn10.Width = 150;
            // 
            // gridColumn2
            // 
            this.gridColumn2.DataPropertyName = "Explanation";
            this.gridColumn2.HeaderText = "说明";
            this.gridColumn2.Name = "Explanation";
            // 
            // gridColumn3
            // 
            this.gridColumn3.HeaderText = "收款比例";
            this.gridColumn3.Name = "Ratio";
            // 
            // gridColumn4
            // 
            this.gridColumn4.HeaderText = "收款金额";
            this.gridColumn4.Name = "Amount";
            // 
            // gridColumn5
            // 
            this.gridColumn5.HeaderText = "收款条件";
            this.gridColumn5.Name = "Condition";
            // 
            // gridColumn6
            // 
            this.gridColumn6.HeaderText = "完成情况";
            this.gridColumn6.Name = "FinishStatusName";
            // 
            // gridColumn7
            // 
            this.gridColumn7.HeaderText = "收款日期";
            this.gridColumn7.Name = "InDate";
            // 
            // gridColumn8
            // 
            this.gridColumn8.HeaderText = "备注";
            this.gridColumn8.Name = "Remark";
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.txtAmount);
            this.groupPanel2.Controls.Add(this.txtExplanation);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.txtSBatchNo);
            this.groupPanel2.Controls.Add(this.cbSFinishStatus);
            this.groupPanel2.Controls.Add(this.btnSSave);
            this.groupPanel2.Controls.Add(this.btnSClear);
            this.groupPanel2.Controls.Add(this.dtSInDate);
            this.groupPanel2.Controls.Add(this.txtSRemark);
            this.groupPanel2.Controls.Add(this.txtSCondition);
            this.groupPanel2.Controls.Add(this.labelX54);
            this.groupPanel2.Controls.Add(this.labelX53);
            this.groupPanel2.Controls.Add(this.labelX52);
            this.groupPanel2.Controls.Add(this.labelX51);
            this.groupPanel2.Controls.Add(this.labelX50);
            this.groupPanel2.Controls.Add(this.intSRatio);
            this.groupPanel2.Controls.Add(this.labelX49);
            this.groupPanel2.Controls.Add(this.labelX48);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Location = new System.Drawing.Point(5, 253);
            this.groupPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(834, 254);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 0;
            this.groupPanel2.Text = "编辑收款信息";
            // 
            // txtAmount
            // 
            // 
            // 
            // 
            this.txtAmount.Border.Class = "TextBoxBorder";
            this.txtAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAmount.Location = new System.Drawing.Point(104, 107);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.PreventEnterBeep = true;
            this.txtAmount.Size = new System.Drawing.Size(251, 21);
            this.txtAmount.TabIndex = 32;
            // 
            // txtExplanation
            // 
            // 
            // 
            // 
            this.txtExplanation.Border.Class = "TextBoxBorder";
            this.txtExplanation.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExplanation.Location = new System.Drawing.Point(104, 44);
            this.txtExplanation.Name = "txtExplanation";
            this.txtExplanation.PreventEnterBeep = true;
            this.txtExplanation.Size = new System.Drawing.Size(251, 21);
            this.txtExplanation.TabIndex = 31;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 46);
            this.labelX1.Margin = new System.Windows.Forms.Padding(2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(74, 18);
            this.labelX1.TabIndex = 30;
            this.labelX1.Text = "收款说明：";
            // 
            // txtSBatchNo
            // 
            // 
            // 
            // 
            this.txtSBatchNo.Border.Class = "TextBoxBorder";
            this.txtSBatchNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSBatchNo.Location = new System.Drawing.Point(104, 11);
            this.txtSBatchNo.Name = "txtSBatchNo";
            this.txtSBatchNo.PreventEnterBeep = true;
            this.txtSBatchNo.Size = new System.Drawing.Size(251, 21);
            this.txtSBatchNo.TabIndex = 29;
            // 
            // cbSFinishStatus
            // 
            this.cbSFinishStatus.DisplayMember = "Text";
            this.cbSFinishStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbSFinishStatus.FormattingEnabled = true;
            this.cbSFinishStatus.ItemHeight = 19;
            this.cbSFinishStatus.Location = new System.Drawing.Point(474, 14);
            this.cbSFinishStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cbSFinishStatus.Name = "cbSFinishStatus";
            this.cbSFinishStatus.Size = new System.Drawing.Size(252, 25);
            this.cbSFinishStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbSFinishStatus.TabIndex = 2;
            // 
            // btnSSave
            // 
            this.btnSSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSSave.Location = new System.Drawing.Point(566, 203);
            this.btnSSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSSave.Name = "btnSSave";
            this.btnSSave.Size = new System.Drawing.Size(56, 18);
            this.btnSSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSSave.TabIndex = 8;
            this.btnSSave.Text = "保存";
            this.btnSSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSClear
            // 
            this.btnSClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSClear.Location = new System.Drawing.Point(474, 201);
            this.btnSClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnSClear.Name = "btnSClear";
            this.btnSClear.Size = new System.Drawing.Size(56, 18);
            this.btnSClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSClear.TabIndex = 9;
            this.btnSClear.Text = "清空";
            this.btnSClear.Click += new System.EventHandler(this.btnSClear_Click);
            // 
            // dtSInDate
            // 
            // 
            // 
            // 
            this.dtSInDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtSInDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtSInDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtSInDate.ButtonDropDown.Visible = true;
            this.dtSInDate.IsPopupCalendarOpen = false;
            this.dtSInDate.Location = new System.Drawing.Point(104, 198);
            this.dtSInDate.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.dtSInDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtSInDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtSInDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtSInDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtSInDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtSInDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtSInDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtSInDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtSInDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtSInDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtSInDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtSInDate.MonthCalendar.DisplayMonth = new System.DateTime(2017, 3, 1, 0, 0, 0, 0);
            this.dtSInDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtSInDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtSInDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtSInDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtSInDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtSInDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtSInDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtSInDate.MonthCalendar.TodayButtonVisible = true;
            this.dtSInDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtSInDate.Name = "dtSInDate";
            this.dtSInDate.Size = new System.Drawing.Size(251, 21);
            this.dtSInDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtSInDate.TabIndex = 6;
            // 
            // txtSRemark
            // 
            this.txtSRemark.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSRemark.Border.Class = "TextBoxBorder";
            this.txtSRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSRemark.DisabledBackColor = System.Drawing.Color.White;
            this.txtSRemark.ForeColor = System.Drawing.Color.Black;
            this.txtSRemark.Location = new System.Drawing.Point(474, 48);
            this.txtSRemark.Margin = new System.Windows.Forms.Padding(2);
            this.txtSRemark.Multiline = true;
            this.txtSRemark.Name = "txtSRemark";
            this.txtSRemark.PreventEnterBeep = true;
            this.txtSRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSRemark.Size = new System.Drawing.Size(251, 138);
            this.txtSRemark.TabIndex = 7;
            // 
            // txtSCondition
            // 
            this.txtSCondition.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSCondition.Border.Class = "TextBoxBorder";
            this.txtSCondition.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSCondition.DisabledBackColor = System.Drawing.Color.White;
            this.txtSCondition.ForeColor = System.Drawing.Color.Black;
            this.txtSCondition.Location = new System.Drawing.Point(104, 143);
            this.txtSCondition.Margin = new System.Windows.Forms.Padding(2);
            this.txtSCondition.Multiline = true;
            this.txtSCondition.Name = "txtSCondition";
            this.txtSCondition.PreventEnterBeep = true;
            this.txtSCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSCondition.Size = new System.Drawing.Size(251, 43);
            this.txtSCondition.TabIndex = 5;
            // 
            // labelX54
            // 
            this.labelX54.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX54.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX54.Location = new System.Drawing.Point(381, 14);
            this.labelX54.Margin = new System.Windows.Forms.Padding(2);
            this.labelX54.Name = "labelX54";
            this.labelX54.Size = new System.Drawing.Size(74, 18);
            this.labelX54.TabIndex = 22;
            this.labelX54.Text = "完成情况：";
            // 
            // labelX53
            // 
            this.labelX53.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX53.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX53.Location = new System.Drawing.Point(381, 46);
            this.labelX53.Margin = new System.Windows.Forms.Padding(2);
            this.labelX53.Name = "labelX53";
            this.labelX53.Size = new System.Drawing.Size(74, 18);
            this.labelX53.TabIndex = 28;
            this.labelX53.Text = "备注：";
            // 
            // labelX52
            // 
            this.labelX52.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX52.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX52.Location = new System.Drawing.Point(12, 200);
            this.labelX52.Margin = new System.Windows.Forms.Padding(2);
            this.labelX52.Name = "labelX52";
            this.labelX52.Size = new System.Drawing.Size(74, 18);
            this.labelX52.TabIndex = 27;
            this.labelX52.Text = "*收款日期：";
            // 
            // labelX51
            // 
            this.labelX51.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX51.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX51.Location = new System.Drawing.Point(12, 142);
            this.labelX51.Margin = new System.Windows.Forms.Padding(2);
            this.labelX51.Name = "labelX51";
            this.labelX51.Size = new System.Drawing.Size(74, 18);
            this.labelX51.TabIndex = 26;
            this.labelX51.Text = "收款条件：";
            // 
            // labelX50
            // 
            this.labelX50.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX50.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX50.Location = new System.Drawing.Point(12, 110);
            this.labelX50.Margin = new System.Windows.Forms.Padding(2);
            this.labelX50.Name = "labelX50";
            this.labelX50.Size = new System.Drawing.Size(74, 18);
            this.labelX50.TabIndex = 25;
            this.labelX50.Text = "收款金额：";
            // 
            // intSRatio
            // 
            // 
            // 
            // 
            this.intSRatio.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSRatio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSRatio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSRatio.Location = new System.Drawing.Point(104, 77);
            this.intSRatio.Margin = new System.Windows.Forms.Padding(2);
            this.intSRatio.MinValue = 0;
            this.intSRatio.Name = "intSRatio";
            this.intSRatio.ShowUpDown = true;
            this.intSRatio.Size = new System.Drawing.Size(251, 21);
            this.intSRatio.TabIndex = 3;
            this.intSRatio.ValueChanged += new System.EventHandler(this.intSRatio_ValueChanged);
            // 
            // labelX49
            // 
            this.labelX49.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX49.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX49.Location = new System.Drawing.Point(12, 78);
            this.labelX49.Margin = new System.Windows.Forms.Padding(2);
            this.labelX49.Name = "labelX49";
            this.labelX49.Size = new System.Drawing.Size(74, 18);
            this.labelX49.TabIndex = 23;
            this.labelX49.Text = "收款比例：";
            // 
            // labelX48
            // 
            this.labelX48.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX48.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX48.Location = new System.Drawing.Point(12, 14);
            this.labelX48.Margin = new System.Windows.Forms.Padding(2);
            this.labelX48.Name = "labelX48";
            this.labelX48.Size = new System.Drawing.Size(74, 18);
            this.labelX48.TabIndex = 21;
            this.labelX48.Text = "*收款批次：";
            // 
            // FormReceivables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 509);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Name = "FormReceivables";
            this.Text = "收款信息";
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtSInDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSRatio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbSFinishStatus;
        private DevComponents.DotNetBar.ButtonX btnSSave;
        private DevComponents.DotNetBar.ButtonX btnSClear;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtSInDate;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSRemark;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSCondition;
        private DevComponents.DotNetBar.LabelX labelX54;
        private DevComponents.DotNetBar.LabelX labelX53;
        private DevComponents.DotNetBar.LabelX labelX52;
        private DevComponents.DotNetBar.LabelX labelX51;
        private DevComponents.DotNetBar.LabelX labelX50;
        private DevComponents.Editors.IntegerInput intSRatio;
        private DevComponents.DotNetBar.LabelX labelX49;
        private DevComponents.DotNetBar.LabelX labelX48;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gridSK;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSBatchNo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExplanation;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAmount;
    }
}