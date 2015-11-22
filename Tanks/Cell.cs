using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks {
    class Cell {
        public bool isEmpty;
        public bool isShot;

        private Point coords;

        public Cell(Point coords, bool isEmpty = true, bool isShot = false) {
            this.isEmpty = isEmpty;
            this.isShot = isShot;
            Network net = new Network();
            
            this.coords = coords;
        }

        public Point getCoords() {
            return coords;
        }
    }
}
