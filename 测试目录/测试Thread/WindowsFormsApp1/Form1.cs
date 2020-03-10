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
using System.IO;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string mdfFile = String.Empty;
        string ldfFile = String.Empty;
        string mdfFileName = String.Empty;
        string ldfFileName = String.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connec = @"server = .;database = CatactDB;uid = sa;pwd=123";
            string sql = @"select filename from master..sysaltfiles where name like 'shj%'";
            GetDataBasePath(connec, sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connec = @"server = .;database = master;uid = sa;pwd=123";
            string sql = @"select filename from master..sysaltfiles where name like 'CatactDb%'";
            GetDataBasePath(connec, sql);
            string sqlExec = @"exec sp_detach_db CatactDb ";
            string SqlExecAtta = @"exec sp_attach_db CatactDb, " + "'" + mdfFile + "'";
            #region 第一种方式
            {
                Action func1 = () =>
                 {
                     Console.WriteLine($"开始分离数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                     ExecSql(connec, sqlExec);
                     Console.WriteLine($"结束分离数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                 };
                func1.BeginInvoke(action3 =>
                {
                    Console.WriteLine($"开始附加数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                    ExecSql(connec, SqlExecAtta);
                    Console.WriteLine($"结束附加数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                }, null);
            }
            #endregion

            #region 第二种
            {
                Action action1 = () =>
                {
                    Console.WriteLine($"开始分离数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                    ExecSql(connec, sqlExec);
                    Console.WriteLine($"结束分离数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                };
                Action action2 = () =>
                {
                    Console.WriteLine($"开始附加数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                    ExecSql(connec, SqlExecAtta);
                    Console.WriteLine($"结束附加数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                };
            
                ThreadStart threadStart1 = () =>
                {
                    //Console.WriteLine($"开始附加数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                    //ExecSql(connec, SqlExecAtta);
                    //Console.WriteLine($"结束分离数据库,{DateTime.Now.ToString("HH:mm:ss.fff")}");
                    action1.Invoke();
                    action2.Invoke();
                };
                Thread thread1 = new Thread(threadStart1);
                thread1.Start();
            }
            #endregion

        }

        public void GetDataBasePath(string Connection,string sql)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection(Connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
            
            sqlDataAdapter.TableMappings.Add("Table", "path");
            sqlDataAdapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables["path"].Rows)
            {
                string name = row.ItemArray[0].ToString();
                //Console.WriteLine(Path.GetExtension(name));
                if (Path.GetExtension(name) == ".mdf")
                {
                    mdfFile = name;
                    mdfFileName = Path.GetFileNameWithoutExtension(mdfFile);
                    //Console.WriteLine(mdfFileName);
                }
                else
                {
                    ldfFile = name;
                    ldfFileName = Path.GetFileNameWithoutExtension(ldfFile);
                    //Console.WriteLine(ldfFileName);
                }
            }

        }
        public void ExecSql(string Connection, string Sql)
        {
            SqlConnection sqlConnection = new SqlConnection(Connection);
            SqlCommand cmd = new SqlCommand(Sql, sqlConnection);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
