using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFmpegKit
{
    public partial class SeparateForm : Form
    {
        private bool isAllChecked = true; // 用于记录当前是全选还是全不选的状态切换

        public SeparateForm()
        {
            InitializeComponent();
            this.Load += SeparateForm_Load;

            btnAddFiles.Click += BtnAddFiles_Click;
            btnRemove.Click += BtnRemove_Click;
            btnTop.Click += BtnTop_Click;
            btnUp.Click += BtnUp_Click;
            btnDown.Click += BtnDown_Click;
            btnBottom.Click += BtnBottom_Click;
            btnClear.Click += BtnClear_Click;

            btnSelectOutput.Click += BtnSelectOutput_Click;
            btnStart.Click += BtnStart_Click;

            lvFiles.ColumnClick += LvFiles_ColumnClick;


        }

        private void SeparateForm_Load(object sender, EventArgs e)
        {
            txtOutputFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        // ====================== 鼠标拖拽排序逻辑 ======================

        // 1. 开始拖拽项时触发
        private void LvFiles_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // 发起拖放操作，并将当前被拖拽的项作为数据传递
            lvFiles.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        // 2. 当拖拽对象进入控件边界时触发
        private void LvFiles_DragEnter(object sender, DragEventArgs e)
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
        private void LvFiles_DragDrop(object sender, DragEventArgs e)
        {
            // --- 处理外部文件拖入 ---
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // 定义支持的后缀白名单 *.mp4;*.mkv;*.avi;*.mov;*.wmv;*.flv;*.webm;*.mpg;*.ts
                var extWhiteList = new[] { ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv", ".webm", ".mpg", ".ts" };

                lvFiles.BeginUpdate();
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
                lvFiles.EndUpdate();
                return; // 外部文件处理完直接返回
            }

            // 获取被拖拽的项
            ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

            // 获取鼠标松开位置对应的目标项
            Point targetPoint = lvFiles.PointToClient(new Point(e.X, e.Y));
            ListViewItem targetItem = lvFiles.GetItemAt(targetPoint.X, targetPoint.Y);

            if (draggedItem == null) return;

            // 如果没有明确的目标项（比如拖到了列表空白处），默认移到最后
            int targetIndex = lvFiles.Items.Count - 1;

            if (targetItem != null)
            {
                targetIndex = targetItem.Index;
            }

            // 执行位置调整
            lvFiles.Items.Remove(draggedItem);
            lvFiles.Items.Insert(targetIndex, draggedItem);

            // 保持选中状态并聚焦
            draggedItem.Selected = true;
            // 确保拖拽后的项在视野内
            draggedItem.EnsureVisible();
            lvFiles.Focus();
        }

        // 辅助方法：检查重复并验证后缀
        private void AddFileWithCheck(string filePath, string[] whiteList)
        {
            string ext = Path.GetExtension(filePath).ToLower();

            // 检查后缀是否在白名单中
            if (!whiteList.Contains(ext)) return;

            // 检查是否已存在于列表中 (根据 Tag 存储的全路径判断)
            bool exists = lvFiles.Items.Cast<ListViewItem>().Any(i => (string)i.Tag == filePath);

            if (!exists)
            {
                var item = new ListViewItem(new[] { "", Path.GetFileName(filePath), filePath });
                item.Checked = true;
                item.Tag = filePath;
                lvFiles.Items.Add(item);
            }
        }

        private void BtnAddFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "视频文件|*.mp4;*.mkv;*.avi;*.mov;*.wmv;*.flv;*.webm;*.mpg;*.ts|所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!lvFiles.Items.Cast<ListViewItem>().Any(i => (string)i.Tag == file))
                        {
                            var item = new ListViewItem(new[] { "", Path.GetFileName(file), file });
                            item.Checked = true;
                            item.Tag = file;
                            lvFiles.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            for (int i = lvFiles.Items.Count - 1; i >= 0; i--)
            {
                if (lvFiles.Items[i].Checked)
                    lvFiles.Items.RemoveAt(i);
            }
        }

        private void BtnTop_Click(object sender, EventArgs e) => MoveSelectedItem(true, true);
        private void BtnUp_Click(object sender, EventArgs e) => MoveSelectedItem(true, false);
        private void BtnDown_Click(object sender, EventArgs e) => MoveSelectedItem(false, false);
        private void BtnBottom_Click(object sender, EventArgs e) => MoveSelectedItem(false, true);

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lvFiles.Items.Clear();
        }

        // 通用移动方法
        private void MoveSelectedItem(bool up, bool toEnd)
        {
            if (lvFiles.SelectedItems.Count == 0) return;

            var item = lvFiles.SelectedItems[0];
            int index = lvFiles.Items.IndexOf(item);
            int newIndex = index;

            if (toEnd)
            {
                newIndex = up ? 0 : lvFiles.Items.Count - 1;
            }
            else
            {
                newIndex = up ? index - 1 : index + 1;
            }

            // --- 边界检查 ---
            if (newIndex < 0 || newIndex >= lvFiles.Items.Count)
            {
                item.Selected = true;
                item.EnsureVisible();
                lvFiles.Focus(); // 强制拉回焦点，确保蓝色条高亮
                return;
            }

            // 只有位置真的发生变化时，才执行重新插入
            if (newIndex != index)
            {
                lvFiles.BeginUpdate(); // 防止闪烁
                lvFiles.Items.Remove(item);
                lvFiles.Items.Insert(newIndex, item);
                lvFiles.EndUpdate();
            }

            item.Selected = true;
            item.EnsureVisible();
            lvFiles.Focus(); // 确保蓝色高亮条持续显示
        }

        // 点击列标题实现全选/全不选切换
        private void LvFiles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                // 如果当前已经全部勾选了，则点击后全不选；否则一律视为全选
                bool allCheckedNow = lvFiles.CheckedItems.Count == lvFiles.Items.Count;
                bool targetState = !allCheckedNow;

                lvFiles.BeginUpdate();
                foreach (ListViewItem item in lvFiles.Items)
                {
                    item.Checked = targetState;
                }
                lvFiles.EndUpdate();
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

        private void BtnStart_Click(object sender, EventArgs e)
        {
            var selectedFiles = lvFiles.CheckedItems.Cast<ListViewItem>()
                .Select(item => (string)item.Tag)
                .ToList();

            if (selectedFiles.Count == 0)
            {
                MessageBox.Show("请至少勾选一个文件", "提示");
                return;
            }

            if (string.IsNullOrEmpty(txtOutputFolder.Text))
            {
                MessageBox.Show("请选择输出文件夹", "提示");
                return;
            }

            ////调试用，实际使用时请删除
            //MessageBox.Show($"勾选了 {selectedFiles.Count} 个文件，将开始分离");

            FFmpegHelper.ExecuteMultiSeparate(selectedFiles, txtOutputFolder.Text);
        }
    }
}