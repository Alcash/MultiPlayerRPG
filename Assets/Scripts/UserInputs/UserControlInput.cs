using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер ввода управления игрока
/// </summary>
public class UserControlInput : MonoBehaviour
{
    [SerializeField]
    private IUserInput userInput;

    private AvatarControl avatarControl;

    private Vector2 directionShoot;

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

        if (userInput.Exist() == false)
        {
            FindInput();
        }

        Vector2 movement = userInput.GetMovement();
        bool shootOnce = userInput.GetShoot();
        avatarControl.SetShoot(directionShoot, shootOnce);
        avatarControl.SetMovement(movement);       
        directionShoot = userInput.GetAttackDirection();
    }

    private void Start()
    {
        FindInput();
    }

    private void FindInput()
    {
        userInput = gameObject.GetComponent<IUserInput>();
        this.enabled = userInput != null;
    }
}
