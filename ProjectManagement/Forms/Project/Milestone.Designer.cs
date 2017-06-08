namespace ProjectManagement.Forms.Project
{
    partial class FormMilestone
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
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnLClear = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.cbLStatus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtLCREATED = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtLFinish = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX26 = new DevComponents.DotNetBar.LabelX();
            this.labelX27 = new DevComponents.DotNetBar.LabelX();
            this.labelX28 = new DevComponents.DotNetBar.LabelX();
            this.txtLRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtLCondition = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtLName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX29 = new DevComponents.DotNetBar.LabelX();
            this.labelX30 = new DevComponents.DotNetBar.LabelX();
            this.labelX31 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel5 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridLCB = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.LCBNo = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBID = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBName = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBFinishDate = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBFinishStatusName = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBCondition = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBRemark = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBCREATED = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBFinishStatus = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LCBStatus = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtLCREATED)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLFinish)).BeginInit();
            this.groupPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.HeaderText = "编号";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.HeaderText = "里程碑名称";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.HeaderText = "完成日期";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn5
            // 
            this.gridColumn5.HeaderText = "完成依据";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn4
            // 
            this.gridColumn4.HeaderText = "完成情况";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn6
            // 
            this.gridColumn6.HeaderText = "存在问题";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnLClear);
            this.groupPanel1.Controls.Add(this.btnSave);
            this.groupPanel1.Controls.Add(this.cbLStatus);
            this.groupPanel1.Controls.Add(this.dtLCREATED);
            this.groupPanel1.Controls.Add(this.dtLFinish);
            this.groupPanel1.Controls.Add(this.labelX26);
            this.groupPanel1.Controls.Add(this.labelX27);
            this.groupPanel1.Controls.Add(this.labelX28);
            this.groupPanel1.Controls.Add(this.txtLRemark);
            this.groupPanel1.Controls.Add(this.txtLCondition);
            this.groupPanel1.Controls.Add(this.txtLName);
            this.groupPanel1.Controls.Add(this.labelX29);
            this.groupPanel1.Controls.Add(this.labelX30);
            this.groupPanel1.Controls.Add(this.labelX31);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(3, 286);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(909, 224);
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
            this.groupPanel1.TabIndex = 40;
            this.groupPanel1.Text = "编辑里程碑信息";
            // 
            // btnLClear
            // 
            this.btnLClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLClear.Location = new System.Drawing.Point(424, 139);
            this.btnLClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnLClear.Name = "btnLClear";
            this.btnLClear.Size = new System.Drawing.Size(56, 18);
            this.btnLClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLClear.TabIndex = 8;
            this.btnLClear.Text = "清空";
            this.btnLClear.Click += new System.EventHandler(this.btnLClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(518, 139);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 18);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbLStatus
            // 
            this.cbLStatus.DisplayMember = "Text";
            this.cbLStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLStatus.FormattingEnabled = true;
            this.cbLStatus.ItemHeight = 19;
            this.cbLStatus.Location = new System.Drawing.Point(99, 39);
            this.cbLStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cbLStatus.Name = "cbLStatus";
            this.cbLStatus.Size = new System.Drawing.Size(210, 25);
            this.cbLStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbLStatus.TabIndex = 3;
            // 
            // dtLCREATED
            // 
            // 
            // 
            // 
            this.dtLCREATED.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtLCREATED.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLCREATED.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtLCREATED.ButtonDropDown.Visible = true;
            this.dtLCREATED.Enabled = false;
            this.dtLCREATED.IsPopupCalendarOpen = false;
            this.dtLCREATED.Location = new System.Drawing.Point(424, 83);
            this.dtLCREATED.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.dtLCREATED.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLCREATED.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLCREATED.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtLCREATED.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtLCREATED.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtLCREATED.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtLCREATED.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtLCREATED.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtLCREATED.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtLCREATED.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtLCREATED.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLCREATED.MonthCalendar.DisplayMonth = new System.DateTime(2017, 3, 1, 0, 0, 0, 0);
            this.dtLCREATED.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtLCREATED.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtLCREATED.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLCREATED.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtLCREATED.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtLCREATED.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtLCREATED.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLCREATED.MonthCalendar.TodayButtonVisible = true;
            this.dtLCREATED.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtLCREATED.Name = "dtLCREATED";
            this.dtLCREATED.Size = new System.Drawing.Size(209, 21);
            this.dtLCREATED.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtLCREATED.TabIndex = 6;
            // 
            // dtLFinish
            // 
            // 
            // 
            // 
            this.dtLFinish.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtLFinish.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLFinish.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtLFinish.ButtonDropDown.Visible = true;
            this.dtLFinish.IsPopupCalendarOpen = false;
            this.dtLFinish.Location = new System.Drawing.Point(424, 3);
            this.dtLFinish.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.dtLFinish.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLFinish.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLFinish.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtLFinish.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtLFinish.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtLFinish.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtLFinish.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtLFinish.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtLFinish.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtLFinish.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtLFinish.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLFinish.MonthCalendar.DisplayMonth = new System.DateTime(2017, 3, 1, 0, 0, 0, 0);
            this.dtLFinish.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtLFinish.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtLFinish.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLFinish.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtLFinish.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtLFinish.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtLFinish.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLFinish.MonthCalendar.TodayButtonVisible = true;
            this.dtLFinish.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtLFinish.Name = "dtLFinish";
            this.dtLFinish.Size = new System.Drawing.Size(209, 21);
            this.dtLFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtLFinish.TabIndex = 2;
            // 
            // labelX26
            // 
            this.labelX26.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX26.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX26.Location = new System.Drawing.Point(332, 80);
            this.labelX26.Margin = new System.Windows.Forms.Padding(2);
            this.labelX26.Name = "labelX26";
            this.labelX26.Size = new System.Drawing.Size(88, 18);
            this.labelX26.TabIndex = 33;
            this.labelX26.Text = "添加日期：";
            // 
            // labelX27
            // 
            this.labelX27.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX27.Location = new System.Drawing.Point(7, 39);
            this.labelX27.Margin = new System.Windows.Forms.Padding(2);
            this.labelX27.Name = "labelX27";
            this.labelX27.Size = new System.Drawing.Size(88, 18);
            this.labelX27.TabIndex = 33;
            this.labelX27.Text = "*完成情况：";
            // 
            // labelX28
            // 
            this.labelX28.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX28.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX28.Location = new System.Drawing.Point(332, 3);
            this.labelX28.Margin = new System.Windows.Forms.Padding(2);
            this.labelX28.Name = "labelX28";
            this.labelX28.Size = new System.Drawing.Size(88, 18);
            this.labelX28.TabIndex = 33;
            this.labelX28.Text = "*完成日期：";
            // 
            // txtLRemark
            // 
            this.txtLRemark.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLRemark.Border.Class = "TextBoxBorder";
            this.txtLRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLRemark.DisabledBackColor = System.Drawing.Color.White;
            this.txtLRemark.ForeColor = System.Drawing.Color.Black;
            this.txtLRemark.Location = new System.Drawing.Point(99, 83);
            this.txtLRemark.Margin = new System.Windows.Forms.Padding(2);
            this.txtLRemark.Multiline = true;
            this.txtLRemark.Name = "txtLRemark";
            this.txtLRemark.PreventEnterBeep = true;
            this.txtLRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLRemark.Size = new System.Drawing.Size(209, 67);
            this.txtLRemark.TabIndex = 5;
            // 
            // txtLCondition
            // 
            this.txtLCondition.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLCondition.Border.Class = "TextBoxBorder";
            this.txtLCondition.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLCondition.DisabledBackColor = System.Drawing.Color.White;
            this.txtLCondition.ForeColor = System.Drawing.Color.Black;
            this.txtLCondition.Location = new System.Drawing.Point(424, 37);
            this.txtLCondition.Margin = new System.Windows.Forms.Padding(2);
            this.txtLCondition.Name = "txtLCondition";
            this.txtLCondition.PreventEnterBeep = true;
            this.txtLCondition.Size = new System.Drawing.Size(209, 21);
            this.txtLCondition.TabIndex = 4;
            // 
            // txtLName
            // 
            this.txtLName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLName.Border.Class = "TextBoxBorder";
            this.txtLName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLName.DisabledBackColor = System.Drawing.Color.White;
            this.txtLName.ForeColor = System.Drawing.Color.Black;
            this.txtLName.Location = new System.Drawing.Point(99, 3);
            this.txtLName.Margin = new System.Windows.Forms.Padding(2);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(209, 21);
            this.txtLName.TabIndex = 1;
            // 
            // labelX29
            // 
            this.labelX29.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX29.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX29.Location = new System.Drawing.Point(7, 80);
            this.labelX29.Margin = new System.Windows.Forms.Padding(2);
            this.labelX29.Name = "labelX29";
            this.labelX29.Size = new System.Drawing.Size(88, 18);
            this.labelX29.TabIndex = 33;
            this.labelX29.Text = "备注：";
            // 
            // labelX30
            // 
            this.labelX30.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX30.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX30.Location = new System.Drawing.Point(332, 39);
            this.labelX30.Margin = new System.Windows.Forms.Padding(2);
            this.labelX30.Name = "labelX30";
            this.labelX30.Size = new System.Drawing.Size(88, 18);
            this.labelX30.TabIndex = 33;
            this.labelX30.Text = "*完成依据：";
            // 
            // labelX31
            // 
            this.labelX31.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX31.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX31.Location = new System.Drawing.Point(7, 3);
            this.labelX31.Margin = new System.Windows.Forms.Padding(2);
            this.labelX31.Name = "labelX31";
            this.labelX31.Size = new System.Drawing.Size(88, 18);
            this.labelX31.TabIndex = 33;
            this.labelX31.Text = "*里程碑名称：";
            // 
            // groupPanel5
            // 
            this.groupPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel5.AutoScroll = true;
            this.groupPanel5.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel5.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel5.Controls.Add(this.gridLCB);
            this.groupPanel5.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel5.Location = new System.Drawing.Point(6, 6);
            this.groupPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.Size = new System.Drawing.Size(909, 276);
            // 
            // 
            // 
            this.groupPanel5.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel5.Style.BackColorGradientAngle = 90;
            this.groupPanel5.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel5.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderBottomWidth = 1;
            this.groupPanel5.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel5.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderLeftWidth = 1;
            this.groupPanel5.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderRightWidth = 1;
            this.groupPanel5.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderTopWidth = 1;
            this.groupPanel5.Style.CornerDiameter = 4;
            this.groupPanel5.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel5.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel5.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel5.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel5.TabIndex = 24;
            this.groupPanel5.Text = "里程碑列表";
            // 
            // gridLCB
            // 
            this.gridLCB.BackColor = System.Drawing.Color.White;
            this.gridLCB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLCB.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gridLCB.ForeColor = System.Drawing.Color.Black;
            this.gridLCB.Location = new System.Drawing.Point(0, 0);
            this.gridLCB.Margin = new System.Windows.Forms.Padding(2);
            this.gridLCB.Name = "gridLCB";
            // 
            // 
            // 
            this.gridLCB.PrimaryGrid.AutoGenerateColumns = false;
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBNo);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBID);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBName);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBFinishDate);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBFinishStatusName);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBCondition);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBRemark);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBCREATED);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBFinishStatus);
            this.gridLCB.PrimaryGrid.Columns.Add(this.LCBStatus);
            this.gridLCB.PrimaryGrid.ReadOnly = true;
            this.gridLCB.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.gridLCB.Size = new System.Drawing.Size(903, 252);
            this.gridLCB.TabIndex = 0;
            this.gridLCB.Text = "superGridControl1";
            this.gridLCB.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.gridLCB_RowClick);
            // 
            // LCBNo
            // 
            this.LCBNo.HeaderText = "编号";
            this.LCBNo.Name = "RowNo";
            // 
            // LCBID
            // 
            this.LCBID.Name = "ID";
            this.LCBID.Visible = false;
            // 
            // LCBName
            // 
            this.LCBName.HeaderText = "里程碑名称";
            this.LCBName.Name = "Name";
            // 
            // LCBFinishDate
            // 
            this.LCBFinishDate.HeaderText = "完成日期";
            this.LCBFinishDate.Name = "FinishDate";
            // 
            // LCBFinishStatusName
            // 
            this.LCBFinishStatusName.HeaderText = "完成情况";
            this.LCBFinishStatusName.Name = "FinishStatusName";
            // 
            // LCBCondition
            // 
            this.LCBCondition.HeaderText = "完成依据";
            this.LCBCondition.Name = "Condition";
            // 
            // LCBRemark
            // 
            this.LCBRemark.HeaderText = "备注";
            this.LCBRemark.Name = "Remark";
            // 
            // LCBCREATED
            // 
            this.LCBCREATED.HeaderText = "创建日期";
            this.LCBCREATED.Name = "CREATED";
            this.LCBCREATED.Visible = false;
            // 
            // LCBFinishStatus
            // 
            this.LCBFinishStatus.HeaderText = "完成情况";
            this.LCBFinishStatus.Name = "FinishStatus";
            this.LCBFinishStatus.Visible = false;
            // 
            // LCBStatus
            // 
            this.LCBStatus.Name = "Status";
            this.LCBStatus.Visible = false;
            // 
            // FormMilestone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 523);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.groupPanel5);
            this.Name = "FormMilestone";
            this.Text = "里程碑管理";
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtLCREATED)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLFinish)).EndInit();
            this.groupPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel5;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gridLCB;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnLClear;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLStatus;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtLCREATED;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtLFinish;
        private DevComponents.DotNetBar.LabelX labelX26;
        private DevComponents.DotNetBar.LabelX labelX27;
        private DevComponents.DotNetBar.LabelX labelX28;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLRemark;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLCondition;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLName;
        private DevComponents.DotNetBar.LabelX labelX29;
        private DevComponents.DotNetBar.LabelX labelX30;
        private DevComponents.DotNetBar.LabelX labelX31;

        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBNo;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBID;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBName;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBFinishDate;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBCondition;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBRemark;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBFinishStatus;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBCREATED;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBStatus;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LCBFinishStatusName;
    }
}