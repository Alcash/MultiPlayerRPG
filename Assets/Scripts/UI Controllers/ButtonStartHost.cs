using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Кнопка начать игру как хост
/// </summary>
public class ButtonStartHost : BaseButtonController
{
    protected override void ButtonClicked()
    {
        GameNetworkManager.singleton.StartHost();
    }
}
