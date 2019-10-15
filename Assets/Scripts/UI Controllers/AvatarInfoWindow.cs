using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер меню информации о аватаре
/// </summary>
[RequireComponent(typeof(Canvas))]
public class AvatarInfoWindow : BaseWindowMenu
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        HideMenu();
        OnOpenMenu += ShowMenu;
        OnCloseMenu += HideMenu;
    }

    private void ShowMenu()
    {
        canvas.enabled = true;
    }

    private void HideMenu()
    {
        canvas.enabled = false;
    }
}
