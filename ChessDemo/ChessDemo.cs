using System;
using System.Collections.Generic;
using ChessLibrary;

namespace ChessDemo
{
    class ChessDemo
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            List<string> list;

            Random random = new Random();

            while (true)
            {
                list = chess.GetAllMoves();
                Console.WriteLine(chess.PrintFen());
                Print(ChessToAscii(chess));
                Console.WriteLine(chess.IsCheck() ? "Check" : "");
                foreach (string moves in list)
                    {
                    Console.Write(moves + "\t");
                    }

                Console.WriteLine();

                Console.Write("> ");
                string move = Console.ReadLine();
                if (move == "q") break;
                if (move == "") { move = list[random.Next(list.Count)]; }
                chess = chess.Move(move);    
                
            }
        }

        static string ChessToAscii(Chess chess)
        {
            string text = "  +----------------+\n";
            for (int y =  7; y>=0;y--)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++){
                    text += chess.GetFigureAt(x, y) + " ";
                }
                text += "\n";
            }
            text += "  +----------------+\n";
            text += "    a b c d e f g h\n";

            return text;
        }

        static void Print(string text)
        {
            ConsoleColor oldForeColor = Console.ForegroundColor;

            foreach (char x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else 
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }
            Console.ForegroundColor = oldForeColor;

        }


    }
}
