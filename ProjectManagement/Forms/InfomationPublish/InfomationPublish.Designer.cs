namespace ProjectManagement.Forms.InfomationPublish
{
    partial class InfomationPublish
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
            this.wbsTree = new DevComponents.AdvTree.AdvTree();
            this.node1 = new DevComponents.AdvTree.Node();
            this.node2 = new DevComponents.AdvTree.Node();
            this.node8 = new DevComponents.AdvTree.Node();
            this.node3 = new DevComponents.AdvTree.Node();
            this.node9 = new DevComponents.AdvTree.Node();
            this.node10 = new DevComponents.AdvTree.Node();
            this.node11 = new DevComponents.AdvTree.Node();
            this.node4 = new DevComponents.AdvTree.Node();
            this.node5 = new DevComponents.AdvTree.Node();
            this.node6 = new DevComponents.AdvTree.Node();
            this.node7 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.txtContent = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtTitle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCC = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtSend = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.btnAddFile = new DevComponents.DotNetBar.ButtonX();
            this.btnSend = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnAddCC = new DevComponents.DotNetBar.ButtonX();
            this.btnAddSend = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.gridFile = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            ((System.ComponentModel.ISupportInitialize)(this.wbsTree)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbsTree
            // 
            this.wbsTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.wbsTree.AllowDrop = true;
            this.wbsTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.wbsTree.BackgroundStyle.Class = "TreeBorderKey";
            this.wbsTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wbsTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.wbsTree.Location = new System.Drawing.Point(5, 1);
            this.wbsTree.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.wbsTree.Name = "wbsTree";
            this.wbsTree.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.wbsTree.NodesConnector = this.nodeConnector1;
            this.wbsTree.NodeStyle = this.elementStyle1;
            this.wbsTree.PathSeparator = ";";
            this.wbsTree.Size = new System.Drawing.Size(249, 499);
            this.wbsTree.Styles.Add(this.elementStyle1);
            this.wbsTree.TabIndex = 5;
            this.wbsTree.Text = "advTree1";
            this.wbsTree.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.wbsTree_AfterCheck);
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node2,
            this.node3,
            this.node4,
            this.node5,
            this.node6,
            this.node7});
            this.node1.Text = "徐州肉菜项目";
            // 
            // node2
            // 
            this.node2.Expanded = true;
            this.node2.Name = "node2";
            this.node2.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node8});
            this.node2.Text = "项目启动";
            // 
            // node8
            // 
            this.node8.Name = "node8";
            this.node8.Text = "项目启动会";
            // 
            // node3
            // 
            this.node3.Expanded = true;
            this.node3.Name = "node3";
            this.node3.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node9,
            this.node10,
            this.node11});
            this.node3.Text = "项目规划";
            // 
            // node9
            // 
            this.node9.Name = "node9";
            this.node9.Text = "工作分解";
            // 
            // node10
            // 
            this.node10.Name = "node10";
            this.node10.Text = "项目计划";
            // 
            // node11
            // 
            this.node11.Name = "node11";
            this.node11.Text = "风险识别";
            // 
            // node4
            // 
            this.node4.Name = "node4";
            this.node4.Text = "项目执行";
            // 
            // node5
            // 
            this.node5.Name = "node5";
            this.node5.Text = "项目收尾";
            // 
            // node6
            // 
            this.node6.Name = "node6";
            this.node6.Text = "例会";
            // 
            // node7
            // 
            this.node7.Name = "node7";
            this.node7.Text = "阶段汇报";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // gridColumn3
            // 
            this.gridColumn3.HeaderText = "删除";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn2
            // 
            this.gridColumn2.HeaderText = "附件名称";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn1
            // 
            this.gridColumn1.HeaderText = "编号";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtContent.Border.Class = "TextBoxBorder";
            this.txtContent.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtContent.DisabledBackColor = System.Drawing.Color.White;
            this.txtContent.ForeColor = System.Drawing.Color.Black;
            this.txtContent.Location = new System.Drawing.Point(83, 101);
            this.txtContent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.PreventEnterBeep = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(559, 184);
            this.txtContent.TabIndex = 3;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTitle.Border.Class = "TextBoxBorder";
            this.txtTitle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTitle.DisabledBackColor = System.Drawing.Color.White;
            this.txtTitle.ForeColor = System.Drawing.Color.Black;
            this.txtTitle.Location = new System.Drawing.Point(83, 68);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PreventEnterBeep = true;
            this.txtTitle.Size = new System.Drawing.Size(559, 21);
            this.txtTitle.TabIndex = 2;
            // 
            // txtCC
            // 
            this.txtCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCC.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCC.Border.Class = "TextBoxBorder";
            this.txtCC.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCC.DisabledBackColor = System.Drawing.Color.White;
            this.txtCC.ForeColor = System.Drawing.Color.Black;
            this.txtCC.Location = new System.Drawing.Point(83, 36);
            this.txtCC.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtCC.Name = "txtCC";
            this.txtCC.PreventEnterBeep = true;
            this.txtCC.Size = new System.Drawing.Size(488, 21);
            this.txtCC.TabIndex = 1;
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(12, 291);
            this.labelX5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(67, 19);
            this.labelX5.TabIndex = 1;
            this.labelX5.Text = "附件列表：";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 101);
            this.labelX4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(56, 19);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "内容：";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 69);
            this.labelX3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 19);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "标题：";
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSend.Border.Class = "TextBoxBorder";
            this.txtSend.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSend.DisabledBackColor = System.Drawing.Color.White;
            this.txtSend.ForeColor = System.Drawing.Color.Black;
            this.txtSend.Location = new System.Drawing.Point(83, 3);
            this.txtSend.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSend.Name = "txtSend";
            this.txtSend.PreventEnterBeep = true;
            this.txtSend.Size = new System.Drawing.Size(488, 21);
            this.txtSend.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 3);
            this.labelX1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(67, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "收件人：";
            // 
            // gridColumn4
            // 
            this.gridColumn4.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            this.gridColumn4.HeaderText = "删除";
            this.gridColumn4.Name = "Del";
            this.gridColumn4.NullString = "删除";
            // 
            // gridColumn5
            // 
            this.gridColumn5.HeaderText = "附件名称";
            this.gridColumn5.Name = "Name";
            // 
            // gridColumn6
            // 
            this.gridColumn6.HeaderText = "编号";
            this.gridColumn6.Name = "RowNo";
            // 
            // btnAddFile
            // 
            this.btnAddFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddFile.Location = new System.Drawing.Point(83, 445);
            this.btnAddFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(56, 19);
            this.btnAddFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddFile.TabIndex = 5;
            this.btnAddFile.Text = "添加附件";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSend.Location = new System.Drawing.Point(268, 445);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(56, 19);
            this.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "发送";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(360, 445);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 19);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddCC
            // 
            this.btnAddCC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddCC.Location = new System.Drawing.Point(586, 36);
            this.btnAddCC.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddCC.Name = "btnAddCC";
            this.btnAddCC.Size = new System.Drawing.Size(56, 19);
            this.btnAddCC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddCC.TabIndex = 7;
            this.btnAddCC.Text = "添加";
            this.btnAddCC.Click += new System.EventHandler(this.btnAddCC_Click);
            // 
            // btnAddSend
            // 
            this.btnAddSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddSend.Location = new System.Drawing.Point(586, 3);
            this.btnAddSend.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddSend.Name = "btnAddSend";
            this.btnAddSend.Size = new System.Drawing.Size(56, 19);
            this.btnAddSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddSend.TabIndex = 7;
            this.btnAddSend.Text = "添加";
            this.btnAddSend.Click += new System.EventHandler(this.btnAddSendTo_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(175, 445);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(56, 19);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 6;
            this.buttonX1.Text = "草稿";
            // 
            // gridFile
            // 
            this.gridFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.gridFile.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gridFile.ForeColor = System.Drawing.Color.Black;
            this.gridFile.Location = new System.Drawing.Point(83, 291);
            this.gridFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridFile.Name = "gridFile";
            // 
            // 
            // 
            this.gridFile.PrimaryGrid.AllowEdit = false;
            this.gridFile.PrimaryGrid.AutoGenerateColumns = false;
            this.gridFile.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.gridFile.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.gridFile.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.gridFile.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.gridFile.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.gridFile.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.gridFile.Size = new System.Drawing.Size(559, 148);
            this.gridFile.TabIndex = 4;
            this.gridFile.Text = "superGridControl1";
            this.gridFile.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.gridFile_CellClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Name = "ID";
            this.gridColumn7.Visible = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Name = "Path";
            this.gridColumn8.Visible = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Name = "NodeID";
            this.gridColumn9.Visible = false;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(12, 37);
            this.labelX6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(56, 19);
            this.labelX6.TabIndex = 1;
            this.labelX6.Text = "抄送：";
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnAddFile);
            this.groupPanel1.Controls.Add(this.btnSend);
            this.groupPanel1.Controls.Add(this.btnClose);
            this.groupPanel1.Controls.Add(this.btnAddCC);
            this.groupPanel1.Controls.Add(this.btnAddSend);
            this.groupPanel1.Controls.Add(this.buttonX1);
            this.groupPanel1.Controls.Add(this.gridFile);
            this.groupPanel1.Controls.Add(this.txtContent);
            this.groupPanel1.Controls.Add(this.txtTitle);
            this.groupPanel1.Controls.Add(this.txtCC);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.txtSend);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(254, 1);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(654, 499);
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
            this.groupPanel1.TabIndex = 8;
            this.groupPanel1.Text = "邮件信息";
            // 
            // InfomationPublish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 502);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.wbsTree);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "InfomationPublish";
            this.Text = "信息发布";
            ((System.ComponentModel.ISupportInitialize)(this.wbsTree)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree wbsTree;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.AdvTree.Node node8;
        private DevComponents.AdvTree.Node node3;
        private DevComponents.AdvTree.Node node9;
        private DevComponents.AdvTree.Node node10;
        private DevComponents.AdvTree.Node node11;
        private DevComponents.AdvTree.Node node4;
        private DevComponents.AdvTree.Node node5;
        private DevComponents.AdvTree.Node node6;
        private DevComponents.AdvTree.Node node7;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtContent;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTitle;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCC;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSend;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.ButtonX btnAddFile;
        private DevComponents.DotNetBar.ButtonX btnSend;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnAddCC;
        private DevComponents.DotNetBar.ButtonX btnAddSend;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gridFile;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
    }
}