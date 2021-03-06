﻿using System;
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
        /// <summary>
        /// 添加IP集合的跨线程方法。
        /// </summary>
        public void AddUser()
        {
            if (txtUser.InvokeRequired)
            {
                Action action = () => { txtUser.Items.Add(socketCommunication.RemoteEndPoint.ToString()); txtUser.Text = socketCommunication.RemoteEndPoint.ToString(); };
                txtUser.Invoke(action, null);
            }
            else
            {
                txtUser.Items.Add(socketCommunication.RemoteEndPoint.ToString());
                txtUser.Text = socketCommunication.RemoteEndPoint.ToString();
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
            List<byte> newBuffer = new List<byte>();
            newBuffer.Add(0);
            newBuffer.AddRange(Data);
            
            Socket name = CommunicationDic[txtUser.SelectedItem.ToString()];
            ShowMessage(txtPath.Text.Trim(), txtUser.SelectedItem.ToString());
            name.Send(newBuffer.ToArray());
            txtPath.Text = "";
            
        }

        private void txtUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"E:\";
            openFileDialog.Filter = "所有文件|*.*|文本文件|*.txt|Word文档|*.doc";
            openFileDialog.ShowDialog();
            txtPath.Text = openFileDialog.FileName;
        }

        private void SendFile_Click(object sender, EventArgs e)
        {
            //FileStream fileStream = new FileStream(txtPath.Text.Trim(), FileMode.Open, FileAccess.Read);
            //byte[] Data = new byte[1024 * 1024 * 10];
            //int Length = fileStream.Read(Data, 0, Data.Length);
            //List<byte> byteList = new List<byte>();
            //byteList.Add(1);
            //byteList.AddRange(Data);

            //Socket name = CommunicationDic[txtUser.SelectedItem.ToString()];
            //name.Send(byteList.ToArray(), 0, Length + 1, SocketFlags.None);
            //string fileName = txtPath.Text;
            //ShowMessage($"{Path.GetFileNameWithoutExtension(fileName)}", txtUser.SelectedItem.ToString());
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = @"E:\";
                ofd.Filter = "所有文件|*.*|文本文件|*.txt|Word文档|*.doc";
                if (ofd.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
               
                //txtPath.Text = ofd.FileName;
                byte[] buffer = File.ReadAllBytes(ofd.FileName);
                byte[] NewBuffer = new byte[buffer.Length + 1];
                NewBuffer[0] = 1;
                Buffer.BlockCopy(buffer, 0, NewBuffer, 1, buffer.Length);
                CommunicationDic[txtUser.SelectedItem.ToString()].Send(NewBuffer);
            }
        }
    }
}
