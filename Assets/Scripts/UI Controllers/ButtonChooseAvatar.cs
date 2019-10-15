/// <summary>
/// Кнопка выбора аватара
/// </summary>
public class ButtonChooseAvatar : BaseButtonController
{
    [UnityEngine.SerializeField]
    private AvatarData avatarData;

    protected override void ButtonClicked()
    {
        PlayerSettings.SetNewAvatar(avatarData);
    }
}
