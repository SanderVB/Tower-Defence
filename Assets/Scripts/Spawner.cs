using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Range(0.1f, 120f)][SerializeField] float spawnDelay = 5f;
    [SerializeField] int numberToSpawn = 25;
    [SerializeField] EnemyController enemyPrefab;
    bool isSpawning = false;
    Coroutine spawner;

    private void Update()
    {
        if(!isSpawning)
            spawner = StartCoroutine(SpawnEnemy());
        if (numberToSpawn <= 0)
            StopCoroutine(spawner);
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);
        EnemyController newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        newEnemy.transform.parent = this.gameObject.transform;
        isSpawning = false;
        numberToSpawn--;
    }
}
