using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Мобильное управление
/// </summary>
public class MobileUserInput : BaseUserInput
{    
    private JoystickController moveJoystick;    
    private JoystickController attackJoystick;
        
    /// <summary>
    /// Возвращает направление атаки
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetAttackDirection()
    {
        return attackJoystick.GetInputDirection(-1);
    }

    /// <summary>
    /// Возвращает направление движения
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetMovement()
    {
        return moveJoystick.GetInputDirection();
    }

    /// <summary>
    /// Возвращает атакует ли
    /// </summary>
    /// <returns></returns>
    public override bool GetShoot()
    {
       return attackJoystick.GetTouch();
    }

    private void Awake()
    {
#if !UNITY_IPHONE || !UNITY_ANDROID
        Destroy(FindObjectOfType<MobileInputHolder>().gameObject);
        Destroy(GetComponent<MobileUserInput>());        
#endif
    }
    
    private void Start()
    {
        MobileInputHolder holder = FindObjectOfType<MobileInputHolder>();

        if(holder != null)
        {
            moveJoystick = holder.LeftJoystick;
            attackJoystick = holder.RightJoystick;
        }
        else
        {
            Destroy(GetComponent<MobileUserInput>());
        }
    }
}
