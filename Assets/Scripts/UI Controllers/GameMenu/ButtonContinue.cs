using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Кнопка закрытия окна
/// </summary>
public class ButtonContinue : BaseButtonController
{
    [SerializeField]
    private BaseWindowMenu windowMenu;

    protected override void ButtonClicked()
    {
        windowMenu.OnCloseMenu();
    }
}
