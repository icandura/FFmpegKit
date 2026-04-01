using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FFmpegKit
{
    public partial class ProgressForm : Form
    {
        private Process ffmpegProcess;
        private bool isCancelled = false;
        private bool taskCompletedHandled = false;
        private Queue<string> taskQueue = new Queue<string>();
        private string currentArguments;
        private bool isBatchMode = false;
        private int totalTasks = 0;
        private int completedTasks = 0;

        public ProgressForm()
        {
            InitializeComponent();
            this.Text = "FFmpeg 执行进度";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            btnCancel.Click += BtnCancel_Click;
            this.FormClosing += ProgressForm_FormClosing;
        }

        // 单任务启动
        public void StartProcess(string ffmpegPath, string arguments)
        {
            StartTasks(new List<string> { arguments }, "单个任务");
        }

        // 批量任务启动
        public void StartTasks(List<string> argumentsList, string description = "批量任务")
        {
            if (argumentsList.Count == 0)
            {
                lblStatus.Text = "没有任务";
                btnCancel.Enabled = false;
                return;
            }

            isBatchMode = argumentsList.Count > 1;
            totalTasks = argumentsList.Count;
            completedTasks = 0;
            taskQueue.Clear();

            foreach (var arg in argumentsList)
                taskQueue.Enqueue(arg);

            rtbLog.Clear();
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Value = 0;
            lblStatus.Text = isBatchMode ? $"批量任务：{totalTasks} 个待处理" : "任务运行中...";
            btnCancel.Enabled = true;
            isCancelled = false;
            taskCompletedHandled = false;

            AppendLog($"{description} 开始，共 {totalTasks} 个任务", Color.Green);
            AppendLog("----------------------------------------");
            AppendLog("");

            ProcessNextTask();
        }

        private void ProcessNextTask()
        {
            if (isCancelled)
            {
                lblStatus.Text = "任务已取消";
                btnCancel.Enabled = false;
                progressBar1.Style = ProgressBarStyle.Blocks;
                AppendLog("用户取消了剩余任务", Color.Red);
                return;
            }

            if (taskQueue.Count == 0)
            {
                lblStatus.Text = "所有任务完成";
                btnCancel.Enabled = false;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                AppendLog("批量任务全部完成！", Color.Green);
                AppendLog($"成功处理 {completedTasks}/{totalTasks} 个文件", Color.Green);
                return;
            }

            currentArguments = taskQueue.Dequeue();
            completedTasks++;  // 当前开始处理第几个

            AppendLog($"[{completedTasks}/{totalTasks}] 开始处理下一个任务",Color.Green);
            AppendLog($"命令：{currentArguments}",Color.Red);
            AppendLog("----------------------------------------");

            ffmpegProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ConfigManager.FFmpegPath,
                    Arguments = currentArguments,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    StandardErrorEncoding = System.Text.Encoding.UTF8,
                    StandardOutputEncoding = System.Text.Encoding.UTF8
                }
            };

            ffmpegProcess.ErrorDataReceived += FfmpegProcess_ErrorDataReceived;
            ffmpegProcess.OutputDataReceived += FfmpegProcess_OutputDataReceived;
            ffmpegProcess.Exited += FfmpegProcess_Exited;

            try
            {
                ffmpegProcess.EnableRaisingEvents = true;
                ffmpegProcess.Start();
                ffmpegProcess.BeginErrorReadLine();
                ffmpegProcess.BeginOutputReadLine();

                lblStatus.Text = isBatchMode
                    ? $"处理中... [{completedTasks}/{totalTasks}] 剩余 {taskQueue.Count} 个"
                    : "任务运行中...";
            }
            catch (Exception ex)
            {
                AppendLog("启动失败：" + ex.Message,Color.Red);
                // 出错也继续下一个
                OnTaskFinished();
            }
        }

        private void FfmpegProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    AppendLog(e.Data);
                    UpdateProgress(e.Data);
                });
            }
        }

        private void FfmpegProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    AppendLog("[stdout] " + e.Data);
                });
            }
        }

        private void FfmpegProcess_Exited(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string msg = ffmpegProcess.ExitCode == 0
                    ? "当前任务完成"
                    : $"当前任务结束（退出码 {ffmpegProcess.ExitCode}）";

                AppendLog(msg,Color.Green);
                AppendLog("");

                OnTaskFinished();
            });
        }

        private void OnTaskFinished()
        {
            // 继续下一个任务
            ProcessNextTask();
        }

        private void AppendLog(string text, Color? color = null)
        {
            if (rtbLog.TextLength > 32768) // 如果超过 32768 字符，清空前面的
            {
                rtbLog.Clear();
                rtbLog.AppendText("--- 日志过长，已自动清理旧显示 ---" + Environment.NewLine);
            }

            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => AppendLog(text, color)));
                return;
            }

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;


            if (color.HasValue)
                rtbLog.SelectionColor = color.Value;

            rtbLog.AppendText(text + Environment.NewLine);
            rtbLog.SelectionColor = rtbLog.ForeColor;
            rtbLog.ScrollToCaret();

            // --- 同时写入本地日志 ---
            // 如果有颜色，通常是任务节点，强制记录
            bool isMilestone = color.HasValue;
            Logger.WriteLog(text, isMilestone);
        }

        private void UpdateProgress(string line)
        {
            // 原有进度解析逻辑（保持不变）
            if (line.Contains("time="))
            {
                int idx = line.IndexOf("time=");
                if (idx >= 0)
                {
                    string timeStr = line.Substring(idx + 5).TrimStart().Split(' ')[0];
                    if (TimeSpan.TryParse(timeStr, out TimeSpan ts))
                    {
                        lblStatus.Text = $"处理中... 时间：{ts:hh\\:mm\\:ss} " +
                            (isBatchMode ? $"[{completedTasks}/{totalTasks}]" : "");
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            isCancelled = true;
            if (ffmpegProcess != null && !ffmpegProcess.HasExited)
            {
                try
                {
                    ffmpegProcess.Kill();
                    AppendLog("[用户] 已请求取消当前任务...", Color.Green);
                }
                catch { }
            }
            // 剩余队列会因为 isCancelled = true 而停止
            lblStatus.Text = "正在取消...";
            btnCancel.Enabled = false;
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ffmpegProcess != null && !ffmpegProcess.HasExited)
            {
                if (MessageBox.Show("有任务正在运行，确定关闭并终止？", "确认",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try { ffmpegProcess.Kill(); }
                    catch { }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        // 静态便捷方法（单任务调用方式）
        public static void Execute(string arguments)
        {
            using (var form = new ProgressForm())
            {
                form.StartProcess(ConfigManager.FFmpegPath, arguments);
                form.ShowDialog();
            }
        }
    }
}