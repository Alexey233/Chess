using System;

namespace ChessLibrary
{
    public class Chess1
    {
        string fen;
        
        public Chess1(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
        }

        public Chess1 Move(string move)
        {
            Chess nextChess = new Chess(fen);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            return '.';
        }
    }
}
