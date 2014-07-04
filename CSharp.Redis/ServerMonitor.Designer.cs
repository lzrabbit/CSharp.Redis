namespace CSharp.Redis
{
    partial class ServerMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtExeuctedCommands = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtExeuctedCommands
            // 
            this.txtExeuctedCommands.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtExeuctedCommands.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtExeuctedCommands.ForeColor = System.Drawing.Color.Lime;
            this.txtExeuctedCommands.Location = new System.Drawing.Point(0, -1);
            this.txtExeuctedCommands.MaxLength = 1000;
            this.txtExeuctedCommands.Multiline = true;
            this.txtExeuctedCommands.Name = "txtExeuctedCommands";
            this.txtExeuctedCommands.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtExeuctedCommands.Size = new System.Drawing.Size(743, 336);
            this.txtExeuctedCommands.TabIndex = 9;
            this.txtExeuctedCommands.WordWrap = false;
            this.txtExeuctedCommands.Click += new System.EventHandler(this.txtExeuctedCommands_Click);
            this.txtExeuctedCommands.TextChanged += new System.EventHandler(this.txtExeuctedCommands_TextChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(235, 348);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(356, 348);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // ServerMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 383);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtExeuctedCommands);
            this.Name = "ServerMonitor";
            this.Text = "ServerMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerMonitor_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExeuctedCommands;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
    }
}