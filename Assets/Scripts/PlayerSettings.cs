using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Настройки игрока
/// </summary>
public class PlayerSettings : MonoBehaviour
{
    public static UnityAction<AvatarData> SetNewAvatar;
    public static UnityAction<string> SetNewName;

    private AvatarData playerAvatarData;

    public AvatarData AvatarData
    {
        get
        {
            return playerAvatarData;
        }
    }

    private string playerName;

    public string PlayerName
    {
        get
        {
            return playerName;
        }  
    }


    private void Awake()
    {
        SetNewAvatar += SelectAvatar;
        SetNewName += SetName;
    }

    private void SelectAvatar(AvatarData avatarData)
    {
        playerAvatarData = avatarData;
    }

    private void SetName(string name)
    {
        playerName = name;
    }
}
