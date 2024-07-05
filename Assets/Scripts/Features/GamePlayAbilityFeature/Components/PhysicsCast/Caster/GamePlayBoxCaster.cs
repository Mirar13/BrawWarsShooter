using System;
using Features.GamePlayAbilityFeature.Components.PhysicsCast;
using UnityEngine;

public class GamePlayBoxCaster : GamePlayCaster
{
    public Vector3 Offset;
    public Vector3 BoxSize;

    protected int _castHitCount;
    protected Collider[] _castResult = new Collider[10];

    private GamePlayCasterHitInfo[] _casterHitInfos = new GamePlayCasterHitInfo[10];

    public override void Cast()
    {
        _castHitCount = Physics.OverlapBoxNonAlloc(transform.position + Offset, BoxSize / 2f, _castResult, transform.rotation,
            CastLayers);
    }

    public override  (int hitCount, GamePlayCasterHitInfo[] infos) GetCasterHitInfos()
    {
        for (int i = 0; i < _castHitCount; i++)
        {
            _casterHitInfos[i] = new GamePlayCasterHitInfo()
            {
                Collider = _castResult[i]
            };
        }
        return (_castHitCount, _casterHitInfos);
    }

    #if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Offset, BoxSize);
    }
    #endif
}
