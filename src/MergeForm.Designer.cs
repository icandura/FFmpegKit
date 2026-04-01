namespace FFmpegKit
{
    partial class MergeForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabVideo = new System.Windows.Forms.TabPage();
            this.tabAudio = new System.Windows.Forms.TabPage();
            this.tabSubtitle = new System.Windows.Forms.TabPage();

            this.lblVideo = new System.Windows.Forms.Label();
            this.txtVideoFile = new System.Windows.Forms.TextBox();
            this.btnBrowseVideo = new System.Windows.Forms.Button();
            this.chkDiscardOriginalAudio = new System.Windows.Forms.CheckBox();

            this.lvAudio = new System.Windows.Forms.ListView();
            this.btnAddAudio = new System.Windows.Forms.Button();
            this.btnRemoveAudio = new System.Windows.Forms.Button();
            this.btnTopAudio = new System.Windows.Forms.Button();
            this.btnUpAudio = new System.Windows.Forms.Button();
            this.btnDownAudio = new System.Windows.Forms.Button();
            this.btnBottomAudio = new System.Windows.Forms.Button();

            this.lvSubtitle = new System.Windows.Forms.ListView();
            this.btnAddSubtitle = new System.Windows.Forms.Button();
            this.btnRemoveSubtitle = new System.Windows.Forms.Button();
            this.btnTopSubtitle = new System.Windows.Forms.Button();
            this.btnUpSubtitle = new System.Windows.Forms.Button();
            this.btnDownSubtitle = new System.Windows.Forms.Button();
            this.btnBottomSubtitle = new System.Windows.Forms.Button();

            this.lblOutputFormat = new System.Windows.Forms.Label();
            this.cmbOutputFormat = new System.Windows.Forms.ComboBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.lblOutputName = new System.Windows.Forms.Label();
            this.txtOutputName = new System.Windows.Forms.TextBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.radFastMode = new System.Windows.Forms.RadioButton();
            this.radCompatibleMode = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabVideo.SuspendLayout();
            this.tabAudio.SuspendLayout();
            this.tabSubtitle.SuspendLayout();
            this.SuspendLayout();

            // TabControl
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Size = new System.Drawing.Size(760, 380);
            this.tabControl.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
        this.tabVideo, this.tabAudio, this.tabSubtitle });

            // ==================== Tab 视频 ====================
            this.tabVideo.Text = "视频轨（必选）";

            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(20, 20);
            this.lblVideo.Text = "视频文件：";

            this.txtVideoFile.Location = new System.Drawing.Point(100, 17);
            this.txtVideoFile.Size = new System.Drawing.Size(520, 30);
            this.txtVideoFile.ReadOnly = true;

            this.btnBrowseVideo.Location = new System.Drawing.Point(630, 15);
            this.btnBrowseVideo.Size = new System.Drawing.Size(100, 30);
            this.btnBrowseVideo.Text = "浏览...";

            this.chkDiscardOriginalAudio.AutoSize = true;
            this.chkDiscardOriginalAudio.Location = new System.Drawing.Point(100, 60);
            this.chkDiscardOriginalAudio.Text = "丢弃原视频自带的音频流";
            this.chkDiscardOriginalAudio.Checked = true;

            this.chkGPU = new System.Windows.Forms.CheckBox();
            this.chkGPU.AutoSize = true;
            this.chkGPU.Location = new System.Drawing.Point(100, 90);
            this.chkGPU.Text = "使用 GPU 加速";
            this.chkGPU.Checked = false;
            this.chkGPU.Enabled = false;    // 默认禁用，只有兼容模式才启用

            // 新增：时长处理选项（仅兼容模式可用）
            this.grpDuration = new System.Windows.Forms.GroupBox();
            this.grpDuration.Location = new System.Drawing.Point(20, 120);
            this.grpDuration.Size = new System.Drawing.Size(710, 140);
            this.grpDuration.Text = "时长不一致处理（仅兼容模式有效）";
            this.grpDuration.Enabled = false;   // 默认禁用，只有兼容模式才启用

            this.lblDurationBase = new System.Windows.Forms.Label();
            this.lblDurationBase.Location = new System.Drawing.Point(20, 25);
            this.lblDurationBase.Text = "最终视频时长以：";
            this.lblDurationBase.AutoSize = true;

            this.radVideoLength = new System.Windows.Forms.RadioButton();
            this.radVideoLength.Location = new System.Drawing.Point(160, 25);
            this.radVideoLength.Text = "视频轨长度为准";
            this.radVideoLength.Checked = true;
            this.radVideoLength.AutoSize = true;

            this.radLongestAudio = new System.Windows.Forms.RadioButton();
            this.radLongestAudio.Location = new System.Drawing.Point(160, 50);
            this.radLongestAudio.Text = "最长音频轨长度为准";
            this.radLongestAudio.Checked = false;
            this.radLongestAudio.AutoSize = true;

            this.lblAudioLonger = new System.Windows.Forms.Label();
            this.lblAudioLonger.Location = new System.Drawing.Point(20, 80);
            this.lblAudioLonger.Text = "当音频比视频长时：";
            this.lblAudioLonger.AutoSize = true;

            this.radPadLastFrame = new System.Windows.Forms.RadioButton();
            this.radPadLastFrame.Location = new System.Drawing.Point(160, 80);
            this.radPadLastFrame.Text = "填充最后画面帧";
            this.radPadLastFrame.Checked = false;
            this.radPadLastFrame.AutoSize = true;

            this.radPadBlackFrame = new System.Windows.Forms.RadioButton();
            this.radPadBlackFrame.Location = new System.Drawing.Point(160, 105);
            this.radPadBlackFrame.Text = "填充黑屏";
            this.radPadBlackFrame.Checked = false;
            this.radPadBlackFrame.AutoSize = true;

            // --- 创建第一组容器（时长基准） ---
            this.pnlBaseGroup = new System.Windows.Forms.Panel();
            //this.pnlBaseGroup.Location = new System.Drawing.Point(160, 20);
            //this.pnlBaseGroup.Size = new System.Drawing.Size(500, 60);
            this.pnlBaseGroup.Location = new System.Drawing.Point(20, 20);
            this.pnlBaseGroup.Size = new System.Drawing.Size(650, 60);

            this.lblDurationBase.Location = new System.Drawing.Point(0, 5);
            this.radVideoLength.Location = new System.Drawing.Point(140, 5);
            this.radLongestAudio.Location = new System.Drawing.Point(140, 30);

            this.pnlBaseGroup.Controls.Add(this.lblDurationBase);
            this.pnlBaseGroup.Controls.Add(this.radVideoLength);
            this.pnlBaseGroup.Controls.Add(this.radLongestAudio);

            // --- 创建第二组容器（填充模式） ---
            this.pnlFillOptions = new System.Windows.Forms.Panel();
            //this.pnlFillOptions.Location = new System.Drawing.Point(160, 75);
            //this.pnlFillOptions.Size = new System.Drawing.Size(500, 60);
            this.pnlFillOptions.Location = new System.Drawing.Point(20, 75);
            this.pnlFillOptions.Size = new System.Drawing.Size(650, 60);

            this.lblAudioLonger.Location = new System.Drawing.Point(0, 5);
            this.radPadLastFrame.Location = new System.Drawing.Point(140, 5);
            this.radPadBlackFrame.Location = new System.Drawing.Point(140, 30);

            this.pnlFillOptions.Controls.Add(this.lblAudioLonger);
            this.pnlFillOptions.Controls.Add(this.radPadLastFrame);
            this.pnlFillOptions.Controls.Add(this.radPadBlackFrame);

            // --- 将 Panel 放入 GroupBox ---
            //this.grpDuration.Controls.Add(this.lblDurationBase);
            //this.grpDuration.Controls.Add(this.lblAudioLonger);
            this.grpDuration.Controls.Add(this.pnlBaseGroup);
            this.grpDuration.Controls.Add(this.pnlFillOptions);


            this.tabVideo.Controls.Add(this.lblVideo);
            this.tabVideo.Controls.Add(this.txtVideoFile);
            this.tabVideo.Controls.Add(this.btnBrowseVideo);
            this.tabVideo.Controls.Add(this.chkDiscardOriginalAudio);
            this.tabVideo.Controls.Add(this.chkGPU);
            this.tabVideo.Controls.Add(this.grpDuration);

            // ==================== Tab 音频 ====================
            this.tabAudio.Text = "音频轨（可多个）";

            this.lvAudio.CheckBoxes = true;
            this.lvAudio.FullRowSelect = true;
            this.lvAudio.Location = new System.Drawing.Point(20, 20);
            this.lvAudio.Size = new System.Drawing.Size(710, 260);
            this.lvAudio.View = System.Windows.Forms.View.Details;
            this.lvAudio.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader() { Text = "文件名", Width = 300 },
                new System.Windows.Forms.ColumnHeader() { Text = "标签/描述", Width = 380 }});

            this.btnAddAudio.Location = new System.Drawing.Point(20, 290);
            this.btnAddAudio.Size = new System.Drawing.Size(100, 35);
            this.btnAddAudio.Text = "添加音频";

            this.btnRemoveAudio.Location = new System.Drawing.Point(130, 290);
            this.btnRemoveAudio.Size = new System.Drawing.Size(100, 35);
            this.btnRemoveAudio.Text = "移除勾选";

            this.btnTopAudio.Location = new System.Drawing.Point(240, 290);
            this.btnTopAudio.Size = new System.Drawing.Size(70, 35);
            this.btnTopAudio.Text = "置顶";

            this.btnUpAudio.Location = new System.Drawing.Point(320, 290);
            this.btnUpAudio.Size = new System.Drawing.Size(70, 35);
            this.btnUpAudio.Text = "上移";

            this.btnDownAudio.Location = new System.Drawing.Point(400, 290);
            this.btnDownAudio.Size = new System.Drawing.Size(70, 35);
            this.btnDownAudio.Text = "下移";

            this.btnBottomAudio.Location = new System.Drawing.Point(480, 290);
            this.btnBottomAudio.Size = new System.Drawing.Size(70, 35);
            this.btnBottomAudio.Text = "置底";

            this.tabAudio.Controls.Add(this.lvAudio);
            this.tabAudio.Controls.Add(this.btnAddAudio);
            this.tabAudio.Controls.Add(this.btnRemoveAudio);
            this.tabAudio.Controls.Add(this.btnTopAudio);
            this.tabAudio.Controls.Add(this.btnUpAudio);
            this.tabAudio.Controls.Add(this.btnDownAudio);
            this.tabAudio.Controls.Add(this.btnBottomAudio);

            // ==================== Tab 字幕 ====================
            this.tabSubtitle.Text = "字幕轨（可多个）";

            this.lvSubtitle.CheckBoxes = true;
            this.lvSubtitle.FullRowSelect = true;
            this.lvSubtitle.Location = new System.Drawing.Point(20, 20);
            this.lvSubtitle.Size = new System.Drawing.Size(710, 260);
            this.lvSubtitle.View = System.Windows.Forms.View.Details;
            this.lvSubtitle.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader() { Text = "文件名", Width = 300 },
                new System.Windows.Forms.ColumnHeader() { Text = "标签/描述", Width = 380 }});

            this.btnAddSubtitle.Location = new System.Drawing.Point(20, 290);
            this.btnAddSubtitle.Size = new System.Drawing.Size(100, 35);
            this.btnAddSubtitle.Text = "添加字幕";

            this.btnRemoveSubtitle.Location = new System.Drawing.Point(130, 290);
            this.btnRemoveSubtitle.Size = new System.Drawing.Size(100, 35);
            this.btnRemoveSubtitle.Text = "移除勾选";

            this.btnTopSubtitle.Location = new System.Drawing.Point(240, 290);
            this.btnTopSubtitle.Size = new System.Drawing.Size(70, 35);
            this.btnTopSubtitle.Text = "置顶";

            this.btnUpSubtitle.Location = new System.Drawing.Point(320, 290);
            this.btnUpSubtitle.Size = new System.Drawing.Size(70, 35);
            this.btnUpSubtitle.Text = "上移";

            this.btnDownSubtitle.Location = new System.Drawing.Point(400, 290);
            this.btnDownSubtitle.Size = new System.Drawing.Size(70, 35);
            this.btnDownSubtitle.Text = "下移";

            this.btnBottomSubtitle.Location = new System.Drawing.Point(480, 290);
            this.btnBottomSubtitle.Size = new System.Drawing.Size(70, 35);
            this.btnBottomSubtitle.Text = "置底";

            this.tabSubtitle.Controls.Add(this.lvSubtitle);
            this.tabSubtitle.Controls.Add(this.btnAddSubtitle);
            this.tabSubtitle.Controls.Add(this.btnRemoveSubtitle);
            this.tabSubtitle.Controls.Add(this.btnTopSubtitle);
            this.tabSubtitle.Controls.Add(this.btnUpSubtitle);
            this.tabSubtitle.Controls.Add(this.btnDownSubtitle);
            this.tabSubtitle.Controls.Add(this.btnBottomSubtitle);

            // ==================== 输出设置 ====================
            this.lblOutputFormat.AutoSize = true;
            this.lblOutputFormat.Location = new System.Drawing.Point(12, 400);
            this.lblOutputFormat.Text = "输出格式：";

            this.cmbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputFormat.Location = new System.Drawing.Point(120, 397);
            this.cmbOutputFormat.Size = new System.Drawing.Size(120, 30);
            this.cmbOutputFormat.Items.AddRange(new object[] { "mkv", "mp4" });
            this.cmbOutputFormat.SelectedIndex = 0;

            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(12, 440);
            this.lblOutputFolder.Text = "输出文件夹：";

            this.txtOutputFolder.Location = new System.Drawing.Point(120, 437);
            this.txtOutputFolder.Size = new System.Drawing.Size(530, 30);
            this.txtOutputFolder.ReadOnly = true;

            this.btnBrowseOutput.Location = new System.Drawing.Point(660, 435);
            this.btnBrowseOutput.Size = new System.Drawing.Size(100, 30);
            this.btnBrowseOutput.Text = "浏览...";

            this.lblOutputName.AutoSize = true;
            this.lblOutputName.Location = new System.Drawing.Point(12, 480);
            this.lblOutputName.Text = "输出文件名：";

            this.txtOutputName.Location = new System.Drawing.Point(120, 477);
            this.txtOutputName.Size = new System.Drawing.Size(530, 30);

            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(12, 520);
            this.lblMode.Text = "合并模式：";

            this.radFastMode.AutoSize = true;
            this.radFastMode.Location = new System.Drawing.Point(120, 517);
            this.radFastMode.Text = "快速模式";
            this.radFastMode.Checked = true;

            this.radCompatibleMode.AutoSize = true;
            this.radCompatibleMode.Location = new System.Drawing.Point(280, 517);
            this.radCompatibleMode.Text = "兼容模式";

            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(280, 560);
            this.btnStart.Size = new System.Drawing.Size(200, 50);
            this.btnStart.Text = "开始合并";

            this.Icon = global::FFmpegKit.Properties.Resources.LOGO_64;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 630);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblOutputFormat);
            this.Controls.Add(this.cmbOutputFormat);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.lblOutputName);
            this.Controls.Add(this.txtOutputName);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.radFastMode);
            this.Controls.Add(this.radCompatibleMode);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MergeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FFmpegKit - 视频/音频/字幕合并";

            this.tabControl.ResumeLayout(false);
            this.tabVideo.ResumeLayout(false);
            this.tabAudio.ResumeLayout(false);
            this.tabSubtitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ==================== 控件字段声明 ====================
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabVideo;
        private System.Windows.Forms.TabPage tabAudio;
        private System.Windows.Forms.TabPage tabSubtitle;

        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.TextBox txtVideoFile;
        private System.Windows.Forms.Button btnBrowseVideo;
        private System.Windows.Forms.CheckBox chkDiscardOriginalAudio;
        private System.Windows.Forms.CheckBox chkGPU;
        private System.Windows.Forms.GroupBox grpDuration;
        private System.Windows.Forms.Label lblDurationBase;
        private System.Windows.Forms.RadioButton radVideoLength;
        private System.Windows.Forms.RadioButton radLongestAudio;
        private System.Windows.Forms.Label lblAudioLonger;
        private System.Windows.Forms.RadioButton radPadLastFrame;
        private System.Windows.Forms.RadioButton radPadBlackFrame;

        private System.Windows.Forms.Panel pnlBaseGroup;        //时长基准
        private System.Windows.Forms.Panel pnlFillOptions;      //具体操作（此处后续改为填充选项）

        private System.Windows.Forms.ListView lvAudio;
        private System.Windows.Forms.Button btnAddAudio;
        private System.Windows.Forms.Button btnRemoveAudio;
        private System.Windows.Forms.Button btnTopAudio;
        private System.Windows.Forms.Button btnUpAudio;
        private System.Windows.Forms.Button btnDownAudio;
        private System.Windows.Forms.Button btnBottomAudio;

        private System.Windows.Forms.ListView lvSubtitle;
        private System.Windows.Forms.Button btnAddSubtitle;
        private System.Windows.Forms.Button btnRemoveSubtitle;
        private System.Windows.Forms.Button btnTopSubtitle;
        private System.Windows.Forms.Button btnUpSubtitle;
        private System.Windows.Forms.Button btnDownSubtitle;
        private System.Windows.Forms.Button btnBottomSubtitle;

        private System.Windows.Forms.Label lblOutputFormat;
        private System.Windows.Forms.ComboBox cmbOutputFormat;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Label lblOutputName;
        private System.Windows.Forms.TextBox txtOutputName;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.RadioButton radFastMode;
        private System.Windows.Forms.RadioButton radCompatibleMode;
        private System.Windows.Forms.Button btnStart;
    }
}