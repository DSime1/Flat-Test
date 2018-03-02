using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    [SerializeField] WayPoint startWayPoint, endWayPoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();

    Queue<WayPoint> queue = new Queue<WayPoint>();

    bool pathSearchIsRunning = true;

    WayPoint searchCenter;

    public List<WayPoint> path = new List<WayPoint>();

    Vector2Int[] directions = { 
       
        //search order clockwise starting from UP

        new Vector2Int(0,1), // up or Vector2Int.up
       // new Vector2Int(1,1), // up and right
        new Vector2Int(1,0), // right or Vector2Int.right
       // new Vector2Int(1,-1), // down and right
        new Vector2Int(0,-1), // down or Vector2Int.down
       // new Vector2Int(-1,-1), // down and left
        new Vector2Int(-1,0), // left or Vector2Int.left
       // new Vector2Int(1,1) // up and left

    };
	


    public List<WayPoint> GetPath()
    {
        LoadTheWaypoints();

        SetStartEndColor();

        BreadthFirstSearch();

        CreatePath();
        return path;
    }


    //creating actual path from start to end
    private void CreatePath()
    {
        //starts adding end point
        path.Add(endWayPoint);

        //checks the end point exploredFrom and stores it

        WayPoint previous = endWayPoint.exploredFrom;

        while (previous != startWayPoint){

            //add the waypoint to the path
            path.Add(previous);

            //updates the waypoint to the exploredFrom waypoint
            //so that next iteration it will move backwards
            previous = previous.exploredFrom;

        }

        path.Add(startWayPoint);

        path.Reverse();

    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWayPoint);

        while(queue.Count >0 && pathSearchIsRunning){

            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;

           // print("I am at"+ searchCenter);

            HaltIfEndFound();

            ExploreNeighbours();
            searchCenter.isExplored = true;
        }


    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWayPoint){

            pathSearchIsRunning = false;


            print("End found therefore stopping");
        }
    }

    private void ExploreNeighbours()
    {

        if (!pathSearchIsRunning) { return; }

        foreach(Vector2Int direction in directions){

            //print("exploring from " + from.GetGridPos() + " looking into " + direction);
           
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;

            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    { 
        WayPoint neighbour = grid[neighbourCoordinates];

        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing
        }

        else
        {
            //neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void SetStartEndColor()
    {
        startWayPoint.SetTopColor(Color.green);
        endWayPoint.SetTopColor(Color.red);
    }

    private void LoadTheWaypoints()
    {
        var waypoints = FindObjectsOfType<WayPoint>();

        foreach(WayPoint waypoint in waypoints){
            
            if (grid.ContainsKey(waypoint.GetGridPos()))
            { 
                Debug.LogWarning("Skipping overlapping cube at " + waypoint); 
            }
            else{
                grid.Add(waypoint.GetGridPos(), waypoint);

                //change color of the top of the cube
                waypoint.SetTopColor(Color.white);
            }
        }

        //print("Loaded "+ grid.Count + " waypoints");
    }


    private void Update()
    {
      //  ExploreNeighbours();
    }

}
