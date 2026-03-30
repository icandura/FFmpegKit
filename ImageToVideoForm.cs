using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFmpegKit
{
    public partial class ImageToVideoForm : Form
    {
        private bool isAllChecked = true; // 用于记录当前是全选还是全不选的状态切换
        private double currentDuration = 5.0;
        private int selectedCount = 0;

        public ImageToVideoForm()
        {
            InitializeComponent();
            this.Load += ImageToVideoForm_Load;

            // 文件操作按钮事件
            btnAddImages.Click += BtnAddImages_Click;
            btnRemove.Click += BtnRemove_Click;
            btnTop.Click += BtnTop_Click;
            btnUp.Click += BtnUp_Click;
            btnDown.Click += BtnDown_Click;
            btnBottom.Click += BtnBottom_Click;
            btnClear.Click += BtnClear_Click;

            btnSelectOutput.Click += BtnSelectOutput_Click;
            btnStart.Click += BtnStart_Click;

            lvImages.ColumnClick += lvImages_ColumnClick;

            // 实时更新预计时长
            nudDuration.ValueChanged += (s, e) => UpdateEstimatedDuration();
            lvImages.ItemChecked += (s, e) => UpdateEstimatedDuration();

            //实时更新宽度计算结果
            nudHeight.ValueChanged += (s, e) => UpdateWidthDisplay();
            cmbAspect.SelectedIndexChanged += (s, e) => UpdateWidthDisplay();
        }

        private void ImageToVideoForm_Load(object sender, EventArgs e)
        {
            txtOutputFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // 配置是否允许使用GPU加速
            if (ConfigManager.DefaultGPU == "none")
            {
                chkGPU.Checked = false;
                chkGPU.Enabled = false;
            }
            else
            {
                chkGPU.Enabled = true;
                chkGPU.Checked = ConfigManager.DefaultGPU != "none"; 
            }

            UpdateEstimatedDuration();
            UpdateWidthDisplay();
        }

        // ====================== 鼠标拖拽排序逻辑 ======================

        // 1. 开始拖拽项时触发
        private void lvImages_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // 发起拖放操作，并将当前被拖拽的项作为数据传递
            lvImages.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        // 2. 当拖拽对象进入控件边界时触发
        private void lvImages_DragEnter(object sender, DragEventArgs e)
        {
            // 1. 如果拖拽的是外部文件
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // 显示“复制”图标
            }
            // 2. 如果是内部 ListViewItem
            else if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.Move; // 显示“移动”图标
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // 3. 松开鼠标完成拖放时触发
        private void lvImages_DragDrop(object sender, DragEventArgs e)
        {
            // --- 处理外部文件拖入 ---
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // 定义支持的后缀白名单   图片文件|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.webp
                var extWhiteList = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp" };

                lvImages.BeginUpdate();
                foreach (string path in files)
                {
                    // 1. 检查是文件还是文件夹
                    if (File.Exists(path))
                    {
                        AddFileWithCheck(path, extWhiteList);
                    }
                    else if (Directory.Exists(path))
                    {
                        // 如果是文件夹，递归获取下面所有文件
                        string[] subFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                        foreach (string subPath in subFiles)
                        {
                            AddFileWithCheck(subPath, extWhiteList);
                        }
                        
                    }
                }
                lvImages.EndUpdate();
                UpdateEstimatedDuration();
                return; // 外部文件处理完直接返回
            }

            // 获取被拖拽的项
            ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

            // 获取鼠标松开位置对应的目标项
            Point targetPoint = lvImages.PointToClient(new Point(e.X, e.Y));
            ListViewItem targetItem = lvImages.GetItemAt(targetPoint.X, targetPoint.Y);

            if (draggedItem == null) return;

            // 如果没有明确的目标项（比如拖到了列表空白处），默认移到最后
            int targetIndex = lvImages.Items.Count - 1;

            if (targetItem != null)
            {
                targetIndex = targetItem.Index;
            }

            // 执行位置调整
            lvImages.Items.Remove(draggedItem);
            lvImages.Items.Insert(targetIndex, draggedItem);

            // 保持选中状态并聚焦
            draggedItem.Selected = true;
            // 确保拖拽后的项在视野内
            draggedItem.EnsureVisible();
            lvImages.Focus();
        }

        // 辅助方法：检查重复并验证后缀
        private void AddFileWithCheck(string filePath, string[] whiteList)
        {
            string ext = Path.GetExtension(filePath).ToLower();

            // 检查后缀是否在白名单中
            if (!whiteList.Contains(ext)) return;

            // 检查是否已存在于列表中 (根据 Tag 存储的全路径判断)
            bool exists = lvImages.Items.Cast<ListViewItem>().Any(i => (string)i.Tag == filePath);

            if (!exists)
            {
                using (Image img = Image.FromFile(filePath))
                {
                    string size = $"{img.Width}x{img.Height}";
                    var item = new ListViewItem(new[] { "", Path.GetFileName(filePath), size, filePath });
                    item.Checked = true;
                    item.Tag = filePath;
                    lvImages.Items.Add(item);
                }
            }
        }

        private void BtnAddImages_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.webp|所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!lvImages.Items.Cast<ListViewItem>().Any(i => (string)i.Tag == file))
                        {
                            using (Image img = Image.FromFile(file))
                            {
                                string size = $"{img.Width}x{img.Height}";
                                var item = new ListViewItem(new[] { "", Path.GetFileName(file), size, file });
                                item.Checked = true;
                                item.Tag = file;
                                lvImages.Items.Add(item);
                            }
                        }
                    }
                    UpdateEstimatedDuration();
                }
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            for (int i = lvImages.Items.Count - 1; i >= 0; i--)
            {
                if (lvImages.Items[i].Checked)
                    lvImages.Items.RemoveAt(i);
            }
            UpdateEstimatedDuration();
        }

        private void BtnTop_Click(object sender, EventArgs e) => MoveSelectedItem(true, true);
        private void BtnUp_Click(object sender, EventArgs e) => MoveSelectedItem(true, false);
        private void BtnDown_Click(object sender, EventArgs e) => MoveSelectedItem(false, false);
        private void BtnBottom_Click(object sender, EventArgs e) => MoveSelectedItem(false, true);

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lvImages.Items.Clear();
            UpdateEstimatedDuration();
        }

        // 通用移动方法
        private void MoveSelectedItem(bool up, bool toEnd)
        {
            if (lvImages.SelectedItems.Count == 0) return;

            var item = lvImages.SelectedItems[0];
            int index = lvImages.Items.IndexOf(item);
            int newIndex = index;

            if (toEnd)
            {
                newIndex = up ? 0 : lvImages.Items.Count - 1;
            }
            else
            {
                newIndex = up ? index - 1 : index + 1;
            }

            // --- 边界检查 ---
            if (newIndex < 0 || newIndex >= lvImages.Items.Count)
            {
                item.Selected = true;
                item.EnsureVisible();
                lvImages.Focus(); // 强制拉回焦点，确保蓝色条高亮
                return;
            }

            // 只有位置真的发生变化时，才执行重新插入
            if (newIndex != index)
            {
                lvImages.BeginUpdate(); // 防止闪烁
                lvImages.Items.Remove(item);
                lvImages.Items.Insert(newIndex, item);
                lvImages.EndUpdate();
            }

            item.Selected = true;
            item.EnsureVisible();
            lvImages.Focus(); // 确保蓝色高亮条持续显示
        }

        // 点击列标题实现全选/全不选切换
        private void lvImages_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                // 如果当前已经全部勾选了，则点击后全不选；否则一律视为全选
                bool allCheckedNow = lvImages.CheckedItems.Count == lvImages.Items.Count;
                bool targetState = !allCheckedNow;

                lvImages.BeginUpdate();
                foreach (ListViewItem item in lvImages.Items)
                {
                    item.Checked = targetState;
                }
                lvImages.EndUpdate();
            }
        }

        private void BtnSelectOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtOutputFolder.Text = fbd.SelectedPath;
            }
        }

        private void UpdateEstimatedDuration()
        {
            int count = lvImages.CheckedItems.Count;
            double totalSec = count * (double)nudDuration.Value;
            TimeSpan ts = TimeSpan.FromSeconds(totalSec);

            // 使用显式格式化，兼容旧框架（避免使用 TimeSpan.ToString(format)）
            int hours = (int)ts.TotalHours;
            string timeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, ts.Minutes, ts.Seconds);

            lblEstimated.Text = $"预计视频时长：{timeStr}（{count} 张图片）";
            selectedCount = count;
        }

        private void UpdateWidthDisplay()
        {
            int height = (int)nudHeight.Value;
            string ratio = cmbAspect.Text;

            int width = 0;
            switch (ratio)
            {
                case "16:9": width = height * 16 / 9; break;
                case "4:3": width = height * 4 / 3; break;
                case "1:1": width = height; break;
                case "9:16": width = height * 9 / 16; break;
                case "21:9": width = height * 21 / 9; break;
            }

            // 保证偶数
            if (width % 2 == 1) width--;

            lblWidthAuto.Text = $"自动宽度：{width}px";
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (lvImages.Items.Count == 0)
            {
                MessageBox.Show("请至少添加一张图片", "提示");
                return;
            }

            if (string.IsNullOrEmpty(txtOutputFolder.Text))
            {
                MessageBox.Show("请选择输出文件夹", "提示");
                return;
            }

            int height = (int)nudHeight.Value;
            string ratio = cmbAspect.Text;
            int fps = int.Parse(cmbFPS.Text.Split(' ')[0]);

            // 计算宽度
            int width = 0;
            switch (ratio)
            {
                case "16:9": width = height * 16 / 9; break;
                case "4:3": width = height * 4 / 3; break;
                case "1:1": width = height; break;
                case "9:16": width = height * 9 / 16; break;
                case "21:9": width = height * 21 / 9; break;
            }
            if (width % 2 == 1) width--;

            // 获取填充颜色
            string paddingColor = "black";
            switch (cmbPaddingColor.Text)
            {
                case "白色": paddingColor = "white"; break;
                case "灰色": paddingColor = "gray"; break;
            }

            // 收集已选择文件路径（按当前顺序）
            var imagePaths = lvImages.CheckedItems.Cast<ListViewItem>()
                .Select(i => (string)i.Tag)
                .ToList();

            FFmpegHelper.ExecuteImageToVideo(
                imagePaths,
                txtOutputFolder.Text,
                (double)nudDuration.Value,
                fps,
                width,
                height,
                chkGPU.Checked,
                paddingColor
            );
        }
    }
}