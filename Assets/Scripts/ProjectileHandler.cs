using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    Vector3 explosionLocation;
    [SerializeField] float destroyTimer = 1f;
    [SerializeField] GameObject projectileExplosion;
    bool hit = false;

    private void OnParticleCollision(GameObject other)
    {
        if (!hit)
        {
            hit = true;
            var laserFX = Instantiate(projectileExplosion, other.transform.position, Quaternion.identity);
            Destroy(laserFX, destroyTimer);
        }
    }

    private void Update()
    {
        if (hit)
            Destroy(gameObject, destroyTimer);
        else
            Destroy(gameObject, destroyTimer * 5); //in case it never hits
    }
}
