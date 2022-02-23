using System;
using ChessLibrary;

namespace ChessDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            while (true)
            {
                Console.WriteLine(chess.PrintFen());
                string move = Console.ReadLine();
                chess.Move(move);
            }
        }
    }
}
