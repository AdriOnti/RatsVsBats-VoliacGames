using TMPro;
using UnityEngine;
using System;

public class TabNavigation : MonoBehaviour
{
    public TMP_InputField[] inputFields;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            // Find the focused inputField
            TMP_InputField currentInputField = null;
            foreach (TMP_InputField inputField in inputFields)
            {
                if (inputField.isFocused)
                {
                    currentInputField = inputField;
                    break;
                }
            }

            if (currentInputField != null)
            {
                // Find the actual index and the next idex
                int currentIndex = Array.IndexOf(inputFields, currentInputField);
                int nextIndex = (currentIndex + 1) % inputFields.Length;

                // Toggle the focus between the input fields
                currentInputField.DeactivateInputField();
                inputFields[nextIndex].ActivateInputField();
            }
            else
            {
                inputFields[0].ActivateInputField();
            }
        }
    }
}
