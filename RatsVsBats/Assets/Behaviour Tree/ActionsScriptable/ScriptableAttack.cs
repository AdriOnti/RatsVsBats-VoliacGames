using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAttack", menuName =
    "ScriptableObjects2/ScriptableAction/ScriptableAttack", order = 1)]
public class ScriptableAttack : ScriptableAction
{
    public override void OnFinishedState()
    {
        GameManager.Instance.UpdateText("va te perdono");
    }

    public override void OnSetState(StateController2 sc)
    {
        base.OnSetState(sc);
        GameManager.Instance.UpdateText("a q te meto");
    }

    public override void OnUpdate()
    {
        GameManager.Instance.UpdateText("que te meto asin");
    }
}
