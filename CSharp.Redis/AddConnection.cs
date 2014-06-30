using CSharp.Redis.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp.Redis
{
    public partial class AddConnection : Form
    {
        public AddConnection()
        {
            InitializeComponent();
        }

        private void AddConnection_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.txtPort.Text = "6379";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DbConnection dbconn = new DbConnection
                {
                    Name = string.IsNullOrEmpty(this.txtConnName.Text) ? this.txtHost.Text.Trim() : this.txtConnName.Text,
                    Host = this.txtHost.Text.Trim(),
                    Port = int.Parse(this.txtPort.Text.Trim()),
                    Password = this.txtPassword.Text,
                };
                using (RedisClient client = new RedisClient(dbconn.Host, dbconn.Port, dbconn.Password))
                {
                    client.Ping();
                }
                if (dbconn != null)
                {
                    dbconn.Save();
                    ((RedisTookit)this.Owner).LoadConns();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
