using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class Player
    {
        public Player()
        {
            position = new Vector2Int();
            sightDist = 0;
        }

        public Player(Vector2Int pos, int sight)
        {
            position = pos;
            sightDist = sight;
        }

        public Vector2Int GetPos()
        {
            return position;
        }

        public int GetSightDist()
        {
            return sightDist;
        }

        public void SetSightDist(int newSight)
        {
            sightDist = newSight;
        }

        public void SetPosition(Vector2Int pos)
        {
            position = pos;
        }

        public bool Move(Vector2Int endpos, int input)
        {
            //Input: up, down, left, right
            Vector2Int offset = new Vector2Int();
            switch(input)
            {
                case 0:
                    offset = new Vector2Int(0, 1);
                    break;
                case 1:
                    offset = new Vector2Int(0, -1);
                    break;
                case 2:
                    offset = new Vector2Int(-1, 0);
                    break;
                case 3:
                    offset = new Vector2Int(1, 0);
                    break;
            }

            position += offset;

            //Return true if position is now equal to end
            if (position == endpos)
                return true;

            return false;
        }

        private Vector2Int position;
        private int sightDist;
    }
}
