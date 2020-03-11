using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace HN运维小工具.MainBusinessProcedure
{
    class DatabaseMigration
    {
        public string LdfFileName { get; set; }
        public string LdfFile { get; set; }
        public string OldDirPath { get; set; }
        public string FileName { get; set; }

        public string DirPath { get; set; }

        /// <summary>
        /// 数据库的连接地址
        /// </summary>
        public string DataBaseServer { get; set; }

        /// <summary>
        /// 数据库名字
        /// </summary>
        public string DataBaseName { get; set; }
        /// <summary>
        /// 数据库登录id
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 数据库登录密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 数据库的连接字符串
        /// </summary>
        public string connectionStr { get; private set; }


        MianForm mianForm = new MianForm();
        public DatabaseMigration()
        {
            DataBaseServer = ".";
            DataBaseName = "yc2019";
            Uid = "sa";
            Pwd = "123";
            DirPath = @"E:\HNSqlData";
            //OldDirPath = @"C:\一卡通收购管理系统_凉山\PurClientControl\hndata\data.mdf";
        }

        public void Interface(TextBox textBox)
        {
            SetConfig();
            Task task = new Task(() => 
            {
                Detach(textBox);
                TransferFile(textBox);
                Attach(textBox);
            });
            task.Start();
            

            
        }
        delegate void SetTextCallback(TextBox textbox, string message);
        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="message"></param>
        public void SetText(TextBox textBox, string message)
        {
            if (textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                textBox.Invoke(d, new object[] { textBox, message });
            }
            else
            {
                textBox.AppendText(DateTime.Now.ToString("F")+ " " + message + "\r\n");
            }
        }

        /// <summary>
        /// 设置配置文件中的数据库连接
        /// </summary>
        public string SetConfig() => connectionStr = $"server = {DataBaseServer}; database = master;uid = {Uid};pwd = {Pwd}";

        /// <summary>
        /// 创建文件夹和移动MDF、LDF文件
        /// </summary>
        /// <param name="textBox"></param>
        public void TransferFile(TextBox textBox)
        {
            string FullMDFPath = Path.Combine(DirPath, FileName + ".mdf");
            string FullLDFPath = Path.Combine(DirPath, FileName +"_log"+ ".ldf");
            SetText(textBox, $"检查文件夹{Path.GetFileNameWithoutExtension(DirPath)}是否存在");
            if (!Directory.Exists(DirPath))
            {
                SetText(textBox, $"文件夹{Path.GetFileNameWithoutExtension(DirPath)}-不存在，正在创建...");

                Directory.CreateDirectory(DirPath);
                SetText(textBox, $"{Path.GetFileNameWithoutExtension(DirPath)}文件夹，创建完成。");
                SetText(textBox, $"正在迁移{FileName}文件...");
                File.Copy(OldDirPath, FullMDFPath);
                File.Copy(LdfFile, FullLDFPath);
                SetText(textBox, $"{FileName}文件迁移完成...");

            }
            else
            {
                //textBox.Text += DateTime.Now.ToString("F") + $" {Path.GetDirectoryName(DirPath)}-已存在\r\n";
                SetText(textBox, $"文件夹{Path.GetFileNameWithoutExtension(DirPath)}-已存在，正在检查{FileName}...是否存在 ");
                if (!File.Exists(FullMDFPath))
                {
                    SetText(textBox, $"{FileName}文件-不存在，正在迁移{FileName}文件... ");

                    File.Copy(OldDirPath, FullMDFPath);
                    File.Copy(LdfFile, FullLDFPath);
                    SetText(textBox, $"{FileName}迁移文件完成，准备附加数据库...");

                }
                else
                {
                    //textBox.Text += DateTime.Now.ToString("F") + $" {FileName}-已存在\r\n";
                    SetText(textBox, $"{FileName}-已存在，准备附加数据库...");
                }
                
            }
        }

        /// <summary>
        /// 分离数据库
        /// </summary>
        public void Detach(TextBox textBox)
        {
            var TableName = GetTableNames();
            SqlConnection sqlConnection = new SqlConnection(SetConfig());
            SqlCommand cmd = new SqlCommand(@"exec sp_detach_db " + DataBaseName, sqlConnection);
            
            foreach (DataRow row in TableName)
            {
                if (row["name"].ToString().ToUpper() == DataBaseName.ToUpper()) //还差条件，不然容易误操作。
                {
                    try
                    {
                        sqlConnection.Open();
                        if (sqlConnection.State.ToString() == "Open")
                        {
                            //textBox.Text += DateTime.Now.ToString("F") + $" 分离数据库-{FileName}-开始\r\n";
                            SetText(textBox,$"分离数据库-{FileName}-开始");
                        }
                        cmd.ExecuteNonQuery();

                        sqlConnection.Close();
                        if (sqlConnection.State.ToString() == "Closed")
                        {
                            //textBox.Text += DateTime.Now.ToString("F") + $" 分离数据库-{FileName}-完成\r\n";
                            SetText(textBox,  $"分离数据库-{FileName}-完成");
                        }
                    }
                    catch (SqlException)
                    {

                        throw;
                    }
                }
            }
        }
        //Action<TextBox, string> action = (textBox, message) => textBox.AppendText(DateTime.Now.ToString("F") + message);

        
        //public void Log(TextBox textBox, string message)
        //{
        //    textBox.AppendText(DateTime.Now.ToString("F") + message);
        //}
        /// <summary>
        /// 附加数据库
        /// </summary>
        /// <param name="textBox"></param>
        public void Attach(TextBox textBox)
        {
            if (Directory.Exists(DirPath))
            {
                //文件存在
                //textBox.Text += DateTime.Now.ToString("F") + $" 已找到文件-{FileName}\r\n";
                string MDFFile = @"exec sp_attach_db " + DataBaseName + "," + "'" + DirPath + @"\" + FileName + ".mdf" + "'," + "'" + DirPath + @"\" + LdfFileName + ".ldf'";
                SqlConnection sqlConnection = new SqlConnection(SetConfig());
                SqlCommand cmd = new SqlCommand(MDFFile, sqlConnection);
                sqlConnection.Open();
                if (sqlConnection.State.ToString() == "Open")
                {
                    //textBox.Text += DateTime.Now.ToString("F") + $" 附加数据库-{FileName}-开始\r\n";
                    SetText(textBox, $"附加数据库-{FileName}-开始");
                }
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                if (sqlConnection.State.ToString() == "Closed")
                {
                    //textBox.Text += DateTime.Now.ToString("F") + $" 附加数据库-{FileName}-完成\r\n";
                    SetText(textBox, $"附加数据库-{FileName}-完成");
                }
            }
            else
            {
                //textBox.Text += DateTime.Now.ToString("F") + $" 未找到文件-{FileName}\r\n";
                //SetText(textBox, $" 未找到文件-{FileName}\r\n");
            }
        }
        /// <summary>
        /// 获取全部数据库名
        /// </summary>
        /// <returns></returns>
        public DataRowCollection GetTableNames()
        {
            DataSet dataSet = new DataSet();
            string sql = $"SELECT Name FROM Master..SysDatabases ORDER BY Name; use yc2019 select physical_name from sys.database_files";
            SqlConnection sqlConnection = new SqlConnection(connectionStr);
            SqlDataAdapter sqlData = new SqlDataAdapter(sql, sqlConnection);
            sqlData.TableMappings.Add("Table", "name");
            sqlData.TableMappings.Add("Table1", "path");
            sqlData.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables["path"].Rows)
            {

                if (Path.GetExtension(row["physical_name"].ToString()) == ".mdf")
                {
                    OldDirPath = row["physical_name"].ToString();
                    FileName = Path.GetFileNameWithoutExtension(OldDirPath);
                }
                else
                {
                    LdfFile = row["physical_name"].ToString();
                    LdfFileName = Path.GetFileNameWithoutExtension(LdfFile);
                }
                //if (Path.GetFileName(row["physical_name"].ToString().ToUpper()) == DataBaseName.ToUpper() + ".mdf")
                //{
                //    //OldDirPath = row["physical_name"].ToString();
                //    FileName = Path.GetFileNameWithoutExtension(OldDirPath);
                //}
                //else
                //{
                //    LdfFile = row["physical_name"].ToString();
                //    LdfFileName = Path.GetFileNameWithoutExtension(LdfFile);
                //}
            }
            var row1 = dataSet.Tables["name"].Rows;
            return row1;
        }

        /// <summary>
        /// 获取数据库的物理文件路径
        /// </summary>
        //public void GetOldDirPath() //数据库被占用
        //{
        //    DataTable OldDirPathes = new DataTable();
        //    SqlConnection sqlConnection = new SqlConnection($"Server = .;database = {DataBaseName};uid = sa; pwd = 123");
        //    SqlDataAdapter sqlDataPath = new SqlDataAdapter($"select physical_name from sys.database_files", sqlConnection);
        //    //DataSet dataSet = new DataSet();
        //    sqlDataPath.Fill(OldDirPathes);
        //    foreach (DataRow row in OldDirPathes.Rows)
        //    {
        //        if (Path.GetFileNameWithoutExtension(row["physical_name"].ToString()) == DataBaseName.ToUpper() + ".mdf")
        //        {
        //            OldDirPath = row["physical_name"].ToString();
        //            FileName = Path.GetFileNameWithoutExtension(OldDirPath);
        //        }
        //    }
        //    sqlConnection.Dispose();
        //}
    }
}
