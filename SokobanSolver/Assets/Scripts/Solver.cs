using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Solver
{
    private Node startNode;
    protected Node StartNode { get => startNode; }

    public Solver(Node start)
    {
        this.startNode = start;
    }

    public abstract Node FindTerminalNode();

    public Stack<State> Solution(Node terminalNode)
    {
        if (terminalNode == null)
        {
            throw new Exception("nem talált megoldást");
        }

        Stack<State> solution = new Stack<State>();
        Node n = terminalNode;
        while (n != null)
        {
            solution.Push(n.State);
            n = n.Parent;
        }
        return solution;
    }
}

