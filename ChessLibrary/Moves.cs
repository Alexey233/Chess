using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    class Moves
    {
        FigureMoving figureMoving;
        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving figureMoving)
        {
            this.figureMoving = figureMoving;
            return
                CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();
        }
        private bool CanMoveFrom()
        {
            return figureMoving.squareFrom.OnBoard() && 
                figureMoving.figure.GetColor() == board.moveColor;
        }
        private bool CanMoveTo()
        {
            return figureMoving.squareTo.OnBoard() &&
                figureMoving.squareFrom != figureMoving.squareTo &&
                (board.GetFigureAt(figureMoving.squareTo).GetColor() != board.moveColor);
        }

        private bool CanFigureMove()
        {
            switch (figureMoving.figure)
            {
                 
                case Figure.whiteKnight:
                case Figure.blackKnight:                  
                    return CanKnightMove();

                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanQueenMove();


                case Figure.whiteRook:
                case Figure.blackRook:
                    return CanRookMove();


                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return CanBishopMove();

                
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();

                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();


                default:
                    return false;
            }
        }

        private bool CanKnightMove()
        {
            if (figureMoving.AbsDeltaX == 1 && figureMoving.AbsDeltaY == 2)
                return true;
            if (figureMoving.AbsDeltaX == 2 && figureMoving.AbsDeltaY == 1)
                return true;
            return false;
        }

        private bool CanPawnMove()
        {
            if (figureMoving.squareFrom.y < 1 || figureMoving.squareFrom.y > 6)
                return false;

            int stepY = figureMoving.figure.GetColor() == Color.white ? 1 : -1;
            return
                CanPawnGo(stepY) ||
                CanPawnJump(stepY) ||
                CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(figureMoving.squareTo) != Figure.none)
                if (figureMoving.DeltaX == 1)
                    if (figureMoving.DeltaY == stepY)
                        return true;
                
            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(figureMoving.squareTo) == Figure.none)
                if (figureMoving.DeltaX == 0)
                    if (figureMoving.DeltaY == 2* stepY)
                        if (figureMoving.squareFrom.y == 1 || figureMoving.squareFrom.y == 6)
                            if (board.GetFigureAt(new Square(figureMoving.squareFrom.x, figureMoving.squareFrom.y + stepY)) == Figure.none)
                                return true;

            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(figureMoving.squareTo) == Figure.none)
                if (figureMoving.DeltaX == 0)
                    if (figureMoving.DeltaY == stepY)
                        return true;

            return false;

        }



        private bool CanBishopMove()
        {
            return (figureMoving.SignX != 0 && figureMoving.SignY != 0) &&
                      CanStraightMove();
        }

        private bool CanRookMove()
        {
            return (figureMoving.SignX == 0 || figureMoving.SignY == 0) &&
                     CanStraightMove();
        }

        private bool CanQueenMove()
        {
            return CanStraightMove();
        }

        private bool CanStraightMove()
        {
            Square at = figureMoving.squareFrom;
            do
            {
                at = new Square(at.x + figureMoving.SignX, at.y + figureMoving.SignY);
                if (at == figureMoving.squareTo)
                    return true;
            } while (at.OnBoard() && board.GetFigureAt(at)==Figure.none);

            return false;
        }

        private bool CanKingMove()
        {
            if (figureMoving.DeltaX <= 1 && figureMoving.AbsDeltaY <= 1)
                return true;
            return false;
        }
    }
}
