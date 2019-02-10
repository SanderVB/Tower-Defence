using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] int maxHealth = 3;
    [SerializeField] float destroyTimer = 1f;
    [SerializeField] int scoreValue = 1;
    [SerializeField] int damageValue = 1;
    [SerializeField] GameObject deathEffect;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject healthBar, barHolder;

    int wayPointPosition = 1;
    int currentHealth;
    Camera myCam;
    bool isDying = false;
    List<Waypoint> route;

    //public for debugging
    public Waypoint targetWaypoint;
    public float step, distance;

    // Use this for initialization
    void Start ()
    {
        myCam = FindObjectOfType<Camera>();
        currentHealth = maxHealth;
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        route = pathfinder.GetPath();
        targetWaypoint = route[1];

	}

    private void Update()
    {
        gameObject.transform.LookAt(targetWaypoint.transform);
        if(!isDying)
            FollowRoute();
    }

    private void LateUpdate()
    {
        //attempt to always make the health bar face the camera.
        barHolder.transform.LookAt(barHolder.transform.position + myCam.transform.rotation * Vector3.back, myCam.transform.rotation * Vector3.down);
    }

    private void FollowRoute()
    {
        step = moveSpeed * Time.deltaTime;
        Vector3 target = new Vector3(targetWaypoint.transform.position.x, targetWaypoint.transform.position.y , targetWaypoint.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, step);

        distance = Vector3.Distance(transform.position, target);
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
                if (!isDying)
                {
                    FindObjectOfType<GameSession>().SetPlayerHealth(damageValue);
                    KillEnemy(); //end is reached, kill enemy and lower player health
                }
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
        int damage = FindObjectOfType<TowerController>().GetDamageValue();

        float healthBarScale = -(1 / (float)maxHealth * damage);
        currentHealth -= damage;
        healthBar.transform.localScale += new Vector3(healthBarScale, 0, 0);
        if(healthBar.transform.localScale.x<0)
        {
            healthBar.transform.localScale = new Vector3(0,0,0);
        }

        if (currentHealth <= 0)
        {
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
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
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            Destroy(this.gameObject, destroyTimer / 2);
            Destroy(deathFX, destroyTimer);
        }
    }
}
