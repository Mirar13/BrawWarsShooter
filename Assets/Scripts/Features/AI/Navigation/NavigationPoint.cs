using Features.AI.Navigation;
using UnityEngine;

public class NavigationPoint : MonoBehaviour
{
    [SerializeField] public PointType Type;
    [SerializeField] public float Radius;
    [SerializeField] public float Weight;

    private void OnDrawGizmos()
    {
        switch (Type)
        {
            case PointType.Interest:
                Gizmos.color = Color.green;
                break;
            case PointType.Retreat:
                Gizmos.color = Color.yellow;
                break;
        }
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
