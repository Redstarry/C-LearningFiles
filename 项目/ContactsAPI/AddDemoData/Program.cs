using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AddDemoData
{
    class Program
    {
        private static readonly object obj = new object();
        static void Main(string[] args)
        {
            
            SqlConnection connec = new SqlConnection("server = .;database =ContactInformation ;uid = sa;pwd = 123");
            List<Task> tasks = new List<Task>();
            TaskFactory taskFactory = new TaskFactory();
            connec.Open();

            for (int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    Console.WriteLine(i);
                    Random rdom = new Random();
                    string name = "Demo" + i;
                    int a = rdom.Next(0, 2);
                    string sql = $"Insert into DemoData values('{name}',{a})";
                    SqlCommand cmd = new SqlCommand(sql, connec);
                    cmd.ExecuteNonQuery();
                }));
                Task.WaitAll(tasks.ToArray());
            }

            Console.ReadKey();
        }
    }
}
