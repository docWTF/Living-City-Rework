using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellTypeOther : MonoBehaviour
{
    public CellType type;
    public GridManager gridManager;
    public Vector3Int objectPosition;



    private void Start()
    {
        objectPosition = Vector3Int.FloorToInt(this.transform.position);
        gridManager = FindAnyObjectByType<GridManager>();


        gridManager.PlaceObjectOnTheMap(objectPosition, gameObject, type, 1, 1);




        // gridManager.placementGrid[objectPosition.x, objectPosition.z] = type; 
        // gridManager.structureDictionary.Add(objectPosition, structure);
    }

}
