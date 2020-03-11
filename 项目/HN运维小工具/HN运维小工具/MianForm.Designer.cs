namespace HN运维小工具
{
    partial class MianForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageQueue1 = new System.Messaging.MessageQueue();
            this.BtnSet = new System.Windows.Forms.Panel();
            this.BtnTransferFIle = new System.Windows.Forms.Button();
            this.CreateBackUpDir = new System.Windows.Forms.Button();
            this.Log = new System.Windows.Forms.Panel();
            this.TextLog = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.BtnSet.SuspendLayout();
            this.Log.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(931, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据库设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 数据库设置ToolStripMenuItem
            // 
            this.数据库设置ToolStripMenuItem.Name = "数据库设置ToolStripMenuItem";
            this.数据库设置ToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.数据库设置ToolStripMenuItem.Text = "数据库设置";
            // 
            // messageQueue1
            // 
            this.messageQueue1.DefaultPropertiesToSend.HashAlgorithm = System.Messaging.HashAlgorithm.Sha512;
            this.messageQueue1.MessageReadPropertyFilter.LookupId = true;
            this.messageQueue1.SynchronizingObject = this;
            // 
            // BtnSet
            // 
            this.BtnSet.BackColor = System.Drawing.Color.Transparent;
            this.BtnSet.Controls.Add(this.BtnTransferFIle);
            this.BtnSet.Controls.Add(this.CreateBackUpDir);
            this.BtnSet.Location = new System.Drawing.Point(13, 36);
            this.BtnSet.Name = "BtnSet";
            this.BtnSet.Size = new System.Drawing.Size(200, 728);
            this.BtnSet.TabIndex = 1;
            // 
            // BtnTransferFIle
            // 
            this.BtnTransferFIle.Location = new System.Drawing.Point(4, 90);
            this.BtnTransferFIle.Name = "BtnTransferFIle";
            this.BtnTransferFIle.Size = new System.Drawing.Size(193, 47);
            this.BtnTransferFIle.TabIndex = 1;
            this.BtnTransferFIle.Text = "数据库文件迁移";
            this.BtnTransferFIle.UseVisualStyleBackColor = true;
            this.BtnTransferFIle.Click += new System.EventHandler(this.BtnTransferFIle_Click);
            // 
            // CreateBackUpDir
            // 
            this.CreateBackUpDir.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CreateBackUpDir.Location = new System.Drawing.Point(3, 4);
            this.CreateBackUpDir.Name = "CreateBackUpDir";
            this.CreateBackUpDir.Size = new System.Drawing.Size(193, 47);
            this.CreateBackUpDir.TabIndex = 0;
            this.CreateBackUpDir.Text = "创建备份文件夹";
            this.CreateBackUpDir.UseVisualStyleBackColor = true;
            this.CreateBackUpDir.Click += new System.EventHandler(this.CreateBackUpDir_Click);
            // 
            // Log
            // 
            this.Log.Controls.Add(this.TextLog);
            this.Log.Location = new System.Drawing.Point(220, 36);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(688, 728);
            this.Log.TabIndex = 2;
            // 
            // TextLog
            // 
            this.TextLog.Location = new System.Drawing.Point(4, 4);
            this.TextLog.Multiline = true;
            this.TextLog.Name = "TextLog";
            this.TextLog.Size = new System.Drawing.Size(681, 721);
            this.TextLog.TabIndex = 0;
            // 
            // MianForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 776);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.BtnSet);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MianForm";
            this.Text = "HN运维小工具";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.BtnSet.ResumeLayout(false);
            this.Log.ResumeLayout(false);
            this.Log.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据库设置ToolStripMenuItem;
        private System.Messaging.MessageQueue messageQueue1;
        private System.Windows.Forms.Panel Log;
        private System.Windows.Forms.Panel BtnSet;
        private System.Windows.Forms.TextBox TextLog;
        private System.Windows.Forms.Button CreateBackUpDir;
        private System.Windows.Forms.Button BtnTransferFIle;
    }
}

