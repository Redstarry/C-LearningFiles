using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoubleChromosphere
{
    class Business
    {
        public static bool onoff { get; set; }
        private static readonly object lockobject = new object();
        /// <summary>
        /// 判断该随机数是否是已重复的值，若重复就重新获取随机数，反之把该值赋值给Label.
        /// </summary>
        /// <param name="label">控件</param>
        /// <param name="randomNum">随机数</param>
        /// <param name="groupBox">label的父控件</param>
        public void SetLableValue(Label label, string randomNum, GroupBox groupBox)
        {
                lock (lockobject)//防止每个球的数字重复，获取Text的值形成一个list，然后判断随机出来的数字在list里面没有，如果有，说明就是重复值，name就需要重新随机了
                {
                    List<string> list = GetLableValue(groupBox);
                    if (!list.Contains(randomNum))
                    {
                        Thread.Sleep(5);
                        label.Invoke(new Action(() => { label.Text = randomNum; }));
                    }
                }
        }
        /// <summary>
        /// 获取 label控件的值，用于判断该数字是否存在
        /// </summary>
        /// <param name="groupBox">label的父控件</param>
        /// <returns></returns>
        public List<string> GetLableValue(GroupBox groupBox)
        {
            List<string> LableValueList = new List<string>();
            foreach (var item in groupBox.Controls)
            {
                if (item is Label)
                {
                    Label label = (Label)item;
                    LableValueList.Add(label.Text);
                }
            }
            return LableValueList;
        }
        /// <summary>
        /// 把最后的结果已弹窗的形式显示出来
        /// </summary>
        /// <param name="LableName"></param>
        public void ShowResult(List<Label> LableName)
        {
            MessageBox.Show($"{LableName[0].Text}, {LableName[1].Text}, {LableName[2].Text}, {LableName[3].Text}, {LableName[4].Text}, {LableName[5].Text}, {LableName[6].Text}");
        }
    }
}
