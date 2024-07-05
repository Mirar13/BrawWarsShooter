using UnityEngine;

namespace DefaultNamespace.Features.GameMode
{
    public abstract class GameModeCondition : MonoBehaviour
    {
        public abstract bool IsMatch();
    }
}