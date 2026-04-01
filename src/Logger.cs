using System;
using System.IO;
using System.Linq;

namespace FFmpegKit
{
    public static class Logger
    {
        /// <summary>
        /// 写入日志并尝试清理旧文件
        /// </summary>
        public static void WriteLog(string message, bool force = false)
        {
            // 如果不是强制写入，且包含进度信息，则直接返回，不写每帧更新的信息
            if (!force && message.Contains("time="))
            {
                return;
            }
            try
            {
                // 文件名 (例如: Logs\2026-03-25.log)
                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                string filePath = Path.Combine(ConfigManager.LogDirectory, fileName);

                // 格式化内容
                string content = string.Format("[{0:HH:mm:ss}] {1}{2}",
                    DateTime.Now, message, Environment.NewLine);

                // 追加写入
                File.AppendAllText(filePath, content);

                // 顺便执行一次清理（可以加个随机概率或者按小时触发，避免频繁扫描磁盘）
                if (new Random().Next(0, 100) == 0) // 1% 的概率触发清理
                {
                    CleanOldLogs(30); // 默认保留30天
                }
            }
            catch { }
        }

        public static void CleanOldLogs(int daysToKeep)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(ConfigManager.LogDirectory);
                DateTime cutoff = DateTime.Now.AddDays(-daysToKeep);

                var oldFiles = di.GetFiles("*.log")
                                 .Where(f => f.LastWriteTime < cutoff);

                foreach (var file in oldFiles)
                {
                    file.Delete();
                }
            }
            catch { }
        }
    }
}