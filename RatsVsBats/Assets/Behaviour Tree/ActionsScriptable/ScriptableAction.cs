using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{
    protected StateController2 sc;
    public abstract void OnFinishedState();

    public virtual void OnSetState(StateController2 sc) {
        this.sc = sc;
    }

    public abstract void OnUpdate();
}
