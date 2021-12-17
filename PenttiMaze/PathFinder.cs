using System.Collections.Generic;
using System.Linq;

namespace PenttiMaze
{
    internal class PathFinder
    {
        private List<List<Node>> nodes;

        public PathFinder(List<List<Node>> nodeList)
        {
            nodes = nodeList;
        }

        // Find a path from start to end
        public List<Node> FindPath(Node start, Node end)
        {
            
            List<Node> path = new List<Node>();
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            List<Node> adjacentNodes;
            Node current = start;

            openList.Add(start);

            // Iterate until a path to the goal is found
            while (openList.Count != 0 && !closedList.Exists(node => (node.x == end.x && node.y == end.y)))
            {
                current = openList[0];
                openList.Remove(current);
                closedList.Add(current);
                adjacentNodes = GetAdjacentNodes(current);

                foreach (Node n in adjacentNodes)
                {
                    if (!closedList.Contains(n) && (n.type == Cell.Empty || n.type == Cell.Exit))
                    {
                        if (!openList.Contains(n))
                        {
                            n.parent = current;
                            n.cost = n.parent.cost + 1;
                            openList.Add(n);
                            openList = openList.OrderBy(node => node.cost).ToList<Node>();
                        }
                    }
                }
            }

            // return null if path was not found
            if (!closedList.Exists(node => (node.x == end.x && node.y == end.y)))
            {
                return null;
            }

            // return path
            path.Add(current);
            while (current != start)
            {
                current = current.parent;
                path.Add(current);
            }

            return path;
        }

        // Get nodes adjacent to given node
        public List<Node> GetAdjacentNodes(Node node)
        {
            List<Node> adjacentNodes = new List<Node>();
            int x = node.x;
            int y = node.y;
            if (node.x > 0) adjacentNodes.Add(nodes[y][x - 1]);
            if (node.x < nodes[y].Count - 1) adjacentNodes.Add(nodes[y][x + 1]);
            if (node.y > 0) adjacentNodes.Add(nodes[y - 1][x]);
            if (node.y < nodes.Count - 1) adjacentNodes.Add(nodes[y + 1][x]);
            return adjacentNodes;
        }
    }
}
