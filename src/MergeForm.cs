using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFmpegKit
{
    // ====================== 公共数据类 ======================
    // 供 MergeForm 和 FFmpegHelper 共同使用
    public class MediaFile
    {
        public string FilePath { get; set; }
        public string Label { get; set; }

        public MediaFile(string path, string label = "")
        {
            FilePath = path;
            Label = label ?? "";
        }
    }

    public partial class MergeForm : Form
    {
        private TextBox txtInlineEditor;
        private ListViewItem editingItem;
        private ListView activeListView; // 记录当前正在编辑哪一个 ListView

        public MergeForm()
        {
            InitializeComponent();
            this.Load += MergeForm_Load;

            btnBrowseVideo.Click += BtnBrowseVideo_Click;
            btnBrowseOutput.Click += BtnBrowseOutput_Click;
            btnStart.Click += BtnStart_Click;

            // 模式切换时控制选项可用性
            radFastMode.CheckedChanged += Mode_CheckedChanged;
            radCompatibleMode.CheckedChanged += Mode_CheckedChanged;

            radVideoLength.CheckedChanged += DurationOption_Linkage;
            radLongestAudio.CheckedChanged += DurationOption_Linkage;

            // 音频和字幕的按钮事件
            btnAddAudio.Click += BtnAddAudio_Click;
            btnRemoveAudio.Click += BtnRemoveAudio_Click;
            btnTopAudio.Click += BtnTopAudio_Click;
            btnUpAudio.Click += BtnUpAudio_Click;
            btnDownAudio.Click += BtnDownAudio_Click;
            btnBottomAudio.Click += BtnBottomAudio_Click;

            btnAddSubtitle.Click += BtnAddSubtitle_Click;
            btnRemoveSubtitle.Click += BtnRemoveSubtitle_Click;
            btnTopSubtitle.Click += BtnTopSubtitle_Click;
            btnUpSubtitle.Click += BtnUpSubtitle_Click;
            btnDownSubtitle.Click += BtnDownSubtitle_Click;
            btnBottomSubtitle.Click += BtnBottomSubtitle_Click;

            // --- 新增：初始化行内编辑器 ---
            InitInlineEditor();

            // 由于双击默认会改动多选框，改为绑定 MouseUp
            lvAudio.MouseUp += new MouseEventHandler(ListView_MouseUp);
            lvSubtitle.MouseUp += new MouseEventHandler(ListView_MouseUp);
        }

        // ListView行内编辑器实现
        private void InitInlineEditor()
        {
            txtInlineEditor = new TextBox();
            txtInlineEditor.Visible = false;
            txtInlineEditor.BorderStyle = BorderStyle.FixedSingle;
            // 兼容 .NET 3.5 的委托写法
            txtInlineEditor.KeyDown += new KeyEventHandler(TxtInlineEditor_KeyDown);
            txtInlineEditor.LostFocus += new EventHandler(TxtInlineEditor_LostFocus);
            this.Controls.Add(txtInlineEditor);
            txtInlineEditor.BringToFront();
        }

        private void ListView_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有左键单击才触发
            if (e.Button != MouseButtons.Left) return;

            ListView lv = (ListView)sender;
            ListViewItem item = lv.GetItemAt(e.X, e.Y);

            if (item == null) return;

            // 1. 计算点击的是哪一列
            int columnIndex = 0;
            int currentLeft = 0;
            bool found = false;

            for (int i = 0; i < lv.Columns.Count; i++)
            {
                if (e.X >= currentLeft && e.X <= currentLeft + lv.Columns[i].Width)
                {
                    columnIndex = i;
                    found = true;
                    break;
                }
                currentLeft += lv.Columns[i].Width;
            }

            // 2. 只允许编辑“标签”列（索引为 1）
            if (found && columnIndex == 1)
            {
                activeListView = lv;
                editingItem = item;

                // 获取单元格矩形（.NET 3.5 中 SubItem 的 Bounds 逻辑）
                Rectangle rect = item.SubItems[columnIndex].Bounds;

                // 坐标转换（确保在 TabPage 内部也能准确定位）
                Point screenPoint = lv.PointToScreen(new Point(rect.X, rect.Y));
                Point clientPoint = this.PointToClient(screenPoint);

                // 3. 显示 TextBox
                txtInlineEditor.SetBounds(clientPoint.X, clientPoint.Y, lv.Columns[columnIndex].Width, rect.Height);
                txtInlineEditor.Text = item.SubItems[columnIndex].Text;
                txtInlineEditor.Visible = true;
                txtInlineEditor.BringToFront();
                txtInlineEditor.Focus();
                txtInlineEditor.SelectAll();
            }
        }

        private void TxtInlineEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyEdit();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtInlineEditor.Visible = false;
            }
        }

        private void TxtInlineEditor_LostFocus(object sender, EventArgs e)
        {
            ApplyEdit();
        }

        private void ApplyEdit()
        {
            if (txtInlineEditor.Visible && editingItem != null)
            {
                // 更新 ListViewItem 的 SubItem 文本
                editingItem.SubItems[1].Text = txtInlineEditor.Text;
                txtInlineEditor.Visible = false;
                editingItem = null;
                activeListView = null;
            }
        }
        // ListView行内编辑器实现结束


        private void MergeForm_Load(object sender, EventArgs e)
        {
            txtOutputFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            txtOutputName.Text = "合并视频_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }

        private void BtnBrowseVideo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "视频文件|*.mp4;*.mkv;*.avi;*.mov;*.wmv;*.flv;*.webm|所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtVideoFile.Text = ofd.FileName;
                }
            }
        }

        private void BtnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtOutputFolder.Text = fbd.SelectedPath;
            }
        }
        // 同步持续时间相关选项的UI状态
        private void SyncDurationUI()
        {
            if (radVideoLength.Checked)
            {
                pnlFillOptions.Enabled = false;
                // 确保子选项不被选中（可选，取决于你是否想保留上次的选择）
                radPadLastFrame.Checked = false;
                radPadBlackFrame.Checked = false;
            }
            else if (radLongestAudio.Checked)
            {
                pnlFillOptions.Enabled = true;
                // 如果开启了最长音频，但没选填充方式，默认选一个
                if (!radPadLastFrame.Checked && !radPadBlackFrame.Checked)
                {
                    radPadBlackFrame.Checked = true;
                }
            }
        }

        private void Mode_CheckedChanged(object sender, EventArgs e)
        {
            bool isCompatible = radCompatibleMode.Checked;

            // 配置是否允许使用GPU加速
            if (ConfigManager.DefaultGPU == "none")
            {
                chkGPU.Checked = false;
                chkGPU.Enabled = false;
            }
            else
            {
                chkGPU.Enabled = isCompatible;
                chkGPU.Checked = ConfigManager.DefaultGPU != "none";
            }

            grpDuration.Enabled = isCompatible;

            // 快速模式下，强制关闭 GPU（因为快速模式主要靠 copy）
            if (isCompatible)
            {
                // 当切换到兼容模式时，立即根据当前的单选框状态同步 UI
                SyncDurationUI();
            }
            else
            {
                chkGPU.Checked = false;
            }
        }

        private void DurationOption_Linkage(object sender, EventArgs e)
        {
            // 只有当状态变为 Checked 时才处理，避免触发两次
            if (!(sender as RadioButton).Checked) return;

            SyncDurationUI();
        }

        // ====================== 音频轨 ======================
        private void BtnAddAudio_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "音频文件|*.mp3;*.m4a;*.wav;*.aac;*.flac;*.wma|所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!lvAudio.Items.Cast<ListViewItem>().Any(i => (string)i.Tag == file))
                        {
                            var item = new ListViewItem(new[] { Path.GetFileName(file), "" });
                            item.Tag = file;
                            item.Checked = true;
                            lvAudio.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void BtnRemoveAudio_Click(object sender, EventArgs e) => RemoveSelectedItems(lvAudio);
        private void BtnTopAudio_Click(object sender, EventArgs e) => MoveSelectedItem(lvAudio, true, true);
        private void BtnUpAudio_Click(object sender, EventArgs e) => MoveSelectedItem(lvAudio, true, false);
        private void BtnDownAudio_Click(object sender, EventArgs e) => MoveSelectedItem(lvAudio, false, false);
        private void BtnBottomAudio_Click(object sender, EventArgs e) => MoveSelectedItem(lvAudio, false, true);

        // ====================== 字幕轨 ======================
        private void BtnAddSubtitle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "字幕文件|*.srt;*.ass;*.ssa;*.vtt|所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!lvSubtitle.Items.Cast<ListViewItem>().Any(i => (string)i.Tag == file))
                        {
                            var item = new ListViewItem(new[] { Path.GetFileName(file), "" });
                            item.Tag = file;
                            item.Checked = true;
                            lvSubtitle.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void BtnRemoveSubtitle_Click(object sender, EventArgs e) => RemoveSelectedItems(lvSubtitle);
        private void BtnTopSubtitle_Click(object sender, EventArgs e) => MoveSelectedItem(lvSubtitle, true, true);
        private void BtnUpSubtitle_Click(object sender, EventArgs e) => MoveSelectedItem(lvSubtitle, true, false);
        private void BtnDownSubtitle_Click(object sender, EventArgs e) => MoveSelectedItem(lvSubtitle, false, false);
        private void BtnBottomSubtitle_Click(object sender, EventArgs e) => MoveSelectedItem(lvSubtitle, false, true);

        // ====================== 通用移动和移除方法 ======================
        private void MoveSelectedItem(ListView listView, bool up, bool toEnd)
        {
            if (listView.SelectedItems.Count == 0) return;

            var item = listView.SelectedItems[0];
            int index = listView.Items.IndexOf(item);

            if (toEnd)
            {
                index = up ? 0 : listView.Items.Count - 1;
            }
            else
            {
                index = up ? index - 1 : index + 1;
                if (index < 0 || index >= listView.Items.Count) return;
            }

            listView.Items.Remove(item);
            listView.Items.Insert(index, item);
            item.Selected = true;
        }

        private void RemoveSelectedItems(ListView listView)
        {
            for (int i = listView.Items.Count - 1; i >= 0; i--)
            {
                if (listView.Items[i].Selected)
                    listView.Items.RemoveAt(i);
            }
        }

        private List<MediaFile> GetAudioFiles()
        {
            var list = new List<MediaFile>();
            foreach (ListViewItem item in lvAudio.CheckedItems)
            {
                string path = (string)item.Tag;
                string label = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
                list.Add(new MediaFile(path, label));
            }
            return list;
        }

        private List<MediaFile> GetSubtitleFiles()
        {
            var list = new List<MediaFile>();
            foreach (ListViewItem item in lvSubtitle.CheckedItems)
            {
                string path = (string)item.Tag;
                string label = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
                list.Add(new MediaFile(path, label));
            }
            return list;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVideoFile.Text))
            {
                MessageBox.Show("请选择视频文件", "提示");
                return;
            }

            if (string.IsNullOrEmpty(txtOutputFolder.Text))
            {
                MessageBox.Show("请选择输出文件夹", "提示");
                return;
            }

            if (string.IsNullOrEmpty(txtOutputName.Text))
            {
                MessageBox.Show("请输入输出文件名", "提示");
                return;
            }

            string outputPath = Path.Combine(txtOutputFolder.Text, txtOutputName.Text);
            string ext = cmbOutputFormat.Text.ToLower() == "mkv" ? ".mkv" : ".mp4";

            if (!outputPath.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
            {
                outputPath += ext;
            }

            FFmpegHelper.ExecuteMerge(
              txtVideoFile.Text,
              GetAudioFiles(),
              GetSubtitleFiles(),
              outputPath,
              radFastMode.Checked,
              chkDiscardOriginalAudio.Checked,
              useLongestAudio: radLongestAudio.Checked,
              padBlackWhenAudioLonger: radPadBlackFrame.Checked,
              useGpu: chkGPU.Checked
            );
        }
    }
}