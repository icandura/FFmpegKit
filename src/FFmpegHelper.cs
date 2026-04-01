using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FFmpegKit
{
    public static class FFmpegHelper
    {
        /// <summary>
        /// 功能01：音视频格式转换
        /// </summary>
        /// <param name="inputFiles">输入文件列表</param>
        /// <param name="outputFolder">输出文件夹</param>
        /// <param name="outputFormat">输出格式</param>
        /// <param name="useGpu">是否使用GPU加速</param>
        /// <param name="bitrateKbps">视频比特率，单位：kbps</param>
        /// <param name="targetHeight">目标高度，单位：像素</param>
        public static void ExecuteMultiConvert(
            List<string> inputFiles,
            string outputFolder,
            string outputFormat,
            bool useGpu,
            int? bitrateKbps = null,
            int? targetHeight = null)
        {
            if (inputFiles.Count == 0) return;

            string gpuType = ConfigManager.DefaultGPU.ToLower();
            bool isOutputVideo = !IsAudioOnly(outputFormat);

            var commands = new List<string>();

            foreach (string input in inputFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(input);

                string outputFile = Path.Combine(outputFolder, $"{fileName}_converted{outputFormat}");

                bool isInputAudioOnly = IsAudioOnly(Path.GetExtension(input));

                StringBuilder sb = new StringBuilder();

                sb.Append($"-hide_banner ");

                // ==================== GPU 参数 ====================
                if (useGpu && !isInputAudioOnly)
                {
                    if (gpuType == "nvenc") sb.Append("-hwaccel cuda -hwaccel_output_format cuda ");
                    else if (gpuType == "amf") sb.Append("-hwaccel amf ");
                    //else if (gpuType == "qsv") sb.Append("-init_hw_device qsv:hw -hwaccel qsv -hwaccel_output_format qsv ");
                    //经测试在某些版本下异常，intel 显卡此处改用 auto
                    else if (gpuType == "qsv") sb.Append("-hwaccel auto ");
                    else sb.Append("-hwaccel auto ");
                }

                sb.Append($"-i \"{input}\" ");

                // ==================== 纯音频转视频的特殊处理 ====================
                if (isInputAudioOnly && isOutputVideo)
                {
                    // 固定 16:9 比例，根据用户输入的高度计算宽度
                    int height = targetHeight.HasValue && targetHeight.Value > 0 ? targetHeight.Value : 720;
                    int width = (int)Math.Round(height * 16.0 / 9.0);
                    if (width % 2 == 1) width--;   // 保证偶数

                    // 生成黑屏视频源 + 音频同步
                    sb.Append($"-f lavfi -i color=c=black:s={width}x{height}:r=30 ");

                    string videoCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";
                    //sb.Append($"-c:v {videoCodec} -preset medium -crf 23 ");
                    // 根据不同GPU类型确定质量参数
                    string qualityParams = GetGpuQualityParams(gpuType, 23, useGpu);
                    sb.Append($"-c:v {videoCodec} -preset medium {qualityParams}");

                    sb.Append("-c:a aac -b:a 192k -shortest ");   // -shortest 保证时长一致
                }
                else
                {
                    // ==================== 正常音视频转换 ====================
                    if (isOutputVideo)   // 输出是视频格式
                    {
                        string videoCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";
                        //sb.Append($"-c:v {videoCodec} -preset medium ");
                        // 根据不同GPU类型确定质量参数
                        string qualityParams = GetGpuQualityParams(gpuType, 20, useGpu);
                        sb.Append($"-c:v {videoCodec} -preset medium {qualityParams}");

                        if (targetHeight.HasValue && targetHeight > 0)
                        {
                            if (gpuType == "nvenc" && useGpu)
                            {
                                int origW, origH;
                                if (TryGetVideoResolution(input, out origW, out origH) && origW > 0 && origH > 0)
                                {
                                    int newWidth = (int)Math.Round((double)targetHeight.Value * origW / origH / 2) * 2;
                                    sb.Append($"-vf scale_cuda={newWidth}:{targetHeight.Value} ");
                                }
                                else
                                {
                                    sb.Append($"-vf scale_cuda=-2:{targetHeight.Value} ");
                                }
                            }
                            else
                            {
                                sb.Append($"-vf scale=-2:{targetHeight.Value} ");
                            }
                        }

                        if (bitrateKbps.HasValue && bitrateKbps > 0)
                            sb.Append($"-b:v {bitrateKbps}k ");
                    }
                    else
                    {
                        sb.Append("-vn ");
                    }

                    // 音频处理
                    if (!isOutputVideo)   // 输出是音频格式
                    {
                        if (outputFormat.ToLower() == ".mp3")
                            sb.Append("-c:a libmp3lame -q:a 2 ");
                        else
                            sb.Append("-c:a aac -b:a 192k ");
                    }
                    else
                    {
                        sb.Append("-c:a copy ");
                    }
                }

                sb.Append("-y ");
                sb.Append($"\"{outputFile}\"");

                commands.Add(sb.ToString());
            }

            using (var form = new ProgressForm())
            {
                form.StartTasks(commands, "批量格式转换");
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 功能02：音视频切割
        /// </summary>
        /// <param name="inputFiles">输入文件列表</param>
        /// <param name="outputFolder">输出文件夹</param>
        /// <param name="useGpu">是否使用GPU加速</param>
        /// <param name="useStartEndMode">是否使用起始时间+结束时间模式</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="trimHead">裁剪头部</param>
        /// <param name="trimTail">裁剪尾部</param>
        /// <param name="precise">是否精确到毫秒</param>
        public static void ExecuteMultiCut(
            List<string> inputFiles,
            string outputFolder,
            bool useGpu,
            bool useStartEndMode,
            TimeSpan startTime,
            TimeSpan endTime,
            TimeSpan trimHead,
            TimeSpan trimTail,
            bool precise = false)
        {
            if (inputFiles.Count == 0) return;

            string gpuType = ConfigManager.DefaultGPU.ToLower();

            var commands = new List<string>();

            foreach (string input in inputFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(input);
                string ext = Path.GetExtension(input);

                TimeSpan actualStart = startTime;
                TimeSpan actualDuration = TimeSpan.Zero;

                if (!useStartEndMode)
                {
                    double totalSec = GetDuration(input);
                    if (totalSec <= 0) totalSec = 3600;

                    actualStart = trimHead;
                    actualDuration = TimeSpan.FromSeconds(totalSec) - trimHead - trimTail;
                }
                else
                {
                    actualDuration = endTime - startTime;
                }

                // 输出文件名（原文件名_起始时间_结束时间，毫秒不放文件名中）
                string startStr = $"{actualStart.Hours:D2}{actualStart.Minutes:D2}{actualStart.Seconds:D2}";
                string endStr = $"{(actualStart + actualDuration).Hours:D2}{(actualStart + actualDuration).Minutes:D2}{(actualStart + actualDuration).Seconds:D2}";
                string outputFile = Path.Combine(outputFolder, $"{fileName}_{startStr}_{endStr}{ext}");

                StringBuilder sb = new StringBuilder();

                sb.Append($"-hide_banner ");

                // GPU 参数
                if (useGpu)
                {
                    if (gpuType == "nvenc") sb.Append("-hwaccel cuda -hwaccel_output_format cuda ");
                    else if (gpuType == "amf") sb.Append("-hwaccel amf ");
                    //else if (gpuType == "qsv") sb.Append("-init_hw_device qsv:hw -hwaccel qsv -hwaccel_output_format qsv ");
                    //经测试在某些版本下异常，intel 显卡此处改用 auto
                    else if (gpuType == "qsv") sb.Append("-hwaccel auto ");
                    else sb.Append("-hwaccel auto ");
                }

                sb.Append($"-i \"{input}\" ");

                // 切割参数（关键）
                if (actualStart.TotalSeconds > 0)
                    sb.Append($"-ss {actualStart.TotalSeconds:F3} ");  // 精确到毫秒

                if (actualDuration.TotalSeconds > 0)
                    sb.Append($"-t {actualDuration.TotalSeconds:F3} ");

                // 根据是否精确模式决定编码方式
                if (precise)
                {
                    // 重新编码（精确切割）
                    string videoCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";
                    //sb.Append($"-c:v {videoCodec} -preset fast -crf 20 ");  // 可调 CRF
                    //原先的-crf在某些显卡下不支持，改为根据不同GPU类型确定质量参数
                    string qualityParams = GetGpuQualityParams(gpuType, 20, useGpu);
                    sb.Append($"-c:v {videoCodec} -preset medium {qualityParams}");
                }
                else
                {
                    // 流复制（快速，但可能有关键帧误差）
                    sb.Append("-c copy ");
                }

                sb.Append("-c:a copy -avoid_negative_ts make_zero -y ");

                sb.Append($"\"{outputFile}\"");

                commands.Add(sb.ToString());
            }

            // 统一弹出一个进度窗执行所有
            using (var form = new ProgressForm())
            {
                form.StartTasks(commands, "批量切割");
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 功能03：音视频倍速处理
        /// </summary>
        /// <param name="inputFiles">已勾选的文件列表</param>
        /// <param name="outputFolder">输出文件夹</param>
        /// <param name="speed">倍率（例如 1.5 表示加速 1.5 倍）</param>
        /// <param name="keepPitch">是否保持音频音调不变</param>
        /// <param name="useGpu">是否使用 GPU 加速</param>
        public static void ExecuteMultiSpeed(
             List<string> inputFiles,
             string outputFolder,
             double speed,
             bool keepPitch,
             bool useGpu)
        {
            if (inputFiles.Count == 0) return;

            string gpuType = ConfigManager.DefaultGPU.ToLower();
            var commands = new List<string>();

            foreach (string input in inputFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(input);
                string ext = Path.GetExtension(input).ToLower();

                bool isAudioOnly = IsAudioOnly(ext);

                string outputExt = isAudioOnly ? ".m4a" : ext;
                string outputFile = Path.Combine(outputFolder, $"{fileName}_{speed:F2}x{outputExt}");

                StringBuilder sb = new StringBuilder();

                sb.Append($"-hide_banner ");

                // GPU 参数（纯音频不需要）
                if (useGpu && !isAudioOnly)
                {
                    if (gpuType == "nvenc") sb.Append("-hwaccel cuda -hwaccel_output_format cuda ");
                    else if (gpuType == "amf") sb.Append("-hwaccel amf ");
                    //else if (gpuType == "qsv") sb.Append("-init_hw_device qsv:hw -hwaccel qsv -hwaccel_output_format qsv ");
                    //经测试在某些版本下异常，intel 显卡此处改用 auto
                    else if (gpuType == "qsv") sb.Append("-hwaccel auto ");
                    else sb.Append("-hwaccel auto ");
                }

                sb.Append($"-i \"{input}\" ");

                // 视频倍速
                if (!isAudioOnly)
                {
                    string videoCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";
                    //增加 fps=source_fps 解决在不同版本ffmpeg下输出帧率不同的问题，此处强制输出原始视频帧率
                    sb.Append($"-filter:v \"setpts=PTS/{speed},fps=source_fps\" ");
                    //sb.Append($"-c:v {videoCodec} -preset fast -crf 23 ");
                    //原先的-crf在某些显卡下不支持，改为根据不同GPU类型确定质量参数
                    string qualityParams = GetGpuQualityParams(gpuType, 20, useGpu);
                    sb.Append($"-c:v {videoCodec} -preset medium {qualityParams}");
                }

                // 音频处理
                if (keepPitch)
                {
                    // 保持原音调
                    sb.Append($"-filter:a atempo={speed} ");
                }
                else
                {
                    // 变速变调
                    sb.Append($"-filter:a \"asetrate=44100*{speed},aresample=44100\" ");
                }

                // 纯音频文件禁用视频流
                if (isAudioOnly)
                {
                    sb.Append("-vn ");
                }

                sb.Append("-c:a aac -b:a 192k -y ");
                sb.Append($"\"{outputFile}\"");

                commands.Add(sb.ToString());
            }

            // 统一弹出一个进度窗执行所有倍速处理任务
            using (var form = new ProgressForm())
            {
                form.StartTasks(commands, $"批量倍速处理 ({inputFiles.Count} 个文件)");
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 功能04：图片合成视频
        /// </summary>
        /// <param name="imagePaths">图片列表</param>
        /// <param name="outputFolder">输出文件夹</param>
        /// <param name="durationPerImage">每张图片的持续时间（秒）</param>
        /// <param name="fps">视频帧率</param>
        /// <param name="width">视频宽度</param>
        /// <param name="height">视频高度</param>
        /// <param name="useGpu">是否使用 GPU 加速</param>
        /// <param name="paddingColor">视频黑边填充颜色（默认 black）</param>
        public static void ExecuteImageToVideo(
            List<string> imagePaths,
            string outputFolder,
            double durationPerImage,
            int fps,
            int width,
            int height,
            bool useGpu,
            string paddingColor = "black")
        {
            if (imagePaths.Count == 0) return;

            string gpuType = ConfigManager.DefaultGPU.ToLower();

            // 输出文件名
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string outputFile = Path.Combine(outputFolder, $"合成视频_{timestamp}.mp4");

            // 临时中间文件（无损）
            string tempFile = Path.Combine(Path.GetTempPath(), $"temp_img2vid_{timestamp}.avi");

            var commands = new List<string>();

            // 第一步：生成无损中间文件（CPU，快速）
            {
                string concatListPath = Path.Combine(Path.GetTempPath(), "images_concat.txt");
                using (var writer = new StreamWriter(concatListPath))
                {
                    foreach (string img in imagePaths)
                    {
                        writer.WriteLine($"file '{img.Replace("\\", "/")}'");
                        writer.WriteLine($"duration {durationPerImage}");
                    }
                }

                StringBuilder sb1 = new StringBuilder();
                sb1.Append($"-hide_banner ");
                sb1.Append($"-f concat -safe 0 -i \"{concatListPath}\" ");

                // 核心滤镜：等比例 + 黑边
                sb1.Append($"-vf \"scale='min({width},iw)':'min({height},ih)':force_original_aspect_ratio=decrease,pad={width}:{height}:(ow-iw)/2:(oh-ih)/2:{paddingColor}\" ");

                sb1.Append($"-r {fps} ");
                sb1.Append("-c:v utvideo -an ");  // 无损视频编码，无音频
                sb1.Append("-y ");
                sb1.Append($"\"{tempFile}\"");

                commands.Add(sb1.ToString());
            }

            // 第二步：压制最终文件（GPU 或 CPU）
            {
                StringBuilder sb2 = new StringBuilder();
                sb2.Append($"-hide_banner ");

                //GPU 加速
                if (useGpu)
                {
                    if (gpuType == "nvenc") sb2.Append("-hwaccel cuda -hwaccel_output_format cuda ");
                    else if (gpuType == "amf") sb2.Append("-hwaccel amf ");
                    //else if (gpuType == "qsv") sb2.Append("-init_hw_device qsv:hw -hwaccel qsv -hwaccel_output_format qsv ");
                    //经测试在某些版本下异常，intel 显卡此处改用 auto
                    else if (gpuType == "qsv") sb2.Append("-hwaccel auto ");
                    else sb2.Append("-hwaccel auto ");
                }

                sb2.Append($"-i \"{tempFile}\" ");
                sb2.Append("-f lavfi -i anullsrc ");  // 静音音频轨
                sb2.Append("-map 0:v -map 1:a ");

                string videoCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";
                //sb2.Append($"-c:v {videoCodec} -preset medium -crf 23 ");
                //原先的-crf在某些显卡下不支持，改为根据不同GPU类型确定质量参数
                string qualityParams = GetGpuQualityParams(gpuType, 23, useGpu);
                sb2.Append($"-c:v {videoCodec} -preset medium {qualityParams}");
                sb2.Append("-c:a aac -b:a 192k -shortest ");

                sb2.Append("-y ");
                sb2.Append($"\"{outputFile}\"");

                commands.Add(sb2.ToString());
            }

            // 批量执行（显示两步）
            using (var form = new ProgressForm())
            {
                form.StartTasks(commands, "图片合成视频（两步处理）");
                form.ShowDialog();
            }

            // 清理临时文件
            try
            {
                File.Delete(tempFile);
            }
            catch { }
        }

        /// <summary>
        /// 功能05：音视频串联
        /// </summary>
        /// <param name="inputFiles">要串联的文件路径列表</param>
        /// <param name="outputFolder">输出文件夹</param>
        /// <param name="isVideoOutput">true=合并成视频，false=合并成音频</param>
        /// <param name="height">输出视频高度</param>
        /// <param name="width">输出视频宽度</param>
        /// <param name="fps">输出帧率</param>
        /// <param name="useGpu">是否使用 GPU 加速</param>
        public static void ExecuteConcat(
            List<string> inputFiles,
            string outputFolder,
            bool isVideoOutput,
            int height,
            int width,
            int fps,
            bool useGpu,
            bool isFastMode)
        {
            if (inputFiles == null || inputFiles.Count == 0) return;

            
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // 最终输出路径
            string outExt = isVideoOutput ? ".mp4" : ".m4a";
            string outputFile = Path.Combine(outputFolder,
                (isFastMode ? $"快速串联_" : (isVideoOutput ? $"视频串联_" : $"音频串联_")) + $"{timestamp}{outExt}");

            var commands = new List<string>();
            List<string> tempFilesToDelete = new List<string>();

            if (isFastMode)
            {
                // 快速模式，直接copy快速合并
                string fastlistPath = Path.Combine(Path.GetTempPath(), $"fast_list_{Guid.NewGuid():N}.txt");
                tempFilesToDelete.Add(fastlistPath);

                try
                {
                    using (StreamWriter sw = new StreamWriter(fastlistPath))
                    {
                        foreach (var file in inputFiles)
                        {
                            // FFmpeg concat 协议要求：路径中的单引号需要转义，且路径建议用正斜杠
                            string safePath = file.Replace("'", "'\\''").Replace("\\", "/");
                            sw.WriteLine($"file '{safePath}'");
                        }
                    }

                    // 核心命令：直接 copy 不转码。-safe 0 允许绝对路径。
                    string concatCmd = $"-hide_banner -f concat -safe 0 -i \"{fastlistPath}\" -c copy -y \"{outputFile}\"";
                    commands.Add(concatCmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("创建任务列表失败：" + ex.Message);
                    return;
                }
            }
            else 
            {
                // 标准模式，逐个转码
                string gpuType = ConfigManager.DefaultGPU.ToLower();
                // 统一音频参数：AAC, 192k, 44100Hz, 双声道 (确保拼接时不因采样率冲突报错)
                string audioNormParams = "-c:a aac -b:a 192k -ar 44100 -ac 2";

                List<string> tsMidFiles = new List<string>();

                // 对每个文件进行标准化预处理
                foreach (string input in inputFiles)
                {
                    string ext = Path.GetExtension(input).ToLower();
                    bool isAudio = IsAudioOnly(ext);

                    // 生成唯一的中间文件名 (.ts 容器对串联的容错性最好)
                    string tempFile = Path.Combine(Path.GetTempPath(), $"concat_temp_{Guid.NewGuid():N}.ts");
                    tsMidFiles.Add(tempFile);
                    tempFilesToDelete.Add(tempFile);

                    StringBuilder sb = new StringBuilder();

                    sb.Append($"-hide_banner ");

                    if (useGpu)
                    {
                        // 使用 auto 模式：FFmpeg 会尝试自动调用可用的硬解，失败时会自动回退到 CPU 软解
                        sb.Append("-hwaccel auto ");
                    }

                    if (isVideoOutput)
                    {
                        // --- 是否视频模式 ---
                        if (isAudio)
                        {
                            // 1. 将音频转为黑屏视频
                            // 生成占位黑屏源 (color)，并绑定音频输入
                            sb.Append($"-f lavfi -i color=c=black:s={width}x{height}:r={fps} ");
                            sb.Append($"-i \"{input}\" ");

                            string vCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";
                            sb.Append($"-c:v {vCodec} -pix_fmt yuv420p -r {fps} ");
                            // -shortest 确保视频长度随音频结束而结束
                            sb.Append($"{audioNormParams} -shortest ");
                        }
                        else
                        {
                            // 2. 视频标准化 (缩放、补边、补音频)
                            // 输入1：原始视频；输入2：静音流 (防止原视频无声音导致拼接流缺失)
                            sb.Append($"-i \"{input}\" -f lavfi -i anullsrc=r=44100:cl=stereo ");

                            // 滤镜：等比例缩放 -> 补黑边 -> 强制重置SAR为1:1 (防止在某些播放器上画面拉伸)
                            string vf = $"scale={width}:{height}:force_original_aspect_ratio=decrease,pad={width}:{height}:(ow-iw)/2:(oh-ih)/2:black,setsar=1";
                            sb.Append($"-vf \"{vf}\" -r {fps} -pix_fmt yuv420p ");

                            string vCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";
                            //sb.Append($"-c:v {vCodec} ");
                            // 根据不同GPU类型确定质量参数
                            string qualityParams = GetGpuQualityParams(gpuType, 20, useGpu);
                            sb.Append($"-c:v {vCodec} -preset medium {qualityParams}");                            

                            // 关键点：映射视频流，尝试映射原音频，若原视频无音频则映射静音流
                            // 使用 -map 0:v:0 -map 0:a? -map 1:a 确保最终 TS 文件必有音频轨
                            sb.Append($"-map 0:v:0 -map 0:a? -map 1:a {audioNormParams} -shortest ");
                        }
                    }
                    else
                    {
                        // --- 音频模式 ---
                        sb.Append($"-i \"{input}\" ");
                        if (!isAudio) sb.Append("-vn -map 0:a:0 "); // 视频则提取第一条音频流
                        sb.Append(audioNormParams);
                    }

                    sb.Append($" -y \"{tempFile}\"");
                    commands.Add(sb.ToString());
                }

                // 创建临时的 concat 列表文件
                string listPath = Path.Combine(Path.GetTempPath(), $"list_{Guid.NewGuid():N}.txt");
                tempFilesToDelete.Add(listPath);

                using (StreamWriter sw = new StreamWriter(listPath))
                {
                    foreach (var temp in tsMidFiles)
                    {
                        // 写入 file '路径'，并将反斜杠替换为正斜杠防止转义错误
                        sw.WriteLine($"file '{temp.Replace("\\", "/")}'");
                    }
                }

                // 最终拼接指令：因为中间件参数已对齐，此处 c:v copy 即可
                string concatCmd = $"-f concat -safe 0 -i \"{listPath}\" -c copy -y \"{outputFile}\"";
                commands.Add(concatCmd);
            }

            // 提交到进度窗口执行
            using (var form = new ProgressForm())
            {
                form.StartTasks(commands, isFastMode ? "快速串联" :(isVideoOutput ? "视频标准化串联" : "音频标准化串联"));
                form.ShowDialog();
            }
            
            // 临时文件清理
            foreach (var path in tempFilesToDelete)
            {
                if (File.Exists(path)) try { File.Delete(path); } catch { }
            }
        }

        /// <summary>
        /// 功能06：视频/音频/字幕合并
        /// </summary>
        /// <param name="videoFile">视频文件路径（必填）</param>
        /// <param name="audioFiles">音频文件列表（带标签）</param>
        /// <param name="subtitleFiles">字幕文件列表（带标签）</param>
        /// <param name="outputFile">完整输出路径（含扩展名）</param>
        /// <param name="fastMode">是否快速模式（尽量 copy）</param>
        /// <param name="discardOriginalAudio">是否丢弃原视频自带的音频流</param>
        /// <param name="useLongestAudio">是否使用最长的音频作为时长基准</param>
        /// <param name="padBlackWhenAudioLonger">如果使用最长音频，是否在视频末尾添加黑屏以匹配时长（否则采用视频末尾帧）</param>
        /// <param name="useGpu">是否使用 GPU 编码</param>
        public static void ExecuteMerge(
            string videoFile,
            List<MediaFile> audioFiles,
            List<MediaFile> subtitleFiles,
            string outputFile,
            bool fastMode,
            bool discardOriginalAudio,
            bool useLongestAudio = false,
            bool padBlackWhenAudioLonger = false,
            bool useGpu = false)
        {
            if (string.IsNullOrEmpty(videoFile) || !File.Exists(videoFile))
            {
                MessageBox.Show("视频文件不存在或未选择", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 0. 计算目标总时长
            double maxDuration = GetDuration(videoFile); // 视频原长作为基准
            if (useLongestAudio)
            {
                foreach (var audio in audioFiles)
                {
                    double audioLen = GetDuration(audio.FilePath);
                    if (audioLen > maxDuration) maxDuration = audioLen;
                }
            }

            StringBuilder sb = new StringBuilder();

            sb.Append($"-hide_banner ");

            // 1. 所有的输入文件声明 (-i)
            sb.Append($"-i \"{videoFile}\" "); // index 0
            foreach (var audio in audioFiles)
            {
                sb.Append($"-i \"{audio.FilePath}\" ");
            }
            foreach (var sub in subtitleFiles)
            {
                sb.Append($"-i \"{sub.FilePath}\" ");
            }

            // 2. 视频流处理与时长逻辑
            if (fastMode)
            {
                // 快速模式：Stream Copy
                sb.Append("-map 0:v:0 ");
                sb.Append("-c:v copy ");
                // 快速模式不支持填充，只能截断
                if (!useLongestAudio) sb.Append("-shortest ");
            }
            else
            {
                // 兼容模式：转码处理
                string gpuType = ConfigManager.DefaultGPU.ToLower();
                string videoCodec = useGpu ? GetGpuVideoCodec(gpuType) : "libx264";

                if (useLongestAudio)
                {
                    // 最长音频为准：填充黑屏或最后一针
                    string mode = padBlackWhenAudioLonger ? "add" : "clone";
                    string color = padBlackWhenAudioLonger ? ":color=black" : "";

                    // 使用 tpad 强行拉长视频（拉长到 7200秒/2小时，确保超过任何可能的音频）
                    // 因为后面有 -t 参数，FFmpeg 不会真的跑 2 小时
                    sb.Append($"-filter_complex \"[0:v]tpad=stop_mode={mode}:stop_duration=7200{color}[v]\" ");
                    sb.Append("-map \"[v]\" ");
                }
                else
                {
                    // 视频长度为准：截断音频
                    sb.Append("-map 0:v:0 ");
                    // 注意：-shortest 会放在输出文件前生效
                }

                //sb.Append($"-c:v {videoCodec} -preset medium -crf 23 ");
                // 根据不同GPU类型确定质量参数
                string qualityParams = GetGpuQualityParams(gpuType, 20, useGpu);
                sb.Append($"-c:v {videoCodec} -preset medium {qualityParams}");
            }

            // 3. 音频流映射与编码
            if (!discardOriginalAudio)
            {
                sb.Append("-map 0:a? ");
            }

            // 计算当前已存在的音频流数量，用于 metadata 索引偏移
            // 假设原视频音频映射为 1 路（简单处理），或者你可以用 ffprobe 获取准确路数
            int currentAudioOutIndex = discardOriginalAudio ? 0 : 1;

            for (int i = 0; i < audioFiles.Count; i++)
            {
                int inputIdx = 1 + i;
                sb.Append($"-map {inputIdx}:a:0 ");
                if (!fastMode) sb.Append("-c:a aac -b:a 192k ");
                else sb.Append($"-c:a:{currentAudioOutIndex} copy ");

                if (!string.IsNullOrEmpty(audioFiles[i].Label))
                {
                    sb.Append($"-metadata:s:a:{currentAudioOutIndex} title=\"{audioFiles[i].Label}\" ");
                }
                currentAudioOutIndex++;
            }

            // 4. 字幕流映射与编码
            int currentSubOutIndex = 0;
            for (int i = 0; i < subtitleFiles.Count; i++)
            {
                int inputIdx = 1 + audioFiles.Count + i;
                sb.Append($"-map {inputIdx}:s:0 ");

                // MP4 只能用 mov_text，MKV 可以 copy (ass/srt)
                if (outputFile.ToLower().EndsWith(".mp4"))
                    sb.Append($"-c:s:{currentSubOutIndex} mov_text ");
                else
                    sb.Append($"-c:s:{currentSubOutIndex} copy ");

                if (!string.IsNullOrEmpty(subtitleFiles[i].Label))
                {
                    sb.Append($"-metadata:s:s:{currentSubOutIndex} language=\"{subtitleFiles[i].Label}\" ");
                }
                currentSubOutIndex++;
            }

            // 5. 结束标志与输出
            // 使用精准计算出的时长，解决 -shortest 导致的多音轨提前结束问题
            if (maxDuration > 0)
            {
                sb.Append($"-t {maxDuration.ToString("F3")} ");
            }
            if (!useLongestAudio)
            {
                // 如果以视频为准，确保所有流（音轨、字幕）在视频结束时强制停止
                sb.Append("-shortest ");
            }

            sb.Append("-max_interleave_delta 0 "); // 处理某些流同步引起的缓冲问题
            sb.Append("-y ");
            sb.Append($"\"{outputFile}\"");

            // 执行
            List<string> commands = new List<string> { sb.ToString() };
            using (var form = new ProgressForm())
            {
                form.StartTasks(commands, "视频/音频/字幕合并");
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 功能07：视频/音频/字幕分离
        /// </summary>
        /// <param name="inputFiles">输入文件列表</param>
        /// <param name="outputFolder">输出文件夹</param>
        public static void ExecuteMultiSeparate(
            List<string> inputFiles, 
            string outputFolder)
        {
            if (inputFiles == null || inputFiles.Count == 0) return;

            var commands = new List<string>();

            foreach (string input in inputFiles)
            {
                if (!File.Exists(input)) continue;

                string baseName = Path.GetFileNameWithoutExtension(input);
                // 调用 GetAllTracks 获取详细流信息
                var tracks = GetAllTracks(input);

                foreach (var track in tracks)
                {
                    // 1. 根据编码获取正确的后缀
                    string ext = GetExtensionByCodec(track.Type, track.Codec);

                    // 2. 构造输出文件名：文件名_类型_索引.后缀
                    string fileName = string.Format("{0}_{1}_{2}{3}", baseName, track.Type, track.Index, ext);
                    string outputFile = Path.Combine(outputFolder, fileName);

                    // 3. 构造 FFmpeg 命令
                    // 默认使用 -c copy (极速无损)
                    //string codecParam = "-c copy";
                    string inputArgs = "";  // 放在 -i 之前的参数
                    string outputArgs = ""; // 放在 -i 之后的参数

                    // 特殊处理：如果是字幕且后缀为 .srt，但原编码不是 srt，则需要转码
                    // 因为 -c copy 无法将非 srt 编码塞进 .srt 文件
                    if (track.Type == "subtitle")
                    {
                        if (ext == ".srt")
                        {
                            // 修正：-sub_charenc 必须在输入文件 -i 之前
                            inputArgs = "-sub_charenc UTF-8";
                            // 修正：指定输出编码器为 srt
                            outputArgs = "-map 0:" + track.Index + " -c:s srt";
                        }
                        else
                        {
                            outputArgs = "-map 0:" + track.Index + " -c copy";
                        }
                    }
                    else
                    {
                        // 视频和音频保持极速 copy
                        outputArgs = "-map 0:" + track.Index + " -c copy";
                    }

                    // 最终组合顺序：[全局参数] [输入参数] -i [输入文件] [输出参数] [输出文件]
                    string cmd = string.Format("-hide_banner {0} -i \"{1}\" {2} -y \"{3}\"",
                                                inputArgs, input, outputArgs, outputFile);

                    commands.Add(cmd);
                }
            }

            if (commands.Count == 0)
            {
                MessageBox.Show("未发现有效的音视频轨道。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 4. 提交给进度窗口执行
            using (var form = new ProgressForm())
            {
                form.StartTasks(commands, string.Format("轨道分离 (共 {0} 个文件，提取 {1} 条轨道)", inputFiles.Count, commands.Count));
                form.ShowDialog();
            }
        }


        /// <summary>
        /// 检查 ffmpeg 是否可用（路径存在且可执行）
        /// </summary>
        public static bool CheckFFmpegAvailable(out string errorMessage)
        {
            errorMessage = string.Empty;
            string path = ConfigManager.FFmpegPath;

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                errorMessage = "ffmpeg.exe 路径无效或文件不存在。请在设置中指定正确路径。";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取默认GPU编码器（从配置读取）
        /// </summary>
        public static string GetDefaultGpuEncoder()
        {
            string gpu = ConfigManager.DefaultGPU;
            if (string.IsNullOrEmpty(gpu))
            {
                return null;
            }

            string gpuLower = gpu.ToLower();
            switch (gpuLower)
            {
                case "nvenc":
                    return "h264_nvenc";
                case "amf":
                    return "h264_amf";
                case "qsv":
                    return "h264_qsv";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 简单判断是否为纯音频后缀
        /// </summary>
        private static bool IsAudioOnly(string ext)
        {
            string[] audioExts = { ".mp3", ".wav", ".m4a", ".flac", ".aac", ".ogg", ".wma" };
            return audioExts.Contains(ext.ToLower());
        }

        /// <summary>
        /// 获取GPU编码器（根据GPU类型）
        /// </summary>
        private static string GetGpuVideoCodec(string gpuType)
        {
            switch (gpuType)
            {
                case "nvenc": return "h264_nvenc";
                case "amf": return "h264_amf";
                case "qsv": return "h264_qsv";
                default: return "libx264";
            }
        }

        /// <summary>
        /// 根据GPU类型设置质量参数
        /// </summary>
        /// <param name="gpuType">GPU类型</param>
        /// <param name="qualityValue">质量值（默认20，数字越小质量越高）</param>
        /// <returns></returns>
        private static string GetGpuQualityParams(string gpuType, int qualityValue = 20, bool useGpu = false)
        {
            if (useGpu)
            {
                switch (gpuType.ToLower())
                {
                    case "nvenc":
                        // Nvidia 支持 -cq，但建议开启 vbr 模式
                        return $"-rc vbr -cq {qualityValue} -qmin {qualityValue} -qmax {qualityValue} ";
                    case "qsv":
                        // Intel QSV 使用 -global_quality，这是你报错的核心原因
                        return $"-global_quality {qualityValue} ";
                    case "amf":
                        // AMD 比较特殊，通常使用恒定质量 CQP 模式
                        return $"-rc cqp -qp_i {qualityValue} -qp_p {qualityValue} ";
                    default:
                        // 默认回退到 CPU 的 CRF
                        return $"-crf {qualityValue} ";
                }
            }
            else 
            {
                return $"-crf {qualityValue} ";
            }
        }

        /// <summary>
        /// 获取视频文件分辨率
        /// </summary>
        /// <param name="inputFile">视频文件路径</param>
        /// <param name="width">视频宽度</param>
        /// <param name="height">视频高度</param>
        /// <returns></returns>
        private static bool TryGetVideoResolution(string inputFile, out int width, out int height)
        {
            width = 0;
            height = 0;

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = ConfigManager.FFmpegPath,
                    Arguments = $"-i \"{inputFile}\" -hide_banner",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var p = Process.Start(psi))
                {
                    string output = p.StandardError.ReadToEnd();
                    p.WaitForExit();

                    // 解析 Stream #0:0: Video: ..., 1920x1080
                    var match = System.Text.RegularExpressions.Regex.Match(output,
                        @"Video:.*?, (\d+)x(\d+)");

                    if (match.Success)
                    {
                        width = int.Parse(match.Groups[1].Value);
                        height = int.Parse(match.Groups[2].Value);
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// 获取视频总时长（秒）
        /// </summary>
        /// <param name="inputFile">视频文件路径</param>
        /// <returns>视频时长（秒）</returns>
        private static double GetDuration(string inputFile)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = ConfigManager.FFmpegPath,
                    Arguments = $"-i \"{inputFile}\" -hide_banner",     //尝试使用ffmpeg -hide_banner参数减少输出干扰，直接从错误流解析时长信息
                    //Arguments = $"-i \"{inputFile}\" -f null -",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var p = Process.Start(psi))
                {
                    string output = p.StandardError.ReadToEnd();
                    p.WaitForExit();

                    // 解析 Duration: 00:03:27.45
                    var match = System.Text.RegularExpressions.Regex.Match(output,
                        @"Duration:\s*(\d+):(\d+):(\d+\.\d+)");

                    if (match.Success)
                    {
                        int h = int.Parse(match.Groups[1].Value);
                        int m = int.Parse(match.Groups[2].Value);
                        double s = double.Parse(match.Groups[3].Value);
                        return h * 3600 + m * 60 + s;
                    }
                }
            }
            catch { }
            return 0;
        }

        /// <summary>
        /// 获取视频轨道信息
        /// </summary>
        /// <param name="inputFile">视频文件路径</param>
        /// <returns>轨道信息列表</returns>
        private static List<TrackInfo> GetAllTracks(string inputFile)
        {
            var tracks = new List<TrackInfo>();

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = ConfigManager.FFmpegPath,
                    // 使用 -hide_banner 减少干扰信息
                    Arguments = string.Format("-i \"{0}\" -hide_banner", inputFile),
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var p = Process.Start(psi))
                {
                    // FFmpeg 的流信息输出在 StandardError 中
                    string output = p.StandardError.ReadToEnd();
                    p.WaitForExit();

                    // 正则解析逻辑：
                    // Group 1: Index (数字)
                    // Group 2: Type (Video/Audio/Subtitle)
                    // Group 3: Codec (第一个冒号后的单词)
                    string pattern = @"Stream #0:(\d+)(?:\[.*?\])?(?:\(.*\))?:\s*(Video|Audio|Subtitle):\s*([a-zA-Z0-9_]+)";
                    var matches = System.Text.RegularExpressions.Regex.Matches(output, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    foreach (System.Text.RegularExpressions.Match match in matches)
                    {
                        tracks.Add(new TrackInfo
                        {
                            Index = int.Parse(match.Groups[1].Value),
                            Type = match.Groups[2].Value.ToLower(),
                            Codec = match.Groups[3].Value.ToLower()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录错误或输出到控制台
                System.Diagnostics.Debug.WriteLine("解析轨道失败: " + ex.Message);
            }

            // 兜底策略：如果完全没解析到，假设第0轨是视频
            if (tracks.Count == 0)
            {
                tracks.Add(new TrackInfo { Index = 0, Type = "video", Codec = "h264" });
            }

            return tracks;
        }
        
        /// <summary>
        /// 媒体文件编码信息
        /// </summary>
        public class TrackInfo
        {
            public int Index { get; set; }
            public string Type { get; set; }
            public string Codec { get; set; } // 新增：保存编码名称，如 aac, mp3, ac3, flac
        }

        /// <summary>
        /// 根据轨道类型和编码获取文件扩展名
        /// </summary>
        /// <param name="type">轨道类型，如 "video"、"audio"、"subtitle"</param>
        /// <param name="codec">轨道编码，如 "h264"、"aac"、"mp3"</param>
        /// <returns>文件扩展名，如 ".mp4"、".m4a"、".mp3"</returns>
        private static string GetExtensionByCodec(string type, string codec)
        {
            if (type == "video")
            {
                // 视频流 copy 到 mp4 是最通用的，如果是特殊格式如 vp9 也可以考虑 .mkv
                return ".mp4";
            }

            if (type == "subtitle")
            {
                // 如果原格式是 ass/ssa，保持原样后缀
                if (codec.Contains("ass") || codec.Contains("ssa")) return ".ass";
                //if (codec.Contains("srt") || codec.Contains("subrip")) return ".srt";
                // 如果是图形字幕（如 pgs, dvdsub），copy 出来必须用 .mks (Matroska Subtitle)
                //return ".mks";

                // 其他所有文本字幕（含 mov_text, srt, sxt 等）统一导出为 .srt
                return ".srt";
            }

            if (type == "audio")
            {
                switch (codec)
                {
                    case "aac": return ".m4a";
                    case "mp3": return ".mp3";
                    case "ac3": return ".ac3";
                    case "dts": return ".dts";
                    case "flac": return ".flac";
                    case "opus": return ".opus";
                    case "vorbis": return ".ogg";
                    case "pcm_s16le":
                    case "pcm_s24le": return ".wav";
                    default:
                        // 万能音频容器，如果遇到不认识的编码，用 .mka 绝对不会错
                        return ".mka";
                }
            }

            return ".bin";
        }
    }
}