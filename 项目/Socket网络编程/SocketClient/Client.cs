using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketClient
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        Socket socketCommuiation = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void txtConnec_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(txtIP.Text.Trim());
            EndPoint endPoint = new IPEndPoint(ip, int.Parse(txtPort.Text.Trim()));
            socketCommuiation.Connect(endPoint);
            ShowLog("连接成功");
            ReceiveInformation();
        }

        public void ShowLog(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                Action<string> action = (message) => { txtLog.AppendText($"{DateTime.Now.ToString("F")} {message}"); };
                txtLog.Invoke(action, msg);
            }
            else
            {
                txtLog.AppendText($"{DateTime.Now.ToString("F")} {msg} \r\n");
            }
        }
        public void ShowMessage(string message,string use)
        {
            if (txtMsg.InvokeRequired)
            {
                Action<string,string> action = (msg,user) => { txtMsg.AppendText($"\r\n {DateTime.Now.ToString("F")} {user} \r\n {msg} "); };
                txtMsg.Invoke(action, message,use);
            }
            else
            {
                txtMsg.AppendText($"\r\n {DateTime.Now.ToString("F")} \r\n {message} ");
            }
            
        }

        private void txtSend_Click(object sender, EventArgs e)
        {
            byte[] Data = new byte[1024 * 1024 * 2];
            string mes = txtStr.Text.Trim();
            ShowMessage(mes, socketCommuiation.RemoteEndPoint.ToString());
            Data = Encoding.UTF8.GetBytes(mes);
            socketCommuiation.Send(Data);
            txtStr.Text = "";
        }

        public void ReceiveInformation()
        {
            Task.Run(()=> {
                while (true)
                {
                    byte[] Data = new byte[1024 * 1024 * 10];
                    int Result = socketCommuiation.Receive(Data);
                    string message = String.Empty;
                    if (Result <= 0)
                    {
                        socketCommuiation.Shutdown(SocketShutdown.Both);
                        socketCommuiation.Close();
                        return;
                    }
                    switch (Data[0])
                    {
                        case 0:
                            message = Encoding.UTF8.GetString(Data, 1, Result - 1);
                            ShowMessage(message, socketCommuiation.RemoteEndPoint.ToString());
                            break;
                        case 1:
                            GetFile(Result,Data);
                            message = Encoding.UTF8.GetString(Data, 1, Result - 1);
                            ShowMessage(message, socketCommuiation.RemoteEndPoint.ToString());
                            break;
                    }
                }
            });
            
        }
        public void GetFile(int Result, byte[] Data)
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"E:\";
            saveFileDialog.Filter = "所有文件|*.*|文本文件|*.txt|Word文档|*.doc";
            saveFileDialog.Title = "另存为";
           
            saveFileDialog.ShowDialog(this);

            string path = saveFileDialog.FileName;
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Write);
            fileStream.Write(Data, 1, Result-1);
            MessageBox.Show("保存成功");
        }
    }
}
