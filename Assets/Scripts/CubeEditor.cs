using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour {

    //[SerializeField] [Range(1, 20)] int gridSize = 10;
    TextMesh textMesh;
    Waypoint wayPoint;

    private void Awake()
    {
        wayPoint = GetComponent<Waypoint>();
    }

    void Update ()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = wayPoint.GetGridsize();
        transform.position = new Vector3(
            wayPoint.GetGridPos().x * gridSize,
            transform.position.y,
            wayPoint.GetGridPos().y * gridSize); 
    }

    private void UpdateLabel()
    {
        string labelText = wayPoint.GetGridPos().x + "," + wayPoint.GetGridPos().y;
        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }
}
