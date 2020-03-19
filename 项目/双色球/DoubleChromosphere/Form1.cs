using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoubleChromosphere
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        Business business = new Business();
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private static readonly object lockobject = new object();
        private void btnStart_Click(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label>() { num01, num02, num03, num04, num05, num06, num07};
            List<Task> tasks = new List<Task>();
            business.onoff = true;
            Random random = new Random();
            foreach (var item in labels)
            {
                tasks.Add(Task.Run(()=>{
                    if (item.Name != "num07")
                    {
                        while (business.onoff)
                        {
                            string num = random.Next(1, 34).ToString();
                            lock (lockobject)//防止每个球的数字重复，获取Text的值形成一个list，然后判断随机出来的数字在list里面没有，如果有，说明就是重复值，name就需要重新随机了
                            {
                                List<string> list = business.GetLableValue(groupBox1);
                                if (!list.Contains(num))
                                {
                                    Thread.Sleep(5);
                                    item.Invoke(new Action(() => { item.Text = num; }));
                                }
                            }
                        }
                    }
                    else
                    {
                        while (business.onoff)
                        {
                            string num = random.Next(1, 17).ToString();
                            lock (lockobject)
                            {
                                List<string> list = business.GetLableValue(groupBox1);
                                if (!list.Contains(num))
                                {
                                    Thread.Sleep(5);
                                    item.Invoke(new Action(() => { item.Text = num; }));
                                }
                            }
                        }
                    }
                    //Task.Factory.ContinueWhenAll(tasks.ToArray(),t => {
                    //    ShowResult();
                    //});
                }));
                
            }

            
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            business.onoff = false;
        }
        public void ShowResult()
        {
            MessageBox.Show($"{num01}, {num02}, {num03}, {num04}, {num05},{num06}, {num07}");
        }
    }
}
