using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenttiMaze
{
    public enum Cell
    {
        Empty,
        Wall,
        Exit
    }

    internal class Node
    {
        public Node parent;
        public Cell type;
        public int cost;
        public int x;
        public int y;
    }
}
