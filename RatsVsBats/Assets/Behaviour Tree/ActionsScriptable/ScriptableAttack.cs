using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAttack", menuName =
    "ScriptableObjects2/ScriptableAction/ScriptableAttack", order = 1)]
public class ScriptableAttack : ScriptableAction
{
    private EnemyController3 _enemyController;
    public override void OnFinishedState()
    {
        GameManager.Instance.UpdateText("va te perdono");
        _enemyController.animator.SetBool("isAttacking", false);
    }

    public override void OnSetState(StateController2 sc)
    {
        base.OnSetState(sc);
        GameManager.Instance.UpdateText("a q te meto");
        _enemyController = (EnemyController3)sc;
        _enemyController.animator.SetBool("isAttacking", true);
    }

    public override void OnUpdate(StateController2 sc)
    {
        GameManager.Instance.UpdateText("que te meto asin");
    }
}
