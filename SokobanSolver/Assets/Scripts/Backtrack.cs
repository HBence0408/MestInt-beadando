using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backtrack : Solver
{
    private int maxDepth;

    public Backtrack(int maxDepth, Node start) : base(start)
    {
        this.maxDepth = maxDepth;
    }

    public override Node FindTerminalNode()
    {
        return FindTerminalNode(base.StartNode);
    }

    private Node FindTerminalNode(Node currentNode)
    {
        if (currentNode.Depth > maxDepth)
        {
            return null;
        }

        List<Node> children = currentNode.Extend();
        foreach (Node n in children)
        {
            Node terminal = FindTerminalNode(n);
            if (terminal != null)
            {
                return terminal;
            }
        }

        return null;
    }
}

