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
        if (path.Count == 0)
        {
            CalculatePath();
        }

        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endPoint);
        Waypoint previousPoint = endPoint.foundFrom;
        while (previousPoint != startPoint)
        {
            SetAsPath(previousPoint);
            previousPoint = previousPoint.foundFrom;
        }
        SetAsPath(startPoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
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

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();

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
            queue.Enqueue(neighbour);
            neighbour.foundFrom = searchCenter;
        }
    }

    private void EndFound()
    {
        isRunning = false;
    }
}
