using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class Ghost
    {
        public Ghost()
        {
            position = new Vector2Int();
            playerSeen = false;
        }

        public Ghost(Vector2Int pos)
        {
            position = pos;
            playerSeen = false;
        }

        public bool GetSeen()
        {
            return playerSeen;
        }

        public void SetSeen(bool seen)
        {
            playerSeen = seen;
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

        public void MovePosition(RealMap map, int seed)
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            Random rand = new Random((int)t.TotalSeconds + seed);

            int[] priority = { -1, -1, -1, -1 };

            for(int i = 0; i < priority.Length; i++)
            {
                int index;
                do
                {
                    index = rand.Next(priority.Length);
                } while (priority[index] != -1);

                priority[index] = i;
            }

            for(int i = 0; i < priority.Length; i++)
            {
                Vector2Int offset;

                switch(priority[i])
                {
                    case 0:
                        offset = new Vector2Int(0, 1);
                        break;
                    case 1:
                        offset = new Vector2Int(0, -1);
                        break;
                    case 2:
                        offset = new Vector2Int(1, 0);
                        break;
                    case 3:
                        offset = new Vector2Int(-1, 0);
                        break;
                    default:
                        Console.WriteLine("Switch Error");
                        return;
                }

                MapNode nextPos = map.GetNodeByPos(position + offset);

                if (nextPos != null)
                {
                    if(!nextPos.GetBlocked())
                    {
                        position = nextPos.GetPos();
                        return;
                    }
                }
            }
        }

        public void MovePosition(Vector2Int playerPos, RealMap map)
        {
            SearchMap newMap = new SearchMap(map);

            Vector2Int newPos = NextPosition(playerPos, newMap);

            List<Ghost> otherGhosts = map.GetGhostPositions();

            bool blocked = false;
            for(int i = 0; i < otherGhosts.Count; i++)
            {
                if(otherGhosts[i].GetPos() == newPos)
                {
                    blocked = true;
                }
            }

            if(!blocked)
                position = newPos;
        }

        public Vector2Int NextPosition(Vector2Int playerPos, SearchMap map)
        {
            map.SetStart(position);
            map.SetEnd(playerPos);
            Stack<Vector2Int> result = map.BFS();
            
            if(result != null)
                if(result.Count > 0)
                    return result.Peek();

            return position;
        }

        public int GetLoseDist()
        {
            return loseDist;
        }

        public void SetLoseDist(int lose)
        {
            loseDist = lose;
        }

        private int loseDist;
        private Vector2Int position;
        private bool playerSeen;
        private int sightDist;
    }
}
