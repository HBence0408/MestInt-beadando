using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AStar : Solver
{
    private List<Node> closedNodes;
    private MinBinaryHeap<Node> openNodes;

    public AStar(Node node) : base(node)
    {
        closedNodes = new List<Node>();
        openNodes = new MinBinaryHeap<Node>();
        StartNode.HCost = Heuristic(StartNode);
        openNodes.Insert(StartNode);
    }

    private int Heuristic(Node node)
    {
        return Math.Abs(3 * 7 - node.State.Balls[0].Item2 - node.State.Balls[0].Item2 - node.State.Balls[0].Item2);
    }

    public override Node FindTerminalNode()
    {
        while (!openNodes.IsEmty)
        {
            Node actual = openNodes.ExctractMin();

            if (actual.IsTerminal)
                return actual;

            closedNodes.Add(actual);

            foreach (Node child in actual.Extend())
            {
                if (closedNodes.Contains(child))
                {
                    continue;
                }
                if (!child.State.IsSolveAble)
                {
                    continue;
                }

                int f = child.Depth + Heuristic(child);

                bool shouldEnqueNode = true;
                foreach (Node node in openNodes)
                {
                    if (node.Equals(child) && node.HCost <= f)
                    {
                        shouldEnqueNode = false;
                        break;
                    }
                }

                if (shouldEnqueNode)
                    child.HCost = f;
                    openNodes.Insert(child);
            }
        }

        return null;
    }

}

