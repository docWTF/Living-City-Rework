using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{
    public MeshRenderer trafficLightGreen;
    public MeshRenderer trafficLightYellow;
    public MeshRenderer trafficLightRed;

    public TrafficLightColor lightColor;


    [SerializeField]
    private int timerGreen = 25;
    [SerializeField]
    private int timerYellow = 5;
    [SerializeField]
    private int timerRed = 25;
    public enum TrafficLightColor
    {
        Green,
        Yellow,
        Red,
    }

    public enum TrafficLightStart
    {
        StartGreen,
        StartRed
    }

    public TrafficLightStart startColor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TrafficLightsTimer(this, timerGreen, timerYellow, timerRed, startColor));
    }

    IEnumerator TrafficLightsTimer(TrafficLights trafficLightObject,int timerGreen, int timerYellow, int timerRed, TrafficLightStart startColor)
    {
            if (startColor == TrafficLightStart.StartGreen)
            {
                while (true)
                {
                Debug.Log("Turn Green");
                    trafficLightObject.GetComponent<BoxCollider>().enabled = false;
                    trafficLightObject.lightColor = TrafficLightColor.Green;
                    //TrafficLightsChangeColor(trafficLightObject, trafficLightObject.lightColor);
                    yield return new WaitForSeconds(timerGreen);

                Debug.Log("Turn Yellow");
                    trafficLightObject.GetComponent<BoxCollider>().enabled = true;
                    trafficLightObject.lightColor = TrafficLightColor.Yellow;
                   //TrafficLightsChangeColor(trafficLightObject, trafficLightObject.lightColor);
                    yield return new WaitForSeconds(timerYellow);


                Debug.Log("Turn Red");
                trafficLightObject.lightColor = TrafficLightColor.Red;
                    //TrafficLightsChangeColor(trafficLightObject, trafficLightObject.lightColor);
                    yield return new WaitForSeconds(timerRed);
                }

            }
            if (startColor == TrafficLightStart.StartRed)
            {
                while(true)
                {
                Debug.Log("Turn Yellow");
                trafficLightObject.GetComponent<BoxCollider>().enabled = true;
                trafficLightObject.lightColor = TrafficLightColor.Red;
                //TrafficLightsChangeColor(trafficLightObject, trafficLightObject.lightColor);
                yield return new WaitForSeconds(timerRed);

                Debug.Log("Turn Green");
                trafficLightObject.GetComponent<BoxCollider>().enabled = false;
                trafficLightObject.lightColor = TrafficLightColor.Green;
                //TrafficLightsChangeColor(trafficLightObject, trafficLightObject.lightColor);
                yield return new WaitForSeconds(timerGreen);

                Debug.Log("Turn Red");
                trafficLightObject.GetComponent<BoxCollider>().enabled = true;
                trafficLightObject.lightColor = TrafficLightColor.Yellow;
                //TrafficLightsChangeColor(trafficLightObject, trafficLightObject.lightColor);
                yield return new WaitForSeconds(timerYellow);

                }
            }
    }

    /* private void TrafficLightsChangeColor(TrafficLights trafficLightObject, TrafficLightColor lightColor)
    {
        if (trafficLightObject.lightColor == TrafficLightColor.Green)
        {
            trafficLightObject.GetComponentInChildren<MeshRenderer>().material = trafficLightObject.trafficLightGreen.material;
        }
        else if (trafficLightObject.lightColor == TrafficLightColor.Yellow)
        {
            trafficLightObject.GetComponentInChildren<MeshRenderer>().material = trafficLightObject.trafficLightYellow.material;
        }
        else if (trafficLightObject.lightColor == TrafficLightColor.Red)
        {
            trafficLightObject.GetComponentInChildren<MeshRenderer>().material = trafficLightObject.trafficLightRed.material;
        }
    } */


}
