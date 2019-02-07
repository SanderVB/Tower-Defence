using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] int health = 3;
    [SerializeField] float destroyTimer = 1f;
    [SerializeField] int enemyValue = 1;
    [SerializeField] GameObject deathEffect;
    [SerializeField] float moveSpeed = 10f;

    int wayPointPosition = 1;

    //[SerializeField] Transform parent;
    bool isDying = false;
    List<Waypoint> route;

    //public for debugging
    public Waypoint targetWaypoint;
    public float step, distance;

    // Use this for initialization
    void Start ()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        //var route = pathfinder.GetPath();
        route = pathfinder.GetPath();
        targetWaypoint = route[1];
        //StartCoroutine(FollowPath(route));
	}

    private void Update()
    {
        FollowRoute();
    }

    private void FollowRoute()
    {
        /*float*/ step = moveSpeed * Time.deltaTime;
        Vector3 target = new Vector3(targetWaypoint.transform.position.x, targetWaypoint.transform.position.y , targetWaypoint.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, step);

        /*float */ distance = Vector3.Distance(transform.position, target);
        if (distance <= 0.1)
        {
            // Jump to next position in list
            if (wayPointPosition < route.Count - 1)
            {
                wayPointPosition += 1;
                targetWaypoint = route[wayPointPosition];
            }
            else
            {
                KillEnemy(); //end is reached, kill enemy and TODO:lower player health
            }
        }
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint wayPoint in path)
        {
            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        health -= FindObjectOfType<TowerController>().GetDamageValue();
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        if (!isDying)
        {
            isDying = true;
            GameObject deathFX = Instantiate(deathEffect, transform.position, Quaternion.identity);
            //deathFX.transform.parent = parent;
            Destroy(this.gameObject, destroyTimer / 2);
            Destroy(deathFX, destroyTimer);
        }
        //FindObjectOfType<GameSession>().AddToScore(enemyValue);
    }

}
