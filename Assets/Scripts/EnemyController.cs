﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var route = pathfinder.GetPath();
        StartCoroutine(FollowPath(route));
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    IEnumerator FollowPath(List<Waypoint> path)
    {
        Debug.Log("Starting route");
        foreach (Waypoint wayPoint in path)
        {
            transform.position = wayPoint.transform.position;
            Debug.Log("Position is: " + transform.position.x / 10 + "," + transform.position.z / 10);
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Route finished");
    }
}
