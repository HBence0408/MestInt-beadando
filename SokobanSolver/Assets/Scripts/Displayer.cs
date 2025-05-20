using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displayer : MonoBehaviour
{
    private static Displayer instance;
    public static Displayer Instance { get => instance; }

    [SerializeField] private Transform man;
    [SerializeField] private Transform[] balls = new Transform[3];
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject goalPrefab;
    Stack<State> solution;
    private bool solve = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("mutiple dispalyer singletons");
        }
    }

    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (State.Map[i, j] == '#')
                {
                    Instantiate(wallPrefab, new Vector3(j, i, 0), this.transform.rotation, null);
                }
                if (State.Map[i, j] == '1')
                {
                    Instantiate(goalPrefab, new Vector3(j, i, 0), this.transform.rotation, null);
                }
            }
        }

        (int, int)[] ballsStartPos = { (2, 2), (2, 3), (3, 2) };
        (int, int) manStartPos = (1, 1);
        // Backtrack backtrack = new Backtrack(10, new Node(new State(manStartPos, ballsStartPos)));

        //Node terminalNode = backtrack.FindTerminalNode();

        //solution = backtrack.Solution(terminalNode);

        //Depthfirst depthfirst = new Depthfirst(new Node(new State(manStartPos, ballsStartPos)));
        //Node terminalNode = depthfirst.FindTerminalNode();

        //solution = depthfirst.Solution(terminalNode);

        AStar astar = new AStar(new Node(new State(manStartPos, ballsStartPos)));
        Node terminalNode = astar.FindTerminalNode();
        solution = astar.Solution(terminalNode);
    }

    private void Update()
    {

        //if (solve)
        //{
           

        //    solve = false;
        //}


        if (Input.GetKeyDown(KeyCode.Space))
        {
            State s = solution.Pop();

            man.position = new Vector3(s.Man.Item2, s.Man.Item1, 1);
            for (int i = 0; i < 3; i++)
            {
                balls[i].position = new Vector3(s.Balls[i].Item2, s.Balls[i].Item1, 1);
            }
        }
    }
}

