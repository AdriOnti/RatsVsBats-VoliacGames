using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Run", menuName = "ScriptableNodes/ScriptableConditions/Run")]

public class CheckRun : ScriptableCondition
{
    public override bool Check(StateController2 sc)
    { 
        var ec = (EnemyController3)sc;
        return ec.HP < 3;
    }
}

