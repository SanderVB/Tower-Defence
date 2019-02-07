using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Range(0.1f, 120f)][SerializeField] float spawnDelay = 5f;
    [SerializeField] EnemyController enemyPrefab;
    bool isSpawning = false;

    private void Update()
    {
        if(!isSpawning)
            StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);
        EnemyController newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        newEnemy.transform.parent = this.gameObject.transform;
        isSpawning = false;
    }

    IEnumerator SpawnEnemiesRepeadetly()
    {
        yield return new WaitForSeconds(spawnDelay);
    }
}
