using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField] Transform objectToPan, target, weaponLocation;
    [SerializeField] ParticleSystem weaponEffect;
    [SerializeField] float fireCooldown = 1f;
    [SerializeField] int weaponDamage = 1;
    bool hasFired = false;

    private void Update()
    {
        objectToPan.LookAt(target);
        Fire();
    }

    void Fire()
    {
        StartCoroutine(FireRoutine());

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
