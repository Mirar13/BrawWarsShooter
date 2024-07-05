using System.Collections.Generic;
using DefaultNamespace.Features.ProjectGamePlay;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Global
{
    public class GamePlayEntitiesHolder : MonoBehaviour
    {
        public static GamePlayEntitiesHolder _instance;

        public static GamePlayEntitiesHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("GamePlayEntitiesHolder").AddComponent<GamePlayEntitiesHolder>();
                }
                return _instance;
            }
        }

        private List<GamePlayEntity> _entities = new ();
        private Dictionary<int,List<GamePlayEntity>> _entitiesByTeam = new ();
        
        public void Register(GamePlayEntity entity)
        {
            _entities.Add(entity);
            if (entity.TryGetEntityComponent(out GamePlayTeamEntityComponent teamEntityComponent))
            {
                var team = teamEntityComponent.Team;
                if (!_entitiesByTeam.ContainsKey(team))
                {
                    _entitiesByTeam[team] = new List<GamePlayEntity>();
                }
                _entitiesByTeam[team].Add(entity);
            }
        }

        public void Unregister(GamePlayEntity entity)
        {
            _entities.Remove(entity);
            if (entity.TryGetEntityComponent(out GamePlayTeamEntityComponent teamEntityComponent))
            {
                var team = teamEntityComponent.Team;
                if (_entitiesByTeam.ContainsKey(team))
                {
                    _entitiesByTeam[team].Remove(entity);
                }
            }
        }

        public (JobHandle jobHandle, ClosestFindIndexJob job) GetClosest(GamePlayEntity entity, float targetDistance)
        {
            var team = -1;
            if (entity.TryGetEntityComponent(out GamePlayTeamEntityComponent teamEntityComponent))
            {
                team = teamEntityComponent.Team;
            }

            var result = new NativeArray<int>(2, Allocator.TempJob);
            result[0] = -1;
            
            var positionsList = new List<Vector3>();
            var positionsTeamEndIndexesList = new List<int>();
            var teamIndexesList = new List<int>();
            if (team == -1)
            {
                for (int i = 0; i < _entities.Count; i++)
                {
                    positionsList.Add(_entities[i].transform.position);
                }
                positionsTeamEndIndexesList.Add(positionsList.Count);
            }
            else
            {
                foreach (var (teamId, entities) in _entitiesByTeam)
                {
                    if (teamId == team)
                    {
                        continue;
                    }
                    foreach (var targetEntity in entities)
                    {
                        positionsList.Add(targetEntity.transform.position);
                    }
                    teamIndexesList.Add(teamId);
                    positionsTeamEndIndexesList.Add(positionsList.Count);
                }
            }
            
            var positions = new NativeArray<Vector3>(positionsList.ToArray(), Allocator.TempJob);
            var teamIndexes = new NativeArray<int>(teamIndexesList.ToArray(), Allocator.TempJob);
            var positionsTeamEndIndexes = new NativeArray<int>(positionsTeamEndIndexesList.ToArray(), Allocator.TempJob);
            var job = new ClosestFindIndexJob()
            {
                PositionsByTeams = positions,
                TeamEndIndexes = positionsTeamEndIndexes,
                TeamIndexes = teamIndexes,
                SelfPosition = entity.transform.position,
                TargetDistance = targetDistance,
                Result = result
            };
            
            JobHandle jobHandle = job.Schedule();
            return (jobHandle, job);
        }

        public bool TryGetByIndex(int teamIndex, int index, out GamePlayEntity result)
        {
            result = null;
            if (!_entitiesByTeam.TryGetValue(teamIndex, out var team))
            {
                return false;
            }

            if (index < 0 || index >= team.Count)
            {
                return false;
            }

            result = team[index];
            return true;
        }
    }

    [BurstCompile]
    public struct ClosestFindIndexJob : IJob
    {
        [ReadOnly] public NativeArray<Vector3> PositionsByTeams;
        [ReadOnly] public NativeArray<int> TeamEndIndexes;
        [ReadOnly] public NativeArray<int> TeamIndexes;
        [ReadOnly] public Vector3 SelfPosition;
        [ReadOnly] public float TargetDistance;
        public NativeArray<int> Result;

        public void Execute()
        {
            float minDistance = float.MaxValue;
            int foundedTeamIndex = -1;
            int foundedIndex = -1;

            var teamIndex = 0;
            for (var i = 0; i < PositionsByTeams.Length; i++)
            {
                if (i >= TeamEndIndexes[teamIndex])
                {
                    teamIndex++;
                }
                
                if (SelfPosition == PositionsByTeams[i])
                {
                    continue;
                }
            
                var distance = Vector3.Distance(SelfPosition, PositionsByTeams[i]);
                if (distance > TargetDistance)
                {
                    continue;
                }
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    foundedIndex = i;
                    foundedTeamIndex = TeamIndexes[teamIndex];
                }
            }

            Result[0] = foundedTeamIndex;
            Result[1] = foundedIndex;
        }
    }
}