using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    const int gridSize = 10;
    Vector2Int gridPos;

    //can be public since it's a data class
    public bool isExplored = false;
    bool colorChanged = false;
    [SerializeField] Color activatedColor = Color.blue;

    public Waypoint foundFrom;

    public int GetGridsize()
    {
        return gridSize;
    }

    private void Update()
    {
        if (isExplored && !colorChanged)
            SetTopColor(activatedColor);
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
        colorChanged = true;
    }
}
