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
        if (playerAvatars.ContainsKey(avatarName))
        {
            return playerAvatars[avatarName];
        }
        else
        {
           // NetworkDebug.LogError("Avatar name: " + avatarName +" not found");
            return null;
        }

    }
}
