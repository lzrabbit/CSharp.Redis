using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSharp.Redis.Client;

namespace CSharp.Redis
{
    public partial class ServerMonitor : Form
    {
        public RedisMonitor Monitor;
        public ServerMonitor(string host, int port, string password)
        {
            try
            {
                InitializeComponent();
                this.btnStop.Enabled = false;
                this.Monitor = new RedisMonitor(host, port, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ServerMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Monitor.Stop();
            }
            catch
            {

            }
        }

        private void txtExeuctedCommands_TextChanged(object sender, EventArgs e)
        {
            this.txtExeuctedCommands.ScrollToCaret();
        }

        private void txtExeuctedCommands_Click(object sender, EventArgs e)
        {
            this.txtExeuctedCommands.SelectionStart = this.txtExeuctedCommands.TextLength;
            this.txtExeuctedCommands.ScrollToCaret();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Monitor.Start(this.txtExeuctedCommands);
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.Monitor.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }


    }
}
