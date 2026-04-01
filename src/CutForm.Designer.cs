using System.Windows.Forms;

namespace FFmpegKit
{
    partial class CutForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.lvFiles = new System.Windows.Forms.ListView();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnTop = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnBottom = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();

            this.btnSelectOutput = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.chkGPU = new System.Windows.Forms.CheckBox();
            this.chkPrecise = new System.Windows.Forms.CheckBox();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.radStartEnd = new System.Windows.Forms.RadioButton();
            this.radTrimHeadTail = new System.Windows.Forms.RadioButton();
            this.grpTime = new System.Windows.Forms.GroupBox();
            this.lblhhmmss1 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.nudStartH = new System.Windows.Forms.NumericUpDown();
            this.nudStartM = new System.Windows.Forms.NumericUpDown();
            this.nudStartS = new System.Windows.Forms.NumericUpDown();
            this.nudStartMS = new System.Windows.Forms.NumericUpDown();
            this.lblEnd = new System.Windows.Forms.Label();
            this.nudEndH = new System.Windows.Forms.NumericUpDown();
            this.nudEndM = new System.Windows.Forms.NumericUpDown();
            this.nudEndS = new System.Windows.Forms.NumericUpDown();
            this.nudEndMS = new System.Windows.Forms.NumericUpDown();
            this.lblHead = new System.Windows.Forms.Label();
            this.nudHeadH = new System.Windows.Forms.NumericUpDown();
            this.nudHeadM = new System.Windows.Forms.NumericUpDown();
            this.nudHeadS = new System.Windows.Forms.NumericUpDown();
            this.nudHeadMS = new System.Windows.Forms.NumericUpDown();
            this.lblTail = new System.Windows.Forms.Label();
            this.nudTailH = new System.Windows.Forms.NumericUpDown();
            this.nudTailM = new System.Windows.Forms.NumericUpDown();
            this.nudTailS = new System.Windows.Forms.NumericUpDown();
            this.nudTailMS = new System.Windows.Forms.NumericUpDown();
            this.lblStartH = new System.Windows.Forms.Label();
            this.lblStartM = new System.Windows.Forms.Label();
            this.lblStartS = new System.Windows.Forms.Label();
            this.lblStartMS = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grpMode.SuspendLayout();
            this.grpTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailMS)).BeginInit();
            this.SuspendLayout();
            // 
            // lvFiles
            // 
            this.lvFiles.CheckBoxes = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader() { Text = "选中", Width = 50 },
                new System.Windows.Forms.ColumnHeader() { Text = "文件名", Width = 280 },
                new System.Windows.Forms.ColumnHeader() { Text = "路径", Width = 400 }});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.Location = new System.Drawing.Point(12, 12);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(760, 260);
            this.lvFiles.TabIndex = 0;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            // 开启拖放功能
            this.lvFiles.AllowDrop = true;
            // 允许拖动项
            this.lvFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LvFiles_ItemDrag);
            this.lvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvFiles_DragEnter);
            this.lvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvFiles_DragDrop);
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(12, 278);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(120, 35);
            this.btnAddFiles.TabIndex = 1;
            this.btnAddFiles.Text = "添加文件";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(140, 278);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 35);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "移除勾选";

            this.btnTop.Location = new System.Drawing.Point(248, 278);
            this.btnTop.Size = new System.Drawing.Size(80, 35);
            this.btnTop.Text = "置顶";

            this.btnUp.Location = new System.Drawing.Point(336, 278);
            this.btnUp.Size = new System.Drawing.Size(80, 35);
            this.btnUp.Text = "上移";

            this.btnDown.Location = new System.Drawing.Point(424, 278);
            this.btnDown.Size = new System.Drawing.Size(80, 35);
            this.btnDown.Text = "下移";

            this.btnBottom.Location = new System.Drawing.Point(512, 278);
            this.btnBottom.Size = new System.Drawing.Size(80, 35);
            this.btnBottom.Text = "置底";

            this.btnClear.Location = new System.Drawing.Point(600, 278);
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.Text = "全部清空";
            // 
            // btnSelectOutput
            // 
            this.btnSelectOutput.Location = new System.Drawing.Point(12, 320);
            this.btnSelectOutput.Name = "btnSelectOutput";
            this.btnSelectOutput.Size = new System.Drawing.Size(120, 30);
            this.btnSelectOutput.TabIndex = 3;
            this.btnSelectOutput.Text = "输出文件夹";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(140, 320);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(630, 25);
            this.txtOutputFolder.TabIndex = 4;
            // 
            // chkGPU
            // 
            this.chkGPU.AutoSize = true;
            this.chkGPU.Checked = true;
            this.chkGPU.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGPU.Location = new System.Drawing.Point(12, 360);
            this.chkGPU.Name = "chkGPU";
            this.chkGPU.Size = new System.Drawing.Size(116, 23);
            this.chkGPU.TabIndex = 5;
            this.chkGPU.Text = "使用 GPU 加速";
            // 
            // chkPrecise
            // 
            this.chkPrecise.AutoSize = true;
            this.chkPrecise.Location = new System.Drawing.Point(180, 360);
            this.chkPrecise.Name = "chkPrecise";
            this.chkPrecise.Size = new System.Drawing.Size(314, 23);
            this.chkPrecise.TabIndex = 6;
            this.chkPrecise.Text = "精确模式（毫秒级切割，会重新编码，耗时较长）";
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.radStartEnd);
            this.grpMode.Controls.Add(this.radTrimHeadTail);
            this.grpMode.Location = new System.Drawing.Point(12, 390);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(760, 60);
            this.grpMode.TabIndex = 7;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "切割方式";
            // 
            // radStartEnd
            // 
            this.radStartEnd.AutoSize = true;
            this.radStartEnd.Checked = true;
            this.radStartEnd.Location = new System.Drawing.Point(20, 25);
            this.radStartEnd.Name = "radStartEnd";
            this.radStartEnd.Size = new System.Drawing.Size(199, 23);
            this.radStartEnd.TabIndex = 0;
            this.radStartEnd.TabStop = true;
            this.radStartEnd.Text = "方式1：起始时间 → 结束时间";
            // 
            // radTrimHeadTail
            // 
            this.radTrimHeadTail.AutoSize = true;
            this.radTrimHeadTail.Location = new System.Drawing.Point(300, 25);
            this.radTrimHeadTail.Name = "radTrimHeadTail";
            this.radTrimHeadTail.Size = new System.Drawing.Size(196, 23);
            this.radTrimHeadTail.TabIndex = 1;
            this.radTrimHeadTail.Text = "方式2：去掉头部 + 去掉尾部";
            // 
            // grpTime
            // 
            this.grpTime.Controls.Add(this.lblStart);
            this.grpTime.Controls.Add(this.nudStartH);
            this.grpTime.Controls.Add(this.nudStartM);
            this.grpTime.Controls.Add(this.nudStartS);
            this.grpTime.Controls.Add(this.nudStartMS);
            this.grpTime.Controls.Add(this.lblEnd);
            this.grpTime.Controls.Add(this.nudEndH);
            this.grpTime.Controls.Add(this.nudEndM);
            this.grpTime.Controls.Add(this.nudEndS);
            this.grpTime.Controls.Add(this.nudEndMS);
            this.grpTime.Controls.Add(this.lblHead);
            this.grpTime.Controls.Add(this.nudHeadH);
            this.grpTime.Controls.Add(this.nudHeadM);
            this.grpTime.Controls.Add(this.nudHeadS);
            this.grpTime.Controls.Add(this.nudHeadMS);
            this.grpTime.Controls.Add(this.lblTail);
            this.grpTime.Controls.Add(this.nudTailH);
            this.grpTime.Controls.Add(this.nudTailM);
            this.grpTime.Controls.Add(this.nudTailS);
            this.grpTime.Controls.Add(this.nudTailMS);
            this.grpTime.Controls.Add(this.lblhhmmss1);
            this.grpTime.Controls.Add(this.label1);
            this.grpTime.Location = new System.Drawing.Point(12, 460);
            this.grpTime.Name = "grpTime";
            this.grpTime.Size = new System.Drawing.Size(760, 140);
            this.grpTime.TabIndex = 8;
            this.grpTime.TabStop = false;
            this.grpTime.Text = "时间设置";
            // 
            // lblhhmmss1
            // 
            this.lblhhmmss1.Location = new System.Drawing.Point(242, 42);
            this.lblhhmmss1.Name = "lblhhmmss1";
            this.lblhhmmss1.Size = new System.Drawing.Size(250, 25);
            this.lblhhmmss1.TabIndex = 20;
            this.lblhhmmss1.Text = "时　　　　分　　　　秒　　　　　毫秒";
            this.lblhhmmss1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStart
            // 
            this.lblStart.Location = new System.Drawing.Point(16, 42);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(180, 25);
            this.lblStart.TabIndex = 0;
            this.lblStart.Text = "起始时间（hh:mm:ss:SSS）";
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudStartH
            // 
            this.nudStartH.Location = new System.Drawing.Point(202, 42);
            this.nudStartH.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudStartH.Name = "nudStartH";
            this.nudStartH.Size = new System.Drawing.Size(40, 25);
            this.nudStartH.TabIndex = 1;
            // 
            // nudStartM
            // 
            this.nudStartM.Location = new System.Drawing.Point(268, 42);
            this.nudStartM.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudStartM.Name = "nudStartM";
            this.nudStartM.Size = new System.Drawing.Size(40, 25);
            this.nudStartM.TabIndex = 2;
            // 
            // nudStartS
            // 
            this.nudStartS.Location = new System.Drawing.Point(334, 42);
            this.nudStartS.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudStartS.Name = "nudStartS";
            this.nudStartS.Size = new System.Drawing.Size(40, 25);
            this.nudStartS.TabIndex = 3;
            // 
            // nudStartMS
            // 
            this.nudStartMS.Location = new System.Drawing.Point(400, 42);
            this.nudStartMS.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudStartMS.Name = "nudStartMS";
            this.nudStartMS.Size = new System.Drawing.Size(50, 25);
            this.nudStartMS.TabIndex = 4;
            // 
            // lblEnd
            // 
            this.lblEnd.Location = new System.Drawing.Point(16, 88);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(180, 25);
            this.lblEnd.TabIndex = 5;
            this.lblEnd.Text = "结束时间（hh:mm:ss:SSS）";
            this.lblEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudEndH
            // 
            this.nudEndH.Location = new System.Drawing.Point(202, 88);
            this.nudEndH.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudEndH.Name = "nudEndH";
            this.nudEndH.Size = new System.Drawing.Size(40, 25);
            this.nudEndH.TabIndex = 6;
            // 
            // nudEndM
            // 
            this.nudEndM.Location = new System.Drawing.Point(268, 88);
            this.nudEndM.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudEndM.Name = "nudEndM";
            this.nudEndM.Size = new System.Drawing.Size(40, 25);
            this.nudEndM.TabIndex = 7;
            // 
            // nudEndS
            // 
            this.nudEndS.Location = new System.Drawing.Point(334, 88);
            this.nudEndS.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudEndS.Name = "nudEndS";
            this.nudEndS.Size = new System.Drawing.Size(40, 25);
            this.nudEndS.TabIndex = 8;
            // 
            // nudEndMS
            // 
            this.nudEndMS.Location = new System.Drawing.Point(400, 88);
            this.nudEndMS.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudEndMS.Name = "nudEndMS";
            this.nudEndMS.Size = new System.Drawing.Size(50, 25);
            this.nudEndMS.TabIndex = 9;
            // 
            // lblHead
            // 
            this.lblHead.Location = new System.Drawing.Point(16, 42);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(180, 25);
            this.lblHead.TabIndex = 10;
            this.lblHead.Text = "去除开头（hh:mm:ss:SSS）";
            this.lblHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudHeadH
            // 
            this.nudHeadH.Location = new System.Drawing.Point(202, 42);
            this.nudHeadH.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudHeadH.Name = "nudHeadH";
            this.nudHeadH.Size = new System.Drawing.Size(40, 25);
            this.nudHeadH.TabIndex = 11;
            // 
            // nudHeadM
            // 
            this.nudHeadM.Location = new System.Drawing.Point(268, 42);
            this.nudHeadM.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudHeadM.Name = "nudHeadM";
            this.nudHeadM.Size = new System.Drawing.Size(40, 25);
            this.nudHeadM.TabIndex = 12;
            // 
            // nudHeadS
            // 
            this.nudHeadS.Location = new System.Drawing.Point(334, 42);
            this.nudHeadS.Name = "nudHeadS";
            this.nudHeadS.Size = new System.Drawing.Size(40, 25);
            this.nudHeadS.TabIndex = 13;
            // 
            // nudHeadMS
            // 
            this.nudHeadMS.Location = new System.Drawing.Point(400, 42);
            this.nudHeadMS.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudHeadMS.Name = "nudHeadMS";
            this.nudHeadMS.Size = new System.Drawing.Size(50, 25);
            this.nudHeadMS.TabIndex = 14;
            // 
            // lblTail
            // 
            this.lblTail.Location = new System.Drawing.Point(16, 88);
            this.lblTail.Name = "lblTail";
            this.lblTail.Size = new System.Drawing.Size(180, 25);
            this.lblTail.TabIndex = 15;
            this.lblTail.Text = "去除结尾（hh:mm:ss:SSS）";
            this.lblTail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudTailH
            // 
            this.nudTailH.Location = new System.Drawing.Point(202, 88);
            this.nudTailH.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudTailH.Name = "nudTailH";
            this.nudTailH.Size = new System.Drawing.Size(40, 25);
            this.nudTailH.TabIndex = 16;
            // 
            // nudTailM
            // 
            this.nudTailM.Location = new System.Drawing.Point(268, 88);
            this.nudTailM.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudTailM.Name = "nudTailM";
            this.nudTailM.Size = new System.Drawing.Size(40, 25);
            this.nudTailM.TabIndex = 17;
            // 
            // nudTailS
            // 
            this.nudTailS.Location = new System.Drawing.Point(334, 88);
            this.nudTailS.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudTailS.Name = "nudTailS";
            this.nudTailS.Size = new System.Drawing.Size(40, 25);
            this.nudTailS.TabIndex = 18;
            // 
            // nudTailMS
            // 
            this.nudTailMS.Location = new System.Drawing.Point(400, 88);
            this.nudTailMS.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudTailMS.Name = "nudTailMS";
            this.nudTailMS.Size = new System.Drawing.Size(50, 25);
            this.nudTailMS.TabIndex = 19;
            // 
            // lblStartH
            // 
            this.lblStartH.Location = new System.Drawing.Point(0, 0);
            this.lblStartH.Name = "lblStartH";
            this.lblStartH.Size = new System.Drawing.Size(100, 23);
            this.lblStartH.TabIndex = 0;
            // 
            // lblStartM
            // 
            this.lblStartM.Location = new System.Drawing.Point(0, 0);
            this.lblStartM.Name = "lblStartM";
            this.lblStartM.Size = new System.Drawing.Size(100, 23);
            this.lblStartM.TabIndex = 0;
            // 
            // lblStartS
            // 
            this.lblStartS.Location = new System.Drawing.Point(0, 0);
            this.lblStartS.Name = "lblStartS";
            this.lblStartS.Size = new System.Drawing.Size(100, 23);
            this.lblStartS.TabIndex = 0;
            // 
            // lblStartMS
            // 
            this.lblStartMS.Location = new System.Drawing.Point(0, 0);
            this.lblStartMS.Name = "lblStartMS";
            this.lblStartMS.Size = new System.Drawing.Size(100, 23);
            this.lblStartMS.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(280, 610);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(200, 50);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "开始切割";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(242, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "时　　　　分　　　　秒　　　　　毫秒";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CutForm
            // 
            this.Icon = global::FFmpegKit.Properties.Resources.LOGO_64;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 680);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnTop);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnBottom);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSelectOutput);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.chkGPU);
            this.Controls.Add(this.chkPrecise);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.grpTime);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FFmpegKit - 音视频切割";
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.grpTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudStartH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailMS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnBottom;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.CheckBox chkGPU;
        private System.Windows.Forms.CheckBox chkPrecise;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.RadioButton radStartEnd;
        private System.Windows.Forms.RadioButton radTrimHeadTail;
        private System.Windows.Forms.GroupBox grpTime;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblStartH;
        private System.Windows.Forms.NumericUpDown nudStartH;
        private System.Windows.Forms.Label lblStartM;
        private System.Windows.Forms.NumericUpDown nudStartM;
        private System.Windows.Forms.Label lblStartS;
        private System.Windows.Forms.NumericUpDown nudStartS;
        private System.Windows.Forms.Label lblStartMS;
        private System.Windows.Forms.NumericUpDown nudStartMS;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.NumericUpDown nudEndH;
        private System.Windows.Forms.NumericUpDown nudEndM;
        private System.Windows.Forms.NumericUpDown nudEndS;
        private System.Windows.Forms.NumericUpDown nudEndMS;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.NumericUpDown nudHeadH;
        private System.Windows.Forms.NumericUpDown nudHeadM;
        private System.Windows.Forms.NumericUpDown nudHeadS;
        private System.Windows.Forms.NumericUpDown nudHeadMS;
        private System.Windows.Forms.Label lblTail;
        private System.Windows.Forms.NumericUpDown nudTailH;
        private System.Windows.Forms.NumericUpDown nudTailM;
        private System.Windows.Forms.NumericUpDown nudTailS;
        private System.Windows.Forms.NumericUpDown nudTailMS;
        private System.Windows.Forms.Button btnStart;
        private Label lblhhmmss1;
        private Label label1;
    }
}