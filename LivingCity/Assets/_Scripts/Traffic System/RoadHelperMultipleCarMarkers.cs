using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadHelperMultipleCarMarkers : RoadHelper
{

    [SerializeField]
    protected List<Marker> incomingMarkers, outgoingMarkers;

    public override Marker GetPositioForCarToSpawn(Vector3 nextPathPosition)
    {
        return GetClosestMarkerTo(nextPathPosition, outgoingMarkers);
    }

    public override Marker GetPositioForCarToStop(Vector3 previousPathPosition)
    {
        return GetClosestMarkerTo(previousPathPosition, incomingMarkers);
    }
}
