using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Dialogs
{

}

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialog;

    public static DialogManager Instance;
    public DialogsContainer dialogContainer;

    private readonly Dictionary<Dialogs, string> dialogsDatabase = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance);
        else Instance = this;
    }

    private void Start()
    {
        bool sucess;
        foreach (string dialogText in dialogContainer.dialogsDB)
        {
            if (sucess = Enum.TryParse(dialogText.Split("_")[1], out Dialogs value))
                dialogsDatabase.Add(value, dialogText.Split("_")[0]);
        }
    }

    public void ShowDialog(Dialogs dialogText)
    {
        if (dialogsDatabase.TryGetValue(dialogText, out string value))
        {
            dialog.text = value;
        }
    }

    public void ClearDialog()
    {
        dialog.text = string.Empty;
    }
}
