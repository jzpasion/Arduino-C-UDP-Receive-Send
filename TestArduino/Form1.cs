using System;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestArduino
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Connect_Click(object sender, EventArgs e)
        {
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(txtName.Text, Convert.ToInt32(txtPort.Text));
            Byte[] senddata = Encoding.ASCII.GetBytes(txtMsg.Text);
            udpClient.Send(senddata, senddata.Length);
         

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thdUdpServer = new Thread(new ThreadStart(serverThread));
            thdUdpServer.Start();
        }

        public void serverThread()
        {
           
            UdpClient udpClient = new UdpClient(8888);
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 8888);
                Byte[] receivebytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receivebytes);

                MessageBox.Show(returnData.ToString().ToUpper().Replace(" ",""));
                this.Invoke(new MethodInvoker(delegate ()
                {
                    listReceive.Items.Add(RemoteIpEndPoint.ToString() + " : " + returnData.ToString()) ;
                    listReceive.SelectedIndex = listReceive.Items.Count - 1;
                    listReceive.SelectedIndex = -1;
                }));
            }
        }
    }
}
