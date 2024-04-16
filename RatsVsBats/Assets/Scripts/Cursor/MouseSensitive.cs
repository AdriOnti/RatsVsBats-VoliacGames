using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseSensitive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Cambia la escala del objeto para que sea más visual que ha entrado
    /// <para>Tambien comprueba si el botón esta bloqueado</para>
    /// </summary>
    /// <param name="eventData">Evento del mouse</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.12f, 1.12f, 1.12f);
        if (gameObject.GetComponent<Button>().enabled == false) CanvasManager.Instance.InformativeText(gameObject.name);
    }

    /// <summary>
    /// Devuelve la escala del objeto a la normalidad
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        if (gameObject.GetComponent<Button>().enabled == false) GameManager.Instance.GetInfoMenu().SetActive(false);
    }
}
