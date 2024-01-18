using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnTest : MonoBehaviour
{
    public AiDirector aiDirector;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCar();
    }

    IEnumerator SpawnCar()
    {
        for (int i = 0; i < 10; i++)
        {
            aiDirector.SpawnACar();
            Debug.Log("Attempting to spawn a car");
            yield return new WaitForSeconds(2f);
        }
    }

}
