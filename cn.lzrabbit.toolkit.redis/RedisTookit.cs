using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CSharp.Redis.Client;

namespace CSharp.Redis
{
    public partial class RedisTookit : Form
    {
        const string Title = "Redis客户端--懒惰的肥兔";
        public RedisTookit()
        {
            InitializeComponent();
        }

        private void RedisTookit_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtVal.Text = Note.V1;
                this.btnExecute.Enabled = false;
                this.btnInfo.Enabled = false;
                this.Text = string.Format("{0}(v{1})", Title, Application.ProductVersion);

                this.listKeys.Columns.Add("Key");
                this.listKeys.Columns.Add("Type");
                this.listKeys.Columns.Add("Size");
                this.listKeys.Columns.Add("");
                this.listKeys.GridLines = true;
                this.listKeys.View = View.Details;
                this.listKeys.MultiSelect = false;
                LoadConns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadConns()
        {
            this.treeHost.Nodes.Clear();
            var conns = DbConnection.Load();
            conns.ForEach(conn =>
            {
                this.treeHost.Nodes.Add(new TreeNode(conn.Name) { ToolTipText = conn.Host, Tag = conn });
            });
        }

        RedisHelper Redis;

        private void treeHost_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string pattern = this.txtPattern.Text.Trim();
            if (string.IsNullOrEmpty(pattern)) pattern = "*";
            try
            {
                var conn = (DbConnection)e.Node.Tag;
                Redis = new RedisHelper(conn.Host, conn.Port, conn.Password);

                this.btnExecute.Enabled = true;
                this.btnInfo.Enabled = true;

                this.listKeys.Items.Clear();
                var entries = Redis.QueryKeys(pattern);
                int i = 0;
                foreach (var entry in entries)
                {
                    i++;
                    this.listKeys.Items.Add(new ListViewItem(new string[] { entry.Key, entry.Type, entry.ItemCount.ToString(), i.ToString(), }) { Tag = entry });
                }
                this.txtCommand.Text = RedisCommand.Keys.KEYS + " " + pattern;
                this.Text = string.Format("{0}(v{1}) {2}:{3}", Title, Application.ProductVersion, conn.Host, conn.Port);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            ExecuteCommand(RedisCommand.Server.INFO);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            ExecuteCommand(this.txtCommand.Text);
        }

        private void listKeys_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.listKeys.SelectedItems.Count > 0)
            {
                var keyEntry = this.listKeys.SelectedItems[0].Tag as RedisHelper.RedisKeyEntry;
                ExecuteCommand(keyEntry.GetRedisCommand());
            }
        }

        public void ExecuteCommand(string command)
        {
            try
            {
                this.txtCommand.Text = command;

                this.txtVal.Text = string.Format("{0} command {1} success\r\n{2}", DateTime.Now, command, Redis.ExecuteCommand(command.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)));
            }
            catch (Exception ex)
            {
                this.txtVal.Text = string.Format("{0} command {1}\r\n{2}", DateTime.Now, command, ex.Message);
                if (ex.Message.Contains("ERR unknown command"))
                {
                    this.txtVal.Text += "\r\n请确认服务器版本是否支持当前命令";
                }
            }
        }

        private void txtVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void menuAddHost_Click(object sender, EventArgs e)
        {
            AddConnection add = new AddConnection();
            add.ShowDialog(this);
        }

    }
}
