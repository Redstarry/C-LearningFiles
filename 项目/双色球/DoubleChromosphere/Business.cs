using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoubleChromosphere
{
    class Business
    {
        public bool onoff { get; set; }
        public void ThreadLable(Label label, Random random, int num)
        {
            
        }
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
    }
}
