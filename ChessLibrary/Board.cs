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
           
            string[] splitFen = fen.Split();
             
            if (splitFen.Length != 6) { return; }
            InitFigure(splitFen[0]);
            moveColor = (splitFen[1] == "b") ? Color.black : Color.white;
            moveNumber = int.Parse(splitFen[5]);


        }

        void InitFigure(string data)
        {
            for (int j = 8; j >= 2; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }
            data = data.Replace("1", ".");
            string[] lines = data.Split('/');
            for(int y = 7; y >= 0; y--)
            {
                for(int x = 0; x < 8; x++)
                {
                    figures[x, y] = lines[7 - y][x] == '.' ? Figure.none : (Figure)lines[7 - y][x];
                }
            }
        }

        
        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                return figures[square.x, square.y];
            }
            return Figure.none;
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == moveColor)
                    yield return new FigureOnSquare(GetFigureAt(square), square);
            }
                
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard()){
                figures[square.x, square.y] = figure;
            }
        }

        void generateFen()
        {
            //"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
            this.fen = FenFigure() + " " + (moveColor == Color.white ? "w" : "b")  + " - - 0 " + moveNumber.ToString();
        }

        private string FenFigure()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char)figures[x, y]);
                    
                }
                if (y > 0)
                    sb.Append('/');
            }
            string eight = "11111111";
            for (int i = 8; i >= 2; i--)
            {
                sb.Replace(eight.Substring(0, i), i.ToString());
            }

            return sb.ToString();
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
            next.generateFen();
            return next;
        }


        public bool IsCheck()
        {
            Board after = new Board(fen);
            after.moveColor = moveColor.FlipColor();
            return after.CanEatKing();
        }

        private bool CanEatKing()
        {
            Square badKing = FingBadKing();
            Moves moves = new Moves(this);
            foreach (FigureOnSquare fs in YieldFigures())
            {
                FigureMoving fm = new FigureMoving(fs, badKing);
                if (moves.CanMove(fm))
                    return true;
            }
            return false;
        }

        private Square FingBadKing()
        {
            Figure badKing = moveColor == Color.black ? Figure.whiteKing : Figure.blackKing;
            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square) == badKing)
                    return square;
            } 
            return Square.DefaultSquare;
        }


        public bool IsCheckAfterMove(FigureMoving fm)
        {
            Board after = move(fm);
            return after.CanEatKing();
        }
    }
}
