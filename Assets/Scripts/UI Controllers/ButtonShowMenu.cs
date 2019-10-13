using UnityEngine;

/// <summary>
/// Кнопка включения меню
/// </summary>
public class ButtonShowMenu : BaseButtonController
{
    [SerializeField]
    private BaseWindowMenu windowMenu;

    protected override void ButtonClicked()
    {
        windowMenu.OnOpenMenu();
    }    
}
