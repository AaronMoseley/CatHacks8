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
        }

        public Player(Vector2Int pos)
        {
            position = pos;
        }

        public Vector2Int GetPos()
        {
            return position;
        }

        public bool Move(Vector2Int endpos, int input)
        {
            //TODO Change position according to input

            //Return true if position is now equal to end
            if (position == endpos)
                return true;

            return false;
        }

        Vector2Int[] visibleNodes()
        {
            //TODO: No functionality implemented yet
            return new Vector2Int[1];
        }

        private Vector2Int position;
    }
}
