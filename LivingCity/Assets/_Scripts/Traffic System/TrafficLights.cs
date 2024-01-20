using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{

    public TrafficLightColor lightColor;


    [SerializeField]
    private int timerGreen = 25;
    [SerializeField]
    private int timerYellow = 5;
    [SerializeField]
    private int timerRed = 25;
    [SerializeField]
    private int timerWalk = 6;
    [SerializeField]
    private GameObject redLight, yellowLight, greenLight;


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
        StartCoroutine(TrafficLightsTimer(this, timerGreen, timerYellow, timerRed, timerWalk, startColor));
    }


    IEnumerator TrafficLightsTimer(TrafficLights trafficLightObject,int timerGreen, int timerYellow, int timerRed, int timerWalk, TrafficLightStart startColor)
    {
            if (startColor == TrafficLightStart.StartGreen)
            {
                while (true)
                {
                    Debug.Log("Turn Green");
                    trafficLightObject.GetComponent<BoxCollider>().enabled = false;
                    trafficLightObject.lightColor = TrafficLightColor.Green;
                    TrafficLightChangeColor(lightColor);
                    yield return new WaitForSeconds(timerGreen);

                    Debug.Log("Turn Yellow");
                    trafficLightObject.GetComponent<BoxCollider>().enabled = true;
                    trafficLightObject.lightColor = TrafficLightColor.Yellow;
                    TrafficLightChangeColor(lightColor);
                    yield return new WaitForSeconds(timerYellow);


                    Debug.Log("Turn Red");
                    trafficLightObject.lightColor = TrafficLightColor.Red;
                    TrafficLightChangeColor(lightColor);
                    yield return new WaitForSeconds(timerRed);
                    yield return new WaitForSeconds(timerWalk);


                }

            }
            if (startColor == TrafficLightStart.StartRed)
            {
                while(true)
                {
                Debug.Log("Turn Red");
                trafficLightObject.GetComponent<BoxCollider>().enabled = true;
                trafficLightObject.lightColor = TrafficLightColor.Red;
                TrafficLightChangeColor(lightColor);
                yield return new WaitForSeconds(timerRed);
                yield return new WaitForSeconds(timerWalk);

                Debug.Log("Turn Green");
                trafficLightObject.GetComponent<BoxCollider>().enabled = false;
                trafficLightObject.lightColor = TrafficLightColor.Green;
                TrafficLightChangeColor(lightColor);
                yield return new WaitForSeconds(timerGreen);

                Debug.Log("Turn Yellow");
                trafficLightObject.GetComponent<BoxCollider>().enabled = true;
                trafficLightObject.lightColor = TrafficLightColor.Yellow;
                TrafficLightChangeColor(lightColor);
                yield return new WaitForSeconds(timerYellow);

                }
            }
    }

    private void TrafficLightChangeColor(TrafficLightColor lightColor)
    {
        if (lightColor == TrafficLightColor.Red)
        {
            redLight.SetActive(true);
            yellowLight.SetActive(false);
            greenLight.SetActive(false);
        }
        else if (lightColor == TrafficLightColor.Yellow)
        {
            redLight.SetActive(false);
            yellowLight.SetActive(true);
            greenLight.SetActive(false);
        }
        else if (lightColor == TrafficLightColor.Green)
        {
            redLight.SetActive(false);
            yellowLight.SetActive(false);
            greenLight.SetActive(true);

        }
    }
}
