using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Point> _points;
    [SerializeField] private List<Point> _spawnPoints;
    private int _currentSpawnPoint;
    public List<Point> Points => _points;

    public Point GetSpawnPoint()
    {
        var point = _spawnPoints[_currentSpawnPoint];
        if (_currentSpawnPoint + 1 < _spawnPoints.Count)
        {
            _currentSpawnPoint++;
        }
        else
        {
            _currentSpawnPoint = 0;
        }

        return point;
    }

    public Point GetNext(Point previousPoint, Vector3 position)
    {
        if (previousPoint == null) return _points[0];
        var points = FindTwoNearestPoints(previousPoint.Weight, position);

        if (points.Item1 == null && points.Item2 != null) return points.Item2;
        if (points.Item2 == null && points.Item1 != null) return points.Item1;
        if (points.Item1 == null && points.Item2 == null) return null;
        if (points.Item1.Weight > points.Item2.Weight)
        {
            return points.Item1;
        }
        else
        {
            return points.Item2;
        }
    }

    private (Point, Point) FindTwoNearestPoints(int weight, Vector3 position)
    {
        Point nearest1 = null;
        Point nearest2 = null;
        var minDistance1 = float.MaxValue;
        var minDistance2 = float.MaxValue;
        
        foreach (var point in _points)
        {
            if (point.Weight < weight) continue;
            
            var distance = Vector3.Distance(position, point.transform.position);

            if (distance < minDistance1)
            {
                if (nearest1 != null)
                {
                    minDistance2 = minDistance1;
                    nearest2 = nearest1;
                }

                minDistance1 = distance;
                nearest1 = point;
            }
            else if (distance < minDistance2)
            {
                minDistance2 = distance;
                nearest2 = point;
            }
        }

        return new ValueTuple<Point, Point>(nearest1, nearest2);
    }
}