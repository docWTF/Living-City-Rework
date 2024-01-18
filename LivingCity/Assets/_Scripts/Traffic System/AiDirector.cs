
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;


    public class AiDirector : MonoBehaviour
    {
        public GridManager gridManager;
        public GameObject[] pedestrianPrefabs;

        public GameObject carPrefab;

        AdjacencyGraph pedestrianGraph = new AdjacencyGraph();
        AdjacencyGraph carGraph = new AdjacencyGraph();

        List<Vector3> carPath = new List<Vector3>();

    public void SpawnACar()
        {
            foreach (var spawner in gridManager.GetAllCarSpawner())
            {
                TrySpawninACar(spawner, gridManager.GetRandomCarDespawner());
            }
        }

        private void TrySpawninACar(Structure startStructure, Structure endStructure)
        {
            if (startStructure != null && endStructure != null)
            {
                var startRoadPosition = ((INeedingRoad)startStructure).RoadPosition;
                var endRoadPosition = ((INeedingRoad)endStructure).RoadPosition;

                var path = gridManager.GetPathBetween(startRoadPosition, endRoadPosition, true);
                path.Reverse();

                if (path.Count == 0 && path.Count > 2)
            {
                return;
            }



            var startMarkerPosition = gridManager.GetStructureAt(startRoadPosition).GetCarSpawnMarker(path[1]);
            var stopMarkerPosition = gridManager.GetStructureAt(endRoadPosition).GetCarStopMarker(path[path.Count - 2]);
            carPath = GetCarPath(path, startMarkerPosition.Position, stopMarkerPosition.Position);

            if (carPath.Count > 0)
            {
                var car = Instantiate(carPrefab, startMarkerPosition.Position, Quaternion.identity);
                car.GetComponent<CarAI>().SetPath(carPath);
            }

            }
        }

        private List<Vector3> GetPedestrianPath(List<Vector3Int> path, Vector3 startPosition, Vector3 endPosition)
        {
            pedestrianGraph.ClearGraph();
            CreatAPedestrianGraph(path);
            Debug.Log(pedestrianGraph);
            return AdjacencyGraph.AStarSearch(pedestrianGraph, startPosition, endPosition);
        }

        private void CreatAPedestrianGraph(List<Vector3Int> path)
        {
            Dictionary<Marker, Vector3> tempDictionary = new Dictionary<Marker, Vector3>();

            for (int i = 0; i < path.Count; i++)
            {
                var currentPosition = path[i];
                var roadStructure = gridManager.GetStructureAt(currentPosition);
                var markersList = roadStructure.GetPedestrianMarkers();
                bool limitDistance = markersList.Count == 4;
                tempDictionary.Clear();
                foreach (var marker in markersList)
                {
                    pedestrianGraph.AddVertex(marker.Position);
                    foreach (var markerNeighbourPosition in marker.GetAdjacentPositions())
                    {
                        pedestrianGraph.AddEdge(marker.Position, markerNeighbourPosition);
                    }

                    if (marker.OpenForconnections && i + 1 < path.Count)
                    {
                        var nextRoadStructure = gridManager.GetStructureAt(path[i + 1]);
                        if (limitDistance)
                        {
                            tempDictionary.Add(marker, nextRoadStructure.GetNearestPedestrianMarkerTo(marker.Position));
                        }
                        else
                        {
                            pedestrianGraph.AddEdge(marker.Position, nextRoadStructure.GetNearestPedestrianMarkerTo(marker.Position));
                        }
                    }
                }
                if (limitDistance && tempDictionary.Count == 4)
                {
                    var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.Position, x.Value)).ToList();
                    for (int j = 0; j < 2; j++)
                    {
                        pedestrianGraph.AddEdge(distanceSortedMarkers[j].Key.Position, distanceSortedMarkers[j].Value);
                    }
                }
            }
        }

    private List<Vector3> GetCarPath(List<Vector3Int> path, Vector3 startPosition, Vector3 endPosition)
    {
        carGraph.ClearGraph();
        CreatACarGraph(path);
        Debug.Log(carGraph);
        return AdjacencyGraph.AStarSearch(carGraph, startPosition, endPosition);
    }

    private void CreatACarGraph(List<Vector3Int> path)
    {
        Dictionary<Marker, Vector3> tempDictionary = new Dictionary<Marker, Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            var currentPosition = path[i];
            var roadStructure = gridManager.GetStructureAt(currentPosition);
            var markersList = roadStructure.GetCarMarkers();
            var limitDistance = markersList.Count > 3;
            tempDictionary.Clear();

            foreach (var marker in markersList)
            {
                carGraph.AddVertex(marker.Position);
                foreach (var markerNeighbor in marker.adjacentMarkers)
                {
                    carGraph.AddEdge(marker.Position, markerNeighbor.Position);
                }
                if (marker.OpenForconnections && i + 1 < path.Count)
                {
                    var nextRoadPosition = gridManager.GetStructureAt(path[i+1]);
                    if (limitDistance)
                    {
                        tempDictionary.Add(marker, nextRoadPosition.GetNearestCarMarkerTo(marker.Position));
                    }
                    else
                    {
                        carGraph.AddEdge(marker.Position, nextRoadPosition.GetNearestCarMarkerTo(marker.Position));
                    }
                }
            }
            if (limitDistance)
            {
                var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.Position, x.Value)).ToList();
                for (int j = 0; j < 2; j++)
                {
                    carGraph.AddEdge(distanceSortedMarkers[j].Key.Position, distanceSortedMarkers[j].Value);
                }
            }
        }
    }

    private GameObject GetRandomPedestrian()
        {
            return pedestrianPrefabs[UnityEngine.Random.Range(0, pedestrianPrefabs.Length)];
        }

        private void Update()
    {
        DrawGraph(carGraph);
        for (int i = 1; i < carPath.Count; i++)
        {
            Debug.DrawLine(carPath[i-1] + Vector3.up * 2, carPath[i] + Vector3.up * 2, Color.blue);
        }
    }

    private void DrawGraph(AdjacencyGraph graph)
    {
        foreach (var vertex in graph.GetVertices())
        {
            foreach (var vertexNeighbour in graph.GetConnectedVerticesTo(vertex))
            {
                Debug.DrawLine(vertex.Position + Vector3.up, vertexNeighbour.Position + Vector3.up, Color.red);
            }
        }
    }

    }








