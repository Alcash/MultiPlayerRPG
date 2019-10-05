/// <summary>
/// структура передачи данных о игроке
/// </summary>
public struct PlayerSendData
{
    private string playerName;

    public string PlayerName
    {
        get
        {
            return playerName;
        }
    }

    private string playerAvatar;

    public string PlayerAvatar
    {
        get
        {
            return playerAvatar;
        }
    }

    public PlayerSendData(string playerName, string playerAvatar)
    {
        this.playerName = playerName;
        this.playerAvatar = playerAvatar;
    }  
}
