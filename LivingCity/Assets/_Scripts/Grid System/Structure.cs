using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class Structure: MonoBehaviour, INeedingRoad
{



    public Vector3Int RoadPosition { get; set; }



    public Vector3 GetNearestPedestrianMarkerTo(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetClosestPedestrainPosition(position);
    }

    public Marker GetPedestrianSpawnMarker(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetpositioForPedestrianToSpawn(position);
    }

    public List<Marker> GetPedestrianMarkers()
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetAllPedestrianMarkers();
    }

    public Vector3 GetNearestCarMarkerTo(Vector3 position)
    {
        return transform.GetComponent<RoadHelper>().GetClosestCarMarkerPosition(position);
    }

    internal List<Marker> GetCarMarkers()
    {
        return transform.GetComponent<RoadHelper>().GetAllCarMarkers();
    }

    public Marker GetCarSpawnMarker(Vector3Int nextPathPosition)
    {
        return transform.GetComponent<RoadHelper>().GetPositioForCarToSpawn(nextPathPosition);
    }

    public Marker GetCarStopMarker(Vector3Int previousPathPosition)
    {
        return transform.GetComponent<RoadHelper>().GetPositioForCarToStop(previousPathPosition);
    }
}
