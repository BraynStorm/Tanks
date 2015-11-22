using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tanks {
    class Packet {
        public const byte NAME_PACKET           = 0x01;
        public const byte PLACE_TANK_PACKET     = 0x02;
        public const byte SHOOT_PACKET          = 0x03;
        public const byte SHOOT_RESULT_PACKET   = 0x04;
        public const byte END_GAME_PACKET       = 0x05;

        public static void manage(byte[] packet) {


        }

        public ByteBuffer buffer;
        public TcpClient client;

        public Packet(TcpClient client) {
            this.client = client;
            buffer = ByteBuffer.allocate(1024);
        }

    }
}
