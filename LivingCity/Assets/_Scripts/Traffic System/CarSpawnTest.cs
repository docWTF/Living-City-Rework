using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnTest : MonoBehaviour
{
    public AiDirector aiDirector;


    void Start()
    {
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                aiDirector.SpawnACar();
                Debug.Log("Attempting to spawn a car");
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(10f);
        }
    }


}
