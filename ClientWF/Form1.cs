using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWF
{
    public partial class Form1 : Form
    {
        private ClientWebSocket client;
        public Form1()
        {
            InitializeComponent();
            sendBtn.Click += SendClick;
            ConnectBtn.Click += ConnectClick;
        }

        private async void SendClick(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(inputTB.Text);
                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, false, CancellationToken.None);
                listBox1.Items.Add("You: " + inputTB.Text);
            }
            catch
            {
                listBox1.Items.Add("Cannot connect to server!");
            }
        }

        private async void ConnectClick(object sender, EventArgs e)
        {
            try
            {
                if (client == null)
                {
                    client = new ClientWebSocket();
                    await client.ConnectAsync(new Uri("ws://" + ipTB.Text), CancellationToken.None);
                    listBox1.Items.Add("You've connected to the server!");
                    Receiver();
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Cannot connect to server!");
            }
        }

        private async void Receiver()
        {
            while (client.State == WebSocketState.Open)
            {
                byte[] buffer = new byte[1024];
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                listBox1.Items.Add("Server: " + Encoding.UTF8.GetString(buffer).TrimEnd('\0'));
            }
        }
    }
}
