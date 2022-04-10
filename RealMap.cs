using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class RealMap
    {
        public RealMap(Vector2Int start, Vector2Int end, int sideLength)
        {
            this.sideLength = sideLength;
            startPos = start;
            endPos = end;

            ghosts = new List<Ghost>();
            coinPositions = new List<Vector2Int>();

            nodes = new MapNode[sideLength, sideLength];

            for(int i = 0; i < sideLength; i++)
            {
                for(int j = 0; j < sideLength; j++)
                {
                    nodes[i, j] = new MapNode(new Vector2Int(i, j));
                }
            }
        }

        public MapNode GetNodeByPos(Vector2Int pos)
        {
            if(pos.GetX() < sideLength && pos.GetY() < sideLength && pos.GetX() >= 0 && pos.GetY() >= 0)
            return nodes[pos.GetX(), pos.GetY()];

            return null;
        }

        public List<Ghost> GetGhostPositions()
        {
            if(ghosts.Count > 0)
                return ghosts;

            return null;
        }

        public Vector2Int GetStart()
        {
            return startPos;
        }

        public Vector2Int GetEnd()
        {
            return endPos;
        }

        public int GetSideLength()
        {
            return sideLength;
        }

        public void AddGhost(Ghost newGhost)
        {
            ghosts.Add(newGhost);
        }

        public void GenerateMap(int numRooms, int maxRoomSize, int minRoomSize, int numPossibleCoins)
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            Random rand = new Random((int)t.TotalSeconds);

            Vector2Int[] roomPositions = new Vector2Int[numRooms + 2];

            roomPositions[0] = startPos;
            roomPositions[1] = endPos;

            for(int i = 2; i < numRooms + 2; i++)
            {
                roomPositions[i] = new Vector2Int(rand.Next(sideLength), rand.Next(sideLength));
            }

            Stack<Vector2Int>[] paths = new Stack<Vector2Int>[numRooms + 1];

            for(int i = 0; i < paths.Length; i++)
            {
                paths[i] = new Stack<Vector2Int>();
            }

            SearchMap[] maps = new SearchMap[numRooms + 1];

            maps[0] = new SearchMap(startPos, endPos, sideLength, new Vector2Int(-1, -1));
            paths[0] = maps[0].DFS();

            for(int i = 1; i < paths.Length; i++)
            {
                maps[i] = new SearchMap(startPos, roomPositions[i + 1], sideLength, new Vector2Int(-1, -1));
                paths[i] = maps[i].DFS();
            }

            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i] != null)
                {
                    while (paths[i].Count() > 0)
                    {
                        MapNode currNode = GetNodeByPos(paths[i].Pop());
                        currNode.SetBlocked(false);

                        currNode.SetChar(DetermineChar(currNode));
                    }
                }
            }

            for(int i = 0; i < roomPositions.Length; i++)
            {
                int roomSize = rand.Next(Math.Abs(maxRoomSize - minRoomSize)) + minRoomSize;

                for(int j = -roomSize; j <= roomSize; j++)
                {
                    for(int k = -roomSize; k <= roomSize; k++)
                    {
                        if(GetNodeByPos(roomPositions[i] + new Vector2Int(j, k)) != null)
                        {
                            MapNode currNode = GetNodeByPos(new Vector2Int(roomPositions[i].GetX() + j, roomPositions[i].GetY() + k));
                            currNode.SetBlocked(false);
                            currNode.SetChar(DetermineChar(currNode));
                        }
                    }
                }
            }

            for(int i = 0; i < numPossibleCoins; i++)
            {
                Vector2Int newPos = new Vector2Int(rand.Next(sideLength), rand.Next(sideLength));

                bool match = false;
                for(int j = 0; j < coinPositions.Count; j++)
                {
                    if(newPos == coinPositions[j])
                    {
                        match = true;
                    }
                }

                if (newPos == startPos || newPos == endPos)
                    match = true;

                if(!GetNodeByPos(newPos).GetBlocked() && !match)
                {
                    coinPositions.Add(newPos);
                }
            }
        }

        public List<Vector2Int> GetCoinPositions()
        {
            return coinPositions;
        }

        public void SetCoinPositions(List<Vector2Int> newPos)
        {
            coinPositions = newPos;
        }

        private int DetermineChar(MapNode node)
        {
            if(node.GetPos() == startPos)
            {
                return 5;
            } else if(node.GetPos() == endPos)
            {
                return 3;
            } else
            {
                return 2;
            }
        }

        private int sideLength;
        private MapNode[,] nodes;
        private Vector2Int startPos;
        private Vector2Int endPos;
        private List<Ghost> ghosts;
        private List<Vector2Int> coinPositions;
    }
}
