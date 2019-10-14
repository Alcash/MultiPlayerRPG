using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Мобильное управление
/// </summary>
public class MobileUserInput : MonoBehaviour,IUserInput
{    
    private JoystickController moveJoystick;    
    private JoystickController attackJoystick;

    public Vector2 GetAttackDirection()
    {
        return attackJoystick.GetInputDirection();
    }

    public Vector2 GetMovement()
    {
        return moveJoystick.GetInputDirection();
    }

    public bool GetShoot()
    {
       return attackJoystick.GetTouch();
    }

    private void Awake()
    {
#if !UNITY_IPHONE || !UNITY_ANDROID
        Destroy(GetComponent<MobileUserInput>());
#endif
    }
    
    private void Start()
    {
        MobileInputHolder holder = FindObjectOfType<MobileInputHolder>();

        if(holder != null)
        {
            moveJoystick = holder.LeftJoystick;
            attackJoystick = holder.LeftJoystick;
        }
        else
        {
            Destroy(GetComponent<MobileUserInput>());
        }
    }
}
