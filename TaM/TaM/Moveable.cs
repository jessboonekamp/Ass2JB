using System;
using System.Collections.Generic;
using System.Text;

namespace TaM
{
    class Moveable {
        public int Row;
        public int Column;
        public int[] newCoordinates = new int[2];
        Game Game;
        public Moveable(int column, int row, Game game) {
            Column = column;
            Row = row;
            Game = game;
        }
        public int[] GoDown()
        {
            newCoordinates[0] = Row+1;
            newCoordinates[1] = Column;
            return newCoordinates;
        }

        public int[] GoLeft()
        {
            newCoordinates[0] = Row;
            newCoordinates[1] = Column - 1;
            return newCoordinates;
        }

        public int[] GoRight()
        {
            newCoordinates[0] = Row;
            newCoordinates[1] = Column + 1;
            return newCoordinates;
        }

        public int[] GoUp()
        {
            newCoordinates[0] = Row - 1;
            newCoordinates[1] = Column;
            return newCoordinates;
        }
    
}
}
