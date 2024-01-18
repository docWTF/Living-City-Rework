using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarAI : MonoBehaviour
{

    [SerializeField]
    private List<Vector3> path = null;
    [SerializeField]
    private float arriveDistance = 0.3f, lastpointArriveDistance = 0.1f;
    [SerializeField]
    private float turningAngleOffset = 5;
    [SerializeField]
    private Vector3 currentTargetPosition;
    [SerializeField]
    private GameObject raycastObject;
    [SerializeField]
    private float raycastLength;

    private int index = 0;

    private bool stop;
    private bool stopCollision;

    public bool Stop
    {
        get { return stop || stopCollision; }
        set { stop = value; }
    }

    [field: SerializeField]
    public UnityEvent<Vector2> OnDrive { get; set; }

    private void Start()
    {
        if (path == null || path.Count == 0)
        {
            Stop = true;
        }
        else
        {
            currentTargetPosition = path[index];
        }
    }

    public void SetPath(List<Vector3> path)
    {
        if(path.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
        this.path = path;
        index = 0;
        currentTargetPosition = this.path[index];

        Vector3 relativePoint = transform.InverseTransformPoint(this.path[index + 1]);

        float angle = Mathf.Atan2(relativePoint.x, relativePoint.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
        Stop = false;
    }

    private void Update()
    {
        CheckForCollision();
        CheckIfArrived();
        Drive();
        
    }

   private void CheckForCollision()
    {
        if (Physics.Raycast(raycastObject.transform.position, transform.forward, raycastLength, 1 << gameObject.layer))
        {
            stopCollision = true;
        }
        else
        {
            stopCollision = false;
        }
    }

    private void Drive()
    {
        if (Stop)
        {
            OnDrive?.Invoke(Vector2.zero);
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
        if(!Stop)
        {
            this.GetComponent<BoxCollider>().isTrigger = true;
            Vector3 relativePoint = transform.InverseTransformPoint(currentTargetPosition);

            float angle = Mathf.Atan2(relativePoint.x, relativePoint.z) * Mathf.Rad2Deg;

            var rotateCar = 0;

            if (angle > turningAngleOffset)
            {
                rotateCar = 1;
            }
            else if (angle < -turningAngleOffset) 
            {
                rotateCar = -1;
            }
            OnDrive?.Invoke(new Vector2(rotateCar, 1));
        }
    }

    private void CheckIfArrived()
    {
        if (Stop == false)
        {
            var distanceToCheck = arriveDistance;
            if(index == path.Count -1)
            {
                distanceToCheck = lastpointArriveDistance;
            }
            if(Vector3.Distance(currentTargetPosition, transform.position) < distanceToCheck)
            {
                SetNextTargetIndex();
            }
        }
    }

    private void SetNextTargetIndex()
    {
        index++;
        if (index >= path.Count)
        {
            Stop = true;
            Destroy(gameObject);
        }
        else
        {
            currentTargetPosition = path[index];
        }
    }

}
