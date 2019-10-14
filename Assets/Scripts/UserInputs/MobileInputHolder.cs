using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Держатель джойстиков мобильного управления
/// </summary>
public class MobileInputHolder : MonoBehaviour
{
    [SerializeField]
    private JoystickController leftJoystickController;
    [SerializeField]
    private JoystickController rightJoystickController;

    public JoystickController LeftJoystick
    {
        get
        {
            return leftJoystickController;
        }
    }

    public JoystickController RightJoystick
    {
        get
        {
            return rightJoystickController;
        }
    }
}
