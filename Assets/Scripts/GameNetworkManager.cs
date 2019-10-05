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
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);               
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);

        
    }

    private void Start()
    {
        Instantiate(spawnPrefabs[2], transform);
    }
}
