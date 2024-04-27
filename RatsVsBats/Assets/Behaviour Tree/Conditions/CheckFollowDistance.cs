using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Follow", menuName = "ScriptableNodes/ScriptableConditions/Follow")]
public class CheckFollowDistance : ScriptableCondition
{
 public override bool Check(StateController2 sc)
    {
        var ec = (EnemyController3)sc;
        try { 
            return (sc.target.transform.position - sc.transform.position).magnitude > ec.AttackDistance;
        }
        catch
        {
            return false;
        }
    }
}
