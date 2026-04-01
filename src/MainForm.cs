using System;
using System.Drawing;
using System.Windows.Forms;

namespace FFmpegKit
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 检查是否需要执行初始探测
            if (ConfigManager.IsFFmpegReady && (string.IsNullOrEmpty(ConfigManager.FFmpegVersion) || ConfigManager.FFmpegVersion == "未知版本"))
            {
                // 异步或静默执行一次探测，确保主界面能显示出特性
                ConfigManager.RefreshFFmpegFeatures(ConfigManager.FFmpegPath);
            }

            UpdateStatus();
        }

        private void FunctionButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (!ConfigManager.IsFFmpegReady)
            {
                MessageBox.Show("请先在「设置」中指定有效的 ffmpeg.exe 路径！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btn.Tag is int funcId)
            {
                switch (funcId)
                {
                    case 1:
                        // 功能01：音视频格式转换
                        new ConvertForm().ShowDialog();
                        break;
                    case 2:
                        // 功能02：音视频切割
                        new CutForm().ShowDialog();
                        break;
                    case 3:
                        // 功能03：音视频倍速处理
                        new SpeedForm().ShowDialog();
                        break;
                    case 4:
                        // 功能04：图片合成视频
                        new ImageToVideoForm().ShowDialog();
                        break;
                    case 5:
                        // 功能05：多个音视频拼接
                        new ConcatForm().ShowDialog();
                        break;
                    case 6:
                        // 功能06：视频/音频/字幕合并（混流）
                        new MergeForm().ShowDialog();
                        break;
                    case 7:
                        // 功能07：视频/音频/字幕分离（解流）
                        new SeparateForm().ShowDialog();
                        break;
                    case 8:
                    case 9:
                        MessageBox.Show($"功能 {funcId} 尚未实现", "开发中");
                        break;
                    default:
                        MessageBox.Show("未知功能", "错误");
                        break;
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var frm = new SettingsForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    UpdateStatus();
                }
            }
        }

        private void UpdateStatus()
        {
            // 1. 更新基本路径
            lblFFmpegPath.Text = string.IsNullOrEmpty(ConfigManager.FFmpegPath)
                ? "尚未配置 ffmpeg.exe 路径"
                : ConfigManager.FFmpegPath;

            // 2. 更新状态条
            tslStatus.Text = ConfigManager.IsFFmpegReady ? "就绪" : "请配置 FFmpeg";

            // 3. 更新看板内容
            if (ConfigManager.IsFFmpegReady)
            {
                // 显示版本号（只取简短版本名，防止过长）
                string fullVer = ConfigManager.FFmpegVersion;
                lblVerVal.Text = fullVer.Length > 40 ? fullVer.Substring(0, 37) + "..." : fullVer;
                lblVerVal.ForeColor = System.Drawing.Color.RoyalBlue;

                // 更新 GPU 状态灯效果
                SetStatusStyle(lblNvenc, ConfigManager.SupportNvenc, "NVIDIA");
                SetStatusStyle(lblAmf, ConfigManager.SupportAmf, "AMD");
                SetStatusStyle(lblQsv, ConfigManager.SupportQsv, "Intel");
            }
            else
            {
                lblVerVal.Text = "未探测到有效内核";
                lblVerVal.ForeColor = System.Drawing.Color.Red;
                SetStatusStyle(lblNvenc, false, "NVIDIA");
                SetStatusStyle(lblAmf, false, "AMD");
                SetStatusStyle(lblQsv, false, "Intel");
            }
        }

        /// <summary>
        /// 辅助方法：设置状态标签的样式（模拟指示灯）
        /// </summary>
        private void SetStatusStyle(Label lbl, bool isFullySupported, string name)
        {
            lbl.Text = name;
            if (isFullySupported)
            {
                lbl.BackColor = Color.MediumSeaGreen; // 亮绿灯
                lbl.ForeColor = Color.White;
            }
            else
            {
                lbl.BackColor = Color.Gainsboro; // 灭灯
                lbl.ForeColor = Color.Gray;
            }
        }
    }
}