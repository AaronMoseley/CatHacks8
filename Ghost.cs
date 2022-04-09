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
        }

        public Ghost(Vector2Int pos)
        {
            position = pos;
        }

        public Vector2Int GetPos()
        {
            return position;
        }

        public void MovePosition(Vector2Int playerPos, RealMap map)
        {
            SearchMap newMap = new SearchMap(map);

            Vector2Int newPos = NextPosition(playerPos, newMap);
            position = newPos;
        }

        public Vector2Int NextPosition(Vector2Int playerPos, SearchMap map)
        {
            map.SetStart(position);
            map.SetEnd(playerPos);

            return map.BFS().Peek();
        }

        private Vector2Int position;
    }
}
