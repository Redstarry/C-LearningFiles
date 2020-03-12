﻿namespace SocketServer
{
    partial class SocketServer
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.SocketListen = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.Button();
            this.SelectFile = new System.Windows.Forms.Button();
            this.SendFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(8, 13);
            this.txtIP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(84, 21);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "192.168.1.47";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(108, 13);
            this.txtPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(47, 21);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "50000";
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // SocketListen
            // 
            this.SocketListen.Location = new System.Drawing.Point(169, 8);
            this.SocketListen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SocketListen.Name = "SocketListen";
            this.SocketListen.Size = new System.Drawing.Size(88, 27);
            this.SocketListen.TabIndex = 2;
            this.SocketListen.Text = "开始监听";
            this.SocketListen.UseVisualStyleBackColor = true;
            this.SocketListen.Click += new System.EventHandler(this.SocketListen_Click);
            // 
            // txtUser
            // 
            this.txtUser.FormattingEnabled = true;
            this.txtUser.Location = new System.Drawing.Point(271, 13);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(98, 20);
            this.txtUser.TabIndex = 3;
            this.txtUser.SelectedIndexChanged += new System.EventHandler(this.txtUser_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Location = new System.Drawing.Point(9, 43);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(359, 161);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "消息日志";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(4, 18);
            this.txtLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(353, 140);
            this.txtLog.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMessage);
            this.groupBox2.Location = new System.Drawing.Point(9, 207);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(359, 161);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "消息";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(4, 18);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(353, 140);
            this.txtMessage.TabIndex = 0;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(8, 410);
            this.txtPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPath.Multiline = true;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(222, 24);
            this.txtPath.TabIndex = 6;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(13, 376);
            this.txtSend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(355, 23);
            this.txtSend.TabIndex = 7;
            this.txtSend.Text = "发送";
            this.txtSend.UseVisualStyleBackColor = true;
            this.txtSend.Click += new System.EventHandler(this.txtSend_Click);
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(233, 410);
            this.SelectFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(67, 23);
            this.SelectFile.TabIndex = 8;
            this.SelectFile.Text = "选择文件";
            this.SelectFile.UseVisualStyleBackColor = true;
            // 
            // SendFile
            // 
            this.SendFile.Location = new System.Drawing.Point(304, 410);
            this.SendFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SendFile.Name = "SendFile";
            this.SendFile.Size = new System.Drawing.Size(64, 23);
            this.SendFile.TabIndex = 9;
            this.SendFile.Text = "发送文件";
            this.SendFile.UseVisualStyleBackColor = true;
            // 
            // SocketServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 442);
            this.Controls.Add(this.SendFile);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.SocketListen);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SocketServer";
            this.Text = "Socker服务端";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button SocketListen;
        private System.Windows.Forms.ComboBox txtUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button txtSend;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Button SendFile;
    }
}

