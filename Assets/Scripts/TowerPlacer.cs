using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {

    //this script is to replace the part in the waypoint script

    public bool isPlaceable = true;
    [SerializeField] TowerController towerPrefab;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            Instantiate(towerPrefab, transform.position, transform.rotation);
            isPlaceable = false;
        }
    }
}
