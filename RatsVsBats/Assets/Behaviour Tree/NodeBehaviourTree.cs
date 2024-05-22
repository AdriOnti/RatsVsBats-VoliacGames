using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableNode", menuName = "ScriptableNodes/ScriptableNode", order = 3)]
public class NodeBehaviourTree : ScriptableObject
{
    public NodeBehaviourTree Parent;
    public List<NodeBehaviourTree> Children;
    public ScriptableCondition cond;
    public List<ScriptableCondition> abortConditions;
    public ScriptableAction action;
    //public TypeOfCondition type;
    public bool Condition(StateController2 sc)
    {
        return cond.Check(sc);
    }
    public bool AbortCondition(StateController2 sc)
    {
        var abort = false;
        if(abortConditions!=null)
            foreach (var c in abortConditions)
                abort = abort || c.Check(sc);
        return abort;
    }
    /*public bool AndCondition(ScriptableCondition cond, StateController2 sc)
    {
        return cond.Check(sc) && Condition(sc);
    }
    public bool OrCondition(ScriptableCondition cond, StateController2 sc)
    {
        return cond.Check(sc) || Condition(sc);
    }*/
}
