using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day12 : BaseDay
    {
        public override int DayNumber { get; set; } = 12;

        private int _width;
        private int _height;

        public override string PuzzlePart1(bool training)
        {
            var input = GetInput(training);
            _width = input[0].Length;
            _height = input.Length;
            List<Node> nodes = new List<Node>();
            Node root = null;
            for (int y = 0; y < _height; y++)
            {
                var line = input[y];
                for (int x = 0; x < _width; x++)
                {
                    char c = line[x];
                    Node n = new Node();
                    n.X = x;
                    n.Y = y;
                    n.Explored = false;
                    n.Height = c;
                    if (c == 'E')
                    {
                        n.Goal = true;
                    }
                    nodes.Add(n);
                    if (c == 'S')
                    {
                        root = n;
                    }
                }
            }

            Node goal = BFS(nodes, root);
            int pathLength = 0;
            Node current = goal;
            while(current.Parent != null)
            {
                current = current.Parent;
                pathLength++;
            }

            return pathLength.ToString();
        }

        private Node BFS(List<Node> nodes, Node root)
        {
            Queue<Node> SearchQueue = new Queue<Node>();
            root.Explored = true;
            SearchQueue.Enqueue(root);

            while (SearchQueue.Count != 0)
            {
                var i = SearchQueue.Dequeue();
                if (i.Goal)
                {
                    return i;
                }

                foreach (var n in GetAdjacentDown(i, nodes))
                {
                    if (!n.Explored)
                    {
                        n.Explored = true;
                        n.Parent = i;
                        SearchQueue.Enqueue(n);
                    }
                }
            }

            return null;
        }

        private List<Node> GetAdjacent(Node node, List<Node> nodes)
        {
            List<Node> adj = new List<Node>();
            // Up
            Node n = nodes.Find(n => n.X == node.X && n.Y - 1 == node.Y && CanMove(node.Height, n.Height));
            if (n != null) adj.Add(n);

            // Down
            n = nodes.Find(n => n.X == node.X && n.Y + 1 == node.Y && CanMove(node.Height, n.Height));
            if (n != null) adj.Add(n);

            // Left
            n = nodes.Find(n => n.X + 1 == node.X && n.Y == node.Y && CanMove(node.Height, n.Height));
            if (n != null) adj.Add(n);

            // Right
            n = nodes.Find(n => n.X - 1 == node.X && n.Y == node.Y && CanMove(node.Height, n.Height));
            if (n != null) adj.Add(n);

            return adj;
        }
        private List<Node> GetAdjacentDown(Node node, List<Node> nodes)
        {
            List<Node> adj = new List<Node>();
            // Up
            Node n = nodes.Find(n => n.X == node.X && n.Y - 1 == node.Y && CanMove(n.Height, node.Height));
            if (n != null) adj.Add(n);

            // Down
            n = nodes.Find(n => n.X == node.X && n.Y + 1 == node.Y && CanMove(n.Height, node.Height));
            if (n != null) adj.Add(n);

            // Left
            n = nodes.Find(n => n.X + 1 == node.X && n.Y == node.Y && CanMove(n.Height, node.Height));
            if (n != null) adj.Add(n);

            // Right
            n = nodes.Find(n => n.X - 1 == node.X && n.Y == node.Y && CanMove(n.Height, node.Height));
            if (n != null) adj.Add(n);

            return adj;
        }


        private bool CanMove(char from, char to)
        {
            if (from == 'S' && to == 'a')
            {
                return true;
            }
            if (from == 'z' && to == 'E')
            {
                return true;
            }
            if (from + 1 == to)
            {
                return true;
            }
            return (from >= to && to != 'E');
        }

        public override string PuzzlePart2(bool training)
        {
            var input = GetInput(training);
            _width = input[0].Length;
            _height = input.Length;
            List<Node> nodes = new List<Node>();
            Node root = null;
            for (int y = 0; y < _height; y++)
            {
                var line = input[y];
                for (int x = 0; x < _width; x++)
                {
                    char c = line[x];
                    Node n = new Node();
                    n.X = x;
                    n.Y = y;
                    n.Explored = false;
                    n.Height = c;
                    if (c == 'a')
                    {
                        n.Goal = true;
                    }
                    nodes.Add(n);
                    if (c == 'E')
                    {
                        root = n;
                    }
                }
            }

            Node goal = BFS(nodes, root);
            int pathLength = 0;
            Node current = goal;
            while (current.Parent != null)
            {
                current = current.Parent;
                pathLength++;
            }

            return pathLength.ToString();
        }

        public class Node
        {
            public int X { get; set; }
            
            public int Y { get; set; }

            public bool Explored { get; set; }
            
            public bool Goal { get; set; }

            public Node Parent { get; set; }

            public char Height { get; set; }
        }
    }
}
