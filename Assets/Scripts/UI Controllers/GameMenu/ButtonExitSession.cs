using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

/// <summary>
/// Контроллер выхода из сетевой сессии
/// </summary>
public class ButtonExitSession : BaseButtonController
{
    protected override void ButtonClicked()
    {
        if (GameNetworkManager.singleton != null)
        {
            GameNetworkManager.singleton.StopHost();
        }
    }
}
