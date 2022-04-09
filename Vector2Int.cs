using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class Vector2Int
    {
        public Vector2Int()
        {
            x = 0;
            y = 0;
        }

        public Vector2Int(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        public Vector2Int(Vector2Int copy)
        {
            x = copy.GetX();
            y = copy.GetY();
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public float Distance(Vector2Int other)
        {
            return MathF.Sqrt(MathF.Pow(other.GetX() - x, 2) + MathF.Pow(other.GetY() - y, 2));
        }

        public static bool operator ==(Vector2Int a, Vector2Int b)
        {
            return a.GetX() == b.GetX() && a.GetY() == b.GetY(); 
        }

        public static bool operator !=(Vector2Int a, Vector2Int b)
        {
            return a.GetX() != b.GetX() || a.GetY() != b.GetY();
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.GetX() + b.GetX(), a.GetY() + b.GetY());
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.GetX() - b.GetX(), a.GetY() - b.GetY());
        }

        private int x;
        private int y;
    }
}
