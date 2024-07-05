using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayDestroyExecution : GamePlayExecution
{
    [SerializeField] public GameObject TargetObject;
    
    public override void ExecuteInternal()
    {
        Destroy(TargetObject);    
    }
}
