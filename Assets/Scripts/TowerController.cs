using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField] Transform objectToPan, target, weaponLocation;
    [SerializeField] ParticleSystem weaponEffect;
    [SerializeField] float fireCooldown = 1f;
    [SerializeField] int weaponDamage = 1;
    [SerializeField] float weaponRange = 25f;
    bool hasFired = false;
    bool enemyInRange = false;
    Coroutine firingCoroutine;

    private void Update()
    {
        TargetEnemy();
    }

    private void TargetEnemy()
    {
        var numberOfEnemies = FindObjectsOfType<EnemyController>();
        if(numberOfEnemies.Length == 0) { return; }

        Transform closestEnemy = numberOfEnemies[0].transform;
        foreach(EnemyController testEnemy in numberOfEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        target = closestEnemy;
        objectToPan.LookAt(target);

        CheckTargetDistance();
    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        var oldDistance = Vector3.Distance(gameObject.transform.position, closestEnemy.position);
        var newDistance = Vector3.Distance(gameObject.transform.position, testEnemy.position);

        if (oldDistance > newDistance)
            return testEnemy;
        else
            return closestEnemy;
    }

    private void CheckTargetDistance()
    {
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);

        if (distanceToTarget <= weaponRange)
        {
            enemyInRange = true;
            Fire();
        }
        else
            enemyInRange = false;
    }

    void Fire()
    {
        //StartCoroutine(FireRoutine());

        if (enemyInRange)
            firingCoroutine = StartCoroutine(FireRoutine());
        else
            StopCoroutine(firingCoroutine);

        //Add this when weapon effects aren't showing correctly
        //Coroutine firingCoroutine;
        //if(enemy.isAlive)
        //  firingCoroutine = StartCoroutine(FireRoutine());
        //else
        //  StopCoroutine(firingCoroutine);
    }

    IEnumerator FireRoutine()
    {
        if (!hasFired)
        {
            hasFired = true;
            var weapon = Instantiate(weaponEffect, weaponLocation.position, weaponLocation.rotation);
            weapon.Emit(1);
            yield return new WaitForSeconds(fireCooldown);
            hasFired = false;
        }
    }

    public int GetDamageValue()
    {
        return weaponDamage;
    }
}
