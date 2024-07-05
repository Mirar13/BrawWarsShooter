using UnityEngine;

public class GamePlayExecution : MonoBehaviour
{
    public GamePlayExecution[] OnExecuted;
    protected GamePlayEntity _entity;

    public void Initialize(GamePlayEntity entity)
    {
        _entity = entity;
    }
    
    public void Execute()
    {
        ExecuteInternal();
        foreach (var gamePlayExecution in OnExecuted)
        {
            gamePlayExecution.Execute();
        }
    }
    
    public virtual void ExecuteInternal(){}
}
