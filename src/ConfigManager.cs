using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
//using System.Linq;
using System.Management;

namespace FFmpegKit
{
    public static class ConfigManager
    {
        #region 基础配置属性

        public static string FFmpegPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings["FFmpegPath"];
                // --- 逻辑1：初次启动自动探测 ---
                if (string.IsNullOrEmpty(path))
                {
                    string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
                    if (File.Exists(localPath))
                    {
                        FFmpegPath = localPath; // 触发保存
                        return localPath;
                    }
                }
                return string.IsNullOrEmpty(path) ? "" : path;
            }
            set { UpdateAppSetting("FFmpegPath", value); }
        }

        public static string DefaultGPU
        {
            get
            {
                string gpu = ConfigurationManager.AppSettings["DefaultGPU"];
                return string.IsNullOrEmpty(gpu) ? "none" : gpu.ToLower();
            }
            set { UpdateAppSetting("DefaultGPU", value.ToLower()); }
        }

        #endregion

        #region FFmpeg 特性缓存属性 (新增加)

        public static string FFmpegVersion
        {
            get => ConfigurationManager.AppSettings["FFmpegVersion"] ?? "未知版本";
            set => UpdateAppSetting("FFmpegVersion", value);
        }

        // 硬件加速支持状态 (存储为 "True" 或 "False")
        public static bool SupportNvenc
        {
            get => bool.TryParse(ConfigurationManager.AppSettings["SupportNvenc"], out bool b) && b;
            set => UpdateAppSetting("SupportNvenc", value.ToString());
        }

        public static bool SupportAmf
        {
            get => bool.TryParse(ConfigurationManager.AppSettings["SupportAmf"], out bool b) && b;
            set => UpdateAppSetting("SupportAmf", value.ToString());
        }

        public static bool SupportQsv
        {
            get => bool.TryParse(ConfigurationManager.AppSettings["SupportQsv"], out bool b) && b;
            set => UpdateAppSetting("SupportQsv", value.ToString());
        }

        // 硬件检测属性（缓存硬件是否存在）
        public static bool HasNvidiaHardware { get; private set; }
        public static bool HasAmdHardware { get; private set; }
        public static bool HasIntelHardware { get; private set; }

        #endregion

        #region 特性探测核心逻辑

        /// <summary>
        /// 核心方法：运行 FFmpeg 并解析其支持的特性
        /// </summary>
        /// <param name="path">ffmpeg.exe 的路径</param>
        /// <returns>是否成功</returns>
        public static bool RefreshFFmpegFeatures(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return false;

            try
            {
                string versionInfo = GetCommandOutput(path, "-version");

                // --- 确保是真正的 ffmpeg.exe 而不是 ffprobe ---
                if (!versionInfo.StartsWith("ffmpeg version", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                FFmpegVersion = versionInfo.Split('\n')[0];

                // 先扫描硬件
                ScanPhysicalHardware();

                // 获取编码器列表
                string encoders = GetCommandOutput(path, "-encoders");

                // --- 硬件有 && 内核有 = 真正支持 ---
                SupportNvenc = HasNvidiaHardware && encoders.Contains("nvenc");
                SupportAmf = HasAmdHardware && encoders.Contains("amf");
                SupportQsv = HasIntelHardware && encoders.Contains("qsv");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("探测 FFmpeg 失败: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 扫描物理硬件：利用 wmic 检查显卡名称
        /// </summary>
        public static void ScanPhysicalHardware()
        {
            try
            {
                // 创建查询对象，直接从系统管理层提取显卡名称
                using (var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        string gpuName = obj["Name"]?.ToString().ToLower() ?? "";

                        if (gpuName.Contains("nvidia")) HasNvidiaHardware = true;
                        if (gpuName.Contains("amd") || gpuName.Contains("radeon")) HasAmdHardware = true;
                        if (gpuName.Contains("intel") || gpuName.Contains("graphics")) HasIntelHardware = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WMI 硬件扫描失败: " + ex.Message);
                // 如果 WMI 报错，可以在此处尝试方案一作为备份，或者直接跳过
            }
        }

        private static string GetCommandOutput(string exe, string args)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = exe,
                Arguments = args + " -hide_banner",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8
            };

            using (var process = Process.Start(startInfo))
            {
                return process.StandardOutput.ReadToEnd();
            }
        }

        #endregion

        #region 工具方法

        public static bool IsFFmpegReady
        {
            get
            {
                if (string.IsNullOrEmpty(FFmpegPath)) return false;
                return File.Exists(FFmpegPath);
            }
        }

        private static void UpdateAppSetting(string key, string value)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] == null)
                    config.AppSettings.Settings.Add(key, value);
                else
                    config.AppSettings.Settings[key].Value = value;

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置失败：\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 日志目录属性，确保存在
        public static string LogDirectory
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
        }

        #endregion
    }
}