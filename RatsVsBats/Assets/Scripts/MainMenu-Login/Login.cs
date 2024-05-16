using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    public static Login instance;

    [Header("InputFields")]
    public TMP_InputField email;
    public TMP_InputField password;

    [Header("Error Message")]
    public TextMeshProUGUI errorMessage;

    [Header("Web")]
    public string registerPage;

    [Header("LoginCorrect")]
    public GameObject menu;
    public GameObject login;

    [Header("Debug Mode")]
    public bool isDebugging;
    public string emailDebug;
    public string passwordDebug;

    [Header("Success Log-in")]
    [SerializeField] private GameObject fadeIn;
    [HideInInspector] public bool isLogged;
    [HideInInspector] public int idUser;

    private void Awake()
    { 
        instance = this;
    }

    private void Start()
    {
        login.SetActive(true);
        menu.SetActive(false);
        errorMessage.gameObject.SetActive(false);
        fadeIn.SetActive(false);
        password.onEndEdit.AddListener(ValidadeLogin);

        if (PlayerPrefs.GetInt("back") > 0)
        {
            IsBack();
        }
    }

    public void IsBack()
    {
        idUser = PlayerPrefs.GetInt("profileID");
        email.text = PlayerPrefs.GetString("email");
        menu.SetActive(true);
        login.SetActive(false);
        isLogged = true;

        Account.Instance.JustLogged(idUser, email.text);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (isDebugging)
        {
            email.text = emailDebug;
            password.text = passwordDebug;
            LoginBtn(true);
            isDebugging = false;
        }
    }
#endif

    /// <summary>
    /// Open a new tab in the web browser to the register page
    /// </summary>
    public void RegisterBtn() { Application.OpenURL(registerPage); }


    /// <summary>
    /// Check if the input fields are empty or not
    /// </summary>
    /// <param name="text">This is not used</param>
    private void ValidadeLogin(string text)
    {
        if (email.text == "")
        {
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = "Your address can't be empty";
            return;
        }

        if(password.text == "")
        {
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = "Your password can't be empty";
            return;
        }

        LoginBtn(true);
    }

    /// <summary>
    /// The function to call when the button of Login is pressed
    /// </summary>
    /// <param name="validate">If pass the ValidadeLogin function or not</param>
    public async void LoginBtn(bool validate)
    {
        if (!validate) ValidadeLogin("");

        // Check with the API if the password is correct
        bool correctPassword = await PasswordCorrectAsync(email.text);

        if (correctPassword)
        {
            // Toggle the canvas and activates de isLogged boolean
            isLogged = true;
            login.SetActive(false);
            menu.gameObject.SetActive(true);
            Debug.Log("<color=green>Login successful!</color>");

            // Get the profile data
            Account.Instance.JustLogged(idUser, email.text);

            // Do the Fade In and reset cursor
            CursorManager.Instance.ResetCursor();
            StartCoroutine(Fade());
        }
        else
        {
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = "Email or password\n or both are wrong";
        }
    }

    /// <summary>
    /// Ask to the API and get the info of the user using the email
    /// </summary>
    /// <returns>If the two passoword are correct</returns>
    public async Task<bool> PasswordCorrectAsync(string email)
    {
        try
        {
            string response = await APIManager.instance.GetUsersWhereEmailAsync(email);

            if (string.IsNullOrEmpty(response))
            {
                return false;
            }

            UserData userData = ProcessJSON(response);
            if(userData.userPassword == password.text)
            {
                idUser = userData.idUsers;
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            // Log the exception
            Debug.LogError(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Enable the fade in after login
    /// </summary>
    IEnumerator Fade()
    {
        fadeIn.SetActive(true);
        yield return new WaitForSecondsRealtime(1.51f);
        fadeIn.SetActive(false);
    }

    UserData ProcessJSON(string json)
    {
        return JsonUtility.FromJson<UserData>(json);
    }
}
