using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class SearchNode
    {
        public SearchNode()
        {
            position = new Vector2Int();
            open = true;
            parent = null;
            visited = false;
        }

        public SearchNode(Vector2Int pos, bool open)
        {
            position = pos;
            this.open = open;
            parent = null;
            visited = false;
        }

        public bool GetOpen()
        {
            return open;
        }

        public Vector2Int GetPos()
        {
            return position;
        }

        public void SetOpen(bool newOpen)
        {
            open = newOpen;
        }

        public void SetPosition(Vector2Int pos)
        {
            position = pos;
        }

        public SearchNode GetParent()
        {
            return parent;
        }

        public void SetParent(SearchNode newParent)
        {
            parent = newParent;
        }

        public void SetVisited(bool visit)
        {
            visited = visit;
        }

        public bool GetVisited()
        {
            return visited;
        }

        private bool visited;
        private bool open;
        private SearchNode parent;
        private Vector2Int position;
    }
}
