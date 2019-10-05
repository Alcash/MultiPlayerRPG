using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Кнопка установки нового имени
/// </summary>
public class ButtonSetName : BaseButtonController
{
    [SerializeField]
    private InputField nameField;

    protected override void ButtonClicked()
    {
        if (CheckCorrectName())
        {
            PlayerSettings.SetNewName(nameField.text);
        }
    }

    private bool CheckCorrectName()
    {
        return nameField.text != "";
    }
}
