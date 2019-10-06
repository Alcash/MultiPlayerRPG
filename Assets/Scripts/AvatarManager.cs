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
        if((avatarName == "" || playerAvatars.ContainsKey(avatarName) == false) && playerAvatars.Count > 0)
        {
            List<string> keys = new List<string>(playerAvatars.Keys);
            avatarName = keys[0];
        }

        if (playerAvatars.ContainsKey(avatarName))
        {
            return playerAvatars[avatarName];
        }       
        else
        {
            NetworkDebug.LogError("Avatar Manager empty");
            return null;
        }

    }
}
