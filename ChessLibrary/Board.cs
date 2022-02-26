using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    class Board
    {
        public string fen { get; private set; }

        Figure[,] figures;
        public Color moveColor { get; private set; }
        public int moveNumber { get; private set; }

        public Board(string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }
        void Init()
        {
            SetFigureAt(new Square("a1"), Figure.whiteKing);
            SetFigureAt(new Square("h8"), Figure.blackKing);

        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                return figures[square.x, square.y];
            }
            return Figure.none;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard()){
                figures[square.x, square.y] = figure;
            }
        }

        public Board move(FigureMoving figureMoving)
        {
            Board next = new Board(fen);
            next.SetFigureAt(figureMoving.squareFrom, Figure.none);
            next.SetFigureAt(figureMoving.squareTo, figureMoving.promotion == Figure.none ? figureMoving.figure : figureMoving.promotion);

            if (moveColor == Color.black)
            {
                next.moveNumber++;
            }
            next.moveColor = moveColor.FlipColor();
            return next;
        }

    }
}
