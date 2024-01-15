using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefab;
    void Start()
    {
        Instantiate(SelectRandomCarPrefab(), transform);
    }

    private GameObject SelectRandomCarPrefab()
    {
        var randomIndex = Random.Range(0, carPrefab.Length);

        return carPrefab[randomIndex];
    }
}
