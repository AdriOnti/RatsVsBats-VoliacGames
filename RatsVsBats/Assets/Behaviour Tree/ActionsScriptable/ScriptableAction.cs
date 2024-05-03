using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{
    // Carlos nos ha dicho que pongamos esos parametros en las funciones

    public abstract void OnFinishedState(StateController2 sc);

    public virtual void OnSetState(StateController2 sc) {
        //this.sc = sc;
    }

    public abstract void OnUpdate(StateController2 sc);
}
