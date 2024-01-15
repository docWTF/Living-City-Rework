using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public Grid placementGrid;

    public Dictionary<Vector3Int, Structure> structureDictionary = new Dictionary<Vector3Int, Structure>();

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    internal void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, CellType type, int width = 1, int height = 1)
    {
        Structure structure = structurePrefab.AddComponent<Structure>();

        var structureNeedingRoad = structure.GetComponent<INeedingRoad>();
        if (structureNeedingRoad != null)
        {
            structureNeedingRoad.RoadPosition = GetNearestRoad(position, width, height).Value;
            Debug.Log("My nearest road position is: " + structureNeedingRoad.RoadPosition);
        }
        if (structureNeedingRoad == null)
        {
            Debug.Log("Found problem");
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                placementGrid[newPosition.x, newPosition.z] = type;
                structureDictionary.Add(newPosition, structure);
            }
        }


    }

    internal void PlaceRoadOnTheMap(Vector3Int position, GameObject structurePrefab, CellType type, int width = 1, int height = 1)
    {
        Structure structure = structurePrefab.AddComponent<Structure>();

                placementGrid[position.x, position.z] = type;
                structureDictionary.Add(position, structure);


    }


    internal CellType[] GetNeighbourTypesFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }

    internal bool CheckIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    private Vector3Int? GetNearestRoad(Vector3Int position, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var newPosition = position + new Vector3Int(x, 0, y);
                var roads = GetNeighboursOfTypeFor(newPosition, CellType.Road);
                if (roads.Count > 0)
                {
                    return roads[0];
                }
            }
        }
        return null;
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }


    internal List<Vector3Int> GetNeighboursOfTypeFor(Vector3Int position, CellType type)
    {
        var neighbourVertices = placementGrid.GetAdjacentCellsOfType(position.x, position.z, type);
        List<Vector3Int> neighbours = new List<Vector3Int>();
        foreach (var point in neighbourVertices)
        {
            neighbours.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return neighbours;
    }

    internal List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition, bool isAgent = false)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.z), new Point(endPosition.x, endPosition.z), isAgent);
        List<Vector3Int> path = new List<Vector3Int>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return path;
    }

    public Structure GetRandomRoad()
    {
        var point = placementGrid.GetRandomRoadPoint();
        return GetStructureAt(point);
    }

    public Structure GetRandomPavement()
    {
        var point = placementGrid.GetRandomPavementPoint();
        return GetStructureAt(point);
    }

    public Structure GetRandomSpecialStrucutre()
    {
        var point = placementGrid.GetRandomSpecialStructurePoint();
        return GetStructureAt(point);
    }

    public Structure GetRandomHouseStructure()
    {
        var point = placementGrid.GetRandomHouseStructurePoint();
        return GetStructureAt(point);
    }

    public Structure GetRandomCarSpawner()
    {
        var point = placementGrid.GetRandomCarSpawnerPoint();
        return GetStructureAt(point);
    }

    public Structure GetRandomCarDespawner()
    {
        var point = placementGrid.GetRandomCarDespawnerPoint();
        return GetStructureAt(point);
    }

    public List<Structure> GetAllHouses()
    {
        List<Structure> returnList = new List<Structure>();
        var housePositions = placementGrid.GetAllHouses();
        foreach (var point in housePositions)
        {
            returnList.Add(structureDictionary[new Vector3Int(point.X, 0, point.Y)]);
        }
        return returnList;
    }

    internal List<Structure> GetAllSpecialStructures()
    {
        List<Structure> returnList = new List<Structure>();
        var housePositions = placementGrid.GetAllSpecialStructure();
        foreach (var point in housePositions)
        {
            returnList.Add(structureDictionary[new Vector3Int(point.X, 0, point.Y)]);
        }
        return returnList;
    }

    public List<Structure> GetAllCarSpawner()
    {
        List<Structure> returnList = new List<Structure>();
        var housePositions = placementGrid.GetAllCarSpawner();
        foreach (var point in housePositions)
        {
            returnList.Add(structureDictionary[new Vector3Int(point.X, 0, point.Y)]);
        }
        return returnList;
    }

    public List<Structure> GetAllCarDespawner()
    {
        List<Structure> returnList = new List<Structure>();
        var housePositions = placementGrid.GetAllCarDespawner();
        foreach (var point in housePositions)
        {
            returnList.Add(structureDictionary[new Vector3Int(point.X, 0, point.Y)]);
        }
        return returnList;
    }

    private Structure GetStructureAt(Point point)
    {
        if (point != null)
        {
            return structureDictionary[new Vector3Int(point.X, 0, point.Y)];
        }
        return null;
    }

    public Structure GetStructureAt(Vector3Int position)
    {
        if (structureDictionary.ContainsKey(position))
        {
            return structureDictionary[position];
        }
        return null;
    }
}