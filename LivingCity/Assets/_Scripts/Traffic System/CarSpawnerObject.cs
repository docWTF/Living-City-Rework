using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerObject : MonoBehaviour
{
    List<GameObject> carsInSpawn = new List<GameObject>();

    public bool isOccupied;


    private void OnTriggerStay(Collider other)
    {
        if (carsInSpawn.Count == 0 && other.CompareTag("Car"))
        {
            carsInSpawn.Add(other.gameObject);
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (carsInSpawn.Count > 0 && other.gameObject == carsInSpawn[0])
        {
            carsInSpawn.Clear();
            isOccupied = false;
        }
    }
}
