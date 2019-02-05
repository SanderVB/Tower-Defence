using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Waypoint startPoint, endPoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left };

    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        ColorStartAndEndBlocks();
        BreadthFirstSearch();
        CreatePath();
        return path;
    }
    
    private void CreatePath()
    {
        path.Add(endPoint);
        Waypoint previousPoint = endPoint.foundFrom;
        while(previousPoint!=startPoint)
        {
            path.Add(previousPoint);
            previousPoint = previousPoint.foundFrom;
        }
        path.Add(startPoint);
        path.Reverse();
    }

    private void LoadBlocks()
    {
        var wayPoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint wayPoint in wayPoints)
        {
            var gridPos = wayPoint.GetGridPos();

            bool isOverlapping = grid.ContainsKey(gridPos);

            //overlapping blocks? Don't add
            if (isOverlapping)
                Debug.LogWarning("Overlapping block: " + gridPos);
            else
            {
                grid.Add(gridPos, wayPoint);
            }
        }
    }

    private void ColorStartAndEndBlocks() //TODO put in waypoint
    {
        startPoint.SetTopColor(Color.green);
        endPoint.SetTopColor(Color.red);
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();

            //print("Searching from: " + searchCenter); //remove later

            if (searchCenter == endPoint)
                EndFound();
            else
            {
                ExploreNeighbours();
                searchCenter.isExplored = true;
            }
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            var neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
                print("Exploring " + neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int explorationCoordinates)
    {
        Waypoint neighbour = grid[explorationCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing

        }
        else
        {
            neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            neighbour.foundFrom = searchCenter;
            print("Queuing: " + neighbour);
        }
    }

    private void EndFound()
    {
        print("End reached: " + searchCenter);
        isRunning = false;
    }
}
