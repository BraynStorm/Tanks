using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks {
    class Grid{
        public readonly int width;
        public readonly int height;

        private Dictionary<int, List<Cell>> cells;
        
        public Grid(int width, int height) {
            cells = new Dictionary<int, List<Cell>>(width);
            this.width = width;
            this.height = height;

            for (int i = 0; i < width; i++) {
                for(int j = 0; j < height; j++) {
                    this[i, j] = new Cell(new Point(i,j));
                }
            }
        }



        public void shootAt(int x, int y) {
            Cell cell = this[x, y];

            if (cell.isShot)
                throw new Exception("Invalid shot.");
            if (cell.isEmpty) {
                cell.isShot = true;
            } else {
                cell.isShot = true;

            }
        }

        public Cell getCell(int x, int y) {
            return cells.ElementAt(x).Value[y];
        }

        public void setCell(int x, int y, Cell val) {
            cells.ElementAt(x).Value[y] = val;
        }

        // Stackoverflow strikes again.
        public Cell this[int x, int y] {
            get { return getCell(x, y); }
            set { setCell(x, y, value); }
        }
    }
}
