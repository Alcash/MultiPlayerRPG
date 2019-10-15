using UnityEngine;

public abstract class BaseUserInput : MonoBehaviour
{
    public abstract Vector2 GetMovement();

    public abstract Vector2 GetAttackDirection();

    public abstract bool GetShoot();
}
