using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HNTool
{
    class Directories
    {

        /// <summary>
        /// Master数据库的连接
        /// </summary>
        public string MasterConnection { get; set; }
        /// <summary>
        /// 源文件夹地址
        /// </summary>
        public string SourceDirPath { get; set; }
        /// <summary>
        /// 目标文件夹地址
        /// </summary>
        public string TargetDirPath { get; set; }

        public void Interface()
        {
            //TransferDataFile();
        }
        /// <summary>
        /// 判断文件夹是否存在，若不存在就创建，反之提示用于已存在。
        /// </summary>
        /// <param name="DirName">文件夹名</param>
        public void CreateDir(string DirName)
        {
            bool onoff = true;
            string DirNamePath = @"D:\";
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(DirNamePath, DirName));
            do
            {
                if (!directoryInfo.Exists)
                {
                    Console.WriteLine();
                    Console.WriteLine($"正在检查文件夹--{DirName}...");
                    directoryInfo.Create();
                    onoff = false;
                    Thread.Sleep(2000);
                    Console.WriteLine();
                    Console.WriteLine($"{DirName}--创建完成...");

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"{DirName} -- 已存在.");
                    onoff = false;
                }
            } while (onoff);
        }

        /// <summary>
        /// 对数据库进行迁移
        /// </summary>
        public  void MigratingDatabases()
        {
            DetachOrAttach detachOrAttach = new DetachOrAttach();
            SourceDirPath = @"C:\一卡通收购管理系统_凉山\PurClientControl\hndata\data.MDF";
            TargetDirPath = @"D:\HNSqldata\data.MDF";
            detachOrAttach.SelectTableName = @"SELECT * FROM Master..SysDatabases ORDER BY Name;";
            detachOrAttach.ConnecStr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
           
            foreach (DataRow row in detachOrAttach.GetTableNames().Rows)
            {
                if (row.Table.TableName == "data")
                {
                    detachOrAttach.Sql = @"exec sp_detach_db data";
                    detachOrAttach.DetachOrAttachData();
                }
            }

            File.Copy(SourceDirPath, TargetDirPath);

            detachOrAttach.Sql = @"exec sp_attach_db data,'D:\HNSqldata\data.MDF'";
            detachOrAttach.DetachOrAttachData();
        }
    }
}
