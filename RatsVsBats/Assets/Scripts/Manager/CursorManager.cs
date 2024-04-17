using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    // Instance
    private static CursorManager instance;
    public static CursorManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    /// <summary>
    /// Permite que el botón este bloqueado y que el cursor cambie a bloqueado
    /// </summary>
    /// <param name="go">El boton a bloquear</param>
    public void BlockBtn(GameObject go)
    {
        go.GetComponent<Button>().enabled = false;
        go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Gui_parts/buttonBlocked");
        go.GetComponent<ChangeCursor>().customCursor = Resources.Load<CursorType>("CursorsSO/Block");
    }

    /// <summary>
    /// Permite que el botón se desbloquee y cursor que le pertenece cambie a la mano
    /// </summary>
    /// <param name="go">El botón ha desbloquear</param>
    public void NotBlockBtn(GameObject go)
    {
        go.GetComponent<Button>().enabled = true;
        go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Gui_parts/button");
        go.GetComponent<ChangeCursor>().customCursor = Resources.Load<CursorType>("CursorsSO/Hand");
    }
}
