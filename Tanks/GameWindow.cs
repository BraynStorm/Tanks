using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Tanks
{
    public partial class GameWindow : Form {
        #region Shit.
        public static GameWindow instance;
        public GameWindow() {
            InitializeComponent();
            instance = this;
        }
        #endregion

        Socket socket;
        Random rng = new Random();

        public const int STATE_IDLE = 0;
        public const int STATE_CONNECTING = 1;
        public const int STATE_LISTENING = 2;
        public const int STATE_CONNECTED = 3;

        string[] connectStates = { "Connect", "Connecting", "Disconnect" };
        string[] listenStates = { "Listen", "Listen", "Stop\nListening", "Disconnect" };
        
        public void setState(int state) {
            switch (state) {
                case STATE_IDLE:
                    btnListen.Enabled = true;
                    btnConnect.Enabled = true;
                    break;

                case STATE_CONNECTING:
                    btnListen.Enabled = false;
                    btnConnect.Enabled = true;
                    break;

                case STATE_LISTENING:
                    btnListen.Enabled = true;
                    btnConnect.Enabled = false;
                    break;

                case STATE_CONNECTED:
                    btnListen.Enabled = true;
                    btnConnect.Enabled = true;
                    break;
            }

            btnConnect.Text = connectStates[state];
            btnListen.Text = listenStates[state];
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true 
            );

            setState(STATE_IDLE);

        }

        private void genGrid(Label[,] grid, Point loc, int width, int height, int offset) {
            ToolTip tt = new ToolTip(); // x:218, y:17
            for (int i = 0; i < 12; i++) {
                for (int j = 0; j < 12; j++) {
                    Label p = new Label();
                    p.AutoSize = false;
                    p.Location = new Point(loc.X + i * width + offset, loc.Y + j * height + offset);
                    p.Width = width;
                    p.Height = height;
                    p.TextAlign = ContentAlignment.MiddleCenter;
                    
                    p.BorderStyle = BorderStyle.FixedSingle;
                    
                    p.Text = "?";
                    p.BackColor = Color.FromArgb(150, 255, 255, 255);
                        
                    grid[i, j] = p;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            setState(STATE_CONNECTING);
            Game.connect(yourNameBox.Text,(int)portBox.Value);
        }

        private void btnListen_Click(object sender, EventArgs e) {
            setState(STATE_LISTENING);
            Game.listen(yourNameBox.Text, (int)portBox.Value);
        }

        private void connect(IAsyncResult result) {
            Socket resultSocket = (Socket)result.AsyncState;
            resultSocket.EndConnect(result);

            setState(STATE_CONNECTED);
        }

        private void accept(IAsyncResult result) {
            // "Let the games begin!" - Lord Victor Nefarius
            bool imFirst = rng.NextDouble() < 0.5;
            socket.Send(new byte[] { (byte)(imFirst ? 0 : 1) });
        }
    }
}
