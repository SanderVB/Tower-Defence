using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Range(0.1f, 120f)][SerializeField] float spawnDelay = 5f;
    [SerializeField] int numberToSpawn = 25;
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] AudioClip spawnSound;

    bool isSpawning = false;
    bool levelFinished = false;
    Coroutine spawner;

    private void Update()
    {
        if(!isSpawning)
            spawner = StartCoroutine(SpawnEnemy());
        if (numberToSpawn <= 0 && !levelFinished)
        {
            StopCoroutine(spawner);
            FinishLevel();
        }
    }

    private void FinishLevel()
    {
        int enemies = FindObjectsOfType<EnemyController>().Length; //I would normally not write it like this because it's slow,
        if(enemies == 0)                                           //and I would prefer to use something like a list containing
        {                                                          //enemies and do something when the list is empty
            levelFinished = true;                                  //but that defeats the purpose of this project 
            FindObjectOfType<GameSession>().LevelFinished(true);   //(learning bascis about pathfinding algorithm)
        }
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);
        EnemyController newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        newEnemy.transform.parent = this.gameObject.transform;
        //AudioSource.PlayClipAtPoint(spawnSound, newEnemy.transform.position);
        GetComponent<AudioSource>().PlayOneShot(spawnSound);
        isSpawning = false;
        numberToSpawn--;
    }
}
