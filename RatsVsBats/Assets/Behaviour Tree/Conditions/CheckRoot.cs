using UnityEngine;
[CreateAssetMenu(fileName = "Target", menuName = "ScriptableNodes/ScriptableConditions/Root")]
public class CheckRoot : ScriptableCondition
{
    public override bool Check(StateController2 sc)
    {
        return true;
    }
}
