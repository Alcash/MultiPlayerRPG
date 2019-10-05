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
        NetworkDebug.Log?.Invoke("CmdSetInfoOnServer name " + namePlayer + " avatar " + nameAvatar);
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
        
        NetworkDebug.Log?.Invoke("name " + PlayerInfo.GetPlayerSendInfo().PlayerName + " avatar " + PlayerInfo.GetPlayerSendInfo().PlayerAvatar);
    }
    
    private void SpawnAvatar()
    {
        spawnedPlayerAvatar = Instantiate(PlayerInfo.CurrentAvatarData.AvatarPrefab, transform.position, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(spawnedPlayerAvatar,gameObject);
        NetworkDebug.Log?.Invoke(PlayerInfo.PlayerName + " joined as " + PlayerInfo.CurrentAvatarData.name);        
        RpcSetAvatar(spawnedPlayerAvatar);
    }  

    [ClientRpc]
    private void RpcSetAvatar(GameObject avatar)
    {
        
        spawnedPlayerAvatar = avatar;

        if (isLocalPlayer)
        {
            UnityStandardAssets.Utility.FollowTarget camera = GameObject.FindObjectOfType<UnityStandardAssets.Utility.FollowTarget>();
            camera.target = spawnedPlayerAvatar.transform;

            spawnedPlayerAvatar.GetComponent<AvatarNetworkSetup>().enabled = true;
        }

        spawnedPlayerAvatar.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }

    private void OnDisable()
    {
        Destroy(spawnedPlayerAvatar);
    }  
}
