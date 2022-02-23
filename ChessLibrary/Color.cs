using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    enum Color
    {
        none,
        black,
        white
    }

    static class ColorMethods
    {
        public static Color FlipColor(this Color color)
        {
            if (color == Color.white) { return Color.black; }
            if (color == Color.black) { return Color.white; }
            return Color.none;
        }
    }
}
