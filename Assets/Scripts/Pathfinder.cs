using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Waypoint startPoint, endPoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left };

	// Use this for initialization
	void Start ()
    {
        LoadBlocks();
        ColorStartAndEndBlocks();
        Pathfind();
        //ExploreNeighbours(startPoint);
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

    private void ColorStartAndEndBlocks()
    {
        startPoint.SetTopColor(Color.green);
        endPoint.SetTopColor(Color.red);
    }

    private void Pathfind()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();


            print("Searching from: " + searchCenter); //remove later
            if (searchCenter == endPoint)
                EndFound(searchCenter);
            else
            {
                ExploreNeighbours(searchCenter);
                searchCenter.isExplored = true;
            }
        }
    }

    private void ExploreNeighbours(Waypoint waypoint)
    {
        if (!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            var explorationCoordinates = waypoint.GetGridPos() + direction;
            print("Exploring " + explorationCoordinates);
            try
            {
                QueueNewNeighbours(explorationCoordinates);
            }
            catch { print("Missing a neighbour"); }
        }
    }

    private void QueueNewNeighbours(Vector2Int explorationCoordinates)
    {
        Waypoint neighbour = grid[explorationCoordinates];
        if (!neighbour.isExplored)
        {
            neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            print("Queuing: " + neighbour);
        }
    }

    private void EndFound(Waypoint searchCenter)
    {
        print("End reached: " + searchCenter);
        isRunning = false;
    }


}
