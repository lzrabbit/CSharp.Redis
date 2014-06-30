namespace CSharp.Redis
{
    partial class RedisTookit
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedisTookit));
            this.txtVal = new System.Windows.Forms.TextBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.treeHost = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuAddHost = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.listKeys = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtVal
            // 
            this.txtVal.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtVal.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVal.ForeColor = System.Drawing.Color.Lime;
            this.txtVal.Location = new System.Drawing.Point(13, 296);
            this.txtVal.Multiline = true;
            this.txtVal.Name = "txtVal";
            this.txtVal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtVal.Size = new System.Drawing.Size(926, 293);
            this.txtVal.TabIndex = 8;
            this.txtVal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVal_KeyDown);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(894, 36);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(45, 23);
            this.btnInfo.TabIndex = 10;
            this.btnInfo.Text = "Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // treeHost
            // 
            this.treeHost.Location = new System.Drawing.Point(13, 72);
            this.treeHost.Name = "treeHost";
            this.treeHost.Size = new System.Drawing.Size(161, 201);
            this.treeHost.TabIndex = 11;
            this.treeHost.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeHost_NodeMouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddHost});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(965, 25);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuAddHost
            // 
            this.menuAddHost.Name = "menuAddHost";
            this.menuAddHost.Size = new System.Drawing.Size(68, 21);
            this.menuAddHost.Text = "添加链接";
            this.menuAddHost.Click += new System.EventHandler(this.menuAddHost_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtCommand.ForeColor = System.Drawing.Color.Lime;
            this.txtCommand.Location = new System.Drawing.Point(101, 39);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(696, 21);
            this.txtCommand.TabIndex = 13;
            // 
            // listKeys
            // 
            this.listKeys.Location = new System.Drawing.Point(192, 72);
            this.listKeys.Name = "listKeys";
            this.listKeys.Size = new System.Drawing.Size(747, 201);
            this.listKeys.TabIndex = 15;
            this.listKeys.UseCompatibleStateImageBehavior = false;
            this.listKeys.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listKeys_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "RedisCommand:";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(803, 36);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 17;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // RedisTookit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 613);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listKeys);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.treeHost);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.txtVal);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RedisTookit";
            this.Text = "Redis客户端--懒惰的肥兔";
            this.Load += new System.EventHandler(this.RedisTookit_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.TreeView treeHost;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuAddHost;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.ListView listKeys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExecute;
    }
}

