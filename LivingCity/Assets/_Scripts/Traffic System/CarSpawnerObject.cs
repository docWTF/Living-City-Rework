using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerObject : MonoBehaviour
{

    public List<CarAI> carList = new List<CarAI>();

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (carList.Count == 0 && collision.gameObject.tag == "Car")
        {
            
        }
    }
}
