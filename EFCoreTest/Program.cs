using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTest
{
    class Program
    {
        private static Process agent = null;

        private static FileStream fileStream = null; 

        static void Main(string[] args)
        {
            ProcessStart();

            Console.ReadLine();
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

                    if (fileStream == null)
                    {
                        var logfile = ConfigurationManager.AppSettings["LogFilePath"];
                        var file = string.Format(logfile, DateTime.Now.ToString("yyyyMMdd"));
                        var dir = Path.GetDirectoryName(file);

                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }

                        if (!Directory.Exists(dir))
                        {
                            Directory.Delete(dir);
                        }

                        fileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    }

                    lock (fileStream)
                    {
                        var buffer = Encoding.Default.GetBytes(msg);
                        fileStream.Write(buffer, Convert.ToInt32(fileStream.Length), buffer.Length);
                        fileStream.FlushAsync();
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
        private static void ProcessStart()
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
                agent.WaitForExit();
            }
            catch (Exception ex)
            {
                WriteLogAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:dd} {ex}");
            }
        }
    }
}
