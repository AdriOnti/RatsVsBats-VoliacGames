using UnityEngine;

public class StateController2 : MonoBehaviour
{
    public NodeBehaviourTree currentState;
    public GameObject target = null;
    //protected NodeBehaviourTree stateToPlay = null;
    public void StateTransition()
    {
        if (!currentState.AbortCondition(this))
        {
            if (currentState.Children.Count != 0)
            {
                bool cond = false;
                int count = 0;
                while (!cond && count != currentState.Children.Count)
                {
                    cond = CheckCondition(currentState.Children[count++]);
                }
                if (cond)
                {
                    if(currentState.action != null) currentState.action.OnFinishedState();
                    currentState = currentState.Children[count - 1];
                    if(currentState.action != null) currentState.action.OnSetState(this);
                }
            }
        }
        else
            GoToRootState();
    }
    public void GoToRootState()
    {
        if (currentState.Parent != null)
        {
            if (currentState.action != null) currentState.action.OnFinishedState();
            currentState = currentState.Parent;
            GoToRootState();
        }
    }
    public bool CheckCondition(NodeBehaviourTree node)
    {
        return node.Condition(this);
        /*switch (node.type)
        {
            case TypeOfCondition.SIMPLE:
                return node.Condition(this);
            case TypeOfCondition.AND:
                return node.AndCondition(node.Parent.cond, this);
            case TypeOfCondition.OR:
                return node.OrCondition(node.Parent.cond, this);
            default:
                return false;
        }*/
    }
}
/*
    public enum TypeOfCondition
{
    SIMPLE,
    AND,
    OR
}*/
