namespace SocketClient
{
    partial class Client
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtConnec = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtStr = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器地址：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(85, 9);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(80, 21);
            this.txtIP.TabIndex = 1;
            this.txtIP.Text = "192.168.1.4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口号：";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(218, 9);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(53, 21);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "50000";
            // 
            // txtConnec
            // 
            this.txtConnec.Location = new System.Drawing.Point(277, 8);
            this.txtConnec.Name = "txtConnec";
            this.txtConnec.Size = new System.Drawing.Size(75, 23);
            this.txtConnec.TabIndex = 4;
            this.txtConnec.Text = "打开连接";
            this.txtConnec.UseVisualStyleBackColor = true;
            this.txtConnec.Click += new System.EventHandler(this.txtConnec_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Location = new System.Drawing.Point(13, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 98);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "消息日志";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(7, 21);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(326, 71);
            this.txtLog.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMsg);
            this.groupBox2.Location = new System.Drawing.Point(13, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 151);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "消息";
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(7, 21);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.Size = new System.Drawing.Size(326, 124);
            this.txtMsg.TabIndex = 0;
            // 
            // txtStr
            // 
            this.txtStr.Location = new System.Drawing.Point(20, 293);
            this.txtStr.Multiline = true;
            this.txtStr.Name = "txtStr";
            this.txtStr.Size = new System.Drawing.Size(234, 29);
            this.txtStr.TabIndex = 7;
            this.txtStr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(271, 295);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(75, 23);
            this.txtSend.TabIndex = 8;
            this.txtSend.Text = "发送";
            this.txtSend.UseVisualStyleBackColor = true;
            this.txtSend.Click += new System.EventHandler(this.txtSend_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 339);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtStr);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtConnec);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label1);
            this.Name = "Client";
            this.Text = "SocketClient";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button txtConnec;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtStr;
        private System.Windows.Forms.Button txtSend;
    }
}

