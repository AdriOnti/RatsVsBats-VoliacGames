using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableDie", menuName = "ScriptableObjects2/ScriptableAction/ScriptableDie", order = 2)]

public class ScriptableDie : ScriptableAction
{
    private EnemyController3 _enemyController;
    public override void OnFinishedState()
    {
        GameManager.Instance.UpdateText("me mori");
        _enemyController.animator.SetBool("isDying", false);
    }

    public override void OnSetState(StateController2 sc)
    {
        base.OnSetState(sc);
        GameManager.Instance.UpdateText("me estoy muriendo");
        _enemyController = (EnemyController3)sc;
        _enemyController.animator.SetBool("isDying", true);
    }

    public override void OnUpdate()
    {
        GameManager.Instance.UpdateText("toma mis monedas");
    }
}
