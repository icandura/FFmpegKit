namespace FFmpegKit
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblFunctions = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnSpeed = new System.Windows.Forms.Button();
            this.btnImagesToVideo = new System.Windows.Forms.Button();
            this.btnConcat = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnDemux = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lblNvenc = new System.Windows.Forms.Label();
            this.lblAmf = new System.Windows.Forms.Label();
            this.lblQsv = new System.Windows.Forms.Label();
            this.lblGpuTitle = new System.Windows.Forms.Label();
            this.lblVerVal = new System.Windows.Forms.Label();
            this.lblVerTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblFFmpegLabel = new System.Windows.Forms.Label();
            this.lblFFmpegPath = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlLeft.Controls.Add(this.lblFunctions);
            this.pnlLeft.Controls.Add(this.btnConvert);
            this.pnlLeft.Controls.Add(this.btnCut);
            this.pnlLeft.Controls.Add(this.btnSpeed);
            this.pnlLeft.Controls.Add(this.btnImagesToVideo);
            this.pnlLeft.Controls.Add(this.btnConcat);
            this.pnlLeft.Controls.Add(this.btnMerge);
            this.pnlLeft.Controls.Add(this.btnDemux);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(220, 429);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblFunctions
            // 
            this.lblFunctions.AutoSize = true;
            this.lblFunctions.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFunctions.Location = new System.Drawing.Point(20, 20);
            this.lblFunctions.Name = "lblFunctions";
            this.lblFunctions.Size = new System.Drawing.Size(74, 22);
            this.lblFunctions.TabIndex = 0;
            this.lblFunctions.Text = "功能列表";
            // 
            // btnConvert
            // 
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnConvert.Location = new System.Drawing.Point(20, 60);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(180, 40);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Tag = 1;
            this.btnConvert.Text = "音视频格式转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.FunctionButton_Click);
            // 
            // btnCut
            // 
            this.btnCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCut.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCut.Location = new System.Drawing.Point(20, 110);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(180, 40);
            this.btnCut.TabIndex = 2;
            this.btnCut.Tag = 2;
            this.btnCut.Text = "音视频切割";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.FunctionButton_Click);
            // 
            // btnSpeed
            // 
            this.btnSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpeed.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSpeed.Location = new System.Drawing.Point(20, 160);
            this.btnSpeed.Name = "btnSpeed";
            this.btnSpeed.Size = new System.Drawing.Size(180, 40);
            this.btnSpeed.TabIndex = 3;
            this.btnSpeed.Tag = 3;
            this.btnSpeed.Text = "音视频倍速处理";
            this.btnSpeed.UseVisualStyleBackColor = true;
            this.btnSpeed.Click += new System.EventHandler(this.FunctionButton_Click);
            // 
            // btnImagesToVideo
            // 
            this.btnImagesToVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImagesToVideo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnImagesToVideo.Location = new System.Drawing.Point(20, 210);
            this.btnImagesToVideo.Name = "btnImagesToVideo";
            this.btnImagesToVideo.Size = new System.Drawing.Size(180, 40);
            this.btnImagesToVideo.TabIndex = 4;
            this.btnImagesToVideo.Tag = 4;
            this.btnImagesToVideo.Text = "图片合成视频";
            this.btnImagesToVideo.UseVisualStyleBackColor = true;
            this.btnImagesToVideo.Click += new System.EventHandler(this.FunctionButton_Click);
            // 
            // btnConcat
            // 
            this.btnConcat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConcat.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnConcat.Location = new System.Drawing.Point(20, 260);
            this.btnConcat.Name = "btnConcat";
            this.btnConcat.Size = new System.Drawing.Size(180, 40);
            this.btnConcat.TabIndex = 5;
            this.btnConcat.Tag = 5;
            this.btnConcat.Text = "音视频串联";
            this.btnConcat.UseVisualStyleBackColor = true;
            this.btnConcat.Click += new System.EventHandler(this.FunctionButton_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMerge.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnMerge.Location = new System.Drawing.Point(20, 310);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(180, 40);
            this.btnMerge.TabIndex = 6;
            this.btnMerge.Tag = 6;
            this.btnMerge.Text = "视频/音频/字幕合并";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.FunctionButton_Click);
            // 
            // btnDemux
            // 
            this.btnDemux.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemux.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnDemux.Location = new System.Drawing.Point(20, 360);
            this.btnDemux.Name = "btnDemux";
            this.btnDemux.Size = new System.Drawing.Size(180, 40);
            this.btnDemux.TabIndex = 7;
            this.btnDemux.Tag = 7;
            this.btnDemux.Text = "视频/音频/字幕分离";
            this.btnDemux.UseVisualStyleBackColor = true;
            this.btnDemux.Click += new System.EventHandler(this.FunctionButton_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.label1);
            this.pnlRight.Controls.Add(this.grpStatus);
            this.pnlRight.Controls.Add(this.lblWelcome);
            this.pnlRight.Controls.Add(this.lblFFmpegLabel);
            this.pnlRight.Controls.Add(this.lblFFmpegPath);
            this.pnlRight.Controls.Add(this.btnSettings);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(220, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(560, 429);
            this.pnlRight.TabIndex = 1;
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.lblNvenc);
            this.grpStatus.Controls.Add(this.lblAmf);
            this.grpStatus.Controls.Add(this.lblQsv);
            this.grpStatus.Controls.Add(this.lblGpuTitle);
            this.grpStatus.Controls.Add(this.lblVerVal);
            this.grpStatus.Controls.Add(this.lblVerTitle);
            this.grpStatus.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.grpStatus.Location = new System.Drawing.Point(40, 100);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(480, 150);
            this.grpStatus.TabIndex = 4;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "FFmpeg 运行环境";
            // 
            // lblNvenc
            // 
            this.lblNvenc.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNvenc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblNvenc.ForeColor = System.Drawing.Color.Gray;
            this.lblNvenc.Location = new System.Drawing.Point(100, 85);
            this.lblNvenc.Name = "lblNvenc";
            this.lblNvenc.Size = new System.Drawing.Size(70, 25);
            this.lblNvenc.TabIndex = 5;
            this.lblNvenc.Text = "NVIDIA";
            this.lblNvenc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAmf
            // 
            this.lblAmf.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAmf.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblAmf.ForeColor = System.Drawing.Color.Gray;
            this.lblAmf.Location = new System.Drawing.Point(180, 85);
            this.lblAmf.Name = "lblAmf";
            this.lblAmf.Size = new System.Drawing.Size(70, 25);
            this.lblAmf.TabIndex = 4;
            this.lblAmf.Text = "AMD";
            this.lblAmf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQsv
            // 
            this.lblQsv.BackColor = System.Drawing.Color.Gainsboro;
            this.lblQsv.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblQsv.ForeColor = System.Drawing.Color.Gray;
            this.lblQsv.Location = new System.Drawing.Point(260, 85);
            this.lblQsv.Name = "lblQsv";
            this.lblQsv.Size = new System.Drawing.Size(70, 25);
            this.lblQsv.TabIndex = 3;
            this.lblQsv.Text = "Intel";
            this.lblQsv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGpuTitle
            // 
            this.lblGpuTitle.AutoSize = true;
            this.lblGpuTitle.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblGpuTitle.Location = new System.Drawing.Point(20, 90);
            this.lblGpuTitle.Name = "lblGpuTitle";
            this.lblGpuTitle.Size = new System.Drawing.Size(68, 17);
            this.lblGpuTitle.TabIndex = 2;
            this.lblGpuTitle.Text = "硬件加速：";
            // 
            // lblVerVal
            // 
            this.lblVerVal.AutoSize = true;
            this.lblVerVal.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblVerVal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblVerVal.Location = new System.Drawing.Point(100, 45);
            this.lblVerVal.Name = "lblVerVal";
            this.lblVerVal.Size = new System.Drawing.Size(56, 17);
            this.lblVerVal.TabIndex = 1;
            this.lblVerVal.Text = "未就绪";
            // 
            // lblVerTitle
            // 
            this.lblVerTitle.AutoSize = true;
            this.lblVerTitle.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblVerTitle.Location = new System.Drawing.Point(20, 45);
            this.lblVerTitle.Name = "lblVerTitle";
            this.lblVerTitle.Size = new System.Drawing.Size(68, 17);
            this.lblVerTitle.TabIndex = 0;
            this.lblVerTitle.Text = "内核版本：";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWelcome.Location = new System.Drawing.Point(40, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(247, 31);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "欢迎使用 FFmpegKit";
            // 
            // lblFFmpegLabel
            // 
            this.lblFFmpegLabel.AutoSize = true;
            this.lblFFmpegLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblFFmpegLabel.Location = new System.Drawing.Point(40, 280);
            this.lblFFmpegLabel.Name = "lblFFmpegLabel";
            this.lblFFmpegLabel.Size = new System.Drawing.Size(140, 20);
            this.lblFFmpegLabel.TabIndex = 1;
            this.lblFFmpegLabel.Text = "当前 FFmpeg 路径：";
            // 
            // lblFFmpegPath
            // 
            this.lblFFmpegPath.AutoSize = true;
            this.lblFFmpegPath.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblFFmpegPath.ForeColor = System.Drawing.Color.DimGray;
            this.lblFFmpegPath.Location = new System.Drawing.Point(40, 310);
            this.lblFFmpegPath.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblFFmpegPath.Name = "lblFFmpegPath";
            this.lblFFmpegPath.Size = new System.Drawing.Size(0, 14);
            this.lblFFmpegPath.TabIndex = 2;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSettings.Location = new System.Drawing.Point(40, 360);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(140, 40);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "打开设置";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 429);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(32, 17);
            this.tslStatus.Text = "就绪";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(377, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Made with ♥ by Candura";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 451);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::FFmpegKit.Properties.Resources.LOGO_64;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFmpegKit v1.0 - by Candura";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblFunctions;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Button btnSpeed;
        private System.Windows.Forms.Button btnImagesToVideo;
        private System.Windows.Forms.Button btnConcat;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Button btnDemux;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblFFmpegLabel;
        private System.Windows.Forms.Label lblFFmpegPath;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;

        // 新增的看板控件
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblVerVal;
        private System.Windows.Forms.Label lblVerTitle;
        private System.Windows.Forms.Label lblNvenc;
        private System.Windows.Forms.Label lblAmf;
        private System.Windows.Forms.Label lblQsv;
        private System.Windows.Forms.Label lblGpuTitle;
        private System.Windows.Forms.Label label1;
    }
}