using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Контроллер игрока
/// </summary>
[RequireComponent(typeof(UserControlInput))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]    
    private GameObject playerAvatar;

    [SerializeField]
    private PlayerInfo playerData;

    private UserControlInput userControlInput;

    public PlayerInfo PlayerInfo
    {
        get
        {
            return playerData;
        }
    }
    [SyncVar]
    private GameObject spawnedPlayerAvatar;

    public GameObject GetSpawnedAvatar
    {
        get
        {
            return spawnedPlayerAvatar;
        }
    }
    [SyncVar]
    private GameObject spawnedPlayerPerson;

    /// <summary>
    /// Установка информации о игроке
    /// </summary>
    /// <param name="playerSettings"></param> 
    [Command]
    public void CmdSetInfoOnServer(string namePlayer)
    {      
        PlayerInfo.SetPlayerName(namePlayer);        
    }

    private void Start()
    {       
        if (isLocalPlayer)
        {
            InitPlayerLocaly();
            PlayerSendData sendData = PlayerInfo.GetPlayerSendInfo();

            CmdSetInfoOnServer(PlayerInfo.PlayerName);

            CmdSpawnAvatar(PlayerInfo.CurrentAvatarData.name);
            userControlInput = GetComponent<UserControlInput>();
        }   
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        spawnedPlayerPerson.transform.SetParent(spawnedPlayerAvatar.transform);
    }

    private void InitPlayerLocaly()
    {
        PlayerSettings playerSettings = NetworkManager.singleton.gameObject.GetComponent<PlayerSettings>();
        PlayerInfo.SetPlayerName(playerSettings.PlayerName);
        PlayerInfo.SetAvatar(playerSettings.AvatarData);
    }

    [Command]
    private void CmdSpawnAvatar(string avatarPersonName)
    {
        GameObject spawn = Instantiate(playerAvatar, transform.position, Quaternion.identity);      
        NetworkServer.SpawnWithClientAuthority(spawn, gameObject);
        spawnedPlayerAvatar = spawn;  
        PlayerInfo.SetAvatar(avatarPersonName);
        spawn = Instantiate(PlayerInfo.CurrentAvatarData.AvatarPrefab, spawnedPlayerAvatar.transform);              
        NetworkServer.SpawnWithClientAuthority(spawn, gameObject);
        spawnedPlayerPerson = spawn;

        //spawnedPlayerPerson.transform.SetParent(spawnedPlayerAvatar.transform);

        RpcSetAvatar(spawnedPlayerAvatar, spawnedPlayerPerson);
    }  

    [ClientRpc]
    private void RpcSetAvatar(GameObject avatar, GameObject avatarPerson)
    {        
        spawnedPlayerAvatar = avatar;
        spawnedPlayerPerson = avatarPerson;

        spawnedPlayerPerson.transform.SetParent(spawnedPlayerAvatar.transform);

        if (isLocalPlayer)
        {
            UnityStandardAssets.Utility.FollowTarget camera = GameObject.FindObjectOfType<UnityStandardAssets.Utility.FollowTarget>();
            camera.target = spawnedPlayerAvatar.transform;

            AvatarControl avatarControl = spawnedPlayerAvatar.GetComponent<AvatarControl>();
            if (avatarControl != null)
            {               
                userControlInput.SetAvatarControl(avatarControl);
                avatarControl.SetAvatarPerson(spawnedPlayerPerson);                
            }
        }     
    }   

    [Command]
    private void CmdSetParent()
    {
        spawnedPlayerPerson.transform.SetParent(spawnedPlayerAvatar.transform);
    }

    private void OnDisable()
    {
        Destroy(spawnedPlayerAvatar);
    }  
}
