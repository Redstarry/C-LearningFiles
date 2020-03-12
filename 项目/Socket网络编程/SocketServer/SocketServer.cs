using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketServer
{
    public partial class SocketServer : Form
    {
        public SocketServer()
        {
            InitializeComponent();
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {

        }


        Socket socketCommunication = null;
        Dictionary<string,Socket> CommunicationDic = new Dictionary<string, Socket>();
        private void SocketListen_Click(object sender, EventArgs e)
        {
            Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var IP = IPAddress.Parse(txtIP.Text.Trim());
            IPEndPoint point = new IPEndPoint(IP, Convert.ToInt32(txtPort.Text.Trim()));


            socketWatch.Bind(point);
            ShowLog("监听开始");

            socketWatch.Listen(10);

            
            Task.Run(() => {
                while (true)
                {
                    try
                    {
                        socketCommunication = socketWatch.Accept();
                        CommunicationDic.Add(socketCommunication.RemoteEndPoint.ToString(), socketCommunication);
                        AddUser();
                        ShowLog($"{socketCommunication.RemoteEndPoint.ToString()}: 连接成功");
                        Task.Run(() =>
                        {
                            byte[] Data = new byte[1024 * 1024 * 2];
                            while (true)
                            {
                                try
                                {
                                    int Result = socketCommunication.Receive(Data);
                                    if (Result <= 0)
                                    {
                                        socketCommunication.Shutdown(SocketShutdown.Both);
                                        socketCommunication.Close();
                                        return;
                                    }
                                    string message = Encoding.UTF8.GetString(Data);
                                    ShowMessage(message, $"{socketCommunication.RemoteEndPoint.ToString()}");
                                }
                                catch (Exception)
                                {

                                }
                            }
                        });
                    }
                    catch (Exception)
                    { 
                    
                    }
                }
            });
            
            
            
        }

        public void AddUser()
        {
            if (txtUser.InvokeRequired)
            {
                Action action = () => { txtUser.Items.Add(socketCommunication.RemoteEndPoint.ToString()); };
                txtUser.Invoke(action, null);
            }
            else
            {
                txtUser.Items.Add(socketCommunication.RemoteEndPoint.ToString());
            }
        }
        /// <summary>
        /// 显示日志信息，解决了跨线程问题
        /// </summary>
        /// <param name="msg"></param>
        void ShowLog(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                Action<string> action = (message) => { txtLog.AppendText($"{DateTime.Now.ToString("F")} {msg} \r\n"); };
                txtLog.Invoke(action, msg);
            }
            else
            {
                txtLog.AppendText($"{DateTime.Now.ToString("F")} {msg} \r\n");
            }
        }
        /// <summary>
        /// 显示接受的消息，解决了跨线程问题
        /// </summary>
        /// <param name="message"></param>
        /// <param name="user"></param>
        void ShowMessage(string message,string user)
        {
            if (txtMessage.InvokeRequired)
            {
                Action<String,string> action = (msg,use) => { txtMessage.AppendText($"\r\n {DateTime.Now.ToString("F")} {use} \r\n {msg} "); };
                txtMessage.Invoke(action, message, user);
            }
            else
            {
                txtMessage.AppendText($"\r\n {DateTime.Now.ToString("F")} {user} \r\n {message} ");
            }
        }

        private void txtSend_Click(object sender, EventArgs e)
        {
            byte[] Data = new byte[1024 * 1024 * 2];
            Data = Encoding.UTF8.GetBytes(txtPath.Text.Trim());
            
            Socket name = CommunicationDic[txtUser.SelectedItem.ToString()];
            ShowMessage(txtPath.Text.Trim(), txtUser.SelectedItem.ToString());
            name.Send(Data);
            txtPath.Text = "";
            
        }

        private void txtUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
