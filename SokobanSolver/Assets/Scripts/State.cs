using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class State : ICloneable
{

    private static char[,] map =
    {
        {'#', '#', '#', '#', '#', '#', '#', '#','#'},
        {'#', '0', '0', '0', '#', '#', '#', '#','#'},
        {'#', '0', '0', '0', '#', '#', '#', '#','#'},
        {'#', '0', '0', '0', '#', '#', '#', '1','#'},
        {'#', '#', '#', '0', '#', '#', '#', '1','#'},
        {'#', '#', '#', '0', '0', '0', '0', '1','#'},
        {'#', '#', '0', '0', '0', '#', '0', '0','#'},
        {'#', '#', '0', '0', '0', '#', '#', '#','#'},
        {'#', '#', '#', '#', '#', '#', '#', '#','#'}
    };

    public static char[,] Map { get => map; }
    
    private (int, int)[] balls;
    private (int, int) man;

    public (int, int)[] Balls { 
        get
        {
            (int, int)[] temp = { balls[0], balls[1], balls[2] };
            return temp;
        }
    }
    public (int, int) Man { get => man;}

    public bool IsState
    {
        get
        {
            if (map[man.Item1,man.Item2] == '#')
            {
                return false;
            }

            for (int i = 0; i < balls.Length; i++)
            {
                if (map[balls[i].Item1, balls[i].Item2] == '#')
                {
                    return false;
                }
                if (balls[i] == man)
                {
                    return false;
                }
                for (int j = 0; j < balls.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (balls[i] == balls[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public bool IsGoalState
    {
        get
        {
            foreach ((int,int) ball in balls)
            {
                if (map[ball.Item1,ball.Item2] != '1')
                {
                    return false;
                }
            }

            return true;
        }
    }

    private State()
    {
        
    }

    public State((int, int) man, (int, int)[] balls)
    {
        this.man = man;
        this.balls = balls;
    }

    public bool IsOperator(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                if (map[man.Item1 + 1, man.Item2] == '#')
                {
                    return false;
                }
                foreach ((int, int) ball in balls)
                {
                    if (ball.Item1 == man.Item1 + 1 && ball.Item2 == man.Item2)
                    {
                        if (map[ball.Item1 + 1, ball.Item2] == '#')
                        {
                            return false;
                        }
                        foreach ((int, int) b in balls)
                        {
                            if (b == (ball.Item1 + 1, ball.Item2))
                            {
                                return false;
                            }
                        }
                    }
                }
                break;
            case Direction.DOWN:
                if (map[man.Item1 - 1, man.Item2] == '#')
                {
                    return false;
                }
                foreach ((int, int) ball in balls)
                {
                    if (ball.Item1 == man.Item1 - 1 && ball.Item2 == man.Item2)
                    {
                        if (map[ball.Item1 - 1, ball.Item2] == '#')
                        {
                            return false;
                        }
                        foreach ((int, int) b in balls)
                        {
                            if (b == (ball.Item1 - 1, ball.Item2))
                            {
                                return false;
                            }
                        }
                    }
                }
                break;
            case Direction.RIGHT:
                if (map[man.Item1, man.Item2 + 1] == '#')
                {
                    return false;
                }
                foreach ((int, int) ball in balls)
                {
                    if (ball.Item1 == man.Item1 && ball.Item2 == man.Item2 + 1)
                    {
                        if (map[ball.Item1 , ball.Item2 + 1] == '#')
                        {
                            return false;
                        }
                        foreach ((int, int) b in balls)
                        {
                            if (b == (ball.Item1, ball.Item2 + 1))
                            {
                                return false;
                            }
                        }
                    }
                }
                break;
            case Direction.LEFT:
                if (map[man.Item1, man.Item2 - 1] == '#')
                {
                    return false;
                }
                foreach ((int, int) ball in balls)
                {
                    if (ball.Item1 == man.Item1 && ball.Item2 == man.Item2 - 1)
                    {
                        if (map[ball.Item1, ball.Item2 - 1] == '#')
                        {
                            return false;
                        }
                        foreach ((int, int) b in balls)
                        {
                            if (b == (ball.Item1, ball.Item2 - 1))
                            {
                                return false;
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
        return true;
    }

    public bool ApplyOperator(Direction direction)
    {
        if (!IsOperator(direction))
        {
            return false;
        }

        switch (direction)
        {
            case Direction.UP:
                man.Item1++;
                for (int i = 0; i < balls.Length; i++)
                {
                    if (balls[i] == man)
                    {
                        balls[i].Item1++;
                        break;
                    }
                }
                break;
            case Direction.DOWN:
                man.Item1--;
                for (int i = 0; i < balls.Length; i++)
                {
                    if (balls[i] == man)
                    {
                        balls[i].Item1--;
                        break;
                    }
                }
                break;
            case Direction.RIGHT:
                man.Item2++;
                for (int i = 0; i < balls.Length; i++)
                {
                    if (balls[i] == man)
                    {
                        balls[i].Item2++;
                        break;
                    }
                }
                break;
            case Direction.LEFT:
                man.Item2--;
                for (int i = 0; i < balls.Length; i++)
                {
                    if (balls[i] == man)
                    {
                        balls[i].Item2--;
                        break;
                    }
                }
                break;
            default:
                break;
        }

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is State)
        {
            State s = (State)obj;
            return s.IsState && s.Balls.Contains(this.balls[0]) && s.Balls.Contains(this.balls[1]) && s.Balls.Contains(this.balls[2]) && s.man == this.man;
        }
        else
        {
            return false;
        }
    }

    public object Clone()
    {
        State temp = new State();
        temp.man = this.Man;
        temp.balls = this.Balls;
        return temp;
    }
}
