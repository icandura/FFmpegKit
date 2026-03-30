using System.Windows.Forms;

namespace FFmpegKit
{
    partial class ImageToVideoForm
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
            this.lvImages = new System.Windows.Forms.ListView();
            this.btnAddImages = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnTop = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnBottom = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblDuration = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.lblFPS = new System.Windows.Forms.Label();
            this.cmbFPS = new System.Windows.Forms.ComboBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.lblAspect = new System.Windows.Forms.Label();
            this.cmbAspect = new System.Windows.Forms.ComboBox();
            this.lblWidthAuto = new System.Windows.Forms.Label();
            this.lblEstimated = new System.Windows.Forms.Label();
            this.btnSelectOutput = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.lblPaddingColor = new System.Windows.Forms.Label();
            this.cmbPaddingColor = new System.Windows.Forms.ComboBox();
            this.chkGPU = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // lvImages
            // 
            this.lvImages.CheckBoxes = true;
            this.lvImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader() { Text = "选中", Width = 50 },
                new System.Windows.Forms.ColumnHeader() { Text = "文件名", Width = 280 },
                new System.Windows.Forms.ColumnHeader() { Text = "原始尺寸", Width = 120 },
                new System.Windows.Forms.ColumnHeader() { Text = "路径", Width = 300 }});
            this.lvImages.FullRowSelect = true;
            this.lvImages.HideSelection = false;
            this.lvImages.Location = new System.Drawing.Point(12, 12);
            this.lvImages.Name = "lvImages";
            this.lvImages.Size = new System.Drawing.Size(760, 280);
            this.lvImages.TabIndex = 0;
            this.lvImages.UseCompatibleStateImageBehavior = false;
            this.lvImages.View = System.Windows.Forms.View.Details;
            // 开启拖放功能
            this.lvImages.AllowDrop = true;
            // 允许拖动项
            this.lvImages.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvImages_ItemDrag);
            this.lvImages.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvImages_DragEnter);
            this.lvImages.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvImages_DragDrop);
            // 
            // btnAddImages
            // 
            this.btnAddImages.Location = new System.Drawing.Point(12, 298);
            this.btnAddImages.Name = "btnAddImages";
            this.btnAddImages.Size = new System.Drawing.Size(120, 35);
            this.btnAddImages.TabIndex = 1;
            this.btnAddImages.Text = "添加图片";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(140, 298);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 35);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "移除勾选";
            // 
            // btnTop
            // 
            this.btnTop.Location = new System.Drawing.Point(248, 298);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(80, 35);
            this.btnTop.TabIndex = 3;
            this.btnTop.Text = "置顶";
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(336, 298);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(80, 35);
            this.btnUp.TabIndex = 4;
            this.btnUp.Text = "上移";
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(424, 298);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(80, 35);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "下移";
            // 
            // btnBottom
            // 
            this.btnBottom.Location = new System.Drawing.Point(512, 298);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(80, 35);
            this.btnBottom.TabIndex = 6;
            this.btnBottom.Text = "置底";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(600, 298);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "全部清空";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(12, 380);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(107, 20);
            this.lblDuration.TabIndex = 10;
            this.lblDuration.Text = "每张图片时长：";
            // 
            // nudDuration
            // 
            this.nudDuration.Location = new System.Drawing.Point(140, 377);
            this.nudDuration.Size = new System.Drawing.Size(100, 30);
            this.nudDuration.DecimalPlaces = 1;
            this.nudDuration.Increment = 0.5M;
            this.nudDuration.Minimum = 0.1M;
            this.nudDuration.Maximum = 60M;
            this.nudDuration.Value = 5.0M;
            // 
            // lblFPS
            // 
            this.lblFPS.AutoSize = true;
            this.lblFPS.Location = new System.Drawing.Point(12, 420);
            this.lblFPS.Name = "lblFPS";
            this.lblFPS.Size = new System.Drawing.Size(51, 20);
            this.lblFPS.TabIndex = 12;
            this.lblFPS.Text = "帧率：";
            // 
            // cmbFPS
            // 
            this.cmbFPS.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFPS.Location = new System.Drawing.Point(140, 417);
            this.cmbFPS.Size = new System.Drawing.Size(120, 30);
            this.cmbFPS.Items.AddRange(new object[] { "24 fps", "25 fps", "30 fps", "50 fps", "60 fps" });
            this.cmbFPS.SelectedIndex = 2;  // 默认 30 fps
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(12, 460);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(79, 20);
            this.lblHeight.TabIndex = 14;
            this.lblHeight.Text = "输出高度：";
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(140, 457);
            this.nudHeight.Size = new System.Drawing.Size(100, 30);
            this.nudHeight.Minimum = 360;
            this.nudHeight.Maximum = 4320;
            this.nudHeight.Value = 1080;
            // 
            // lblAspect
            // 
            this.lblAspect.AutoSize = true;
            this.lblAspect.Location = new System.Drawing.Point(12, 500);
            this.lblAspect.Name = "lblAspect";
            this.lblAspect.Size = new System.Drawing.Size(51, 20);
            this.lblAspect.TabIndex = 16;
            this.lblAspect.Text = "比例：";
            // 
            // cmbAspect
            // 
            this.cmbAspect.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAspect.Location = new System.Drawing.Point(140, 497);
            this.cmbAspect.Size = new System.Drawing.Size(120, 30);
            this.cmbAspect.Items.AddRange(new object[] { "16:9", "4:3", "1:1", "9:16", "21:9" });
            this.cmbAspect.SelectedIndex = 0;  // 默认 16:9
            // 
            // lblWidthAuto
            // 
            this.lblWidthAuto.AutoSize = true;
            this.lblWidthAuto.Location = new System.Drawing.Point(340, 500);
            this.lblWidthAuto.Name = "lblWidthAuto";
            this.lblWidthAuto.Size = new System.Drawing.Size(130, 20);
            this.lblWidthAuto.TabIndex = 18;
            this.lblWidthAuto.Text = "自动宽度：计算中...";
            // 
            // lblEstimated
            // 
            this.lblEstimated.AutoSize = true;
            this.lblEstimated.Location = new System.Drawing.Point(340, 380);
            this.lblEstimated.Name = "lblEstimated";
            this.lblEstimated.Size = new System.Drawing.Size(207, 20);
            this.lblEstimated.TabIndex = 19;
            this.lblEstimated.Text = "预计视频时长：0秒（0张图片）";
            // 
            // btnSelectOutput
            // 
            this.btnSelectOutput.Location = new System.Drawing.Point(12, 340);
            this.btnSelectOutput.Name = "btnSelectOutput";
            this.btnSelectOutput.Size = new System.Drawing.Size(120, 30);
            this.btnSelectOutput.TabIndex = 8;
            this.btnSelectOutput.Text = "输出文件夹";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(140, 340);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(630, 25);
            this.txtOutputFolder.TabIndex = 9;
            // 
            // lblPaddingColor
            // 
            this.lblPaddingColor.AutoSize = true;
            this.lblPaddingColor.Location = new System.Drawing.Point(340, 420);
            this.lblPaddingColor.Name = "lblPaddingColor";
            this.lblPaddingColor.Size = new System.Drawing.Size(79, 20);
            this.lblPaddingColor.TabIndex = 20;
            this.lblPaddingColor.Text = "填充颜色：";
            // 
            // cmbPaddingColor
            // 
            this.cmbPaddingColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaddingColor.Location = new System.Drawing.Point(424, 417);
            this.cmbPaddingColor.Size = new System.Drawing.Size(120, 30);
            this.cmbPaddingColor.Items.AddRange(new object[] { "黑色", "白色", "灰色" });
            this.cmbPaddingColor.SelectedIndex = 0;  // 默认黑色
            // 
            // chkGPU
            // 
            this.chkGPU.AutoSize = true;
            this.chkGPU.Checked = true;
            this.chkGPU.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGPU.Location = new System.Drawing.Point(344, 460);
            this.chkGPU.Name = "chkGPU";
            this.chkGPU.Size = new System.Drawing.Size(121, 24);
            this.chkGPU.TabIndex = 22;
            this.chkGPU.Text = "使用 GPU 加速";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(280, 560);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(200, 50);
            this.btnStart.TabIndex = 23;
            this.btnStart.Text = "开始合成视频";
            // 
            // ImageToVideoForm
            // 
            this.Icon = global::FFmpegKit.Properties.Resources.LOGO_64;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 621);
            this.Controls.Add(this.lvImages);
            this.Controls.Add(this.btnAddImages);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnTop);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnBottom);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSelectOutput);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.nudDuration);
            this.Controls.Add(this.lblFPS);
            this.Controls.Add(this.cmbFPS);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.lblAspect);
            this.Controls.Add(this.cmbAspect);
            this.Controls.Add(this.lblWidthAuto);
            this.Controls.Add(this.lblEstimated);
            this.Controls.Add(this.lblPaddingColor);
            this.Controls.Add(this.cmbPaddingColor);
            this.Controls.Add(this.chkGPU);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ImageToVideoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FFmpegKit - 图片合成视频";
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvImages;
        private System.Windows.Forms.Button btnAddImages;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnBottom;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.Label lblFPS;
        private System.Windows.Forms.ComboBox cmbFPS;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Label lblAspect;
        private System.Windows.Forms.ComboBox cmbAspect;
        private System.Windows.Forms.Label lblWidthAuto;
        private System.Windows.Forms.Label lblEstimated;
        private System.Windows.Forms.Label lblPaddingColor;
        private System.Windows.Forms.ComboBox cmbPaddingColor;
        private System.Windows.Forms.CheckBox chkGPU;
        private System.Windows.Forms.Button btnStart;
    }
}