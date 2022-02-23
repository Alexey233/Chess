using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    class FigureMoving
    {
        public Figure figure { get; private set; }
        public Square squareFrom { get; private set; }
        public Square squareTo { get; private set; }


        public Figure promotion { get; private set; }


        public FigureMoving(FigureOnSquare figureOnSquare, Square squareTo, Figure promotion = Figure.none)
        {
            this.figure = figureOnSquare.figure;
            this.squareFrom = figureOnSquare.square;
            this.squareTo = squareTo;
            this.promotion = promotion;
        }

        public FigureMoving(string move)
        {
            this.figure = (Figure)move[0];
            this.squareFrom = new Square(move.Substring(1,2));
            this.squareTo = new Square(move.Substring(3, 2));
            this.promotion = (move.Length == 6) ? (Figure)move[5] : Figure.none;
        }

    }
}
