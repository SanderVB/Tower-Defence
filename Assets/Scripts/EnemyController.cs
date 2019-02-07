using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] int health = 3;
    [SerializeField] float destroyTimer = 1f;
    [SerializeField] int enemyValue = 1;
    [SerializeField] GameObject deathEffect;
    //[SerializeField] Transform parent;
    bool isDying = false;


    // Use this for initialization
    void Start ()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var route = pathfinder.GetPath();
        StartCoroutine(FollowPath(route));
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

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        health -= FindObjectOfType<TowerController>().GetDamageValue();
        if (health <= 0 && !isDying)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        isDying = true;
        GameObject deathFX = Instantiate(deathEffect, transform.position, Quaternion.identity);
        //deathFX.transform.parent = parent;
        Destroy(this.gameObject, destroyTimer / 2);
        Destroy(deathFX, destroyTimer);

        //FindObjectOfType<GameSession>().AddToScore(enemyValue);
        //if (gameObject.tag == "Boss")
        //    FindObjectOfType<ResultScreenHandler>().HasWon(true);
    }

}
