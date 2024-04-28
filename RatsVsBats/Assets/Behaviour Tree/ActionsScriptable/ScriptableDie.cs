using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableDie", menuName = "ScriptableObjects2/ScriptableAction/ScriptableDie", order = 2)]

public class ScriptableDie : ScriptableAction
{
    public override void OnFinishedState()
    {
        GameManager.Instance.UpdateText("me mori");
    }

    public override void OnSetState(StateController2 sc)
    {
        base.OnSetState(sc);
        GameManager.Instance.UpdateText("me estoy muriendo");
    }

    public override void OnUpdate()
    {
        GameManager.Instance.UpdateText("toma mis monedas");
    }
}
