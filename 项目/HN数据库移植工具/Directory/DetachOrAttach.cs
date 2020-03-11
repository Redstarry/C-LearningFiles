using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HNTool
{
    class DetachOrAttach
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnecStr { get; set; }

        /// <summary>
        /// 附加和分离的sql语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 查询所有的数据库名的SQL语句。
        /// </summary>
        public string SelectTableName { get; set; }

        /// <summary>
        /// 处理数据库，分离和附加
        /// </summary>
        public int DetachOrAttachData()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnecStr);
            SqlCommand cmd = new SqlCommand(Sql, sqlConnection);
            try
            {
                sqlConnection.Open();
                int result = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取数据库中的所有数据库名
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableNames()
        {
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["master"].ConnectionString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SelectTableName, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
