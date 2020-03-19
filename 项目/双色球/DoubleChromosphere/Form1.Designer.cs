namespace DoubleChromosphere
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.num07 = new System.Windows.Forms.Label();
            this.num05 = new System.Windows.Forms.Label();
            this.num04 = new System.Windows.Forms.Label();
            this.num02 = new System.Windows.Forms.Label();
            this.num06 = new System.Windows.Forms.Label();
            this.num03 = new System.Windows.Forms.Label();
            this.num01 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEnd);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.num07);
            this.groupBox1.Controls.Add(this.num05);
            this.groupBox1.Controls.Add(this.num04);
            this.groupBox1.Controls.Add(this.num02);
            this.groupBox1.Controls.Add(this.num06);
            this.groupBox1.Controls.Add(this.num03);
            this.groupBox1.Controls.Add(this.num01);
            this.groupBox1.Location = new System.Drawing.Point(13, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 271);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "双色球";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(450, 181);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(98, 46);
            this.btnEnd.TabIndex = 8;
            this.btnEnd.Text = "结束";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(210, 181);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(98, 46);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // num07
            // 
            this.num07.AutoSize = true;
            this.num07.Location = new System.Drawing.Point(582, 104);
            this.num07.Name = "num07";
            this.num07.Size = new System.Drawing.Size(26, 18);
            this.num07.TabIndex = 6;
            this.num07.Text = "00";
            // 
            // num05
            // 
            this.num05.AutoSize = true;
            this.num05.Location = new System.Drawing.Point(432, 104);
            this.num05.Name = "num05";
            this.num05.Size = new System.Drawing.Size(26, 18);
            this.num05.TabIndex = 5;
            this.num05.Text = "00";
            // 
            // num04
            // 
            this.num04.AutoSize = true;
            this.num04.Location = new System.Drawing.Point(357, 104);
            this.num04.Name = "num04";
            this.num04.Size = new System.Drawing.Size(26, 18);
            this.num04.TabIndex = 4;
            this.num04.Text = "00";
            // 
            // num02
            // 
            this.num02.AutoSize = true;
            this.num02.Location = new System.Drawing.Point(207, 104);
            this.num02.Name = "num02";
            this.num02.Size = new System.Drawing.Size(26, 18);
            this.num02.TabIndex = 3;
            this.num02.Text = "00";
            // 
            // num06
            // 
            this.num06.AutoSize = true;
            this.num06.Location = new System.Drawing.Point(507, 104);
            this.num06.Name = "num06";
            this.num06.Size = new System.Drawing.Size(26, 18);
            this.num06.TabIndex = 2;
            this.num06.Text = "00";
            // 
            // num03
            // 
            this.num03.AutoSize = true;
            this.num03.Location = new System.Drawing.Point(282, 104);
            this.num03.Name = "num03";
            this.num03.Size = new System.Drawing.Size(26, 18);
            this.num03.TabIndex = 1;
            this.num03.Text = "00";
            // 
            // num01
            // 
            this.num01.AutoSize = true;
            this.num01.Location = new System.Drawing.Point(132, 104);
            this.num01.Name = "num01";
            this.num01.Size = new System.Drawing.Size(26, 18);
            this.num01.TabIndex = 0;
            this.num01.Text = "00";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 315);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label num01;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label num07;
        private System.Windows.Forms.Label num05;
        private System.Windows.Forms.Label num04;
        private System.Windows.Forms.Label num02;
        private System.Windows.Forms.Label num06;
        private System.Windows.Forms.Label num03;
    }
}

