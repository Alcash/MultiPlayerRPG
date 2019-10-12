using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Контроллер ввода имени игрока
/// </summary>
[RequireComponent(typeof(InputField))]
public class InputPlayerName : MonoBehaviour
{
    private const string defaultName= "leatherBastard";
    private InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener(EndInput);

        inputField.text = defaultName;
    } 
    
    private void EndInput(string value)
    {

        PlayerSettings.SetNewName(value);
    }
}
