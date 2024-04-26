using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    [SerializeField] private GameObject father;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject fadeOut;

    void Awake()
    {
        if (Instance == null) Instance = this;

        father = GameManager.Instance.GetFade();
        GameObject[] fades = new GameObject[father.transform.childCount];

        for(int i = 0; i < fades.Length; i++) fades[i] = father.transform.GetChild(i).gameObject;

        foreach (GameObject f in fades)
        {
            if (f.name == "FadeOut") fadeOut = f.gameObject;
            if (f.name == "FadeIn") fadeIn = f.gameObject;
        }
    }

    private void Start()
    {
        FadeIn();
    }

#if UNITY_EDITOR
    // Esto es para ir haciendo pruebas desde el editor de unity
    private void Update()
    {
        if (Input.GetKey(KeyCode.F1)) FadeOut();
        if (Input.GetKey(KeyCode.F2)) FadeIn();
    }
#endif

    /// <summary>
    /// Hace un Fade In
    /// </summary>
    public void FadeIn()
    {
        father.SetActive(true);
        fadeIn.SetActive(true);
        fadeOut.SetActive(false);

        StartCoroutine(In());
    }

    /// <summary>
    /// Activa los elementos del HUD
    /// </summary>
    /// <returns>Es una corrutina</returns>
    IEnumerator In()
    {
        yield return new WaitForSeconds(0.1f);
        if(DataManager.Instance.ActiveSceneIndex()) CanvasManager.Instance.HUDFadesIn();
        GameManager.Instance.isFading = false;
        yield return new WaitForSeconds(0.5f);
        father.SetActive(false);
    }

    /// <summary>
    /// Hace un Fade out
    /// </summary>
    public void FadeOut()
    {
        father.SetActive(true);
        GameManager.Instance.isFading = true;
        fadeOut.SetActive(true);
        fadeIn.SetActive(false);
        if(DataManager.Instance.ActiveSceneIndex()) CanvasManager.Instance.HUDFadesOut();

    }
}
