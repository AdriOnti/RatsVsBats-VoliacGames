using System;
using System.Collections;
using System.Data;
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
    /*[HideInInspector]*/ public int idUser;

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
        
        // Values to the call the DBManager
        //string tableName = "Users";
        //string[] columns = { "userEmail", "userPassword" };
        //object[] values = { email.text, password.text };

        //bool correctPassword = PasswordCorrect(tableName, columns, values);
        bool correctPassword = await PasswordCorrectAsync(email.text);

        if (correctPassword)
        {
            // Toggle the canvas and activates de isLogged boolean
            isLogged = true;
            login.SetActive(false);
            menu.gameObject.SetActive(true);
            Debug.Log("<color=green>Login successful!</color>");

            // Get the ID of the user
            //string[] newColumns = { "idUsers", "userEmail"};
            //GetID(tableName, newColumns, values);
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
    /// Check if the password introduced equals the password in the db
    /// </summary>
    /// <param name="tableName">Table of the database</param>
    /// <param name="columns">Columns of the table</param>
    /// <param name="values">The values to check</param>
    /// <returns>If the two passoword are correct</returns>
    //public bool PasswordCorrect(string tableName, string[] columns, object[] values)
    //{
        // SELECT userEmail, userPassword FROM Users WHERE userEmail = 'developer@voliac-games.com';
        //string query = $"SELECT {columns[0]}, {columns[1]} FROM {tableName} WHERE {columns[0]} = \'{values[0].ToString()}\'";

        //try
        //{
        //    // Execute the query
        //    DataSet resultDataSet = DatabaseManager.instance.ExecuteQuery(query);

        //    // Find the password in the result of the select query and saved it
        //    if (resultDataSet != null && resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0)
        //    {
        //        string storedPassword = resultDataSet.Tables[0].Rows[0][columns[1]].ToString();
        //        if (storedPassword == values[1].ToString()) return true;
        //    }
        //}
        //catch { return false; }
        //return false;
    //}

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
    /// Get the ID of the logged user
    /// </summary>
    /// <param name="tableName">Name of the table</param>
    /// <param name="columns">The columns to make the select</param>
    /// <param name="values">The values to check in the where</param>
    //private void GetID(string tableName, string[] columns, object[] values)
    //{
    //    // SELECT idUsers FROM Users WHERE email = 'email';
    //    string query = $"SELECT {columns[0]} FROM {tableName} WHERE {columns[1]} = \'{values[0]}\'";
    //    DataSet resultDataSet = DatabaseManager.instance.ExecuteQuery(query);

    //    // Save the id like a integer
    //    if(resultDataSet != null && resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0)
    //    {
    //        DataRow row = resultDataSet.Tables[0].Rows[0];
    //        idUser = int.Parse(row[columns[0]].ToString());
    //    }

    //    Account.Instance.JustLogged(idUser, email.text);
    //}

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
        // Deserializar el JSON
        //UserData userData = JsonUtility.FromJson<UserData>(json);

        return JsonUtility.FromJson<UserData>(json);

        // Obtener los valores
        //idUser = userData.idUsers;
        //pwd = userData.userPassword;
    }
}
