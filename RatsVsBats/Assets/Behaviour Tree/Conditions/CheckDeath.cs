using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Death", menuName = "ScriptableNodes/ScriptableConditions/Death")]
public class CheckDeath : ScriptableCondition
{
    public override bool Check(StateController2 sc)
    {
        var ec = (EnemyController3)sc;
        return ec.HP <= 0;
    }
}
