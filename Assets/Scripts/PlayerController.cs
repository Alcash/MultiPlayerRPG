using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Контроллер игрока
/// </summary>
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private PlayerInfo playerData;

    public PlayerInfo PlayerInfo
    {
        get
        {
            return playerData;
        }
    }

    private GameObject spawnedPlayerAvatar;

    /// <summary>
    /// Установка информации о игроке
    /// </summary>
    /// <param name="playerSettings"></param> 
    [Command]
    public void CmdSetInfoOnServer(string namePlayer, string nameAvatar)
    {
        CmdLog("CmdSetInfoOnServer name " + namePlayer + " avatar " + nameAvatar);
        PlayerInfo.SetPlayerName(namePlayer);
        PlayerInfo.SetAvatar(nameAvatar);        
        SpawnAvatar();
    }

    private void Start()
    {       
        if (isLocalPlayer)
        {
            InitPlayerLocaly();

            PlayerSendData sendData = PlayerInfo.GetPlayerSendInfo();
            CmdSetInfoOnServer(sendData.PlayerName, sendData.PlayerAvatar);   
        }
        
    }

    
    private void InitPlayerLocaly()
    {
        PlayerSettings playerSettings = NetworkManager.singleton.gameObject.GetComponent<PlayerSettings>();
        PlayerInfo.SetPlayerName(playerSettings.PlayerName);
        PlayerInfo.SetAvatar(playerSettings.AvatarData);

        CmdLog("name " + PlayerInfo.GetPlayerSendInfo().PlayerName + " avatar " + PlayerInfo.GetPlayerSendInfo().PlayerAvatar);
    }
    
    private void SpawnAvatar()
    {
        spawnedPlayerAvatar = Instantiate(PlayerInfo.CurrentAvatarData.AvatarPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(spawnedPlayerAvatar);
        CmdLog(PlayerInfo.PlayerName + " joined as " + PlayerInfo.CurrentAvatarData.name);
    }

    [Command]
    private void CmdLog(string message)
    {
        Debug.Log(message);
    }

    private void OnDisable()
    {
        Destroy(spawnedPlayerAvatar);
    }  
}
