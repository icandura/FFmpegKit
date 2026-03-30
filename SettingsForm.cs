using System;
using System.Drawing;
using System.Windows.Forms;

namespace FFmpegKit
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.Text = "FFmpegKit - 设置";
            this.Size = new Size(520, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            LoadSettings();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newPath = txtFFmpegPath.Text.Trim();

            // 基础合法性检查
            if (string.IsNullOrEmpty(newPath))
            {
                MessageBox.Show("请先选择 ffmpeg.exe 路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.IO.File.Exists(newPath))
            {
                MessageBox.Show("所选文件不存在，请检查路径是否正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 尝试探测 FFmpeg 特性（版本、硬件加速支持等）
            // 该方法会运行 ffmpeg -version 和 -encoders，并将结果存入 ConfigManager
            bool success = ConfigManager.RefreshFFmpegFeatures(newPath);

            if (success)
            {
                // 探测成功，说明文件是有效的 FFmpeg

                // 预选 GPU 逻辑与兼容性警告
                string selectedGpu = "none";
                bool isCompatible = true;

                switch (cmbGPU.SelectedIndex)
                {
                    case 1:
                        selectedGpu = "nvenc";
                        if (!ConfigManager.SupportNvenc) isCompatible = false;
                        break;
                    case 2:
                        selectedGpu = "amf";
                        if (!ConfigManager.SupportAmf) isCompatible = false;
                        break;
                    case 3:
                        selectedGpu = "qsv";
                        if (!ConfigManager.SupportQsv) isCompatible = false;
                        break;
                    default:
                        selectedGpu = "none";
                        break;
                }

                // 如果用户选了某个加速，但探测显示不支持，弹出警告
                if (!isCompatible)
                {
                    string gpuName = cmbGPU.Text;
                    var result = MessageBox.Show(
                        $"警告：检测到当前 FFmpeg 内核似乎不支持 {gpuName}。\n\n" +
                        "强制使用不兼容的加速方式可能导致任务执行失败。\n\n" +
                        "确定要保存当前设置吗？",
                        "硬件兼容性提醒",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No) return; // 用户选择返回修改
                }

                // 正式保存配置到 App.config
                ConfigManager.FFmpegPath = newPath;
                ConfigManager.DefaultGPU = selectedGpu;

                // 提示并关闭
                MessageBox.Show($"FFmpeg 配置已就绪！\n\n内核版本：{ConfigManager.FFmpegVersion}\n硬件支持：{(isCompatible ? "检测通过" : "手动强制")}",
                    "设置成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // 探测失败：文件可能是假的或损坏的
                MessageBox.Show("验证失败！该文件无法返回有效的版本信息，请确认是否为正版 FFmpeg 可执行文件。",
                    "内核验证失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "可执行文件|*.exe";
                ofd.FileName = "ffmpeg.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtFFmpegPath.Text = ofd.FileName;
            }
        }

        private void LoadSettings()
        {
            // 还原路径
            if (txtFFmpegPath != null)
                txtFFmpegPath.Text = ConfigManager.FFmpegPath;

            // 动态更新 ComboBox 的显示文字，告知用户当前内核的支持情况
            if (cmbGPU != null)
            {
                // 只有在 FFmpeg 已就绪的情况下才显示支持状态，否则显示原始名称
                bool ready = ConfigManager.IsFFmpegReady;

                cmbGPU.Items[0] = "无（纯 CPU 软解）";

                cmbGPU.Items[1] = "NVIDIA NVENC " +
                    (ready ? (ConfigManager.SupportNvenc ? " [内核已集成]" : " [内核不支持]") : "");

                cmbGPU.Items[2] = "AMD AMF " +
                    (ready ? (ConfigManager.SupportAmf ? " [内核已集成]" : " [内核不支持]") : "");

                cmbGPU.Items[3] = "Intel Quick Sync " +
                    (ready ? (ConfigManager.SupportQsv ? " [内核已集成]" : " [内核不支持]") : "");

                // 还原用户之前的选择
                switch (ConfigManager.DefaultGPU)
                {
                    case "nvenc": cmbGPU.SelectedIndex = 1; break;
                    case "amf": cmbGPU.SelectedIndex = 2; break;
                    case "qsv": cmbGPU.SelectedIndex = 3; break;
                    default: cmbGPU.SelectedIndex = 0; break;
                }
            }
        }
    }
}
