using UnityEngine;

/// <summary>
/// Инфорация о игроке
/// </summary>
[System.Serializable]
public class PlayerInfo 
{       
    private string playerName = "LeatherBastard";

    /// <summary>
    /// Имя игрока
    /// </summary>    
    public string PlayerName
    {
        get
        {
            return playerName;
        }
    }

    public void SetPlayerName(string _name)
    {
        if (_name != "")
        {
            playerName = _name;
        }
        else
        {
            playerName = "LeatherBastard";
        }
    }

    [SerializeField]
    private AvatarData currentAvatarData;

    //Текущий выбранный аватар
    public AvatarData CurrentAvatarData
    {
        get
        {
            return currentAvatarData;
        }
    }     

    /// <summary>
    /// Установка аватара
    /// </summary>
    /// <param name="avatarData"></param>
    public void SetAvatar(AvatarData avatarData)
    {        
        currentAvatarData = avatarData;
    }

    /// <summary>
    /// Установка аватара по имени
    /// </summary>
    /// <param name="avatarName"></param>
    public void SetAvatar(string avatarName)
    {
        currentAvatarData = AvatarManager.GetAvatarDataByName(avatarName);        
    }

    /// <summary>
    /// Возвращает структуру для отправки по сети
    /// </summary>
    /// <returns></returns>
    public PlayerSendData GetPlayerSendInfo()
    {
        PlayerSendData playerSendData = new PlayerSendData(PlayerName, currentAvatarData.name);
        return playerSendData;
    }

}
