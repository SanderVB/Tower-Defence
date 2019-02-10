using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{

    [SerializeField] int towerLimit = 5;
    [SerializeField] TowerController towerPrefab;

    Queue<TowerController> towerQueue = new Queue<TowerController>(); //would use tuples but using C#4 Queue<(TowerController,Transform)>

    public void AddTower(Transform location)
    {
        int numTowers = towerQueue.Count;

        if (numTowers < towerLimit)
        {
            InstantiateNewTower(location);
        }
        else
        {
            MoveExistingTower(location);
        }
    }

    private void InstantiateNewTower(Transform location)
    {
        var newTower = Instantiate(towerPrefab, location.position, location.rotation);
        newTower.transform.parent = this.gameObject.transform;
        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Transform location)
    {
        var oldTower = towerQueue    .Dequeue();

        oldTower   .transform.position  = location.position;

        towerQueue.     Enqueue(oldTower);
    }

    public bool CheckIfContainsTower(Transform location)
    {
        List<Vector3> towerLocations = new List<Vector3>();
        foreach (TowerController tower in towerQueue)
        {
            towerLocations.Add(tower.transform.position);
        }

        if (towerLocations.Contains(location.position))
        {
            return true;
        }
        else
            return false;
    }
}