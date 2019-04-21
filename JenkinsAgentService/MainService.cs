using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsAgentService
{
    public partial class MainService : ServiceBase
    {
        private Process agent = null;

        private FileStream fileStream = null;

        private Timer agentTimer = null; 
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Jenkins Agent Starting...");

                Task.Factory.StartNew(()=> ProcessStart());

                agentTimer = new Timer(obj =>
                {
                    if (agent == null || agent.HasExited)
                    {
                        WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Jenkins Agent 进程异常，重新启动中.");
                        ProcessStart();
                        WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Jenkins Agent 进程异常，重新启动完成.");
                    }
                }, null, 3000, 3000); 
            }
            catch (Exception ex)
            {
                WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {ex}.");
            }
        }

        protected override void OnStop()
        {
            WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Jenkins Agent ending.");

            if (agent != null && !agent.HasExited)
            {
                agent.Kill();
            }

            WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Jenkins Agent ended.");
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <returns></returns>
        private void WriteLogAsync(string msg)
        {
            try
            {
                Console.WriteLine(msg);

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    msg += Environment.NewLine;

                    //if (fileStream == null)
                    //{
                    var logfile = ConfigurationManager.AppSettings["LogFilePath"];
                    var file = string.Format(logfile, DateTime.Now.ToString("yyyyMMdd"));
                    var dir = Path.GetDirectoryName(file);

                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    //}

                    using (fileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        lock (fileStream)
                        {
                            var buffer = Encoding.Default.GetBytes(msg);
                            fileStream.Position = fileStream.Length;
                            fileStream.Write(buffer, 0, buffer.Length); 
                        }

                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 进程启动
        /// </summary>
        private void ProcessStart()
        {
            try
            {
                ConfigurationManager.RefreshSection("appSettings");
                var execFile = ConfigurationManager.AppSettings["ExecFile"];
                var execArgs = ConfigurationManager.AppSettings["ExecArgs"];
                var account = ConfigurationManager.AppSettings["ExecAccount"];
                var password = ConfigurationManager.AppSettings["ExecPassword"];
                var domian = ConfigurationManager.AppSettings["Domian"];

                agent = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = execFile,
                        Arguments = execArgs,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        StandardErrorEncoding = Encoding.Default
                    }
                };

                if (!string.IsNullOrWhiteSpace(account))
                {
                    var passwords = new SecureString();

                    foreach (var item in password.ToArray())
                    {
                        passwords.AppendChar(item);
                    }

                    agent.StartInfo.CreateNoWindow = false;
                    agent.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    agent.StartInfo.UserName = account;
                    agent.StartInfo.Password = passwords;
                    agent.StartInfo.Domain = domian;
                }

                agent.StartInfo.UseShellExecute = false;
                agent.EnableRaisingEvents = true;
                agent.ErrorDataReceived += (s, e) => WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:dd} {e.Data}");
                agent.OutputDataReceived += (s, e) => WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:dd} {e.Data}");
                agent.Start();
                agent.BeginErrorReadLine();
                agent.BeginOutputReadLine();

                WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Jenkins Agent Started.");

                agent.WaitForExit();
            }
            catch (Exception ex)
            {
                WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:dd} {ex}");
            }
        }
    }
}
