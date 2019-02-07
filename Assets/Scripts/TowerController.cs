using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform target;

    private void Update()
    {
        objectToPan.LookAt(target);
    }

}
