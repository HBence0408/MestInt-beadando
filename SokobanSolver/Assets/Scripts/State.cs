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
}
