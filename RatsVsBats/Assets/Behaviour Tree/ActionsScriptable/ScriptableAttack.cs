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
        SoundManager.Instance.PlayEffect(Audios.effectMonsterAttack);
    }

    public override void OnUpdate()
    {
        GameManager.Instance.UpdateText("que te meto asin");
    }
}