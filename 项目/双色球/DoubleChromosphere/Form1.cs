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
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            Business business = new Business();
            List<Label> labels = new List<Label>() { num01, num02, num03, num04, num05, num06, num07};//Label控件集合
            List<Task> tasks = new List<Task>();//线程集合
            Random random = new Random();
            Business.onoff = true;

            foreach (var item in labels)
            {
                tasks.Add(Task.Run(()=>{
                    if (item.Name != "num07")//判断该控件是否是蓝色球
                    {
                        while (Business.onoff) //数字不停的滚动。
                        {
                            string num = random.Next(1, 34).ToString();
                            business.SetLableValue(item, num, groupBox1);
                        }
                    }
                    else
                    {
                        while (Business.onoff)
                        {
                            string num = random.Next(1, 17).ToString();
                            business.SetLableValue(item, num, groupBox1);
                        }
                    }
                }));
            }
            Task.Factory.ContinueWhenAll(tasks.ToArray(), t => {
                business.ShowResult(labels);
            });

            
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Business.onoff = false;
        }
        
    }
}
