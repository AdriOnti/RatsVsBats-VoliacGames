using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject fadeOut;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) Instance = this;

        GameObject father = GameManager.Instance.GetFade();
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
    private void Update()
    {
        if (Input.GetKey(KeyCode.F1)) FadeOut();
        if (Input.GetKey(KeyCode.F2)) FadeIn();
    }
#endif

    public void FadeIn()
    {
        fadeIn.SetActive(true);
        fadeOut.SetActive(false);

        StartCoroutine(In());
    }

    IEnumerator In()
    {
        yield return new WaitForSeconds(0.1f);
        if(SceneManager.GetActiveScene().buildIndex != 0) CanvasManager.Instance.HUDFadesIn();
        GameManager.Instance.isFading = false;
    }

    public void FadeOut()
    {
        GameManager.Instance.isFading = true;
        fadeOut.SetActive(true);
        fadeIn.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex != 0) CanvasManager.Instance.HUDFadesOut();
    }
}
