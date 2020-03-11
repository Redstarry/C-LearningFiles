using HN运维小工具.MainBusinessProcedure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace HN运维小工具
{
    public partial class MianForm : Form
    {
        public MianForm()
        {
            InitializeComponent();
            
        }

        private void CreateBackUpDir_Click(object sender, EventArgs e)
        {
            CreateBackupDir CBU = new CreateBackupDir();
            CBU.CreateDir(TextLog);
            TextLog.AppendText("--------------------------------------------------------------------------\r\n");

        }
        
        private void BtnTransferFIle_Click(object sender, EventArgs e)
        {
            DatabaseMigration DM = new DatabaseMigration();
            //DM.Interface(TextLog);
            DM.SetConfig();
            Task task = new Task(() =>
            {
                DM.Detach(TextLog);
                DM.TransferFile(TextLog);
                DM.Attach(TextLog);
                DM.SetText(TextLog, "---------------------------------------------------\r\n");
            });
            task.Start();
            
        }
    }
}
