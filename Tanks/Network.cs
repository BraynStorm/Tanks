using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tanks {
    class Network {
        IPHostEntry localhost = Dns.GetHostEntry("localhost");

        public bool isServer { get;  private set; }
        public bool isConnected { get; private set; }

        private TcpClient client;
        private TcpListener listener;

        private Packet packet;

        public Network(bool isServer = false) {
            this.isServer = isServer;
        }

        public void connect(string hostname, int port) {
            client = new TcpClient(hostname, port);
            
        }

        public void disconnect() {
            isConnected = false;
            client.Close();
        }

        public void accept(int port) {
            listener = new TcpListener(port);
            listener.BeginAcceptTcpClient(new AsyncCallback(asyncAccept), listener);
        }

        public void sendPacket(byte id, byte[] data) {
            socket.Send(new byte[] { id, (byte) data.Length });
            socket.Send(data);
        }

        public void sendData(byte[] data) {
            socket.Send(data);
        }

        // !Blocking!
        public void receivePacket() {

            Packet packet = new Packet(client);

            client.GetStream().BeginRead(packet.buffer.getArray(), 0, 1024, new AsyncCallback(asyncReceive), packet);
        }

        private void asyncReceive(IAsyncResult res) {
            Packet packet = (Packet)res.AsyncState;

            int readSoFar = packet.client.GetStream().EndRead(res) + packet.buffer.position;
            if(readSoFar >= 2 && readSoFar >= packet.buffer.get(1)) {
                // Full packet.

            } else {
                packet.client.GetStream().BeginRead(packet.buffer.getArray(), packet.buffer.position, packet.buffer.limit )
            }


            //return data;
        }

        private void asyncAccept(IAsyncResult res) {
            TcpListener listener = (TcpListener)res.AsyncState;
            client = listener.EndAcceptTcpClient(res);
            
            isConnected = true;
            GameWindow.instance.setState(GameWindow.STATE_CONNECTED);
        }

        private void asyncConnect(IAsyncResult res) {
            client = (TcpClient)res.AsyncState;
            client.EndConnect(res);

            Console.WriteLine("[NET] Successful connection to" + client.Client.AddressFamily.ToString());

            isConnected = true;
            GameWindow.instance.setState(GameWindow.STATE_CONNECTED);
        }
    }
}
