using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    [SerializeField]
    private AvatarData[] avatarDatas;

    private static Dictionary<string, AvatarData> playerAvatars;

    private void Awake()
    {
        playerAvatars = new Dictionary<string, AvatarData>();

        foreach (var avatar in avatarDatas)
        {
            playerAvatars.Add(avatar.name, avatar);
        }
    }

    public static AvatarData GetAvatarDataByName(string avatarName)
    {
        Debug.Log("avatarName " + avatarName);

        if((avatarName == "" || playerAvatars.ContainsKey(avatarName) == false) && playerAvatars.Count > 0)
        {
            List<string> keys = new List<string>(playerAvatars.Keys);
            avatarName = keys[0];

            Debug.Log("avatarName 1" + avatarName);
        }

        if (playerAvatars.ContainsKey(avatarName))
        {
            Debug.Log("avatarName 2" + avatarName);
            return playerAvatars[avatarName];            
        }       
        else
        {
            NetworkDebug.LogError("Avatar Manager empty");
            return null;
        }

    }
}
