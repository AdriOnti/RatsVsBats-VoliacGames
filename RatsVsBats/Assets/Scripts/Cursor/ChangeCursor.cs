using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CursorType customCursor;
    public CursorType defaultCursor;

    /// <summary>
    /// Cambia el cursor cuando se pasa por encima
    /// </summary>
    /// <param name="eventData">Evento del mouse</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(customCursor.cursorTexture, customCursor.hotspot, CursorMode.Auto);
    }

    /// <summary>
    /// Devuelve el cursor a la normal cuando deja de estar encima del objeto
    /// </summary>
    /// <param name="eventData">Evento del mouse</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor.cursorTexture, defaultCursor.hotspot, CursorMode.Auto);
    }
}
