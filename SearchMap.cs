using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class SearchMap
    {
        public SearchMap(RealMap map)
        {
            startPos = map.GetStart();
            endPos = map.GetEnd();

            int sideLength = map.GetSideLength();

            nodes = new SearchNode[sideLength, sideLength];

            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    SearchNode newNode = new SearchNode();
                    newNode.SetPosition(new Vector2Int(i, j));
                    newNode.SetOpen(!map.GetNodeByPos(new Vector2Int(i, j)).GetBlocked());

                    nodes[i, j] = newNode;
                }
            }
        }

        public SearchMap(Vector2Int startPos, Vector2Int endPos, int sideLength, Vector2Int blockPos)
        {
            this.startPos = startPos;
            this.endPos = endPos;

            SearchNode[,] newNodes = new SearchNode[sideLength, sideLength];
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    SearchNode node = new SearchNode(new Vector2Int(i, j), true);
                    newNodes[i, j] = node;
                }
            }

            SearchNode blockNode = GetNodeByPos(blockPos);

            if(blockNode != null)
            {
                blockNode.SetOpen(false);
            }

            nodes = newNodes;
        }

        public SearchNode GetNodeByPos(Vector2Int pos)
        {
            if(nodes != null)
                if (pos.GetX() < nodes.GetLength(0) && pos.GetY() < nodes.GetLength(1) && pos.GetX() >= 0 && pos.GetY() >= 0 && nodes.Length > 0)
                    return nodes[pos.GetX(), pos.GetY()];

            return null;
        }

        public void SetStart(Vector2Int newStart)
        {
            startPos = newStart;
        }

        public void SetEnd(Vector2Int newEnd)
        {
            endPos = newEnd;
        }

        public Stack<Vector2Int> DFS()
        {
            SearchNode currNode = GetNodeByPos(startPos);

            if (currNode != null)
            {
                TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                Random rand = new Random((int)t.TotalSeconds);

                while (currNode.GetPos() != endPos)
                {
                    currNode.SetVisited(true);

                    SearchNode nextNode;

                    int[] priority = { -1, -1, -1, -1 };
                    for (int i = 0; i < priority.Length; i++)
                    {
                        int index;
                        do
                        {
                            index = rand.Next(4);
                        } while (priority[index] != -1);

                        priority[index] = i;
                    }

                    bool found = false;
                    for (int i = 0; i < priority.Length; i++)
                    {
                        if (!found)
                        {
                            Vector2Int offset = new Vector2Int();

                            switch (priority[i])
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
                                    break;
                            }

                            if (GetNodeByPos(currNode.GetPos() + offset) != null)
                            {
                                if (!GetNodeByPos(currNode.GetPos() + offset).GetVisited() && GetNodeByPos(currNode.GetPos() + offset).GetOpen())
                                {
                                    nextNode = GetNodeByPos(currNode.GetPos() + offset);
                                    nextNode.SetVisited(true);
                                    nextNode.SetParent(currNode);
                                    currNode = nextNode;
                                    found = true;
                                }
                            }
                        }
                    }

                    if (!found)
                    {
                        if (currNode.GetParent() != null)
                        {
                            currNode = currNode.GetParent();
                        }
                        else
                        {
                            Console.WriteLine("Error 1");
                            return null;
                        }
                    }
                }

                Stack<Vector2Int> positions = new Stack<Vector2Int>();

                while (currNode.GetParent() != null && currNode.GetPos() != currNode.GetParent().GetPos())
                {
                    positions.Push(currNode.GetPos());
                    currNode = currNode.GetParent();
                }

                return positions;
            }

            return null;
        }

        public Stack<Vector2Int> BFS()
        {
            Vector2Int[] offsets = { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

            SearchNode currNode = GetNodeByPos(startPos);

            if (currNode == null)
            {
                return null;
            }

            Queue<SearchNode> nodesToSearch = new Queue<SearchNode>();

            while (currNode.GetPos() != endPos)
            {
                currNode.SetVisited(true);

                for (int i = 0; i < offsets.Length; i++)
                {
                    if (GetNodeByPos(currNode.GetPos() + offsets[i]) != null)
                    {
                        if (GetNodeByPos(currNode.GetPos() + offsets[i]).GetOpen() && !GetNodeByPos(currNode.GetPos() + offsets[i]).GetVisited())
                        {
                            SearchNode peekNode = GetNodeByPos(currNode.GetPos() + offsets[i]);
                            peekNode.SetParent(currNode);
                            peekNode.SetVisited(true);
                            nodesToSearch.Enqueue(peekNode);
                        }
                    }
                }

                if (nodesToSearch.Count > 0)
                {
                    currNode = nodesToSearch.Dequeue();
                } else
                {
                    return null;
                }
            }

            Stack<Vector2Int> result = new Stack<Vector2Int>();
            while (currNode.GetParent() != null && currNode.GetPos() != currNode.GetParent().GetPos())
            {
                result.Push(currNode.GetPos());
                currNode = currNode.GetParent();
            }

            return result;
        }

        private SearchNode[,] nodes;
        private Vector2Int startPos;
        private Vector2Int endPos;
    }
}
