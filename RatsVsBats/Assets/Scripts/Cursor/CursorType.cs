using UnityEngine;

[CreateAssetMenu(fileName = "Cursor", menuName = "Cursor/New Cursor")]
public class CursorType : ScriptableObject
{
    public Texture2D cursorTexture;
    public Vector2 hotspot;
}
