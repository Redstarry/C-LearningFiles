using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace HNTool
{
    class MainForm
    {
        
        public static void Interface()
        {
            MainInterface();
            MenuSelection();

        }
        /// <summary>
        /// 功能界面
        /// </summary>
        public static void MainInterface()
        {
            Console.WriteLine("***********************************************");
            Console.WriteLine("*                                             *");
            Console.WriteLine("*                慧能运维工具                 *");
            Console.WriteLine("*                                             *");
            Console.WriteLine("*        1.检查并创建数据库备份文件夹         *");
            Console.WriteLine("*                                             *");
            Console.WriteLine("*        2.移植数据库物理文件                 *");
            Console.WriteLine("*                                             *");
            Console.WriteLine("*        3.退出                               *");
            Console.WriteLine("*                                             *");
            Console.WriteLine("***********************************************");
        }

        /// <summary>
        /// 选择功能方法
        /// </summary>
        public static void MenuSelection()
        {
            Directories directories = new Directories();
            string backupDirName = DateTime.Now.Year + "年数据库备份";
            string DataBaseMDF = "HNSqldata";
            bool onoff = true;
            do
            {
                Console.WriteLine();
                Console.Write("请输入执行功能的编号：");
                string ReadLineNum = Console.ReadLine();
                switch (ReadLineNum)
                {
                    case "1":
                        directories.CreateDir(backupDirName);
                        break;
                    case "2":
                        directories.CreateDir(DataBaseMDF);
                        directories.MigratingDatabases();
                        break;
                    case "3":
                        onoff = false;
                        break;
                    default:
                        Console.WriteLine("你输入的编号错误，请重新输入...");
                        break;
                }

            } while (onoff);
        }
    }
}
