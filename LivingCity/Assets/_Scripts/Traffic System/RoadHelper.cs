using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class RoadHelper : MonoBehaviour
{
    [SerializeField]
    protected List<Marker> pedestrianMarkers;
    [SerializeField]
    protected List<Marker> carMarkers;
    [SerializeField]
    protected bool isCorner;
    [SerializeField]
    protected bool hasCrosswalks;
    [SerializeField]
    private Marker incoming, outgoing;

    float approximateThresholdCorner = 0.3f;

    public virtual Marker GetpositioForPedestrianToSpawn(Vector3 structurePosition)
    {
        return GetClosestMarkerTo(structurePosition, pedestrianMarkers);
    }

    public virtual Marker GetPositioForCarToSpawn(Vector3 nextPathPosition)
    {
        return outgoing;
    }

    public virtual Marker GetPositioForCarToStop(Vector3 previousPathPosition)
    {
        return incoming;
    }

    protected Marker GetClosestMarkerTo(Vector3 structurePosition, List<Marker> pedestrianMarkers, bool isCorner = false)
    {
        if (isCorner)
        {
            foreach (var marker in pedestrianMarkers)
            {
                var direction = marker.Position - structurePosition;
                direction.Normalize();
                if (Mathf.Abs(direction.x) < approximateThresholdCorner || Mathf.Abs(direction.z) < approximateThresholdCorner)
                {
                    return marker;
                }
            }
            return null;
        }
        else
        {
            Marker closestMarker = null;
            float distance = float.MaxValue;
            foreach (var marker in pedestrianMarkers)
            {
                var markerDistance = Vector3.Distance(structurePosition, marker.Position);
                if (distance > markerDistance)
                {
                    distance = markerDistance;
                    closestMarker = marker;
                }
            }
            return closestMarker;
        }
    }

    public Vector3 GetClosestPedestrainPosition(Vector3 currentPosition)
    {
        return GetClosestMarkerTo(currentPosition, pedestrianMarkers, isCorner).Position;
    }

    public List<Marker> GetAllPedestrianMarkers()
    {
        return pedestrianMarkers;
    }

    public Vector3 GetClosestCarMarkerPosition(Vector3 currentPosition)
    {
        return GetClosestMarkerTo(currentPosition, carMarkers, false).Position;
    }

    public List<Marker> GetAllCarMarkers()
    {
        return carMarkers;
    }
}

