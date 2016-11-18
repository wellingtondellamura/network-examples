using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class FrmMain : Form
    {
        private TcpClient tcpClient;
        StreamWriter socketWriter;
        StreamReader socketReader;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            try
            {
                string host = txtIP.Text;
                int port = Int32.Parse(txtPort.Text);
                tcpClient.Connect(host, port);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(this, "Server not found.");
                return;
            }
            NetworkStream stream = tcpClient.GetStream();
            socketWriter = new System.IO.StreamWriter(stream);
            socketReader = new System.IO.StreamReader(stream);
            MessageBox.Show(this, "Connected.");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                tcpClient.Client.Disconnect(false);
                tcpClient = null;
                MessageBox.Show(this, "Disconnected.");
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string requestStr = "reg;"+txtName.Text;
            socketWriter.Write(requestStr);
            socketWriter.Flush();
            string responseStr = socketReader.ReadLine();
            MessageBox.Show(this, responseStr);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string requestStr = "msg;" + txtMessage.Text;
            socketWriter.Write(requestStr);
            socketWriter.Flush();
            string responseStr = socketReader.ReadLine();
            MessageBox.Show(this, responseStr);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMessageServer.Clear();
            List<String> lst = new List<string>();
            string requestStr = "lst;1";
            socketWriter.Write(requestStr);
            socketWriter.Flush();
            string responseStr = socketReader.ReadLine();
            string[] messages = responseStr.Split('#');
            foreach(string msg in messages)
            {                
                string[] s = msg.Split('|');
                if (s.Length >= 3)
                {
                    lst.Add(string.Format("[{0}] {1}", s[0], s[1]));
                    lst.Add(s[2]);
                }                
            }
            txtMessageServer.Lines = lst.ToArray();
        }
    }
}
