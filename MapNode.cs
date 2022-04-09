using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class MapNode
    {
        public MapNode()
        {
            position = new Vector2Int();
            playerSeen = false;
            blocked = true;
            currChar = 1;
        }

        public MapNode(Vector2Int pos)
        {
            position = pos;
            playerSeen = false;
            blocked = false;
            currChar = 1;
        }

        public void SetPos(Vector2Int newPos)
        {
            position = newPos;
        }

        public void SetSeen(bool seen)
        {
            playerSeen = seen;
        }

        public void SetBlocked(bool block)
        {
            blocked = block;
        }

        public void SetChar(int newChar)
        {
            currChar = newChar;
        }

        public bool GetBlocked()
        {
            return blocked;
        }

        public bool GetSeen()
        {
            return playerSeen;
        }

        public Vector2Int GetPos()
        {
            return position;
        }

        public int GetChar()
        {
            return currChar;
        }

        private Vector2Int position;
        private bool playerSeen;
        private bool blocked;

        //0: nothing
        //1: wall
        //2: floor
        //3: exit
        //4: ghost
        //5: start
        private int currChar;
    }
}
