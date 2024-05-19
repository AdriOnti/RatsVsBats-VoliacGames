using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraScriptableObject", menuName = "Cameras/Create Scriptable Cameras")]
public class CamerasContainer : ScriptableObject
{
    public List<Camera> camerasDB = new();
}
