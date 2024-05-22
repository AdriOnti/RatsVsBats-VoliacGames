using UnityEngine;

[CreateAssetMenu(fileName = "Cursor", menuName = "ScriptableObjects/Cursor/New Cursor", order = 1)]
public class CursorType : ScriptableObject
{
    public Texture2D cursorTexture;
    public Vector2 hotspot;
}
