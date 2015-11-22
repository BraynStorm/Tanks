using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks {
    class Game {
        private static Grid grid;
        private static Network network;

        public static string opponentName; 

        public static void init() {
            grid = new Grid(12, 12);
        }

        public static Grid getGrid() {
            return grid;
        }

        public static void connect(string name, string hostname, int port) {
            if (network != null) { // IDLE / DC state
                disconnect();
                return;
            }

            network = new Network();
            network.connect(hostname, port);
            network.sendPacket(Packet.NAME_PACKET, Utils.stringToBytes(name));
        }

        public static void listen(string name, int port) {
            if (network != null) { // IDLE / DC state
                disconnect();
                return;
            }

            network = new Network(true);
            network.accept(port);

        }

        public static void disconnect() {
            network.disconnect();
            network = null;
        }
    }
}
