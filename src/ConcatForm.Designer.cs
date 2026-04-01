using System.Windows.Forms;

namespace FFmpegKit
{
    partial class ConcatForm
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

            this.radVideo = new System.Windows.Forms.RadioButton();
            this.radAudio = new System.Windows.Forms.RadioButton();

            this.lblHeight = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.lblAspect = new System.Windows.Forms.Label();
            this.cmbAspect = new System.Windows.Forms.ComboBox();
            this.lblWidthAuto = new System.Windows.Forms.Label();
            this.lblFPS = new System.Windows.Forms.Label();
            this.cmbFPS = new System.Windows.Forms.ComboBox();

            this.chkGPU = new System.Windows.Forms.CheckBox();

            this.chkFastMode = new System.Windows.Forms.CheckBox();
            this.lblFastModeNote = new System.Windows.Forms.Label();

            this.btnSelectOutput = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();

            this.btnStart = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 文件列表
            this.lvFiles.CheckBoxes = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader() { Text = "选中", Width = 50 },
                new System.Windows.Forms.ColumnHeader() { Text = "文件名", Width = 280 },
                new System.Windows.Forms.ColumnHeader() { Text = "路径", Width = 400 }});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.Location = new System.Drawing.Point(12, 12);
            this.lvFiles.Size = new System.Drawing.Size(760, 260);
            this.lvFiles.View = System.Windows.Forms.View.Details;
            // 开启拖放功能
            this.lvFiles.AllowDrop = true;
            // 允许拖动项
            this.lvFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LvFiles_ItemDrag);
            this.lvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvFiles_DragEnter);
            this.lvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvFiles_DragDrop);

            // 操作按钮
            this.btnAddFiles.Location = new System.Drawing.Point(12, 280);
            this.btnAddFiles.Size = new System.Drawing.Size(120, 35);
            this.btnAddFiles.Text = "添加文件";

            this.btnRemove.Location = new System.Drawing.Point(140, 280);
            this.btnRemove.Size = new System.Drawing.Size(100, 35);
            this.btnRemove.Text = "移除勾选";

            this.btnTop.Location = new System.Drawing.Point(248, 280);
            this.btnTop.Size = new System.Drawing.Size(80, 35);
            this.btnTop.Text = "置顶";

            this.btnUp.Location = new System.Drawing.Point(336, 280);
            this.btnUp.Size = new System.Drawing.Size(80, 35);
            this.btnUp.Text = "上移";

            this.btnDown.Location = new System.Drawing.Point(424, 280);
            this.btnDown.Size = new System.Drawing.Size(80, 35);
            this.btnDown.Text = "下移";

            this.btnBottom.Location = new System.Drawing.Point(512, 280);
            this.btnBottom.Size = new System.Drawing.Size(80, 35);
            this.btnBottom.Text = "置底";

            this.btnClear.Location = new System.Drawing.Point(600, 280);
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.Text = "全部清空";

            // 输出类型
            this.radVideo.AutoSize = true;
            this.radVideo.Location = new System.Drawing.Point(12, 325);
            this.radVideo.Text = "串联成视频";
            this.radVideo.Checked = true;

            this.radAudio.AutoSize = true;
            this.radAudio.Location = new System.Drawing.Point(180, 325);
            this.radAudio.Text = "串联成音频";

            // 参数设置（仅视频模式使用）
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(12, 360);
            this.lblHeight.Text = "输出高度：";

            this.nudHeight.Location = new System.Drawing.Point(120, 357);
            this.nudHeight.Size = new System.Drawing.Size(100, 30);
            this.nudHeight.Minimum = 360;
            this.nudHeight.Maximum = 4320;
            this.nudHeight.Value = 1080;

            this.lblAspect.AutoSize = true;
            this.lblAspect.Location = new System.Drawing.Point(12, 400);
            this.lblAspect.Text = "比例：";

            this.cmbAspect.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAspect.Location = new System.Drawing.Point(120, 397);
            this.cmbAspect.Size = new System.Drawing.Size(120, 30);
            this.cmbAspect.Items.AddRange(new object[] { "16:9", "9:16", "4:3", "1:1", "21:9" });
            this.cmbAspect.SelectedIndex = 0;

            this.lblWidthAuto.AutoSize = true;
            this.lblWidthAuto.Location = new System.Drawing.Point(260, 400);
            this.lblWidthAuto.Text = "自动宽度：计算中...";

            this.lblFPS.AutoSize = true;
            this.lblFPS.Location = new System.Drawing.Point(12, 440);
            this.lblFPS.Text = "帧率：";

            this.cmbFPS.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFPS.Location = new System.Drawing.Point(120, 437);
            this.cmbFPS.Size = new System.Drawing.Size(120, 30);
            this.cmbFPS.Items.AddRange(new object[] { "24 fps", "25 fps", "30 fps", "60 fps" });
            this.cmbFPS.SelectedIndex = 2;

            this.chkGPU.AutoSize = true;
            this.chkGPU.Location = new System.Drawing.Point(12, 480);
            this.chkGPU.Text = "使用 GPU 加速";

            this.chkFastMode.AutoSize = true;
            this.chkFastMode.Location = new System.Drawing.Point(180, 480); 
            this.chkFastMode.Text = "快速模式 (仅无损合并，不重编码)";

           
            this.lblFastModeNote.AutoSize = true;
            this.lblFastModeNote.Location = new System.Drawing.Point(420, 483);
            this.lblFastModeNote.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.lblFastModeNote.ForeColor = System.Drawing.Color.Gray;
            this.lblFastModeNote.Text = "*要求素材分辨率/格式一致，否则可能合并失败或播放异常。";

            // 输出文件夹
            this.btnSelectOutput.Location = new System.Drawing.Point(12, 520);
            this.btnSelectOutput.Size = new System.Drawing.Size(120, 30);
            this.btnSelectOutput.Text = "输出文件夹";

            this.txtOutputFolder.Location = new System.Drawing.Point(140, 520);
            this.txtOutputFolder.Size = new System.Drawing.Size(630, 30);
            this.txtOutputFolder.ReadOnly = true;

            // 开始按钮
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(280, 570);
            this.btnStart.Size = new System.Drawing.Size(200, 50);
            this.btnStart.Text = "开始串联";

            this.Icon = global::FFmpegKit.Properties.Resources.LOGO_64;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 640);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnTop);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnBottom);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.radVideo);
            this.Controls.Add(this.radAudio);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.lblAspect);
            this.Controls.Add(this.cmbAspect);
            this.Controls.Add(this.lblWidthAuto);
            this.Controls.Add(this.lblFPS);
            this.Controls.Add(this.cmbFPS);
            this.Controls.Add(this.chkGPU);
            this.Controls.Add(this.chkFastMode);
            this.Controls.Add(this.lblFastModeNote);
            this.Controls.Add(this.btnSelectOutput);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConcatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FFmpegKit - 音视频串联";
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
        private System.Windows.Forms.RadioButton radVideo;
        private System.Windows.Forms.RadioButton radAudio;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Label lblAspect;
        private System.Windows.Forms.ComboBox cmbAspect;
        private System.Windows.Forms.Label lblWidthAuto;
        private System.Windows.Forms.Label lblFPS;
        private System.Windows.Forms.ComboBox cmbFPS;
        private System.Windows.Forms.CheckBox chkGPU;
        private System.Windows.Forms.CheckBox chkFastMode;
        private System.Windows.Forms.Label lblFastModeNote;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnStart;
    }
}