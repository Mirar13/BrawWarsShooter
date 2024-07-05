using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.AI.Navigation
{
    public class AINavigationMap : MonoBehaviour
    {
        public static AINavigationMap Instance;
        public NavigationPoint[] NavigationPoints;

        private Dictionary<PointType, List<NavigationPoint>> _pointsByType =
            new Dictionary<PointType, List<NavigationPoint>>();

        private void Awake()
        {
            Instance = this;
            foreach (var point in NavigationPoints)
            {
                if (!_pointsByType.ContainsKey(point.Type))
                {
                    _pointsByType.Add(point.Type, new List<NavigationPoint>());
                }
                _pointsByType[point.Type].Add(point);
            }
        }

        public Vector3 GetPoint(PointType type)
        {
            if (!_pointsByType.TryGetValue(type, out var points))
            {
                return Vector3.zero;
            }

            var weights = points.Select(x => x.Weight).ToList();
            var index = MathUtils.Quantile(weights);
            var selectedPoint = points[index];
            var randomPointCircle = Random.insideUnitCircle * (selectedPoint.Radius / 2f);
            return points[index].transform.position + new Vector3(randomPointCircle.x,0,randomPointCircle.y);
        }
    }
}