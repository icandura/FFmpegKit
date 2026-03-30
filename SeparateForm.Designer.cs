namespace FFmpegKit
{
    partial class SeparateForm
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
            this.btnStart = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 文件列表（多文件模式）
            this.lvFiles.CheckBoxes = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader() { Text = "选中", Width = 50 },
                new System.Windows.Forms.ColumnHeader() { Text = "文件名", Width = 280 },
                new System.Windows.Forms.ColumnHeader() { Text = "路径", Width = 400 }});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.Location = new System.Drawing.Point(12, 12);
            this.lvFiles.Size = new System.Drawing.Size(760, 280);
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.TabIndex = 0;
            // 开启拖放功能
            this.lvFiles.AllowDrop = true;
            // 允许拖动项
            this.lvFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LvFiles_ItemDrag);
            this.lvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvFiles_DragEnter);
            this.lvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvFiles_DragDrop);


            // 操作按钮
            this.btnAddFiles.Location = new System.Drawing.Point(12, 300);
            this.btnAddFiles.Size = new System.Drawing.Size(120, 35);
            this.btnAddFiles.Text = "添加文件";

            this.btnRemove.Location = new System.Drawing.Point(140, 300);
            this.btnRemove.Size = new System.Drawing.Size(100, 35);
            this.btnRemove.Text = "移除勾选";

            this.btnTop.Location = new System.Drawing.Point(248, 300);
            this.btnTop.Size = new System.Drawing.Size(80, 35);
            this.btnTop.Text = "置顶";

            this.btnUp.Location = new System.Drawing.Point(336, 300);
            this.btnUp.Size = new System.Drawing.Size(80, 35);
            this.btnUp.Text = "上移";

            this.btnDown.Location = new System.Drawing.Point(424, 300);
            this.btnDown.Size = new System.Drawing.Size(80, 35);
            this.btnDown.Text = "下移";

            this.btnBottom.Location = new System.Drawing.Point(512, 300);
            this.btnBottom.Size = new System.Drawing.Size(80, 35);
            this.btnBottom.Text = "置底";

            this.btnClear.Location = new System.Drawing.Point(600, 300);
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.Text = "全部清空";

            // 输出文件夹
            this.btnSelectOutput.Location = new System.Drawing.Point(12, 340);
            this.btnSelectOutput.Size = new System.Drawing.Size(120, 30);
            this.btnSelectOutput.Text = "输出文件夹";

            this.txtOutputFolder.Location = new System.Drawing.Point(140, 340);
            this.txtOutputFolder.Size = new System.Drawing.Size(630, 30);
            this.txtOutputFolder.ReadOnly = true;

            // 开始按钮
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(280, 390);
            this.btnStart.Size = new System.Drawing.Size(200, 50);
            this.btnStart.Text = "开始分离";

            this.Icon = global::FFmpegKit.Properties.Resources.LOGO_64;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 460);
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
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SeparateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FFmpegKit - 视频/音频/字幕分离";
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
        private System.Windows.Forms.Button btnStart;
    }
}