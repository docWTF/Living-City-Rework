using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadHelperStraight : RoadHelper
{

    [SerializeField]
    private Marker leftLaneMarker90, rightLaneMarker90;

    public override Marker GetPositioForCarToSpawn(Vector3 nextPathPosition)
    {
        int angle = (int)transform.rotation.eulerAngles.y;
        var direction = nextPathPosition - transform.position;
        return GetCorrectMarker(angle, direction);
    }

    public override Marker GetPositioForCarToStop(Vector3 previousPathPosition)
    {
        int angle = (int)transform.rotation.eulerAngles.y;
        var direction = transform.position - previousPathPosition;
        return GetCorrectMarker(angle, direction);
    }

    private Marker GetCorrectMarker(int angle, Vector3 directionVector)
    {
        var direction = GetDirection(directionVector);

        if (angle == 0)
        {
            if (direction == Direction.left)
            {
                return rightLaneMarker90;
            }
            else
            {
                return leftLaneMarker90;
            }
        }
        else if (angle == 90)
        {
            if (direction == Direction.up)
            {
                return rightLaneMarker90;
            }
            else
            {
                return leftLaneMarker90;
            }
        }
        else if (angle == 270)
        {
            if (direction == Direction.left)
            {
                return leftLaneMarker90;
            }
            else
            {
                return rightLaneMarker90;
            }
        }
        else
        {
            if (direction == Direction.up)
            {
                return leftLaneMarker90;
            }
            else
            {
                return rightLaneMarker90;
            }
        }
    }

    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    public Direction GetDirection(Vector3 direction)
    {
        if(Mathf.Abs(direction.z) > 0.5f)
        {
            if(direction.z > 0.5f)
            {
                return Direction.up;
            }
            else
            {
                return Direction.down;
            }
        }
        else
        {
            if(direction.x > 0.5f)
            {
                return Direction.right;
            }
            else
            {
                return Direction.left;
            }
        }
    }
}
