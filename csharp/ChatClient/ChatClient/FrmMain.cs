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
                MessageBox.Show(this, "Servidor não encontrado.");
                return;
            }
            NetworkStream stream = tcpClient.GetStream();
            socketWriter = new System.IO.StreamWriter(stream);
            socketReader = new System.IO.StreamReader(stream);
            MessageBox.Show(this, "Conectado.");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                tcpClient.Client.Disconnect(false);
                tcpClient = null;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string requestStr = "reg;"+txtName.Text;
            socketWriter.Write(requestStr);
            socketWriter.Flush();
            string responseStr = socketReader.ReadToEnd();
            MessageBox.Show(this, responseStr);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string requestStr = "msg;" + txtName.Text;
            socketWriter.Write(requestStr);
            socketWriter.Flush();
            string responseStr = socketReader.ReadToEnd();
            MessageBox.Show(this, responseStr);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string requestStr = "lst";
            socketWriter.Write(requestStr);
            socketWriter.Flush();
            string responseStr = socketReader.ReadToEnd();
            string[] messages = responseStr.Split('#');
            txtMessageServer.Clear();
            txtMessageServer.Lines = messages;
        }
    }
}
