using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Игровой контроллер сети
/// </summary>
[RequireComponent(typeof(PlayerSettings))]
public class GameNetworkManager : NetworkManager
{
    private PlayerSettings playerSettings;

    private void Awake()
    {
        playerSettings = GetComponent<PlayerSettings>();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);               
    }

   
}
