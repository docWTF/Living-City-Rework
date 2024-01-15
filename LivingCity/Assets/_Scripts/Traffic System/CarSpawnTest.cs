using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnTest : MonoBehaviour
{
    public AiDirector aiDirector;

    // Start is called before the first frame update
    void Start()
    {
        aiDirector.SpawnACar();
        Debug.Log("Attempting to spawn a car");
    }


}
