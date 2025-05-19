using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class State 
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
}
