using System;
using ChessLibrary;

namespace ChessDemo
{
    class ChessDemo
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            while (true)
            {
                Console.WriteLine(chess.PrintFen());
                string move = Console.ReadLine();
                if (move == "") break;

                chess = chess.Move(move);              
            }
        }
    }
}
