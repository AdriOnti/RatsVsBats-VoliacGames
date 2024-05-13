using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogScriptableObject", menuName = "Dialogs/Create Scriptable Dialogs")]
public class DialogsContainer : ScriptableObject
{
    public List<string> dialogsDB = new();
}
