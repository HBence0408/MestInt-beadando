using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node :IComparable
{
    private State state;
    private Node parent;
    private int depth;
    private int hCost;

    public int HCost { get => hCost; set => hCost = value; }
    public State State { get => state; }
    public Node Parent { get => parent; }
    public int Depth { get => depth; }
    public bool IsTerminal { get { return this.state.IsGoalState; } }

    public Node(State state)
    {
        this.state = state;
        parent = null;
        depth = 0;
    }

    public Node(Node parent)
    {
        this.state = (State)parent.state.Clone();
        this.parent = parent;
        this.depth = parent.depth + 1;
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj == this)
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        Node other = (Node)obj;
        return this.state.Equals(other.state);
    }

    public List<Node> Extend()
    {

        List<Node> children = new List<Node>();

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            Node newNode = new Node(this);
            if (newNode.state.ApplyOperator(direction))
            {
               children.Add(newNode);
            }
        }

        return children;
    }

    public int CompareTo(object obj)
    {
        if (obj == null || obj is not Node)
        {
            Debug.LogWarning(obj + " cannot be compared to Node");
        }

        Node n = obj as Node;

            if (this.HCost > n.HCost)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        
        
    }
}

