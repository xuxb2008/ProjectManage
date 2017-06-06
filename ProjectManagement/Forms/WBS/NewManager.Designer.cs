namespace ProjectManagement.Forms.WBS
{
    partial class NewManager
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
            this.Btn_OK = new DevComponents.DotNetBar.ButtonX();
            this.Btn_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbManager = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.intWorkload = new DevComponents.Editors.IntegerInput();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.intActualWorkload = new DevComponents.Editors.IntegerInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intWorkload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intActualWorkload)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_OK
            // 
            this.Btn_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Btn_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Btn_OK.Location = new System.Drawing.Point(407, 383);
            this.Btn_OK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(56, 17);
            this.Btn_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Btn_OK.TabIndex = 0;
            this.Btn_OK.Text = "确定";
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Btn_Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.Location = new System.Drawing.Point(491, 383);
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(56, 17);
            this.Btn_Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Btn_Cancel.TabIndex = 1;
            this.Btn_Cancel.Text = "取消";
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.intActualWorkload);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.cbManager);
            this.groupPanel1.Controls.Add(this.intWorkload);
            this.groupPanel1.Controls.Add(this.btnCancel);
            this.groupPanel1.Controls.Add(this.btnSave);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel1.Location = new System.Drawing.Point(12, 7);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(532, 314);
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
            this.groupPanel1.Text = "工作量设置";
            // 
            // cbManager
            // 
            this.cbManager.DisplayMember = "Text";
            this.cbManager.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbManager.FormattingEnabled = true;
            this.cbManager.ItemHeight = 15;
            this.cbManager.Location = new System.Drawing.Point(200, 59);
            this.cbManager.Margin = new System.Windows.Forms.Padding(2);
            this.cbManager.Name = "cbManager";
            this.cbManager.Size = new System.Drawing.Size(184, 21);
            this.cbManager.TabIndex = 13;
            // 
            // intWorkload
            // 
            // 
            // 
            // 
            this.intWorkload.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intWorkload.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intWorkload.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intWorkload.Location = new System.Drawing.Point(200, 97);
            this.intWorkload.Margin = new System.Windows.Forms.Padding(2);
            this.intWorkload.MinValue = 0;
            this.intWorkload.Name = "intWorkload";
            this.intWorkload.ShowUpDown = true;
            this.intWorkload.Size = new System.Drawing.Size(184, 21);
            this.intWorkload.TabIndex = 2;
            this.intWorkload.Value = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(272, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(142, 211);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(115, 101);
            this.labelX2.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(102, 17);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "预计工作量：";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(115, 63);
            this.labelX1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(102, 17);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "责任人：";
            // 
            // intActualWorkload
            // 
            // 
            // 
            // 
            this.intActualWorkload.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intActualWorkload.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intActualWorkload.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intActualWorkload.Location = new System.Drawing.Point(200, 138);
            this.intActualWorkload.Margin = new System.Windows.Forms.Padding(2);
            this.intActualWorkload.MinValue = 0;
            this.intActualWorkload.Name = "intActualWorkload";
            this.intActualWorkload.ShowUpDown = true;
            this.intActualWorkload.Size = new System.Drawing.Size(184, 21);
            this.intActualWorkload.TabIndex = 15;
            this.intActualWorkload.Value = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(115, 142);
            this.labelX3.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(102, 17);
            this.labelX3.TabIndex = 14;
            this.labelX3.Text = "实际工作量：";
            // 
            // NewManager
            // 
            this.AcceptButton = this.Btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Btn_Cancel;
            this.ClientSize = new System.Drawing.Size(555, 341);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "NewManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "创建新项目";
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intWorkload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intActualWorkload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX Btn_OK;
        private DevComponents.DotNetBar.ButtonX Btn_Cancel;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.Editors.IntegerInput intWorkload;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbManager;
        private DevComponents.Editors.IntegerInput intActualWorkload;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}