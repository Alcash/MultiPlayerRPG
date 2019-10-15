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
    private string playerName;

    [SerializeField]
    private AvatarData avatarData;
    private AvatarControl avatarControl;
    private UserControlInput userControlInput;
  
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

    private void Start()
    {       
        if (isLocalPlayer)
        {
            InitPlayerLocaly();     
            CmdSpawnAvatar(avatarData.name);
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
        if(playerSettings == null)
        {
            return;
        }
        if (playerSettings.PlayerName != "")
        {
            playerName = playerSettings.PlayerName;
        }
        if (playerSettings.AvatarData != null)
        {
            avatarData = playerSettings.AvatarData;
        }
    }

    [Command]
    private void CmdSpawnAvatar(string avatarPersonName)
    {
        GameObject spawn = Instantiate(playerAvatar, transform.position, Quaternion.identity);      
        NetworkServer.SpawnWithClientAuthority(spawn, gameObject);
        spawnedPlayerAvatar = spawn;  
        avatarData = AvatarManager.GetAvatarDataByName(avatarPersonName);
        spawn = Instantiate(avatarData.AvatarPrefab, spawnedPlayerAvatar.transform);              
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

            avatarControl = spawnedPlayerAvatar.GetComponent<AvatarControl>();
            if (avatarControl != null)
            {               
                userControlInput.SetAvatarControl(avatarControl);
                avatarControl.SetAvatarPerson(spawnedPlayerPerson);

                avatarControl.OnDefeat += YouDefeat;
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
    
    private void YouDefeat()
    {
        //TODO ShowUI
    }

    private void Respawn()
    {
        //TODO UpdateUI
        //Translate to respawn point
        avatarControl.Revive();
    }
}
