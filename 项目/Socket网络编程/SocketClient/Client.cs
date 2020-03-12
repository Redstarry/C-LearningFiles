﻿using System;
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
        public void ShowMessage(string message)
        {
            if (txtMsg.InvokeRequired)
            {
                Action<string> action = (msg) => { txtMsg.AppendText($"\r\n {DateTime.Now.ToString("F")} \r\n {msg} "); };
                txtMsg.Invoke(action, message);
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
            ShowMessage(mes);
            Data = Encoding.UTF8.GetBytes(mes);
            socketCommuiation.Send(Data);
            txtStr.Text = "";
        }

        public void ReceiveInformation()
        {
            bool Isok = true;
            Task.Run(()=> {
                while (Isok)
                {
                    byte[] Data = new byte[1024 * 1024 * 2];
                    int Result = socketCommuiation.Receive(Data);
                    if (Result <= 0) Isok = false;
                    string message = Encoding.UTF8.GetString(Data);
                    ShowMessage(message);
                }
            });
            
        }
    }
}
