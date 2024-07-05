using UnityEngine;

namespace Features.GamePlayAbilityFeature.Components.PhysicsCast.Caster
{
    public class GamePlaySphereCaster : GamePlayCaster
    {
        public Vector3 Offset;
        public float Radius;
        
        protected int _castHitCount;
        protected Collider[] _castResult = new Collider[10];
        
        private GamePlayCasterHitInfo[] _casterHitInfos = new GamePlayCasterHitInfo[10];

        public override void Cast()
        {
            _castHitCount = Physics.OverlapSphereNonAlloc(transform.position + Offset, Radius, _castResult,CastLayers);
        }

        public override (int hitCount, GamePlayCasterHitInfo[] infos) GetCasterHitInfos()
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
            Gizmos.DrawWireSphere(Offset, Radius);
        }
#endif
    }
}