using System;

namespace ChessLibrary
{
    public class Chess
    {
         private string fen;

        Board board;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
        }

        Chess( Board board) {
            this.board = board;
        }

        public Chess Move(string move)
        {
            FigureMoving figureMoving = new FigureMoving(move);
            Board nextBoart = board.move(figureMoving);

            Chess nextChess = new Chess(nextBoart);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure figure = board.GetFigureAt(square);

            return figure == Figure.none ? '.' : (char)figure;
        }

        public string PrintFen()
        {
            return fen;
        }
    }
}
