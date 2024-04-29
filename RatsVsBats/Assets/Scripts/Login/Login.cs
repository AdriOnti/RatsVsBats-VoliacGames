using System.Text.RegularExpressions;
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

    private void Start()
    {
        menu.SetActive(false);
        errorMessage.gameObject.SetActive(false);
        email.onEndEdit.AddListener(ValidateMail);
        password.onEndEdit.AddListener(ValidatePwd);
    }

    private void ValidateMail(string mail)
    {
        if (!IsValidAddress(mail))
        {
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = "Your address is not correct";
        }
        else
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

    private void ValidatePwd(string password)
    {
        if(!IsValidPwd(password))
        {
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = "Your password is incorrect";
        }
        else
        { 
            errorMessage.gameObject.SetActive(false);
            LoginBtn();
        }
    }

    /// <summary>
    /// Using regex finds if is a valid email address
    /// </summary>
    /// <param name="address">The email to validate</param>
    /// <returns>If is valid or not</returns>
    private bool IsValidAddress(string address)
    {
        string regex = @"(google|yahoo|hotmail|outlook|itb).(com|net|org|gov|cat|es)$";
        return Regex.IsMatch(address, regex, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Using regex finds if is a password meets the conditions
    /// </summary>
    /// <param name="pwd">The password to validate</param>
    /// <returns>Is is valid or not</returns>
    private bool IsValidPwd(string pwd)
    {
        //  (?=.*[a-z])         Debe contener al menos una letra minúscula.
        //  (?=.*[A-Z])         Debe contener al menos una letra mayúscula.
        //  (?=.*\d)            Debe contener al menos un dígito
        //  (?=.*[^\da-zA-Z])   Debe contener al menos un carácter que no sea letra ni dígito
        //  .{8,}               Debe tener una longitud mínima de 8 caracteres
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
        return Regex.IsMatch(pwd, pattern); ;
    }

    /// <summary>
    /// Open a new tab in the web browser to the register page
    /// </summary>
    public void RegisterBtn() { Application.OpenURL(registerPage); }

    public void LoginBtn()
    {
        bool isEmailValid = IsValidAddress(email.text);
        bool isPasswordValid = IsValidPwd(password.text);

        if (!isEmailValid || !isPasswordValid)
        {
            errorMessage.text = "Email or password incorrect";
            return;
        }

        // Llamada al DatabaseManager
        string tableName = "Profiles";
        string[] columns = { "userEmail", "userPassword" };
        object[] values = { email.text, password.text };
        //DatabaseManager.instance.InsertInto(tableName, columns, values);

        Debug.Log($"{tableName}");
        foreach (string column in columns) Debug.Log(column);
        foreach (object value in values) Debug.Log($"{value}");

        gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }
}
