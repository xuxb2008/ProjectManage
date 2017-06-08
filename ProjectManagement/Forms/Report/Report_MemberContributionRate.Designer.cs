namespace ProjectManagement.Forms.Report
{
    partial class Report_MemberContributionRate
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.groupPanel1.Size = new System.Drawing.Size(803, 408);
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
            this.groupPanel1.TabIndex = 7;
            this.groupPanel1.Text = "成员贡献率";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(797, 384);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // superGridControl1
            // 
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
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.ColumnHeader.Visible = false;
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.superGridControl1.PrimaryGrid.EnsureVisibleAfterGrouping = true;
            this.superGridControl1.PrimaryGrid.GridLines = DevComponents.DotNetBar.SuperGrid.GridLines.None;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.PrimaryGrid.ShowColumnHeader = false;
            this.superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            this.superGridControl1.Size = new System.Drawing.Size(791, 348);
            this.superGridControl1.TabIndex = 9;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.DataPropertyName = "RowNo";
            this.gridColumn1.HeaderText = "编号";
            this.gridColumn1.Name = "RowNo";
            // 
            // gridColumn2
            // 
            this.gridColumn2.DataPropertyName = "Source";
            this.gridColumn2.HeaderText = "来源";
            this.gridColumn2.Name = "Source";
            // 
            // gridColumn3
            // 
            this.gridColumn3.DataPropertyName = "name";
            this.gridColumn3.HeaderText = "名称";
            this.gridColumn3.Name = "Name";
            // 
            // gridColumn4
            // 
            this.gridColumn4.DataPropertyName = "desc";
            this.gridColumn4.HeaderText = "描述";
            this.gridColumn4.Name = "Desc";
            // 
            // gridColumn5
            // 
            this.gridColumn5.DataPropertyName = "startedate";
            this.gridColumn5.HeaderText = "开始时间";
            this.gridColumn5.Name = "StarteDate";
            // 
            // gridColumn6
            // 
            this.gridColumn6.DataPropertyName = "enddate";
            this.gridColumn6.HeaderText = "结束时间";
            this.gridColumn6.Name = "EndDate";
            // 
            // gridColumn7
            // 
            this.gridColumn7.DataPropertyName = "workload";
            this.gridColumn7.HeaderText = "工作量";
            this.gridColumn7.Name = "Workload";
            // 
            // gridColumn8
            // 
            this.gridColumn8.DataPropertyName = "zhanbi";
            this.gridColumn8.HeaderText = "占比";
            this.gridColumn8.Name = "Zhanbi";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(739, 2);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(56, 26);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "导出";
            // 
            // Report_MemberContributionRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 411);
            this.Controls.Add(this.groupPanel1);
            this.Name = "Report_MemberContributionRate";
            this.Text = "成员贡献率";
            this.groupPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
    }
}