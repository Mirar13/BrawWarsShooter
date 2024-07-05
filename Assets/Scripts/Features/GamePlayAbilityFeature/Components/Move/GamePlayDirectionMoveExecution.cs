using UnityEngine;

public class GamePlayDirectionMoveExecution : GamePlayExecution
{
    [SerializeField] public Vector3 Direction;
    [SerializeField] public Transform TransformToMove;
    private bool _isExecuted;

    public override void ExecuteInternal()
    {
        _isExecuted = true;
    }

    private void Update()
    {
        if (!_isExecuted)
        {
            return;
        }

        TransformToMove.Translate(Direction * Time.deltaTime);
    }
}
