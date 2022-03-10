using System;
using System.Collections.Generic;

namespace ChessLibrary
{
    public class Chess
    {
        private string fen;
        Board board;
        Moves moves;

        List <FigureMoving> allmoves;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        Chess(Board board) 
        {
            this.board = board;
            this.fen = board.fen;
            this.moves = new Moves(board);
            
        }

        public Chess Move(string move)
        {
            FigureMoving figureMoving = new FigureMoving(move);
            if (!moves.CanMove(figureMoving))
                return this;
            if (board.IsCheckAfterMove(figureMoving))
                    return this;

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

        public void FingAllMoves()
        {
            allmoves = new List<FigureMoving>();
            foreach(FigureOnSquare fs in board.YieldFigures())
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving figuremoving = new FigureMoving(fs, to);
                    if (moves.CanMove(figuremoving))
                    {
                        if (!board.IsCheckAfterMove(figuremoving))
                        allmoves.Add(figuremoving);
                    }
                }


        }


        public List<string> GetAllMoves()
        {
            FingAllMoves(); 

            List<string> list = new List<string>();
            foreach(FigureMoving fm in allmoves)
            {
                list.Add(fm.ToString());
            }
            return list;
        }

         public bool IsCheck()
        {
            return board.IsCheck();
        }

    }
}
