﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {

    //this script is to replace the part in the waypoint script

    public bool isPlaceable = true;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPlaceable = !FindObjectOfType<TowerFactory>().CheckIfContainsTower(transform);

            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(transform);
                isPlaceable = false;
            }
        }
    }
}
