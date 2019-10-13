using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Контроллер игрового меню
/// </summary>
[RequireComponent(typeof(Canvas))]
public class GameMenuController : BaseWindowMenu
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
