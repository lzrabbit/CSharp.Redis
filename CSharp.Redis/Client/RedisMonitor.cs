using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace CSharp.Redis.Client
{
    public class RedisMonitor
    {
        public RedisClient Client;
        Thread _Thread;

        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public RedisMonitor(string host, int port)
            : this(host, port, null)
        {

        }

        public RedisMonitor(string host, int port, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Password = password;

        }

        public void Start(TextBox textBox)
        {
            Start(new TextBoxWriter(textBox));
        }

        public void Start(TextWriter writer)
        {
            Stop();
            this.Client = new RedisClient(Host, Port, Password);
            _Thread = new Thread(() =>
            {
                using (BufferedStream BStream = Client.ExecuteCommand(RedisCommand.Server.MONITOR))
                {
                    while (true)
                    {
                        try
                        {
                            var list = Client.ParseResult<string>(BStream);
                            foreach (var item in list)
                            {
                                writer.WriteLine(item);
                            }
                        }
                        catch (ThreadAbortException ex)
                        {
                            writer.WriteLine("服务器监控已停止");
                            break;
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine(ex.Message);
                            break;
                        }
                    }
                }
            });
            _Thread.IsBackground = true;
            _Thread.Start();
        }

        public void Stop()
        {
            if (this.Client != null)
            {
                this.Client.Dispose();
            }
            if (_Thread != null && _Thread.IsAlive) _Thread.Abort();
        }

        private class TextBoxWriter : StringWriter
        {
            private TextBox _TextBox;

            public TextBoxWriter(TextBox textBox)
            {
                this._TextBox = textBox;
            }

            public override void Write(string value)
            {
                if (this._TextBox.InvokeRequired)
                {
                    this._TextBox.Invoke(new Action(() =>
                    {
                        this._TextBox.AppendText(value);
                    }));
                }
                else
                {
                    this._TextBox.AppendText(value);
                }
            }

            public override void WriteLine(string value)
            {
                if (this._TextBox.InvokeRequired)
                {
                    this._TextBox.Invoke(new Action(() =>
                    {
                        this._TextBox.AppendText(value + "\r\n");
                    }));
                }
                else
                {
                    this._TextBox.AppendText(value + "\r\n");
                }
            }
        }
    }
}
