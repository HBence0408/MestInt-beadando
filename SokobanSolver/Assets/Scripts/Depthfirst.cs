using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Depthfirst : Solver
    {
        private Stack<Node> openSet;
        private List<Node> closedSet;

        public Depthfirst(Node start) : base(start)
        {
            openSet = new Stack<Node>();
            openSet.Push(base.StartNode);
            closedSet = new List<Node>();
        }

        public override Node FindTerminalNode()
        {
            while (openSet.Count != 0)
            {
                Node currentNode = openSet.Pop();

                List<Node> children = currentNode.Extend();
                foreach (Node n in children)
                {
                    if (n.IsTerminal)
                    {
                        return n;
                    }
                    if (!n.State.IsSolveAble)
                    {
                        closedSet.Add(n);
                    }
                    if (!openSet.Contains(n) && !closedSet.Contains(n))
                    {
                        openSet.Push(n);
                    }

                }
                closedSet.Add(currentNode);
            }
            return null;
        }
    }

