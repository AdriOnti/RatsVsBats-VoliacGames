using System.Data;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [Header("InputFields")]
    public TMP_InputField email;
    public TMP_InputField password;

    [Header("Error Message")]
    public TextMeshProUGUI errorMessage;

    [Header("Web")]
    public string registerPage;

    [Header("LoginCorrect")]
    public GameObject menu;

    [Header("Debug Mode")]
    public bool isDebugging;
    public string emailDebug;
    public string passwordDebug;

    public static Login instance;
    [HideInInspector] public bool isLogged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        menu.SetActive(false);
        errorMessage.gameObject.SetActive(false);
        //email.onEndEdit.AddListener(ValidateMail);
        //password.onEndEdit.AddListener(ValidatePwd);
        password.onEndEdit.AddListener(ValidadeLogin);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (isDebugging)
        {
            email.text = emailDebug;
            password.text = passwordDebug;
            LoginBtn(false);
        }
    }
#endif

    //private void ValidateMail(string mail)
    //{
    //    if (!IsValidAddress(mail))
    //    {
    //        errorMessage.gameObject.SetActive(true);
    //        errorMessage.text = "Your address is not correct";
    //    }
    //    else
    //    {
    //        errorMessage.gameObject.SetActive(false);
    //    }
    //}

    //private void ValidatePwd(string password)
    //{
    //    if(!IsValidPwd(password))
    //    {
    //        errorMessage.gameObject.SetActive(true);
    //        errorMessage.text = "Your password is incorrect";
    //    }
    //    else
    //    { 
    //        errorMessage.gameObject.SetActive(false);
    //        LoginBtn(true);
    //    }
    //}

    ///// <summary>
    ///// Using regex finds if is a valid email address
    ///// </summary>
    ///// <param name="address">The email to validate</param>
    ///// <returns>If is valid or not</returns>
    //private bool IsValidAddress(string address)
    //{
    //    string regex = @"(google|yahoo|hotmail|outlook|itb|voliac-games).(com|net|org|gov|cat|es)$";
    //    return Regex.IsMatch(address, regex, RegexOptions.IgnoreCase);
    //}

    ///// <summary>
    ///// Using regex finds if is a password meets the conditions
    ///// </summary>
    ///// <param name="pwd">The password to validate</param>
    ///// <returns>Is is valid or not</returns>
    //private bool IsValidPwd(string pwd)
    //{
    //    //  (?=.*[a-z])         Debe contener al menos una letra minúscula.
    //    //  (?=.*[A-Z])         Debe contener al menos una letra mayúscula.
    //    //  (?=.*\d)            Debe contener al menos un dígito
    //    //  (?=.*[^\da-zA-Z])   Debe contener al menos un carácter que no sea letra ni dígito
    //    //  .{8,}               Debe tener una longitud mínima de 8 caracteres
    //    string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
    //    return Regex.IsMatch(pwd, pattern); ;
    //}

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
            errorMessage.text = "Your address is not correct";
            return;
        }

        if(password.text == "")
        {
            errorMessage.text = "Your password is incorrect";
            return;
        }

        LoginBtn(true);
    }

    public void LoginBtn(bool validate)
    {
        if (!validate) ValidadeLogin("");
        
        // Llamada al DatabaseManager
        string tableName = "Profiles";
        string[] columns = { "userEmail", "userPassword" };
        object[] values = { email.text, password.text };

        bool correctPassword = PasswordCorrect(tableName, columns, values);

        if (correctPassword)
        {
            isLogged = true;
            gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }
        else errorMessage.text = "Email or password incorrect";
    }

    /// <summary>
    /// Check if the password introduced equals the password in the db
    /// </summary>
    /// <param name="tableName">Table of the database</param>
    /// <param name="columns">Columns of the table</param>
    /// <param name="values">The values to check</param>
    /// <returns>If the two passoword are correct</returns>
    public bool PasswordCorrect(string tableName, string[] columns, object[] values)
    {
        string query = $"SELECT {columns[0]}, {columns[1]} FROM {tableName} WHERE {columns[0]} = {values[0].ToString()}";
        DataSet resultDataSet = DatabaseManager.instance.ExecuteQuery(query);

        if (resultDataSet != null && resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0)
        {
            string storedPassword = resultDataSet.Tables[0].Rows[0][columns[1]].ToString();
            if (storedPassword == values[1].ToString()) return true;
        }

        return false;
    }
}
