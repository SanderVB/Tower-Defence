using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    const int gridSize = 10;
    Vector2Int gridPos;

    //can be public since it's a data class
    public bool isExplored = false;
    public bool isPlaceable = true;
    bool colorChanged = false;
    [SerializeField] Color activatedColor = Color.blue;

    public Waypoint foundFrom;

    public int GetGridsize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && isPlaceable)
            Debug.Log(gameObject + ": " + GetGridPos());
    }
}
