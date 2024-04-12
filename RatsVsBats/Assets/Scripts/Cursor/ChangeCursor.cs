using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CursorType customCursor;
    public CursorType defaultCursor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(customCursor.cursorTexture, customCursor.hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor.cursorTexture, defaultCursor.hotspot, CursorMode.Auto);
    }
}
