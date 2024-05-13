
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableFollow", menuName = "ScriptableObjects2/ScriptableAction/ScriptableFollow")]

public class ScriptableFollow : ScriptableAction
{
    private EnemyController3 _enemyController;
    private ChaseBehaviour _chaseBehaviour;
    public override void OnFinishedState()
    {
        _chaseBehaviour.StopChasing();
        _enemyController.animator.SetBool("isJumping", false);
    }

    public override void OnSetState(StateController2 sc)
    {
        base.OnSetState(sc);
        GameManager.Instance.UpdateText("No toques mi llave");
        _chaseBehaviour = sc.GetComponent<ChaseBehaviour>();
        _enemyController = (EnemyController3)sc;
        _enemyController.animator.SetBool("isJumping", true);
    }

    public override void OnUpdate()
    {
        _chaseBehaviour.Chase(_enemyController.target.transform, sc.transform);
    }
}
