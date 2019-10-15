using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Базовый класс окон
/// </summary>
public abstract class BaseWindowMenu : MonoBehaviour
{
    public UnityAction OnOpenMenu;
    public UnityAction OnCloseMenu;    
}
