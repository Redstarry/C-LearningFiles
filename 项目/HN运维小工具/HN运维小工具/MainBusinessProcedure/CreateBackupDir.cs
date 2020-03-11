using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace HN运维小工具.MainBusinessProcedure
{
    
    class CreateBackupDir
    {
        public string DirPath { get; set; }

        /// <summary>
        /// 创建数据库备份文件夹
        /// </summary>
        /// <param name="textLog"></param>
        public void CreateDir(TextBox textLog)
        {
            DatabaseMigration databaseMigration = new DatabaseMigration();
            string name = DateTime.Now.Year + "数据库备份";
            DirPath = @"E:\" + name;
            DirectoryInfo dir = new DirectoryInfo(DirPath);
            //textLog.Text += DateTime.Now.ToString("F") + $" 检查{name}文件夹...\r\n";
            databaseMigration.SetText(textLog, " 检查" + "‘" +name  + "’" + "文件夹...");


            if (!dir.Exists)
            {
                //Thread.Sleep(1000);
                //textLog.Text += DateTime.Now.ToString("F") + $" name文件夹正在创建...\r\n";
                databaseMigration.SetText(textLog, "‘" + name + "’" + "文件夹正在创建...");
                dir.Create();
                //textLog.Text += DateTime.Now.ToString("F") + $" name文件夹创建成功...\r\n";
                databaseMigration.SetText(textLog, "‘" + name + "’" + "文件夹创建成功...");

            }
            else
            {
                //textLog.Text += DateTime.Now.ToString("F") + $" name文件夹已存在。\r\n";
                databaseMigration.SetText(textLog, "‘" + name + "’" + "文件夹已存在。");

            }
            
        }
    }
}
