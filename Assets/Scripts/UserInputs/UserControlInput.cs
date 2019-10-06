using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер ввода управления игрока
/// </summary>
public class UserControlInput : MonoBehaviour
{
    private IUserInput userInput;

    private AvatarControl avatarControl;

    public void SetAvatarControl(AvatarControl _avatarControl)
    {
        avatarControl = _avatarControl;
    }


    private void FixedUpdate()
    {
        SetInput();
    }

    private void SetInput()
    {
        if (userInput == null || avatarControl == null)
        {
            return;
        }
        Vector2 movement = userInput.GetMovement();
        bool shootOnce = userInput.GetShoot();
        Vector2 directionShoot = userInput.GetAttackDirection();

        avatarControl.SetMovement(movement);

        avatarControl.SetShoot(directionShoot, shootOnce);
    }

    private void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        userInput = gameObject.AddComponent<DesktopUserInput>();
#endif      
    }    
}
