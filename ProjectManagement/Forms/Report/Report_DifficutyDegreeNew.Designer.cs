namespace ProjectManagement.Forms.Report
{
    partial class Report_DifficutyDegreeNew
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
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(683, 2);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(61, 26);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 346);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // superGridControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.superGridControl1, 2);
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Location = new System.Drawing.Point(3, 33);
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
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn11);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn12);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn13);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn14);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn15);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn16);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn17);
            this.superGridControl1.PrimaryGrid.EnsureVisibleAfterGrouping = true;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(775, 310);
            this.superGridControl1.TabIndex = 9;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            // 
            // gridColumn7
            // 
            this.gridColumn7.DataPropertyName = "RowNo";
            this.gridColumn7.HeaderText = "编号";
            this.gridColumn7.Name = "rowno";
            // 
            // gridColumn8
            // 
            this.gridColumn8.DataPropertyName = "source";
            this.gridColumn8.HeaderText = "来源";
            this.gridColumn8.Name = "source";
            // 
            // gridColumn11
            // 
            this.gridColumn11.DataPropertyName = "name";
            this.gridColumn11.HeaderText = "名称";
            this.gridColumn11.Name = "name";
            // 
            // gridColumn12
            // 
            this.gridColumn12.DataPropertyName = "desc";
            this.gridColumn12.HeaderText = "描述";
            this.gridColumn12.Name = "desc";
            // 
            // gridColumn13
            // 
            this.gridColumn13.DataPropertyName = "startdate";
            this.gridColumn13.HeaderText = "开始时间";
            this.gridColumn13.Name = "startdate";
            // 
            // gridColumn14
            // 
            this.gridColumn14.DataPropertyName = "enddate";
            this.gridColumn14.HeaderText = "结束时间";
            this.gridColumn14.Name = "enddate";
            // 
            // gridColumn15
            // 
            this.gridColumn15.DataPropertyName = "workload";
            this.gridColumn15.HeaderText = "工作量";
            this.gridColumn15.Name = "workload";
            // 
            // gridColumn16
            // 
            this.gridColumn16.DataPropertyName = "actualworkload";
            this.gridColumn16.HeaderText = "实际工作量";
            this.gridColumn16.Name = "actualworkload";
            // 
            // gridColumn17
            // 
            this.gridColumn17.DataPropertyName = "degree";
            this.gridColumn17.HeaderText = "难度系数";
            this.gridColumn17.Name = "degree";
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
            this.groupPanel1.Text = "工作难度系数";
            // 
            // Report_DifficutyDegreeNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 373);
            this.Controls.Add(this.groupPanel1);
            this.Name = "Report_DifficutyDegreeNew";
            this.Text = "工作困难系数";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17;
    }
}