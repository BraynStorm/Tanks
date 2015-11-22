using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks {
    class Tank {
        public readonly int size;
        public readonly bool isVertical;

        public bool isShot {
            get { return isShot; }
            private set { isShot = value; }
        }


        private Cell[] cells;

        public Tank(Point position, int size, bool isVertical) {
            this.size = size;
            this.isVertical = isVertical;

            cells = new Cell[size];

            if (isVertical) 
                for(int i = 0; i < size; i++) 
                    cells[i] = Game.getGrid()[position.X, position.Y + i];
            else // Horizontal
                for(int i = 0; i < size; i++) 
                    cells[i] = Game.getGrid()[position.X + i, position.Y];
            
        }

        public bool tryShootTank(Cell cell) {
            if (cells.Contains(cell)) {
                isShot = true;

                foreach(Cell c in cells) {
                    c.isShot = true;
                }

                return true;
            } else {
                return false;
            }
        }

        public Cell[] getCells() {
            return cells;
        }
    }
}
